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
    internal class HypnosPlug : ModNPC
    {
        public bool initialized = false;
        public bool afterimages = false;
        public bool hypnosafter = false;
        NPC hypnos;
        float rottimer = 0;
        int redlaserproj = ProjectileType<AstralLaser>();
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("XP-00 Hypnos Plug");
            Main.npcFrameCount[NPC.type] = 1;
            NPCID.Sets.TrailingMode[NPC.type] = 1;
        }

        public override void SetDefaults()
        {
            NPC.noGravity = true;
            NPC.lavaImmune = true;
            NPC.aiStyle = -1;
            NPC.lifeMax = 20000;
            NPC.damage = 0;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.Item14;
            NPC.knockBackResist = 0f;
            NPC.noTileCollide = true;
            NPC.width = 14;
            NPC.height = 14;
        }

        public override void AI()
        {
            if (!initialized)
            {
                hypnos = Main.npc[(int)NPC.ai[0]];
            }
            if (!hypnos.active)
            {
                NPC.active = false;
            }

            int heighoffset = 20;
            int heighoffsetin = 30;
            int innerdist = 70;
            int outerdist = 80;
            Vector2 leftleftplug = new Vector2(hypnos.Center.X - outerdist, hypnos.Center.Y + heighoffset);
            Vector2 leftplug = new Vector2(hypnos.Center.X - innerdist, hypnos.Center.Y + heighoffsetin);
            Vector2 rightplug = new Vector2(hypnos.Center.X + innerdist, hypnos.Center.Y + heighoffsetin);
            Vector2 rightrightplug = new Vector2(hypnos.Center.X + outerdist, hypnos.Center.Y + heighoffset);

            Vector2 pluglocation = hypnos.Center;
            int startneuron = 0;

            switch (NPC.ai[1])
            {
                case 0:
                    pluglocation = leftleftplug;
                    startneuron = 0;
                    NPC.rotation = -(float)Math.PI / 2;
                    break;
                case 1:
                    pluglocation = leftplug;
                    startneuron = 3;
                    NPC.rotation = -(float)Math.PI / 2;
                    break;
                case 2:
                    pluglocation = rightplug;
                    NPC.rotation = (float)Math.PI / 2;
                    startneuron = 6;
                    break;
                case 3:
                    pluglocation = rightrightplug;
                    NPC.rotation = (float)Math.PI / 2;
                    startneuron = 9;
                    break;
            }

            NPC.position = pluglocation;
            if (NPC.ai[2] == 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<AergiaNeuron>(), 0, hypnos.whoAmI, startneuron + i, 0, NPC.whoAmI);
                }
                NPC.ai[2] = 1;
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

        public override bool CheckActive()
        {
            return false;
        }
    }
}