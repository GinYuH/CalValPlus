﻿using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Shaders;
using Terraria.Audio;
using CalamityMod;

namespace CalValPlus.NPCs.Hypnos
{
    internal class Draedon : ModNPC
    {
        public bool initialized = false;
        public static readonly Color TextColor = new(155, 255, 255);
        public static readonly Color TextColorEdgy = new(213, 4, 11);

        bool p2dial = false;
        bool revdial = false;
        public override string Texture => "CalamityMod/NPCs/ExoMechs/Draedon";

        NPC hypnos;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Draedon");
            Main.npcFrameCount[NPC.type] = 12;
            this.HideFromBestiary();
        }

        public override void SetDefaults()
        {
            NPC.noGravity = true;
            NPC.lavaImmune = true;
            NPC.aiStyle = -1;
            NPC.lifeMax = 2;
            NPC.damage = 0;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.Item14;
            NPC.knockBackResist = 0f;
            NPC.noTileCollide = true;
            NPC.width = NPC.height = 86;
        }

        public override void AI()
        {
            NPC.TargetClosest();
            NPC.dontTakeDamage = true;
            int basetime = 120;
            int timemult = 130;
            switch (NPC.ai[0])
            {
                case 0:
                    {
                        Vector2 playerpos = new Vector2(Main.player[NPC.target].Center.X, Main.player[NPC.target].Center.Y - 200);
                        Vector2 distanceFromDestination = playerpos - NPC.Center;
                        CalamityUtils.SmoothMovement(NPC, 100, distanceFromDestination, 30, 1, true);

                        if (NPC.ai[1] == basetime)
                        {
                            SoundEngine.PlaySound(CalamityMod.NPCs.ExoMechs.Draedon.TeleportSound, NPC.Center);
                            Main.NewText("Most peculiar. Your alterations to the Codebreaker have led it to signal a foe I did not intend for you to encounter.", TextColor);
                        }
                        else if (NPC.ai[1] == basetime + timemult)
                        {
                            Main.NewText("Your previous battles have piqued my interest, and now your creativity has, too.", TextColor);
                        }
                        else if (NPC.ai[1] == basetime + timemult * 2)
                        {
                            Main.NewText("You will face one of my older creations. Do not underestimate it.", TextColor);
                        }
                        else if (NPC.ai[1] == basetime + timemult * 3)
                        {
                            Main.NewText("You may proceed after concluding your preparations.", TextColorEdgy);
                            NPC.ai[2] = 1;
                        }
                        else if (NPC.ai[1] == basetime + timemult * 4)
                        {
                            if (Main.netMode != NetmodeID.Server)
                            {
                                SoundEngine.PlaySound(CalamityMod.Sounds.CommonCalamitySounds.FlareSound with { Volume = CalamityMod.Sounds.CommonCalamitySounds.FlareSound.Volume * 1.55f }, NPC.Center);
                            }
                            int hypy = NPC.NewNPC(NPC.GetSource_FromAI(), (int)Main.LocalPlayer.Center.X, (int)(Main.LocalPlayer.Center.Y - 1200), ModContent.NPCType<Hypnos>());
                            hypnos = Main.npc[hypy];
                            NPC.ai[1] = 0;
                            NPC.ai[0] = 1;
                        }
                        NPC.ai[1]++;
                    }
                    break;
                case 1:
                    {
                        int offset = hypnos.ai[0] == 9 ? 1100 : 900;
                        Vector2 playerpos = new Vector2(Main.player[NPC.target].Center.X - offset, Main.player[NPC.target].Center.Y);
                        Vector2 distanceFromDestination = playerpos - NPC.Center;
                        CalamityUtils.SmoothMovement(NPC, 100, distanceFromDestination, 30, 1, true);

                        if (hypnos.ai[0] == 6 && !p2dial)
                        {
                            if (NPC.ai[1] == 20)
                            {
                                Main.NewText("The tethers connecting its attendants to itself are inefficient at best.", TextColor);
                            }
                            else if (NPC.ai[1] == timemult)
                            {
                                Main.NewText("A vestige of my inexperience. Do not let this pollute your judgment of my later creations.", TextColor);
                                NPC.ai[1] = 0;
                                p2dial = true;
                            }
                            NPC.ai[1]++;
                        }
                        if (hypnos.life <= hypnos.lifeMax * 0.25f && !revdial)
                        {
                            if (NPC.ai[1] == 20)
                            {
                                Main.NewText("Fascinating. Its amygdala appears to be administering adrenaline to its mechanical components.", TextColor);
                            }
                            else if (NPC.ai[1] == timemult)
                            {
                                SoundEngine.PlaySound(CalamityMod.NPCs.ExoMechs.Draedon.LaughSound, NPC.Center);
                                Main.NewText("I did not account for this in my calculations. Your current situation appears dire indeed.", TextColor);
                                NPC.ai[1] = 0;
                                revdial = true;
                            }

                            NPC.ai[1]++;
                        }
                        if (!hypnos.active)
                        {
                            NPC.ai[1] = 0;
                            NPC.ai[0] = 2;
                        }

                    }
                    break;
                case 2:
                    {
                        Vector2 playerpos = new Vector2(Main.player[NPC.target].Center.X - 300, Main.player[NPC.target].Center.Y);
                        Vector2 distanceFromDestination = playerpos - NPC.Center;
                        CalamityUtils.SmoothMovement(NPC, 100, distanceFromDestination, 30, 1, true);
                        NPC.ai[1]++;
                        if (NPC.ai[1] == basetime)
                        {
                            Main.NewText("I cannot say I did not expect this to happen.", TextColor);
                        }
                        else if (NPC.ai[1] == basetime + timemult)
                        {
                            Main.NewText("Hypnos lacks synergy with my other creations. The free will of its organic components stifles its potential as an efficient war machine.", TextColor);
                        }
                        else if (NPC.ai[1] == basetime + timemult * 2)
                        {
                            Main.NewText("I have ascertained everything necessary about this creation and its capabilities.", TextColor);
                        }
                        else if (NPC.ai[1] == basetime + timemult * 3)
                        {
                            Main.NewText("Salvage what you wish from it. You will need it more than I for what is to come.", TextColor);
                        }
                        else if (NPC.ai[1] == basetime + timemult * 4)
                        {
                            Main.NewText("And please. Dislodge that blood clot from my device the next time you use it.", TextColor);
                        }
                        else if (NPC.ai[1] >= basetime + timemult * 5)
                        {
                            NPC.alpha += 10;
                            if (NPC.alpha >= 255)
                            {
                                NPC.active = false;
                            }
                        }
                    }
                    break;
            }
        }

        public override bool CheckActive()
        {
            return false;
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(p2dial);
            writer.Write(revdial);
            writer.Write(initialized);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            p2dial = reader.ReadBoolean();
            revdial = reader.ReadBoolean();
            initialized = reader.ReadBoolean();
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.frame.Width = 100;

            int xFrame = NPC.frame.X / NPC.frame.Width;
            int yFrame = NPC.frame.Y / frameHeight;
            int frame = xFrame * Main.npcFrameCount[NPC.type] + yFrame;


            int frameChangeDelay = 7;

            NPC.frameCounter++;
            if (NPC.frameCounter >= frameChangeDelay)
            {
                frame++;

                if (frame >= 16)
                    frame = 11;

                NPC.frameCounter = 0;
            }

            NPC.frame.X = frame / Main.npcFrameCount[NPC.type] * NPC.frame.Width;
            NPC.frame.Y = frame % Main.npcFrameCount[NPC.type] * frameHeight;
        }


        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = Terraria.GameContent.TextureAssets.Npc[NPC.type].Value;
            Texture2D glowmask = ModContent.Request<Texture2D>("CalamityMod/NPCs/ExoMechs/DraedonGlowmask").Value;
            Rectangle frame = NPC.frame;

            Vector2 drawPosition = NPC.Center - screenPos - Vector2.UnitY * 38f;
            Vector2 origin = frame.Size() * 0.5f;
            Color color = NPC.GetAlpha(drawColor);
            SpriteEffects direction = NPC.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            spriteBatch.Draw(texture, drawPosition, frame, drawColor * NPC.Opacity, NPC.rotation, origin, NPC.scale, direction, 0f);
            spriteBatch.Draw(glowmask, drawPosition, frame, Color.White, NPC.rotation, origin, NPC.scale, direction, 0f);

            return false;
        }
    }
}