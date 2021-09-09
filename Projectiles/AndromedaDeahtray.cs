using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.ModLoader;
using CalValPlus.Projectiles;
using CalValPlus.NPCs.Andromeda;

namespace CalValPlus.Projectiles
{
	public class AndromedaDeahtray : ModProjectile
	{
		public override string Texture => "CalValPlus/Projectiles/DeathRayTop";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Andromeda MK VI Deathray");
		}
		public override void SetDefaults()
		{
			projectile.width = 36;
			projectile.height = 36;
			projectile.aiStyle = 84;
			projectile.hostile = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 600;
			projectile.tileCollide = false;
		}
		public override void AI()
		{
			int num760 = (int)projectile.ai[0] - 1;
			NPC nPC7 = Main.npc[num760];
			float num761 = nPC7.Center.Y + 46f;
			int num762 = (int)nPC7.Center.X / 16;
			int num763 = (int)num761 / 16;
			int num764 = 0;
			if (Main.tile[num762, num763].nactive() && Main.tileSolid[Main.tile[num762, num763].type] && !Main.tileSolidTop[Main.tile[num762, num763].type])
			{
				num764 = 1;
			}
			else
			{
				for (; num764 < 150 && num763 + num764 < Main.maxTilesY; num764++)
				{
					int num765 = num763 + num764;
					if (Main.tile[num762, num765].nactive() && Main.tileSolid[Main.tile[num762, num765].type] && !Main.tileSolidTop[Main.tile[num762, num765].type])
					{
						num764--;
						break;
					}
				}
			}
			projectile.position.X = nPC7.Center.X - (float)(projectile.width / 2);
			projectile.position.Y = num761;
			projectile.height = (num764 + 1) * 16;
			int num766 = (int)projectile.position.Y + projectile.height;
			if (Main.tile[num762, num766 / 16].nactive() && Main.tileSolid[Main.tile[num762, num766 / 16].type] && !Main.tileSolidTop[Main.tile[num762, num766 / 16].type])
			{
				int num767 = num766 % 16;
				projectile.height -= num767 - 2;
			}
			for (int num768 = 0; num768 < 2; num768++)
			{
				int num769 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + (float)projectile.height - 16f), projectile.width, 16, 228);
				Main.dust[num769].noGravity = true;
				Dust dust114 = Main.dust[num769];
				Dust dust2 = dust114;
				dust2.velocity *= 0.5f;
				Main.dust[num769].velocity.X -= (float)num768 - nPC7.velocity.X * 2f / 3f;
				Main.dust[num769].scale = 2.8f;
			}
			if (Main.rand.Next(5) == 0)
			{
				int num770 = Dust.NewDust(new Vector2(projectile.position.X + (float)(projectile.width / 2) - (float)(projectile.width / 2 * Math.Sign(nPC7.velocity.X)) - 4f, projectile.position.Y + (float)projectile.height - 16f), 4, 16, 31, 0f, 0f, 100, default(Color), 1.5f);
				Dust dust115 = Main.dust[num770];
				Dust dust2 = dust115;
				dust2.velocity *= 0.5f;
				Main.dust[num770].velocity.X -= nPC7.velocity.X / 2f;
				Main.dust[num770].velocity.Y = 0f - Math.Abs(Main.dust[num770].velocity.Y);
			}

			if (projectile.type == 447 && ++projectile.frameCounter >= 5)
			{
				projectile.frameCounter = 0;
				if (++projectile.frame >= 4)
				{
					projectile.frame = 0;
				}
			}

			
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			int projFrames = 1;
			Texture2D texture2D16 = (ModContent.GetTexture("CalValPlus/Projectiles/DeathRayTop"));
			Texture2D texture2D17 = (ModContent.GetTexture("CalValPlus/Projectiles/DeathRayMiddle"));
			Texture2D texture2D18 = (ModContent.GetTexture("CalValPlus/Projectiles/DeathRayBottom"));
			int num199 = texture2D16.Height / projFrames;
			int y18 = num199 * projectile.frame;
			int num200 = texture2D17.Height / projFrames;
			int num201 = num200 * projectile.frame;
			Microsoft.Xna.Framework.Rectangle value20 = new Microsoft.Xna.Framework.Rectangle(0, num201, texture2D17.Width, num200);
			Vector2 vector26 = projectile.position + new Vector2(projectile.width, 0f) / 2f + Vector2.UnitY * projectile.gfxOffY - Main.screenPosition;
			spriteBatch.Draw(texture2D18, vector26, value20, lightColor, projectile.rotation, new Vector2(texture2D17.Width / 2, 0f), projectile.scale, SpriteEffects.None, 0f);
			int num202 = projectile.height - num199 - 14;
			if (num202 < 0)
			{
				num202 = 0;
			}
			if (num202 > 0)
			{
				if (num201 == num200 * 3)
				{
					num201 = num200 * 2;
				}
				spriteBatch.Draw(texture2D18, vector26 + Vector2.UnitY * (num200 - 1), new Microsoft.Xna.Framework.Rectangle(0, num201 + num200 - 1, texture2D17.Width, 1), lightColor, projectile.rotation, new Vector2(texture2D17.Width / 2, 0f), new Vector2(1f, num202), SpriteEffects.None, 0f);
			}
			value20.Width = texture2D16.Width;
			value20.Y = y18;
			spriteBatch.Draw(texture2D16, vector26 + Vector2.UnitY * (num200 - 1 + num202), value20, lightColor, projectile.rotation, new Vector2((float)texture2D16.Width / 2f, 0f), projectile.scale, SpriteEffects.None, 0f);
			return true;
		}
	}
}

