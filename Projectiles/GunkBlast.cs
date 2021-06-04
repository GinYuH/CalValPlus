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
			projectile.width = 36;
			projectile.height = 32;
			projectile.friendly = true;
			projectile.timeLeft = 140;
			projectile.tileCollide = true;
			projectile.ignoreWater = false;
			projectile.ranged = true;
		}
		public override void AI()
		{
			if (Main.rand.NextFloat() < 0.368421f)
			{
				int dust = Dust.NewDust(projectile.position, 20, 20, 15, 0f, 0f, 0, new Color(255, 255, 255), 1f);
				Main.dust[dust].noGravity = true;
			}
			if (projectile.velocity.X > 0)
			{
				projectile.velocity.X = (projectile.velocity.X - 1);
			}
			if (projectile.velocity.X < 0)
			{
				projectile.velocity.X = (projectile.velocity.X + 1);
			}
			if (projectile.velocity.Y > 0)
			{
				projectile.velocity.Y = (projectile.velocity.Y - 1);
			}
			if (projectile.velocity.Y < 0)
			{
				projectile.velocity.Y = (projectile.velocity.Y + 1);
			}
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(20, 100);
			if (projectile.owner == Main.myPlayer)
			{
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, mod.ProjectileType("Gunksplosion"), (int)(projectile.damage * 4f), (int)projectile.knockBack, projectile.owner);
				projectile.timeLeft = 0;
			}
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, mod.ProjectileType("Gunksplosion"), (int)(projectile.damage * 4f), (int)projectile.knockBack, projectile.owner);
			projectile.timeLeft = 0;
			return true;
		}

		public override void Kill(int timeLeft)
		{
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, mod.ProjectileType("Gunksplosion"), (int)(projectile.damage * 4f), (int)projectile.knockBack, projectile.owner);
			projectile.timeLeft = 0;
		}
	}
}
