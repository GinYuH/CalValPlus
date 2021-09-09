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
			projectile.width = 30;
			projectile.height = 30;
			projectile.scale = 1.2f;
			projectile.timeLeft = 540;
			projectile.tileCollide = false;
			projectile.ignoreWater = false;
			projectile.hostile = false;
			projectile.alpha = 255;
		}
		public override void AI()
		{
			for (int dusttimer = 0; dusttimer < 2; dusttimer++)
			{
				Vector2 dustpos = projectile.position;
				int dusty = Dust.NewDust(projectile.position, projectile.width, projectile.height, 137, 0f, 0f, 0, default, 1f);
				Main.dust[dusty].noGravity = true;
			}
			if (projectile.timeLeft > 500)
			{
				projectile.velocity *= 1.0025f;
				projectile.alpha -= 6;
			}
			else
			{
				projectile.velocity *= 0.0025f;
				projectile.hostile = true;
				projectile.alpha -= 12;
			}
		}
	}
}
