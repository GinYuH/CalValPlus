using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Terraria.ModLoader;
using CalValPlus.Projectiles;

namespace CalValPlus.Projectiles
{
	public class PulseLaser : ModProjectile
	{
		public override string Texture => "CalValPlus/Projectiles/PulseProjectile";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pulse Laser");
		}
		public override void SetDefaults()
		{
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.hostile = true;
			Projectile.timeLeft = 100;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
		}
		public override void AI()
		{
			for (int dusttimer = 0; dusttimer < 2; dusttimer++)
			{
				Vector2 dustpos = Projectile.position;
				int dusty = Dust.NewDust(Projectile.position, 1, 1, 27, 0f, 0f, 0, default, 1f);
				Main.dust[dusty].noGravity = true;
			}
		}
	}
}
