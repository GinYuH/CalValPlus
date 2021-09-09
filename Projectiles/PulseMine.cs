using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Terraria.ModLoader;
using CalValPlus.Projectiles;

namespace CalValPlus.Projectiles
{
	public class PulseMine : ModProjectile
	{
		public override string Texture => "CalValPlus/Projectiles/PulseProjectile";

		private bool hittile = false;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pulse Mine");
		}
		public override void SetDefaults()
		{
			projectile.width = 20;
			projectile.height = 20;
			projectile.scale = 1.6f;
			projectile.hostile = true;
			projectile.timeLeft = 90;
			projectile.tileCollide = false;
			projectile.ignoreWater = false;
		}
		public override void AI()
		{
			for (int dusttimer = 0; dusttimer < 2; dusttimer++)
			{
				Vector2 dustpos = projectile.position;
				int dusty = Dust.NewDust(projectile.position, projectile.width, projectile.height, 27, 0f, 0f, 0, default, 1f);
				Main.dust[dusty].noGravity = true;
			}
		}
		public override void Kill(int timeLeft)
		{
			Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/LaserCannon"));
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -40, 0, mod.ProjectileType("PulseLaser"), 80, (int)projectile.knockBack, projectile.owner);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 40, 0, mod.ProjectileType("PulseLaser"), 80, (int)projectile.knockBack, projectile.owner);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 40, mod.ProjectileType("PulseLaser"), 80, (int)projectile.knockBack, projectile.owner);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, -40, mod.ProjectileType("PulseLaser"), 80, (int)projectile.knockBack, projectile.owner);
		}
	}
}
