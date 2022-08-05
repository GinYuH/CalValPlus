using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Terraria.ModLoader;
using CalValPlus.Projectiles;

namespace CalValPlus.Projectiles
{
	public class PlasmaGunk : ModProjectile
	{
		public override string Texture => "CalValPlus/Projectiles/PlasmaProjectile";

		private bool hittile = false;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Plasma");
		}
		public override void SetDefaults()
		{
			Projectile.width = 20;
			Projectile.height = 20;
			Projectile.timeLeft = 480;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = false;
			Projectile.hostile = true;
		}
		public override void AI()
		{
			//110 is alto a type used by plasma weaponry
			for (int dusttimer = 0; dusttimer < 2; dusttimer++)
			{
				Vector2 dustpos = Projectile.position;
				int dusty = Dust.NewDust(Projectile.position, 1, 1, 107, 0f, 0f, 0, default, 1f);
				Main.dust[dusty].noGravity = true;
			}
			if (Projectile.velocity.Y <= 10f)
            {
                Projectile.velocity.Y += 0.15f;
            }
		}
	}
}
