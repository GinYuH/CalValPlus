using MonoMod.Cil;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using CsvHelper.TypeConversion;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using CalamityMod;
using IL.Terraria.Audio;
using CalamityMod.World;
using CalamityMod.Projectiles.Boss;
using static Terraria.ModLoader.PlayerDrawLayer;
using CalamityMod.Items.Accessories;

namespace CalValPlus.NPCs.Hypnos
{
    internal class AergiaNeuron : ModNPC
    {
        public bool initialized = false;
        public bool afterimages = false;
        public bool hypnosafter = false;
        NPC hypnos;
        float rottimer = 0;
        int redlaserproj = ProjectileType<AstralLaser>();
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("XP-00 Hypnos Aergia Neuron");
            Main.npcFrameCount[NPC.type] = 1;
            NPCID.Sets.TrailingMode[NPC.type] = 1;
        }

        public override void SetDefaults()
        {
            NPC.noGravity = true;
            NPC.lavaImmune = true;
            NPC.aiStyle = -1;
            NPC.lifeMax = 200;
            NPC.damage = 0;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.Item14;
            NPC.knockBackResist = 0f;
            NPC.noTileCollide = true;
            NPC.width = 40;
            NPC.height = 40;
            NPC.dontTakeDamage = true;
        }

        public override void AI()
        {
            if (!initialized)
            {
                hypnos = Main.npc[(int)NPC.ai[0]];
                initialized = true;
            }
            //I'm keeping NPC.ai[0] consistent with Hypnos to reduce confusion
            NPC.ai[0] = hypnos.ai[0];
            Player target = Main.player[hypnos.target];
            switch (NPC.ai[0])
            {
                case 0: //Basic idle
                case 1:
                    {

                        Vector2 velocity = target.Center - NPC.Center;
                        velocity.Normalize();
                        velocity *= 9f;

                        double deg = 30 * NPC.ai[1];
                        double rad = deg * (Math.PI / 180);
                        double dist = 220;
                        NPC.position.X = hypnos.Center.X - (int)(Math.Cos(rad) * dist) - NPC.width / 2;
                        NPC.position.Y = hypnos.Center.Y - (int)(Math.Sin(rad) * dist) - NPC.height / 2;
                    }
                    break;
                case 2: //Fan attack
                    {
                        int stop = 60;
                        Vector2 velocity = target.Center - NPC.Center;
                        velocity.Normalize();
                        velocity *= 9f;

                        double deg = 15 * NPC.ai[1] + 8;
                        double rad = deg * (Math.PI / 180);
                        double dist = 200;
                        NPC.position.X = hypnos.Center.X - (int)(Math.Cos(rad) * dist) - NPC.width / 2;
                        NPC.position.Y = hypnos.Center.Y - (int)(Math.Sin(rad) * dist) - NPC.height / 2;

                        if (hypnos.ai[1] >= stop + 30 && hypnos.ai[1] < stop + 90)
                        {
                            NPC.ai[2]++;
                        }
                        int lasertimer = 20;
                        if (CalamityWorld.death)
                        {
                            lasertimer = 10;
                        }
                        else if (CalamityWorld.revenge)
                        {
                            lasertimer = 12;
                        }
                        else if (Main.expertMode)
                        {
                            lasertimer = 15;
                        }
                        else
                        {
                            lasertimer = 20;
                        }
                        if (NPC.ai[2] >= lasertimer)
                        {
                            Vector2 position = NPC.Center;
                            Vector2 targetPosition = hypnos.Center;
                            Vector2 direction = targetPosition - position;
                            direction.Normalize();
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, direction * -10, redlaserproj, 32, 0);
                            Terraria.Audio.SoundEngine.PlaySound(CalamityMod.Sounds.CommonCalamitySounds.LaserCannonSound  with { Volume = CalamityMod.Sounds.CommonCalamitySounds.LaserCannonSound.Volume - 0.1f}, NPC.Center);
                            NPC.ai[2] = 0;
                        }
                    }
                    break;
                case 3: //Back & forth dashes
                    {
                        int heightoffset = -180;
                        int widthoffset = -349;
                        int spacing = 60;
                        NPC.position = new Vector2(hypnos.Center.X + (NPC.ai[1] * spacing) + widthoffset, hypnos.Center.Y + heightoffset);
                        NPC.ai[2]++;
                        int lasertimer = 60;
                        if (Main.expertMode)
                        {
                            lasertimer -= 5;
                        }
                        if (CalamityWorld.revenge)
                        {
                            lasertimer -= 5;
                        }
                        if (CalamityWorld.death)
                        {
                            lasertimer -= 5;
                        }
                        if (NPC.ai[2] >= lasertimer + Main.rand.Next(-12, 20))
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(0, -8), redlaserproj, 320, 0);
                            Terraria.Audio.SoundEngine.PlaySound(CalamityMod.Sounds.CommonCalamitySounds.LaserCannonSound with { Volume = CalamityMod.Sounds.CommonCalamitySounds.LaserCannonSound.Volume - 0.1f }, NPC.Center); 
                            NPC.ai[2] = 0;
                        }
                    }
                    break;
                case 4: //Neuron charges
                    {
                        rottimer += 12f;
                        Vector2 velocity = target.Center - NPC.Center;
                        velocity.Normalize();
                        velocity *= 9f;

                        double deg = 30 * NPC.ai[1] + rottimer;
                        double rad = deg * (Math.PI / 180);
                        double dist = 200;
                        NPC.position.X = hypnos.Center.X - (int)(Math.Cos(rad) * dist) - NPC.width / 2;
                        NPC.position.Y = hypnos.Center.Y - (int)(Math.Sin(rad) * dist) - NPC.height / 2;

                        NPC.ai[2]++;
                        int lasertimer = 60;
                        if (Main.expertMode)
                        {
                            lasertimer -= 10;
                        }
                        if (CalamityWorld.revenge)
                        {
                            lasertimer -= 5;
                        }
                        if (CalamityWorld.death)
                        {
                            lasertimer -= 5;
                        }
                        if (NPC.ai[2] >= lasertimer)
                        {
                            Vector2 position = NPC.Center;
                            Vector2 targetPosition = hypnos.Center;
                            Vector2 direction = targetPosition - position;
                            direction.Normalize();

                            Terraria.Audio.SoundEngine.PlaySound(CalamityMod.Sounds.CommonCalamitySounds.LaserCannonSound with { Volume = CalamityMod.Sounds.CommonCalamitySounds.LaserCannonSound.Volume - 0.1f }, NPC.Center);
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, direction * -10, redlaserproj, 320, 0);
                            NPC.ai[2] = 0;
                        }
                    }
                    break;
                case 5: //Neuron lightning gates
                    {
                        Vector2 idealpos = NPC.Center; 

                        double deg = 30 * NPC.ai[1];
                        double rad = deg * (Math.PI / 180);
                        double dist = 400;
                        bool bottom = NPC.ai[1] <= 10 && NPC.ai[1] >= 8;

                        if (!bottom)
                        {
                            NPC.ai[2]++;
                        }
                        if (NPC.ai[2] < (60 * NPC.ai[1]) + 60)
                        {
                            idealpos.X = target.Center.X - (int)(Math.Cos(rad) * dist) - NPC.width / 2;
                            idealpos.Y = target.Center.Y - (int)(Math.Sin(rad) * dist) - NPC.height / 2;
                            Vector2 distanceFromDestination = idealpos - NPC.Center;
                            CalamityUtils.SmoothMovement(NPC, 100, distanceFromDestination, 60, 1, true);
                        }
                        else if (NPC.ai[2] == (60 * NPC.ai[1]) + 61)
                        {
                            Vector2 direction = target.Center - NPC.Center;
                            direction.Normalize();
                            NPC.velocity = direction * 20;
                        }
                        else if (NPC.ai[2] >= (60 * NPC.ai[1]) + 81)
                        {
                            NPC.damage = 0;
                            NPC.Calamity().canBreakPlayerDefense = false;
                            NPC.ai[2] = 0;
                        }
                        else
                        {
                            NPC.velocity *= 1.01f;
                            NPC.damage = 200;
                            NPC.Calamity().canBreakPlayerDefense = true;
                        }
                    }
                    break;
                //Phase 2
                case 6: //Rotation attack
                    break;
                case 7: //Predictive charge
                    break;
                case 8: //SWR Yukari attack
                    break;
                case 9: //Lightning wall
                    break;
                case 10: //Vanish
                    break;
            }
        }

        public void ChangePhase(int phasenum, bool reset1 = true, bool reset2 = true, bool reset3 = true)
        {
            NPC.ai[0] = phasenum;
            if (reset1)
            {
                NPC.ai[1] = 0;
            }
            if (reset2)
            {
                NPC.ai[2] = 0;
            }
            if (reset3)
            {
                NPC.Calamity().newAI[3] = 0;
            }
            afterimages = false;
        }
        public override void FindFrame(int frameHeight)
        {
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            DrawHypnos(spriteBatch, screenPos, drawColor);
            drawchain(spriteBatch, screenPos, drawColor);
            return false;
        }

        public void doafterimages(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (NPC.spriteDirection == 1)
                spriteEffects = SpriteEffects.FlipHorizontally;

            Texture2D texture = TextureAssets.Npc[NPC.type].Value;
            Vector2 origin = new Vector2((float)(texture.Width / 2), (float)(texture.Height / Main.npcFrameCount[NPC.type] / 2));
            Color white = Color.White;
            float colorLerpAmt = 0.5f;
            int afterimageAmt = 7;

            if (CalamityConfig.Instance.Afterimages && afterimages)
            {
                for (int i = 1; i < afterimageAmt; i += 2)
                {
                    Color color1 = drawColor;
                    color1 = Color.Lerp(color1, white, colorLerpAmt);
                    color1 = NPC.GetAlpha(color1);
                    color1 *= (float)(afterimageAmt - i) / 15f;
                    Vector2 offset = NPC.oldPos[i] + new Vector2((float)NPC.width, (float)NPC.height) / 2f - screenPos;
                    offset -= new Vector2((float)texture.Width, (float)(texture.Height / Main.npcFrameCount[NPC.type])) * NPC.scale / 2f;
                    offset += origin * NPC.scale + new Vector2(0f, NPC.gfxOffY);
                    spriteBatch.Draw(texture, offset, NPC.frame, color1, NPC.rotation, origin, NPC.scale, spriteEffects, 0f);
                }
            }

            Vector2 npcOffset = NPC.Center - screenPos;
            npcOffset -= new Vector2((float)texture.Width, (float)(texture.Height / Main.npcFrameCount[NPC.type])) * NPC.scale / 2f;
            npcOffset += origin * NPC.scale + new Vector2(0f, NPC.gfxOffY);
            spriteBatch.Draw(texture, npcOffset, NPC.frame, NPC.GetAlpha(drawColor), NPC.rotation, origin, NPC.scale, spriteEffects, 0f);
        }

        public void drawchain(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            int heighoffset = 20;
            int heighoffsetin = 30;
            int innerdist = 70;
            int outerdist = 80;
            Vector2 leftleftplug = new Vector2(hypnos.Center.X - outerdist, hypnos.Center.Y + heighoffset);
            Vector2 leftplug = new Vector2(hypnos.Center.X - innerdist, hypnos.Center.Y + heighoffsetin);
            Vector2 rightplug = new Vector2(hypnos.Center.X + innerdist, hypnos.Center.Y + heighoffsetin);
            Vector2 rightrightplug = new Vector2(hypnos.Center.X + outerdist, hypnos.Center.Y + heighoffset);

            Vector2 pluglocation = hypnos.Center;

            switch (NPC.ai[1])
            {
                case 0:
                case 1:
                case 2:
                    pluglocation = leftleftplug;
                    break;
                case 3:
                case 4:
                case 5:
                    pluglocation = leftplug;
                    break;
                case 6:
                case 7:
                case 8:
                    pluglocation = rightplug;
                    break;
                case 9:
                case 10:
                case 11:
                    pluglocation = rightrightplug;
                    break;
            }

            Vector2 distToProj = NPC.Center;
            float projRotation = NPC.AngleTo(pluglocation) - 1.57f;
            bool doIDraw = true;
            Texture2D texture = Request<Texture2D>("CalValPlus/NPCs/Hypnos/HypnosPlugCable").Value; //change this accordingly to your chain texture

            Color chaincolor = drawColor;

            if ((NPC.ai[2] > (60 * NPC.ai[1]) + 10) && NPC.ai[0] == 5)
            {
                chaincolor = new Color(drawColor.R, drawColor.G, drawColor.B, 20);
            }
            else
            {
                chaincolor = drawColor;
            }

            while (doIDraw)
            {
                float distance = (pluglocation - distToProj).Length();
                if (distance < (texture.Height + 1))
                {
                    doIDraw = false;
                }
                else if (!float.IsNaN(distance))
                {
                    distToProj += NPC.DirectionTo(pluglocation) * texture.Height;
                    spriteBatch.Draw(texture, distToProj - Main.screenPosition,
                        new Rectangle(0, 0, texture.Width, texture.Height), chaincolor, projRotation,
                        Utils.Size(texture) / 2f, 1f, SpriteEffects.None, 0.1f);
                }
            }
        }

        public void DrawHypnos(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (NPC.ai[1] == 11)
            {
                SpriteEffects spriteEffects = SpriteEffects.None;
                if (hypnos.spriteDirection == 1)
                    spriteEffects = SpriteEffects.FlipHorizontally;

                Texture2D texture = TextureAssets.Npc[hypnos.type].Value;
                Vector2 origin = new Vector2((float)(texture.Width / 2), (float)(texture.Height / Main.npcFrameCount[hypnos.type] / 2));
                Color white = Color.White;
                float colorLerpAmt = 0.5f;
                int afterimageAmt = 7;

                if (CalamityConfig.Instance.Afterimages && hypnosafter)
                {
                    for (int i = 1; i < afterimageAmt; i += 2)
                    {
                        Color color1 = drawColor;
                        color1 = Color.Lerp(color1, white, colorLerpAmt);
                        color1 = hypnos.GetAlpha(color1);
                        color1 *= (float)(afterimageAmt - i) / 15f;
                        Vector2 offset = hypnos.oldPos[i] + new Vector2((float)hypnos.width, (float)hypnos.height) / 2f - screenPos;
                        offset -= new Vector2((float)texture.Width, (float)(texture.Height / Main.npcFrameCount[hypnos.type])) * hypnos.scale / 2f;
                        offset += origin * hypnos.scale + new Vector2(0f, hypnos.gfxOffY);
                        spriteBatch.Draw(texture, offset, hypnos.frame, color1, hypnos.rotation, origin, hypnos.scale, spriteEffects, 0f);
                    }
                }

                Vector2 npcOffset = hypnos.Center - screenPos;
                npcOffset -= new Vector2((float)texture.Width, (float)(texture.Height / Main.npcFrameCount[hypnos.type])) * hypnos.scale / 2f;
                npcOffset += origin * hypnos.scale + new Vector2(0f, hypnos.gfxOffY);
                spriteBatch.Draw(texture, npcOffset, hypnos.frame, hypnos.GetAlpha(drawColor), hypnos.rotation, origin, hypnos.scale, spriteEffects, 0f);
            }
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            doafterimages(spriteBatch, screenPos, drawColor);
        }

        public override void OnKill()
        {
        }
    }
}