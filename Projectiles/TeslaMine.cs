using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Terraria.ModLoader;
using CalValPlus.Projectiles;

namespace CalValPlus.Projectiles
{
	public class TeslaMine : ModProjectile
	{
		public override string Texture => "CalValPlus/Projectiles/PlaceholderProjectile";

		private bool hittile = false;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tesla Mine");
		}
		public override void SetDefaults()
		{
			Projectile.width = 30;
			Projectile.height = 30;
			Projectile.scale = 1.2f;
			Projectile.timeLeft = 540;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = false;
			Projectile.hostile = false;
			Projectile.alpha = 255;
		}
		public override void AI()
		{
			for (int dusttimer = 0; dusttimer < 2; dusttimer++)
			{
				Vector2 dustpos = Projectile.position;
				int dusty = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 137, 0f, 0f, 0, default, 1f);
				Main.dust[dusty].noGravity = true;
			}
			if (Projectile.timeLeft > 500)
			{
				Projectile.velocity *= 1.0025f;
				Projectile.alpha -= 6;
			}
			else
			{
				Projectile.velocity *= 0.0025f;
				Projectile.hostile = true;
				Projectile.alpha -= 12;
			}
		}
	}
}
