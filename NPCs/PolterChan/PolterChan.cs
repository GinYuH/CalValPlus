using MonoMod.Cil;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;

namespace CalValPlus.NPCs.PolterChan
{
    internal class PolterChan : ModNPC
    {
        private bool ogai = true;
        int floattimer = 0;
        int attack = 0;
		int second = 0;
		int angulartimer = 0;
		int angularval = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Polter-Chan");
            Main.npcFrameCount[NPC.type] = 3;
        }

        public override void SetDefaults()
        {

            NPC.noGravity = true;
            NPC.lavaImmune = true;
            NPC.aiStyle = -1;
            NPC.npcSlots = 25f;
            NPC.lifeMax = 400000;
            NPC.damage = 0;
            NPC.HitSound = SoundID.NPCHit49;
            NPC.DeathSound = SoundID.NPCDeath51;
            NPC.knockBackResist = 0f;
            NPC.noTileCollide = true;
			NPC.width = 200;
			NPC.height = 200;
			NPC.boss = true;
        }

        public override void AI()
        {
			Mod clamMod = ModLoader.GetMod("CalamityMod");
			if (NPC.ai[0] == 0f)
			{
				attack++;
				second++;
				if (NPC.ai[2] == 0f)
				{
					NPC.TargetClosest();
					if (NPC.Center.X < Main.player[NPC.target].Center.X)
					{
						NPC.ai[2] = 1f;
					}
					else
					{
						NPC.ai[2] = -1f;
					}
				}
				NPC.TargetClosest();
				int num891 = 800;
				float num892 = Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X);
				if (NPC.Center.X < Main.player[NPC.target].Center.X && NPC.ai[2] < 0f && num892 > (float)num891)
				{
					NPC.ai[2] = 0f;
				}
				if (NPC.Center.X > Main.player[NPC.target].Center.X && NPC.ai[2] > 0f && num892 > (float)num891)
				{
					NPC.ai[2] = 0f;
				}
				float num893 = 0.45f;
				float num894 = 7f;
				NPC.velocity.X += NPC.ai[2] * num893;
				if (NPC.velocity.X > num894)
				{
					NPC.velocity.X = num894;
				}
				if (NPC.velocity.X < 0f - num894)
				{
					NPC.velocity.X = 0f - num894;
				}
				float num895 = Main.player[NPC.target].position.Y - (NPC.position.Y + NPC.height);
				if (num895 < 150f)
				{
					NPC.velocity.Y -= 0.2f;
				}
				if (num895 > 200f)
				{
					NPC.velocity.Y += 0.2f;
				}
				if (NPC.velocity.Y > 8f)
				{
					NPC.velocity.Y = 8f;
				}
				if (NPC.velocity.Y < -8f)
				{
					NPC.velocity.Y = -8f;
				}
				/*if ((num892 < 500f || npc.ai[3] < 0f) && npc.position.Y < Main.player[npc.target].position.Y)
				{
					npc.ai[3] += 1f;
					int num896 = 13;
					if (npc.life < npc.lifeMax * 0.75)
					{
						num896 = 12;
					}
					if (npc.life < npc.lifeMax * 0.5)
					{
						num896 = 11;
					}
					if (npc.life < npc.lifeMax * 0.25)
					{
						num896 = 10;
					}
					num896++;
					if (npc.ai[3] > (float)num896)
					{
						npc.ai[3] = -num896;
					}
					if (npc.ai[3] == 0f && Main.netMode != 1)
					{
						Vector2 vector113 = new Vector2(npc.Center.X, npc.Center.Y);
						vector113.X += npc.velocity.X * 7f;
						float num897 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector113.X;
						float num898 = Main.player[npc.target].Center.Y - vector113.Y;
						float num899 = (float)Math.Sqrt(num897 * num897 + num898 * num898);
						float num900 = 6f;
						num899 = num900 / num899;
						num897 *= num899;
						num898 *= num899;
						int num901 = Projectile.NewProjectile(vector113.X, vector113.Y, num897, num898, clamMod.ProjectileType("PhantomBlast"), 42, 0f, Main.myPlayer);
					}
				}*/
				if (attack == 10)
                {
					angulartimer++;
					Vector2 vector113 = new Vector2(NPC.Center.X, NPC.Center.Y);
					angularval = -10 + (angulartimer / 20);
					int poltl = Projectile.NewProjectile(NPC.GetSource_FromAI(), vector113.X, vector113.Y, angularval, 5, clamMod.Find<ModProjectile>("PhantomBlast").Type, 42, 0f, Main.myPlayer);
					int poltr = Projectile.NewProjectile(NPC.GetSource_FromAI(), vector113.X, vector113.Y, -angularval, 5, clamMod.Find<ModProjectile>("PhantomBlast").Type, 42, 0f, Main.myPlayer);
					Main.projectile[poltl].timeLeft = 180;
					Main.projectile[poltr].timeLeft = 180;
					NPC.netUpdate = true;
					if (angularval <= 0)
                    {
						angulartimer = 0;
                    }
					attack = 0;
				}
				if (second == 20)
                {
					int shotspacing = 0;
					shotspacing = (Main.rand.Next(-2, 2));
					float num897 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - NPC.Center.X;
					float num898 = Main.player[NPC.target].Center.Y - NPC.Center.Y;
					float num899 = (float)Math.Sqrt(num897 * num897 + num898 * num898);
					float num900 = 6f;
					num899 = num900 / num899;
					num897 *= num899;
					num898 *= num899;
					int num901 = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, num897 + shotspacing, num898, clamMod.Find<ModProjectile>("PhantomBlast2").Type, 42, 0f, Main.myPlayer);
					second = 0;
				}
				else if (NPC.ai[3] < 0f)
				{
					NPC.ai[3] += 1f;
				}
				/*if (Main.netMode != 1)
				{
					npc.ai[1] += Main.rand.Next(1, 4);
					if (npc.ai[1] > 800f && num892 < 600f)
					{
						npc.ai[0] = -1f;
					}
				}*/
			}
			if (NPC.ai[0] == -1f)
			{
				NPC.TargetClosest();
				NPC.ai[0] = 1;
				NPC.ai[1] = 0f;
				NPC.ai[2] = 0f;
				NPC.ai[3] = 0f;
			}
		}
        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }
        public override void OnKill()
        {
            Mod clamMod = ModLoader.GetMod("CalamityMod");
            Item.NewItem(NPC.GetSource_Death(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, clamMod.Find<ModItem>("RuinousSoul").Type, 12, false, 0, false, false);
        }
    }
}