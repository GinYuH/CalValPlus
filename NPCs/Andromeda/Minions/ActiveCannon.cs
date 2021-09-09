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
			npc.damage = 0;
			npc.npcSlots = 0f;
			npc.width = 68; //324
			npc.height = 44; //216
			npc.defense = 10;
			npc.lifeMax = 2000;
			npc.boss = true;
			npc.aiStyle = -1; //new
			Main.npcFrameCount[npc.type] = 1; //new
			aiType = -1; //new
			animationType = -1; //new
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

		int lasercounter = 0;
		int isleft = 1;
		public override void AI()
		{

			lasercounter++;
			//Vector2 positioning = new Vector2(npc.Center.X, npc.Center.Y);
			//float xpos = Main.npc[CalValPlusGlobalNPC.androalive].Center.X - positioning.X;
			//float ypos = Main.npc[CalValPlusGlobalNPC.androalive].Center.Y - positioning.Y;
			npc.position.Y = Main.npc[CalValPlusGlobalNPC.androalive].Center.Y - 30;
			if (npc.ai[0] == 0f)
			{
				npc.position.X = Main.npc[CalValPlusGlobalNPC.androalive].Center.X + 300;
			}
			if (npc.ai[0] == 1f)
			{
				npc.position.X = Main.npc[CalValPlusGlobalNPC.androalive].Center.X + 340;
			}
			if (npc.ai[0] == 2f)
			{
				npc.position.X = Main.npc[CalValPlusGlobalNPC.androalive].Center.X + 260;
			}
			if (npc.ai[0] == 3f)
			{
				npc.position.X = Main.npc[CalValPlusGlobalNPC.androalive].Center.X - 300;
			}
			if (npc.ai[0] == 4f)
			{
				npc.position.X = Main.npc[CalValPlusGlobalNPC.androalive].Center.X - 340;
			}
			if (npc.ai[0] == 5f)
			{
				npc.position.X = Main.npc[CalValPlusGlobalNPC.androalive].Center.X - 260;
			}

			//Flippe

			if (npc.ai[0] >= 3f)
            {
				isleft = -1;
            }

			//Lasers

			if ((lasercounter == 180 && npc.ai[0] == 2f) || (lasercounter == 190 && npc.ai[0] == 0f) || (lasercounter == 200 && npc.ai[0] == 1f) || (lasercounter == 180 && npc.ai[0] == 5f) || (lasercounter == 190 && npc.ai[0] == 3f) || (lasercounter == 200 && npc.ai[0] == 4f))
            {
				npc.TargetClosest();
				if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
				{
					Vector2 position = npc.Center;
					Vector2 targetPosition = Main.player[npc.target].Center;
					Vector2 direction = targetPosition - position;
					direction.Normalize();
					float speed = 10f;
					int type = ProjectileID.MartianWalkerLaser;
					int damage = npc.damage; 
					Projectile.NewProjectile(position, direction * speed, type, damage, 0f, Main.myPlayer);
				}
			}
			if (lasercounter > 200)
            {
				lasercounter = 0;
            }
		}

		public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			float androframe = 1f / (float)Main.npcFrameCount[npc.type];

			//Wings
			Texture2D wingtexture = (ModContent.GetTexture("CalValPlus/NPCs/Andromeda/Minions/ActiveCannon"));

			int wingtextureheight = (int)((float)(npc.frame.Y / npc.frame.Height) * androframe) * (wingtexture.Height / 1);

			Rectangle wingtexturesquare = new Rectangle(0, wingtextureheight, wingtexture.Width, wingtexture.Height / 1);
			Color wingtexturealpha = npc.GetAlpha(drawColor);
			spriteBatch.Draw(wingtexture, npc.Center - Main.screenPosition + new Vector2(0f, npc.gfxOffY), wingtexturesquare, wingtexturealpha, npc.rotation, Utils.Size(wingtexturesquare) / 2f, npc.scale, isleft == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 4f);
		}
	}
}