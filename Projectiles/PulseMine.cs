using Terraria;
using Terraria.Audio;
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
			Projectile.width = 20;
			Projectile.height = 20;
			Projectile.scale = 1.6f;
			Projectile.hostile = true;
			Projectile.timeLeft = 90;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = false;
		}
		public override void AI()
		{
			for (int dusttimer = 0; dusttimer < 2; dusttimer++)
			{
				Vector2 dustpos = Projectile.position;
				int dusty = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 27, 0f, 0f, 0, default, 1f);
				Main.dust[dusty].noGravity = true;
			}
		}
		public override void Kill(int timeLeft)
		{
			SoundEngine.PlaySound(new Terraria.Audio.SoundStyle("CalValPlus/Sounds/LaserCannon"), Projectile.Center);
			Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, -40, 0, Mod.Find<ModProjectile>("PulseLaser").Type, 80, (int)Projectile.knockBack, Projectile.owner);
			Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.Y, 40, 0, Mod.Find<ModProjectile>("PulseLaser").Type, 80, (int)Projectile.knockBack, Projectile.owner);
			Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.Y, 0, 40, Mod.Find<ModProjectile>("PulseLaser").Type, 80, (int)Projectile.knockBack, Projectile.owner);
			Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.Y, 0, -40, Mod.Find<ModProjectile>("PulseLaser").Type, 80, (int)Projectile.knockBack, Projectile.owner);
		}
	}
}
