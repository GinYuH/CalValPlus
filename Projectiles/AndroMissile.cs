using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Enums;
using Terraria.ModLoader;
using static Terraria.Projectile;

namespace CalValPlus.Projectiles
{
	public class AndroMissile : ModProjectile
	{
		int safetimer = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Andromeda Homing Missile MK VI");
			Main.projFrames[Projectile.type] = 6;
		}

		public override void SetDefaults()
		{
			Projectile.width = 20;
			Projectile.height = 20;
			Projectile.hostile = true;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 180;
			Projectile.CloneDefaults(ProjectileID.SaucerMissile);
			AIType = ProjectileID.SaucerMissile;
		}

		public override void AI()
        {
			float speedX = Projectile.velocity.X;
			float speedY = Projectile.velocity.Y;
			if (Math.Abs(speedX) <= 6f && Math.Abs(speedY) <= 6f)
			{
				Projectile.velocity *= 1.01f;
			}
        }
	}
}