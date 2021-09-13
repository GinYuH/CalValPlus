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
			Main.projFrames[projectile.type] = 6;
		}

		public override void SetDefaults()
		{
			projectile.width = 20;
			projectile.height = 20;
			projectile.hostile = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 180;
			projectile.CloneDefaults(ProjectileID.SaucerMissile);
			aiType = ProjectileID.SaucerMissile;
		}

		public override void AI()
        {
			float speedX = projectile.velocity.X;
			float speedY = projectile.velocity.Y;
			if (Math.Abs(speedX) <= 6f && Math.Abs(speedY) <= 6f)
			{
				projectile.velocity *= 1.01f;
			}
        }
	}
}