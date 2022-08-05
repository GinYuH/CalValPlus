using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalValPlus.Projectiles;
using CalamityMod;
using CalValPlus.NPCs.JohnWulfrum;

namespace CalValPlus.NPCs.Bumblebird
{

	public class Bumblebird : ModNPC

	{
		int laser = 0;
		bool alt = false;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bumblebird");
			Main.npcFrameCount[NPC.type] = 5;
		}

		public override void SetDefaults()
		{
			NPC.damage = 0;
			NPC.npcSlots = 0f;
			NPC.width = 218;
			NPC.height = 200;
			NPC.defense = 50;
			NPC.lifeMax = 300000;
			NPC.aiStyle = -1; 
			AIType = -1; 
			NPC.knockBackResist = 0f;
			NPC.value = Item.buyPrice(1, 0, 0, 0);
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit51;
			NPC.DeathSound = SoundID.NPCDeath46;
			NPC.DR_NERD(0.1f);
		}

		public override void AI()
        {
			if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
			{
				NPC.TargetClosest(true);
			}
			NPC.spriteDirection = NPC.direction;

			//Begin!!!
			if (NPC.ai[0] < 2)
			{
				NPC.ai[1]++;
				NPC.velocity.X = NPC.velocity.Y = 0;
				if (NPC.ai[1] >= 60)
                {
					NPC.ai[1] = 0;
					NPC.ai[0] = 2;
                }
            }
			//Line up with the player
			else if (NPC.ai[0] == 2)
			{

				NPC.TargetClosest();
				NPC.ai[1]++;
				int num412 = 1;
				float num413 = 25f;
				float num414 = 1.2f;
				float distanceX = 1000f;
				float yoffset = 0f;
				Vector2 vector40 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float playerY = Main.player[NPC.target].position.Y - (NPC.position.Y + NPC.height);
				float playerX = Main.player[NPC.target].position.X - (NPC.position.X + NPC.width);
				float num415 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) + (float)(num412 * distanceX) - vector40.X;
				float num416 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) + yoffset - vector40.Y;
				float num417 = (float)Math.Sqrt(num415 * num415 + num416 * num416);
				num417 = num413 / num417;
				num415 *= num417;
				num416 *= num417;
				if (NPC.velocity.X < num415)
				{
					NPC.velocity.X += num414;
					if (NPC.velocity.X < 0f && num415 > 0f)
					{
						NPC.velocity.X += num414;
					}
				}
				else if (NPC.velocity.X > num415)
				{
					NPC.velocity.X -= num414;
					if (NPC.velocity.X > 0f && num415 < 0f)
					{
						NPC.velocity.X -= num414;
					}
				}
				if (NPC.velocity.Y < num416)
				{
					NPC.velocity.Y += num414;
					if (NPC.velocity.Y < 0f && num416 > 0f)
					{
						NPC.velocity.Y += num414;
					}
				}
				else if (NPC.velocity.Y > num416)
				{
					NPC.velocity.Y -= num414;
					if (NPC.velocity.Y > 0f && num416 < 0f)
					{
						NPC.velocity.Y -= num414;
					}
				}
				if (playerX > 800 && playerY < 40 && playerY > -40 && playerX < 1200)
				{
					NPC.velocity.Y = 0;
					NPC.velocity.X = 0;
					NPC.ai[1] = 0;
					NPC.ai[0] = 3;
				}
				if (NPC.ai[1] >= 480)
				{
					NPC.ai[1] = 0;
					NPC.ai[0] = 3;
				}
			}
			else if (NPC.ai[0] == 3)
			{
				NPC.ai[1]++;
				float playerX = Main.player[NPC.target].position.X - (NPC.position.X + NPC.width);
				if (playerX > 800 && NPC.velocity.X > -15f)
                {
					NPC.velocity.X -= 0.2f;
                }
				else if (playerX < -800 && NPC.velocity.X < 15)
                {
					NPC.velocity.X += 0.2f;
				}

				if ((NPC.velocity.X >= 0 && playerX > 1200) || (NPC.velocity.X < 0 && playerX < -1200))
                {
					NPC.ai[0] = 4;
                }
				if (NPC.ai[1] >= 480)
                {
					NPC.ai[1] = 4;
                }
			}
			else if (NPC.ai[0] == 4)
            {
				NPC.ai[1]++;
				NPC.ai[2]++;
				NPC.velocity.X = NPC.velocity.Y = 0;
				if (NPC.ai[2] >= 30)
                {
					SoundEngine.PlaySound(SoundID.Item43, NPC.position);
					Vector2 position = NPC.Center;
					position.X = NPC.Center.X + (10f * NPC.direction);
					Vector2 targetPosition = Main.player[NPC.target].Center;
					Vector2 direction = targetPosition - position;
					direction.Normalize();
					float speed = 10f;
					int type = ModContent.ProjectileType<WulfrumBoltHostile>();
					int damage = Main.expertMode ? 15 : 20;
					Projectile.NewProjectile(NPC.GetSource_FromAI(), position, direction * speed, type, damage, 0f, Main.myPlayer);
					NPC.ai[2] = 0;
				}
				if (NPC.ai[1] >= 480)
                {
					NPC.ai[1] = 0;
					NPC.ai[2] = 0;
					NPC.ai[0] = 5;
                }
            }
			else if (NPC.ai[0] == 5)
			{
				NPC.ai[1]++;
				NPC.ai[2]++;
				NPC.velocity.X = NPC.velocity.Y = 0;
				if (NPC.ai[2] >= 60)
                {
					NPC.ai[3]++;
                }
				if (NPC.ai[3] >= 60)
				{
					SoundEngine.PlaySound(SoundID.Item15, NPC.position);
					NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y + 14, ModContent.NPCType<WulfrumDroid>(), 0, 2f, 0f, 0f, Main.rand.Next(2, 4), 255);
					NPC.ai[3] = 0;
                }
				if (NPC.ai[1] >= 300)
                {
					NPC.ai[1] = 0;
					NPC.ai[2] = 0;
					NPC.ai[3] = 0;
					NPC.ai[0] = 1;
                }
			}
		}
	}
}