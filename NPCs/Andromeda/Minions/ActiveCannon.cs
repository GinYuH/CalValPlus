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

	public class ActiveCannon : ModNPC

	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Heavy Turret");
		}
		public override void SetDefaults()
		{
			NPC.damage = 0;
			NPC.npcSlots = 0f;
			NPC.width = 68; //324
			NPC.height = 44; //216
			NPC.defense = 10;
			NPC.lifeMax = 2000;
			NPC.boss = true;
			NPC.aiStyle = -1; //new
			Main.npcFrameCount[NPC.type] = 1; //new
			AIType = -1; //new
			AnimationType = -1; //new
			NPC.knockBackResist = 0f;
			NPC.value = Item.buyPrice(0, 0, 0, 0);
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.lavaImmune = true;
			NPC.behindTiles = false;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			NPC.dontTakeDamage = false;
		}

		int lasercounter = 0;
		int isleft = 1;
		public override void AI()
		{

			lasercounter++;
			//Vector2 positioning = new Vector2(npc.Center.X, npc.Center.Y);
			//float xpos = Main.npc[CalValPlusGlobalNPC.androalive].Center.X - positioning.X;
			//float ypos = Main.npc[CalValPlusGlobalNPC.androalive].Center.Y - positioning.Y;
			NPC.position.Y = Main.npc[CalValPlusGlobalNPC.androalive].Center.Y - 30;
			if (NPC.ai[0] == 0f)
			{
				NPC.position.X = Main.npc[CalValPlusGlobalNPC.androalive].Center.X + 300;
			}
			if (NPC.ai[0] == 1f)
			{
				NPC.position.X = Main.npc[CalValPlusGlobalNPC.androalive].Center.X + 340;
			}
			if (NPC.ai[0] == 2f)
			{
				NPC.position.X = Main.npc[CalValPlusGlobalNPC.androalive].Center.X + 260;
			}
			if (NPC.ai[0] == 3f)
			{
				NPC.position.X = Main.npc[CalValPlusGlobalNPC.androalive].Center.X - 300;
			}
			if (NPC.ai[0] == 4f)
			{
				NPC.position.X = Main.npc[CalValPlusGlobalNPC.androalive].Center.X - 340;
			}
			if (NPC.ai[0] == 5f)
			{
				NPC.position.X = Main.npc[CalValPlusGlobalNPC.androalive].Center.X - 260;
			}

			//Flippe

			if (NPC.ai[0] >= 3f)
            {
				isleft = -1;
            }

			//Lasers

			if ((lasercounter == 180 && NPC.ai[0] == 2f) || (lasercounter == 190 && NPC.ai[0] == 0f) || (lasercounter == 200 && NPC.ai[0] == 1f) || (lasercounter == 180 && NPC.ai[0] == 5f) || (lasercounter == 190 && NPC.ai[0] == 3f) || (lasercounter == 200 && NPC.ai[0] == 4f))
            {
				NPC.TargetClosest();
				if (NPC.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
				{
					Vector2 position = NPC.Center;
					Vector2 targetPosition = Main.player[NPC.target].Center;
					Vector2 direction = targetPosition - position;
					direction.Normalize();
					float speed = 10f;
					int type = ProjectileID.MartianWalkerLaser;
					int damage = NPC.damage; 
					Projectile.NewProjectile(NPC.GetSource_FromAI(), position, direction * speed, type, damage, 0f, Main.myPlayer);
				}
			}
			if (lasercounter > 200)
            {
				lasercounter = 0;
            }
		}

		public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			float androframe = 1f / (float)Main.npcFrameCount[NPC.type];

			//Wings
			Texture2D wingtexture = (ModContent.Request<Texture2D>("CalValPlus/NPCs/Andromeda/Minions/ActiveCannon").Value);

			int wingtextureheight = (int)((float)(NPC.frame.Y / NPC.frame.Height) * androframe) * (wingtexture.Height / 1);

			Rectangle wingtexturesquare = new Rectangle(0, wingtextureheight, wingtexture.Width, wingtexture.Height / 1);
			Color wingtexturealpha = NPC.GetAlpha(drawColor);
			spriteBatch.Draw(wingtexture, NPC.Center - Main.screenPosition + new Vector2(0f, NPC.gfxOffY), wingtexturesquare, wingtexturealpha, NPC.rotation, Utils.Size(wingtexturesquare) / 2f, NPC.scale, isleft == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 4f);
		}
	}
}