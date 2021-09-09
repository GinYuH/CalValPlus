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
            Main.npcFrameCount[npc.type] = 3;
        }

        public override void SetDefaults()
        {

            npc.noGravity = true;
            npc.lavaImmune = true;
            npc.aiStyle = -1;
            npc.npcSlots = 25f;
            npc.lifeMax = 400000;
            npc.damage = 0;
            npc.HitSound = SoundID.NPCHit49;
            npc.DeathSound = SoundID.NPCDeath51;
            npc.knockBackResist = 0f;
            npc.noTileCollide = true;
			npc.width = 200;
			npc.height = 200;
			npc.boss = true;
        }

        public override void AI()
        {
			Mod clamMod = ModLoader.GetMod("CalamityMod");
			if (npc.ai[0] == 0f)
			{
				attack++;
				second++;
				if (npc.ai[2] == 0f)
				{
					npc.TargetClosest();
					if (npc.Center.X < Main.player[npc.target].Center.X)
					{
						npc.ai[2] = 1f;
					}
					else
					{
						npc.ai[2] = -1f;
					}
				}
				npc.TargetClosest();
				int num891 = 800;
				float num892 = Math.Abs(npc.Center.X - Main.player[npc.target].Center.X);
				if (npc.Center.X < Main.player[npc.target].Center.X && npc.ai[2] < 0f && num892 > (float)num891)
				{
					npc.ai[2] = 0f;
				}
				if (npc.Center.X > Main.player[npc.target].Center.X && npc.ai[2] > 0f && num892 > (float)num891)
				{
					npc.ai[2] = 0f;
				}
				float num893 = 0.45f;
				float num894 = 7f;
				npc.velocity.X += npc.ai[2] * num893;
				if (npc.velocity.X > num894)
				{
					npc.velocity.X = num894;
				}
				if (npc.velocity.X < 0f - num894)
				{
					npc.velocity.X = 0f - num894;
				}
				float num895 = Main.player[npc.target].position.Y - (npc.position.Y + npc.height);
				if (num895 < 150f)
				{
					npc.velocity.Y -= 0.2f;
				}
				if (num895 > 200f)
				{
					npc.velocity.Y += 0.2f;
				}
				if (npc.velocity.Y > 8f)
				{
					npc.velocity.Y = 8f;
				}
				if (npc.velocity.Y < -8f)
				{
					npc.velocity.Y = -8f;
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
					Vector2 vector113 = new Vector2(npc.Center.X, npc.Center.Y);
					angularval = -10 + (angulartimer / 20);
					int poltl = Projectile.NewProjectile(vector113.X, vector113.Y, angularval, 5, clamMod.ProjectileType("PhantomBlast"), 42, 0f, Main.myPlayer);
					int poltr = Projectile.NewProjectile(vector113.X, vector113.Y, -angularval, 5, clamMod.ProjectileType("PhantomBlast"), 42, 0f, Main.myPlayer);
					Main.projectile[poltl].timeLeft = 180;
					Main.projectile[poltr].timeLeft = 180;
					npc.netUpdate = true;
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
					float num897 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - npc.Center.X;
					float num898 = Main.player[npc.target].Center.Y - npc.Center.Y;
					float num899 = (float)Math.Sqrt(num897 * num897 + num898 * num898);
					float num900 = 6f;
					num899 = num900 / num899;
					num897 *= num899;
					num898 *= num899;
					int num901 = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, num897 + shotspacing, num898, clamMod.ProjectileType("PhantomBlast2"), 42, 0f, Main.myPlayer);
					second = 0;
				}
				else if (npc.ai[3] < 0f)
				{
					npc.ai[3] += 1f;
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
			if (npc.ai[0] == -1f)
			{
				npc.TargetClosest();
				npc.ai[0] = 1;
				npc.ai[1] = 0f;
				npc.ai[2] = 0f;
				npc.ai[3] = 0f;
			}
		}
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 0.15f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int frame = (int)npc.frameCounter;
            npc.frame.Y = frame * frameHeight;
        }
        public override void NPCLoot()
        {
            Mod clamMod = ModLoader.GetMod("CalamityMod");
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, clamMod.ItemType("RuinousSoul"), 12, false, 0, false, false);
        }
    }
}