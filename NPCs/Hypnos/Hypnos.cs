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
using static Terraria.ModLoader.PlayerDrawLayer;

namespace CalValPlus.NPCs.Hypnos
{
    [AutoloadBossHead]
    internal class Hypnos : ModNPC
    {
        public bool initialized = false;
        public bool afterimages = false;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("XP-00 Hypnos");
            Main.npcFrameCount[NPC.type] = 1;
            NPCID.Sets.TrailingMode[NPC.type] = 1;
        }

        public override void SetDefaults()
        {
            NPC.noGravity = true;
            NPC.lavaImmune = true;
            NPC.aiStyle = -1;
            NPC.lifeMax = 1320000;
            NPC.damage = 0;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.Item14;
            NPC.knockBackResist = 0f;
            NPC.noTileCollide = true;
            NPC.width = 208;
            NPC.height = 138;
            NPC.boss = true;
            NPC.dontTakeDamage = true;
        }

        public override void AI()
        {
            if (!initialized)
            {
            }
            if (NPC.CountNPCS(ModContent.NPCType<HypnosPlug>()) <= 0)
            {
                NPC.dontTakeDamage = false;
            }
            else
            {
                NPC.dontTakeDamage = true;
            }
            switch (NPC.ai[0])
            {
                case 0: //Spawn animation
                    {
                        NPC.ai[1]++;
                        if (NPC.ai[1] < 20)
                        {
                            if (Main.rand.NextBool(2))
                            {
                                int num5 = Dust.NewDust(NPC.position, NPC.width, NPC.height, 226, 0f, 0f, 200, default, 1.5f);
                                Main.dust[num5].noGravity = true;
                                Main.dust[num5].velocity *= 0.75f;
                                Main.dust[num5].fadeIn = 1.3f;
                                Vector2 vector = new Vector2((float)Main.rand.Next(-400, 401), (float)Main.rand.Next(-400, 401));
                                vector.Normalize();
                                vector *= (float)Main.rand.Next(100, 200) * 0.04f;
                                Main.dust[num5].velocity = vector;
                                vector.Normalize();
                                vector *= 34f;
                                Main.dust[num5].position = NPC.Center - vector;
                            }
                        }
                        else if (NPC.ai[1] >= 20)
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<HypnosPlug>(), 0, NPC.whoAmI, i);
                            }
                            for (int l = 0; l < 48; l++)
                            {
                                Vector2 vector3 = Vector2.UnitX * (float)-(float)NPC.width / 2f;
                                vector3 += -Vector2.UnitY.RotatedBy((double)((float)l * 3.14159274f / 6f), default) * new Vector2(8f, 16f);
                                int num9 = Dust.NewDust(NPC.Center, 0, 0, 221, 0f, 0f, 160, default, 1f);
                                Main.dust[num9].scale = 1.1f;
                                Main.dust[num9].noGravity = true;
                                Main.dust[num9].position = NPC.Center + vector3;
                                Main.dust[num9].velocity = NPC.velocity * 0.1f;
                                Main.dust[num9].velocity = Vector2.Normalize(NPC.Center - NPC.velocity * 3f - Main.dust[num9].position) * 1.25f;
                            }
                            Terraria.Audio.SoundEngine.PlaySound(CalamityMod.Sounds.CommonCalamitySounds.FlareSound, NPC.Center);
                            ChangePhase(1);
                        }
                    }
                    break;
                case 1: //Move downward
                    {
                        NPC.ai[1]++;
                        afterimages = true;
                        Vector2 playerpos = new Vector2(Main.player[NPC.target].Center.X, Main.player[NPC.target].Center.Y + 200);
                        Vector2 distanceFromDestination = playerpos - NPC.Center;
                        CalamityUtils.SmoothMovement(NPC, 100, distanceFromDestination, 30, 1, true);
                        if (NPC.ai[1] == 20)
                        {
                            ChangePhase(2);
                        }
                    }
                    break;
                case 2: //Fan attack
                    {
                        int stop = 60;
                        NPC.ai[1]++;
                        if (NPC.ai[1] >= stop && NPC.ai[1] < stop + 120)
                        {
                            NPC.velocity = Vector2.Zero;
                        }
                        else
                        {
                            Vector2 playerpos = new Vector2(Main.player[NPC.target].Center.X, Main.player[NPC.target].Center.Y + 300);
                            Vector2 distanceFromDestination = playerpos - NPC.Center;
                            CalamityUtils.SmoothMovement(NPC, 100, distanceFromDestination, 30, 1, true);
                        }
                        if (NPC.ai[1] >= stop + 140)
                        {
                            ChangePhase(3);
                        }
                    }
                    break;
                case 3: //Back & forth dashes
                    {
                        NPC.ai[1]++;
                        NPC.ai[2] += 0.04f + (NPC.ai[1] * 0.00005f);
                        if (NPC.ai[1] < 90)
                        {
                            Vector2 playerpos = new Vector2(Main.player[NPC.target].Center.X - 620, Main.player[NPC.target].Center.Y + 400);
                            Vector2 distanceFromDestination = playerpos - NPC.Center;
                            CalamityUtils.SmoothMovement(NPC, 100, distanceFromDestination, 20, 1, true);
                        }
                        else
                        {
                            afterimages = true;
                            Vector2 playerpos = new Vector2(Main.player[NPC.target].Center.X + ((float)Math.Sin(NPC.ai[2]) * 800), Main.player[NPC.target].Center.Y + 400);
                            NPC.position = playerpos;
                            NPC.position.X -= NPC.width;
                            NPC.position.Y -= NPC.height / 2;
                        }
                        if (NPC.ai[1] >= 420)
                        {
                            ChangePhase(4);
                        }
                    }
                    break;
                case 4: //Neuron charges
                    {
                        afterimages = true;
                        Player target = Main.player[NPC.target];
                        int chargetime = 60;
                        int chargespeed = 15;
                        Vector2 position = NPC.Center;
                        Vector2 targetPosition = target.Center;
                        NPC.ai[1]++;
                        if (NPC.ai[1] % chargetime == 0)
                        {
                            Terraria.Audio.SoundEngine.PlaySound(CalamityMod.Sounds.CommonCalamitySounds.PlasmaBoltSound, NPC.Center);
                            if (NPC.ai[1] % (chargetime * 3) == 0)
                            {
                                Terraria.Audio.SoundEngine.PlaySound(SoundID.Roar, position);
                                Vector2 pos = targetPosition + target.velocity * 20f - position;
                                NPC.velocity = Vector2.Normalize(pos) * chargespeed;
                            }
                            else
                            {
                                Vector2 direction = targetPosition - position;
                                direction.Normalize();
                                NPC.velocity = direction * chargespeed;
                            }
                        }
                        if (NPC.ai[1] < (chargetime - 1))
                        {
                            Vector2 playerpos = new Vector2(target.Center.X, target.Center.Y + 300);
                            Vector2 distanceFromDestination = playerpos - position;
                            CalamityUtils.SmoothMovement(NPC, 100, distanceFromDestination, 30, 1, true);

                        }
                        else
                        {
                            NPC.velocity *= 1.01f;
                        }
                        if (NPC.ai[1] >= 360)
                        {
                            ChangePhase(5);
                        }
                    }
                    break;
                case 5: //Neuron lightning gates
                    {
                        NPC.ai[1]++;
                        afterimages = true;
                        Vector2 playerpos = new Vector2(Main.player[NPC.target].Center.X, Main.player[NPC.target].Center.Y + 400);
                        Vector2 distanceFromDestination = playerpos - NPC.Center;
                        CalamityUtils.SmoothMovement(NPC, 100, distanceFromDestination, 30, 1, true);
                        if (NPC.ai[1] >= 480)
                        {
                            ChangePhase(2);
                        }
                    }
                    break;
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
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC neur = Main.npc[i];
                if (neur.type == NPCType<AergiaNeuron>())
                {
                    neur.ai[2] = 0;
                    neur.damage = 0;
                    neur.Calamity().canBreakPlayerDefense = false;
                }
            }
            NPC.TargetClosest();
        }
        public override void FindFrame(int frameHeight)
        {
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            return false;
        }

        public override void OnKill()
        {
        }

        public override bool CheckActive()
        {
            return false;
        }
    }
}