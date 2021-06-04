using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Terraria.ModLoader;

namespace CalValPlus.Projectiles
{
	public class Gunksplosion : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gunk");
		}
		public override void SetDefaults()
		{
			projectile.width = 500;
			projectile.height = 500;
			projectile.friendly = true;
			projectile.timeLeft = 30;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.ranged = true;
			projectile.penetrate = -1;
		}
		public override void AI()
		{
			for (int x = 0; x < 15; x++)
			{
				int dust = Dust.NewDust(projectile.position, 500, 500, 15, 0f, 0f, 0, new Color(255, 255, 255), 1f);
				Main.dust[dust].noGravity = true;
			}
		}
		public override void Kill(int timeLeft)
		{
			int dust = Dust.NewDust(projectile.position, 500, 500, 15, 0f, 0f, 0, new Color(255, 255, 255), 1f);
			int dust2 = Dust.NewDust(projectile.Center, 400, 400, 197, 0f, 0f, 0, new Color(255, 255, 255), 2.105263f);
			Main.dust[dust].noGravity = true;
			Main.dust[dust2].noGravity = true;
			Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 100);
		}
	}
}
