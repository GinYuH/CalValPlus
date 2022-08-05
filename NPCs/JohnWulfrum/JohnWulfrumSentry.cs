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

namespace CalValPlus.NPCs.JohnWulfrum
{
    public class JohnWulfrumSentry : ModNPC
    {
        private static bool shop1;

        private static bool boss;
        int choppercounter = 0;
        bool sizeup = false;
        float sizetimer = 0;

        private bool dialogueprehm = false;

        private bool dialoguehm = false;

        private bool dialogueplant = false;

        private bool dialoguepbg = false;

        private bool dialoguebirb = false;

        private bool dialoguedog = false;

        private bool dialogueyharon = false;

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
                        if (!dialogueyharon && CalamityMod.DownedBossSystem.downedYharon && !CalamityMod.DownedBossSystem.downedSCal)
                        {
                            Main.npcChatText = "The world as of late has been great! My project I've been working on since our first encounter is nearing completion, and that filthy scientist is finally coming out of his rat hole. The world is much crueler now then it was before, he won't last long.";
                            dialogueyharon = true;
                        }
                        else if (!dialoguedog && CalamityMod.DownedBossSystem.downedDoG && !CalamityMod.DownedBossSystem.downedYharon)
                        {
                            Main.npcChatText = "EUREKA, I go take a walk and see a colossal amount of scrap metal from the great Devourer of the Cosmos! I'd like to thank whoever done it, they must have been a powerful person, unlike you.";
                            dialoguedog = true;
                        }
                        else if (!dialoguebirb && CalamityMod.DownedBossSystem.downedDragonfolly && !CalamityMod.DownedBossSystem.downedStormWeaver && !CalamityMod.DownedBossSystem.downedCeaselessVoid&& !CalamityMod.DownedBossSystem.downedSignus)
                        {
                            Main.npcChatText = "Thank goodness you took care of that filthy creature. You've finally done something useful. Now beat it, I need to think for a bit.";
                            dialoguebirb = true;
                        }
                        else if (!dialoguepbg && CalamityMod.DownedBossSystem.downedPlaguebringer && !NPC.downedAncientCultist)
                        {
                            Main.npcChatText = "Finally that darned bee is out of the way. Now I can go back to collecting samples from where it was.";
                            dialoguepbg = true;
                        }
                        else if (!dialogueplant && NPC.downedPlantBoss && !CalamityMod.DownedBossSystem.downedAstrumAureus)
                        {
                            Main.npcChatText = "I can’t get any gosh darn sleep! Some big space crab thing keeps stomping about, and it’s driving me mad!";
                            dialogueplant = true;
                        }
                        else if (!dialoguehm && (NPC.downedMechBoss1 || NPC.downedMechBoss2 || NPC.downedMechBoss3) && !NPC.downedPlantBoss)
                        {
                            Main.npcChatText = "You've encountered and dismantled one of his creations? GRAH you probably tore them apart with fireworks, making the quality of their scraps less salvageable!";
                            dialoguehm = true;
                        }
                        else if (!dialogueprehm && !Main.hardMode)
                        {
                            Main.npcChatText = "You’ve come to humiliate me even more than I already have been? Eheh, you’ve still got a long way to go on your adventure. I suggest spending your time doing something else other than talking to me.";
                            dialogueprehm = true;
                        }
                        else
                        {
                            Main.npcChatText = "We have nothing to discuss.";
                        }
                    }
                }
            }
        }

        public static void AddItem(bool check, string mod, string item, int price, ref Chest shop, ref int nextSlot)
        {
            if (shop1)
            {
                Mod calamityMod = ModLoader.GetMod("CalamityMod");
                shop.item[nextSlot].SetDefaults(ItemType<Items.JohnWulfrumSummon>());
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 10, 0, 0);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemType<Items.JohnWulfrumDoor>());
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(299, 99, 99, 99);
                nextSlot++;
            }
        }

        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            if (shop1)
            {
                Mod calamityMod = ModLoader.GetMod("CalamityMod");
                shop.item[nextSlot].SetDefaults(ItemType<Items.JohnWulfrumSummon>());
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 10, 0, 0);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemType<Items.JohnWulfrumDoor>());
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(299, 99, 99, 99);
                nextSlot++;
            }
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
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(choppercounter);
            writer.Write(sizetimer);
            writer.Write(sizeup);
            writer.Write(dialogueprehm);
            writer.Write(dialoguehm);
            writer.Write(dialogueplant);
            writer.Write(dialoguepbg);
            writer.Write(dialoguebirb);
            writer.Write(dialoguedog);
            writer.Write(dialogueyharon);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            dialogueprehm = reader.ReadBoolean();
            dialoguehm = reader.ReadBoolean();
            dialogueplant = reader.ReadBoolean();
            dialoguepbg = reader.ReadBoolean();
            dialoguebirb = reader.ReadBoolean();
            dialoguedog = reader.ReadBoolean();
            dialogueyharon = reader.ReadBoolean();
            sizeup = reader.ReadBoolean();
            sizetimer = reader.ReadInt32();
            choppercounter = reader.ReadInt32();
        }
    }
}