using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalValPlus.Projectiles;

namespace CalValPlus.NPCs.Andromeda.Minions
{

	public class MachineGunDrone : ModNPC

	{
		public override string Texture => "CalValPlus/NPCs/Andromeda/Minions/AssaultDrone";
		private bool noai = true;
		int lasertimer = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Assault Drone");
			Main.npcFrameCount[npc.type] = 8;
		}
		public override void SetDefaults()
		{
			npc.damage = 0;
			npc.npcSlots = 0f;
			npc.width = 60; //324
			npc.height = 110; //216
			npc.defense = 10;
			npc.lifeMax = 20000;
			npc.aiStyle = -1; //new\
			aiType = -1; //new
			npc.knockBackResist = 0f;
			npc.value = Item.buyPrice(0, 0, 0, 0);
			for (int k = 0; k < npc.buffImmune.Length; k++)
			{
				npc.buffImmune[k] = true;
			}
			npc.lavaImmune = true;
			npc.behindTiles = false;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.HitSound = SoundID.NPCHit4;
			npc.DeathSound = SoundID.NPCDeath14;
			npc.dontTakeDamage = false;
		}

		public override void AI()
		{
			//npc.spriteDirection = npc.direction;
			if (!NPC.AnyNPCs(ModContent.NPCType<Andromeda>()) && !NPC.AnyNPCs(ModContent.NPCType<ItNPC>()))
			{
				npc.active = false;
			}
			npc.TargetClosest();
			if (CalValPlusGlobalNPC.androalive < 0)
			{
				npc.active = false;
				npc.netUpdate = true;
			}
			npc.spriteDirection = -npc.direction;
			if (noai)
			{
				npc.ai[1]++;
				if (npc.ai[1] >= 60)
				{
					npc.ai[1] = 0;
					npc.ai[0] = 1;
					noai = false;
				}
			}
			Vector2 vector97 = new Vector2(npc.Center.X, npc.Center.Y);
			float num816 = Main.npc[CalValPlusGlobalNPC.androalive].Center.X - vector97.X;
			float num817 = Main.npc[CalValPlusGlobalNPC.androalive].Center.Y - vector97.Y;
			
			if (npc.ai[0] == 1)
			{
				npc.ai[1]++;
				int num412 = 1;
				/*if (npc.position.X + (float)(npc.width / 2) < Main.player[npc.target].position.X + (float)Main.player[npc.target].width)
				{
					num412 = -1;
				}*/
				float num413 = 25f;
				float num414 = 1.2f;
				float distanceX = 140f;
				float yoffset = 0f;

				if (npc.ai[3] == 0f)
				{
					distanceX = 340f;
					yoffset = -180f;
					num412 = 1;
				}
				else if (npc.ai[3] == 1f)
				{
					distanceX = 340f;
					yoffset = -180f;
					num412 = -1;
				}
				else if (npc.ai[3] == 2f)
				{
					distanceX = 480f;
					yoffset = -240f;
					num412 = 1;
				}
				else if (npc.ai[3] == 3f)
				{
					distanceX = 480f;
					yoffset = -240f;
					num412 = -1;
				}
				else
				{
					distanceX = 480f;
					yoffset = 240f;
				}
				Vector2 vector40 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
				float num415 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) + (float)(num412 * distanceX) - vector40.X;
				float num416 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) + yoffset - vector40.Y;
				float num417 = (float)Math.Sqrt(num415 * num415 + num416 * num416);
				num417 = num413 / num417;
				num415 *= num417;
				num416 *= num417;
				if (npc.velocity.X < num415)
				{
					npc.velocity.X += num414;
					if (npc.velocity.X < 0f && num415 > 0f)
					{
						npc.velocity.X += num414;
					}
				}
				else if (npc.velocity.X > num415)
				{
					npc.velocity.X -= num414;
					if (npc.velocity.X > 0f && num415 < 0f)
					{
						npc.velocity.X -= num414;
					}
				}
				if (npc.velocity.Y < num416)
				{
					npc.velocity.Y += num414;
					if (npc.velocity.Y < 0f && num416 > 0f)
					{
						npc.velocity.Y += num414;
					}
				}
				else if (npc.velocity.Y > num416)
				{
					npc.velocity.Y -= num414;
					if (npc.velocity.Y > 0f && num416 < 0f)
					{
						npc.velocity.Y -= num414;
					}
				}
				if (npc.ai[1] >= 120)
				{
					npc.ai[1] = 0;
					npc.ai[2] = 0;
					npc.ai[0] = 3;
				}

			}
			if (npc.ai[0] == 3)
			{
				npc.velocity.X = 0;
				npc.velocity.Y = 0;
				npc.ai[1]++;
				npc.ai[2]++;
				if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient && npc.ai[1] == 8f)
				{
					Vector2 position = npc.Center;
					Vector2 targetPosition = Main.player[npc.target].Center;
					Vector2 direction = targetPosition - position;
					direction.Normalize();
					float speed = 10f;
					int type = ProjectileID.MartianWalkerLaser;
					int damage = 40;
					Projectile.NewProjectile(position, direction * speed, type, damage, 0f, Main.myPlayer);
					npc.ai[1] = 0;
				}
				if (npc.ai[2] >= 140)
				{
					npc.ai[1] = 0;
					npc.ai[2] = 0;
					npc.ai[0] = 4;
				}
			}
			if (npc.ai[0] == 4)
            {
				npc.velocity.X = 0;
				npc.velocity.Y = 0;
				npc.ai[1]++;
				if (npc.ai[1] >= 120)
                {
					npc.ai[1] = 0;
					npc.ai[0] = 1;
                }
            }
		}
		public override void FindFrame(int frameHeight)
		{
			npc.frameCounter += 0.15f;
			npc.frameCounter %= Main.npcFrameCount[npc.type];
			int frame = (int)npc.frameCounter;
			npc.frame.Y = frame * frameHeight;
		}
		public override bool CheckActive() { return false; }

		public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			float androframe = 8f / (float)Main.npcFrameCount[npc.type];

			//Wings
			Texture2D wingtexture = (ModContent.GetTexture("CalValPlus/NPCs/Andromeda/Minions/AssaultDroneGlow"));

			int wingtextureheight = (int)((float)(npc.frame.Y / npc.frame.Height) * androframe) * (wingtexture.Height / 8);

			Rectangle wingtexturesquare = new Rectangle(0, wingtextureheight - 5, wingtexture.Width, wingtexture.Height / 8);
			Color wingtexturealpha = npc.GetAlpha(drawColor);
			spriteBatch.Draw(wingtexture, npc.Center - Main.screenPosition + new Vector2(0f, npc.gfxOffY), wingtexturesquare, Color.White, npc.rotation, Utils.Size(wingtexturesquare) / 2f, npc.scale, npc.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
		}
	}
}