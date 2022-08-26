using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Chat;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using CalamityMod.World;
using CalamityMod;
using Terraria.Localization;
using System.IO;
using Terraria.ModLoader.Utilities;

namespace CalValPlus.NPCs.JohnWulfrum
{
    public class JohnWulfrumActive : ModNPC
    {
        private static bool shop1;

        private static bool boss;
        int choppercounter = 0;
        bool sizeup = false;
        float sizetimer = 0;

        private bool dialoguehell = false;

        private bool dialoguehardmode = false;

        private bool dialogueplague = false;

        private bool dialoguejungle = false;

        private bool dialogueplanets = false;

        private int despawntimer;
        public override string Texture => "CalValPlus/NPCs/JohnWulfrum/JohnWulfrumNPC";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("John Wulfrum");
            Main.npcFrameCount[NPC.type] = 1;
            NPCID.Sets.DangerDetectRange[NPC.type] = 700;
            NPCID.Sets.AttackType[NPC.type] = 0;
            NPCID.Sets.AttackTime[NPC.type] = 90;
            NPCID.Sets.AttackAverageChance[NPC.type] = 30;
        }

        public override void SetDefaults()
        {
            NPC.friendly = true;
            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = 7;
            NPC.damage = 1;
            NPC.defense = 250000;
            NPC.lifeMax = 250000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            NPC.DR_NERD(0.999999f);
            //animationType = NPCID.PartyGirl;
        }
        public override bool CanChat()
        {
            return true;
        }

        public override void AI()
        {
            if (NPC.AnyNPCs(ModContent.NPCType<JohnWulfrumSentry>()))
            {
                NPC.active = false;
            }
            NPC.spriteDirection = -NPC.direction;
            NPC.velocity.X = 0;
            choppercounter += 7;
            if (choppercounter >= 704)
            {
                choppercounter = 0;
            }
            bool bossIsAlive = false;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && npc.boss)
                {
                    bossIsAlive = true;
                }
            }
            if (bossIsAlive)
            {
                NPC.alpha += 3;
                if (NPC.alpha >= 255)
                {
                    NPC.active = false;
                }
            }
            if (!bossIsAlive && NPC.alpha > 0)
            {
                NPC.alpha -= 3;
            }
            if (NPC.life < NPC.lifeMax)
            {
                NPC.dontTakeDamage = true;
            }
        }

        public override List<string> SetNPCNameList()/* tModPorter Suggestion: Return a list of names */
        {
            return new List<string> { "John Wulfrum" };
        }

        public override string GetChat()
        {
            return "What!? What do you want!? Oh, it's just you.";
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = "Shop";
            button2 = "Talk";
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
            {
                shop = true;
                {
                    shop1 = true;
                    boss = false;
                }
            }
            else if (!firstButton)
            {
                {
                    if (Main.myPlayer == Main.LocalPlayer.whoAmI)
                    {
                        if (!dialogueplanets && CalamityMod.DownedBossSystem.downedProvidence && !CalamityMod.DownedBossSystem.downedStormWeaver && !CalamityMod.DownedBossSystem.downedSignus && !CalamityMod.DownedBossSystem.downedCeaselessVoid && Main.LocalPlayer.ZoneSkyHeight)
                        {
                            Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_GiftOrReward(), ModLoader.GetMod("CalamityMod").Find<ModItem>("ExodiumClusterOre").Type, 12);
                            Main.npcChatText = "Stop following me! I'm starting to realize that the stronger you get, the more of his creations keep popping out. Are you aware of this and trying to embarrass me by calling all of his works? Well jokes on you! I've been dismantling some of them for supplies for something!";
                            dialogueplanets = true;
                            CalValPlusWorld.spacedialogue = true;
                        }
                        else if (!dialoguejungle && NPC.downedMoonlord && !CalamityMod.DownedBossSystem.downedDragonfolly && Main.LocalPlayer.ZoneJungle)
                        {
                            Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_GiftOrReward(), ModLoader.GetMod("CalamityMod").Find<ModItem>("BirbPheromones").Type, 1);
                            Main.npcChatText = "Grahhh, disgusting birds keep trying to steal my tools! Of all the things that scientist has done, wandering into the field of biological replication was one of the greatest blunders he's done!";
                            dialoguejungle = true;
                            CalValPlusWorld.jungledialogue = true;
                        }
                        else if (!dialogueplague && NPC.downedGolemBoss && !CalamityMod.DownedBossSystem.downedPlaguebringer && Main.LocalPlayer.ZoneJungle)
                        {
                            Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_GiftOrReward(), ModLoader.GetMod("CalamityMod").Find<ModItem>("Abomination").Type, 1);
                            Main.npcChatText = "Pesky bees everywhere! I'm trying to collect samples of the green nanometal that's been appearing all over here as of late, but they keep attacking me! They're likely attracted to less intelligent lifeforms, so go away. ";
                            dialogueplague = true;
                            CalValPlusWorld.plaguedialogue = true;
                        }
                        else if (!dialoguehardmode && Main.hardMode && !Main.dayTime && !NPC.downedMechBossAny)
                        {
                            int choice = Main.rand.Next(3);
                            if (choice == 0)
                            {
                                Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_GiftOrReward(), ItemID.MechanicalWorm, 1);
                            }
                            else if (choice == 1)
                            {
                                Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_GiftOrReward(), ItemID.MechanicalEye, 1);
                            }
                            else
                            {
                                Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_GiftOrReward(), ItemID.MechanicalSkull, 1);
                            }
                            Main.npcChatText = "That wretched man's pathetic little play toys are wandering about it seems. Here, take this junk. This primitive little gadget will probably be of more value to you, than it is to me.";
                            dialoguehardmode = true;
                            CalValPlusWorld.hmdialogue = true;
                        }
                        else if (!dialoguehell && NPC.downedBoss3 && !Main.hardMode && Main.LocalPlayer.ZoneUnderworldHeight)
                        {
                            Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_GiftOrReward(), ItemID.GuideVoodooDoll, 1);
                            Main.npcChatText = "You again! Go back home, I'm not gonna let you find his treasured sword before I do! ... Then again, I have no idea where to look.";
                            dialoguehell = true;
                            CalValPlusWorld.helldialogue = true;
                        }
                        else
                        {
                            Main.npcChatText = "We have nothing to discuss.";
                        }
                    }
                }
            }
        }

        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            /*if (shop1)
            {
                Mod calamityMod = ModLoader.GetMod("CalamityMod");
                shop.item[nextSlot].SetDefaults(ItemType<Items.JohnWulfrumSummon>());
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 10, 0, 0);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemType<Items.JohnWulfrumDoor>());
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(299, 99, 99, 99);
                nextSlot++;
            }*/
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 1;
            knockback = 0f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 100;
            randExtraCooldown = 20;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = (ModLoader.GetMod("CalamityMod").Find<ModProjectile>("InfernadoFriendly").Type);
            attackDelay = 1;
            return;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            randomOffset = 2f;
            multiplier = 24f;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (NPC.dontTakeDamage)
            {
                //float androframe = 11f / (float)Main.npcFrameCount[npc.type];\
                if (sizeup)
                {
                    sizetimer += 0.01f;
                }
                else
                {
                    sizetimer -= 0.01f;
                }
                if (sizetimer > 0.3f)
                {
                    sizeup = false;
                }
                else if (sizetimer < 0.05f)
                {
                    sizeup = true;
                }
                Texture2D wingtexture = (ModContent.Request<Texture2D>("CalValPlus/NPCs/JohnWulfrum/WulfrumShield").Value);

                int wingtextureheight = (int)((float)(choppercounter / NPC.frame.Height)) * (wingtexture.Height / 11);

                Rectangle wingtexturesquare = new Rectangle(0, wingtextureheight, wingtexture.Width, wingtexture.Height / 11);
                Color wingtexturealpha = new Color(255, 255, 255, 0.01f);
                spriteBatch.Draw(wingtexture, NPC.Center - Main.screenPosition + new Vector2(0f + (10 * NPC.spriteDirection), NPC.gfxOffY - 10), wingtexturesquare, wingtexturealpha, NPC.rotation, Utils.Size(wingtexturesquare) / 2f, NPC.scale + sizetimer, SpriteEffects.None, 0f);
            }

        }
        private void EdgyTalk(string text, Color color, bool combatText = false)
        {
            if (combatText)
            {
                CombatText.NewText(NPC.getRect(), color, text, true);
            }
            else
            {
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    Main.NewText(text, color);
                }
                else if (Main.netMode == NetmodeID.Server)
                {
                    ChatHelper.BroadcastChatMessage(NetworkText.FromKey(text), color);
                }
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            EdgyTalk("Hoo hee hoo, you're going to have to try a little harder than that.", Color.LightSkyBlue, true);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!NPC.AnyNPCs(ModContent.NPCType<JohnWulfrumSentry>()) && !NPC.AnyNPCs(ModContent.NPCType<JohnWulfrum>()) && !NPC.AnyNPCs(ModContent.NPCType<JohnWulfrumActive>()))
            {
                if (CalamityMod.DownedBossSystem.downedProvidence && !CalamityMod.DownedBossSystem.downedStormWeaver && !CalamityMod.DownedBossSystem.downedSignus && !CalamityMod.DownedBossSystem.downedCeaselessVoid && !CalValPlusWorld.spacedialogue)
                {
                    return SpawnCondition.Sky.Chance * 0.05f;
                }
                else if (NPC.downedMoonlord && !CalamityMod.DownedBossSystem.downedDragonfolly && !CalValPlusWorld.jungledialogue)
                {
                    return SpawnCondition.SurfaceJungle.Chance * 0.05f;
                }
                else if (NPC.downedGolemBoss && !CalamityMod.DownedBossSystem.downedPlaguebringer && !CalValPlusWorld.plaguedialogue)
                {
                    return SpawnCondition.UndergroundJungle.Chance * 0.05f;
                }
                else if (Main.hardMode && NPC.downedMechBossAny && !CalValPlusWorld.hmdialogue)
                {
                    return SpawnCondition.OverworldNight.Chance * 0.05f;
                }
                else if (NPC.downedBoss3 && !CalValPlusWorld.helldialogue)
                {
                    return SpawnCondition.Underworld.Chance * 0.05f;
                }
                else
                {
                    return 0f;
                }
            }
            else
            {
                return 0f;
            }
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(choppercounter);
            writer.Write(sizetimer);
            writer.Write(sizeup);
            writer.Write(dialoguehell);
            writer.Write(dialoguehardmode);
            writer.Write(dialogueplague);
            writer.Write(dialoguejungle);
            writer.Write(dialogueplanets);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            dialoguehell = reader.ReadBoolean();
            dialoguehardmode = reader.ReadBoolean();
            dialogueplague = reader.ReadBoolean();
            dialoguejungle = reader.ReadBoolean();
            dialogueplanets = reader.ReadBoolean();
            sizeup = reader.ReadBoolean();
            sizetimer = reader.ReadInt32();
            choppercounter = reader.ReadInt32();
        }
    }
}