using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Terraria.ModLoader;
using CalValPlus.Projectiles;

namespace CalValPlus.Projectiles
{
	public class WulfrumBoltHostile : ModProjectile
	{
		public override string Texture => "CalValPlus/Projectiles/InvisibleProj";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Wulfrum Bolt");
		}
		public override void SetDefaults()
		{
			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.hostile = true;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 180;
		}
		public override void AI()
		{
			Vector2 dustpos = Projectile.position;
			for (int dusttimer = 0; dusttimer < 3; dusttimer++)
			{
				int dusty = Dust.NewDust(Projectile.position, 1, 1, 61, 0f, 0f, 100, default, 3f);
				Main.dust[dusty].noGravity = true;
				Main.dust[dusty].noLight = true;
			}
			if (Main.rand.Next(8) == 0)
            {
				int dusto = Dust.NewDust(Projectile.position, 1, 1, 61, 0f, 0f, 100, default, 2.25f);
				Main.dust[dusto].noLight = true;
			}
		}
	}
}
