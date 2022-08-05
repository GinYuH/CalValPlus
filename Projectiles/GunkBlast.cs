using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Terraria.ModLoader;
using CalValPlus.Projectiles;

namespace CalValPlus.Projectiles
{
	public class GunkBlast : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gunk");
		}
		public override void SetDefaults()
		{
			Projectile.width = 36;
			Projectile.height = 32;
			Projectile.friendly = true;
			Projectile.timeLeft = 140;
			Projectile.tileCollide = true;
			Projectile.ignoreWater = false;
			Projectile.DamageType = DamageClass.Ranged;
		}
		public override void AI()
		{
			if (Main.rand.NextFloat() < 0.368421f)
			{
				int dust = Dust.NewDust(Projectile.position, 20, 20, 15, 0f, 0f, 0, new Color(255, 255, 255), 1f);
				Main.dust[dust].noGravity = true;
			}
			if (Projectile.velocity.X > 0)
			{
				Projectile.velocity.X = (Projectile.velocity.X - 1);
			}
			if (Projectile.velocity.X < 0)
			{
				Projectile.velocity.X = (Projectile.velocity.X + 1);
			}
			if (Projectile.velocity.Y > 0)
			{
				Projectile.velocity.Y = (Projectile.velocity.Y - 1);
			}
			if (Projectile.velocity.Y < 0)
			{
				Projectile.velocity.Y = (Projectile.velocity.Y + 1);
			}
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(20, 100);
			if (Projectile.owner == Main.myPlayer)
			{
				Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, 0, 0, Mod.Find<ModProjectile>("Gunksplosion").Type, (int)(Projectile.damage * 4f), (int)Projectile.knockBack, Projectile.owner);
				Projectile.timeLeft = 0;
			}
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, 0, 0, Mod.Find<ModProjectile>("Gunksplosion").Type, (int)(Projectile.damage * 4f), (int)Projectile.knockBack, Projectile.owner);
			Projectile.timeLeft = 0;
			return true;
		}

		public override void Kill(int timeLeft)
		{
			Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, 0, 0, Mod.Find<ModProjectile>("Gunksplosion").Type, (int)(Projectile.damage * 4f), (int)Projectile.knockBack, Projectile.owner);
			Projectile.timeLeft = 0;
		}
	}
}
