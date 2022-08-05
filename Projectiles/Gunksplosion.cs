using Terraria;
using Terraria.Audio;
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
			Projectile.width = 500;
			Projectile.height = 500;
			Projectile.friendly = true;
			Projectile.timeLeft = 30;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = -1;
		}
		public override void AI()
		{
			for (int x = 0; x < 15; x++)
			{
				int dust = Dust.NewDust(Projectile.position, 500, 500, 15, 0f, 0f, 0, new Color(255, 255, 255), 1f);
				Main.dust[dust].noGravity = true;
			}
		}
		public override void Kill(int timeLeft)
		{
			int dust = Dust.NewDust(Projectile.position, 500, 500, 15, 0f, 0f, 0, new Color(255, 255, 255), 1f);
			int dust2 = Dust.NewDust(Projectile.Center, 400, 400, 197, 0f, 0f, 0, new Color(255, 255, 255), 2.105263f);
			Main.dust[dust].noGravity = true;
			Main.dust[dust2].noGravity = true;
			SoundEngine.PlaySound(SoundID.Item100, Projectile.position);
		}
	}
}
