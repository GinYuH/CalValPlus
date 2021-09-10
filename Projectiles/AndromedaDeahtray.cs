using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;
using static Terraria.Projectile;

namespace CalValPlus.Projectiles
{
	public class AndromedaDeahtray : ModProjectile
	{
		public override string Texture => "CalValPlus/Projectiles/DeathRayTop";

		public override void SetDefaults()
		{
			projectile.width = 10;
			projectile.height = 10;
			projectile.hostile = true;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.timeLeft = 90;
		}

		// The AI of the projectile
		public override void AI()
		{
			Vector2? vector56 = null;
			if (projectile.velocity.HasNaNs() || projectile.velocity == Vector2.Zero)
			{
				projectile.velocity = -Vector2.UnitY;
			}
			if (Main.npc[(int)projectile.ai[1]].active && Main.npc[(int)projectile.ai[1]].type == ModContent.NPCType<NPCs.Andromeda.Andromeda>())
			{
				Vector2 offset = new Vector2(Main.npc[(int)projectile.ai[1]].width - 24, 0).RotatedBy(Main.npc[(int)projectile.ai[1]].rotation + 1.57079633);
				projectile.Center = Main.npc[(int)projectile.ai[1]].Center + offset;
			}
			else
			{
				projectile.Kill();
				return;
			}
			if (projectile.velocity.HasNaNs() || projectile.velocity == Vector2.Zero)
			{
				projectile.velocity = -Vector2.UnitY;
			}
			if (projectile.localAI[0] == 0f)
			{
				Main.PlaySound(29, (int)projectile.position.X, (int)projectile.position.Y, 104);
			}
			float num820 = 1f;
			//projectile.ai[0] += 1f;
			if (projectile.localAI[0] >= 180f)
			{
				projectile.Kill();
				return;
			}
			projectile.scale = (float)Math.Sin(projectile.localAI[0] * (float)Math.PI / 180f) * 10f * num820;
			if (projectile.scale > num820)
			{
				projectile.scale = num820;
			}
			float num823 = projectile.velocity.ToRotation();
			num823 += projectile.ai[0];
			projectile.rotation = num823 - (float)Math.PI / 2f;
			projectile.velocity = num823.ToRotationVector2();
			float num824 = 0f;
			float num825 = 0f;
			Vector2 samplingPoint = projectile.Center;
			if (vector56.HasValue)
			{
				samplingPoint = vector56.Value;
			}
			num824 = 3f;
			num825 = projectile.width;
			float[] array5 = new float[(int)num824];
			Collision.LaserScan(samplingPoint, projectile.velocity, num825 * projectile.scale, 2400f, array5);
			float num826 = 0f;
			for (int num827 = 0; num827 < array5.Length; num827++)
			{
				num826 += array5[num827];
			}
			num826 /= num824;
			float amount = 0.5f;
			projectile.localAI[1] = MathHelper.Lerp(projectile.localAI[1], num826, amount);
			Vector2 vector57 = projectile.Center + projectile.velocity * (projectile.localAI[1] - 14f);
			for (int num828 = 0; num828 < 2; num828++)
			{
				float num829 = projectile.velocity.ToRotation() + ((Main.rand.Next(2) == 1) ? (-1f) : 1f) * ((float)Math.PI / 2f);
				float num830 = (float)Main.rand.NextDouble() * 2f + 2f;
				Vector2 vector58 = new Vector2((float)Math.Cos(num829) * num830, (float)Math.Sin(num829) * num830);
				int num831 = Dust.NewDust(vector57, 0, 0, 229, vector58.X, vector58.Y);
				Main.dust[num831].noGravity = true;
				Main.dust[num831].scale = 1.7f;
			}
			if (Main.rand.Next(5) == 0)
			{
				Vector2 value32 = projectile.velocity.RotatedBy(1.5707963705062866) * ((float)Main.rand.NextDouble() - 0.5f) * projectile.width;
				int num832 = Dust.NewDust(vector57 + value32 - Vector2.One * 4f, 8, 8, 31, 0f, 0f, 100, default(Color), 1.5f);
				Dust dust119 = Main.dust[num832];
				Dust dust2 = dust119;
				dust2.velocity *= 0.5f;
				Main.dust[num832].velocity.Y = 0f - Math.Abs(Main.dust[num832].velocity.Y);
			}
			/*DelegateMethods.v3_1 = new Vector3(0.3f, 0.65f, 0.7f);
			Utils.PlotTileLine(projectile.Center, projectile.Center + projectile.velocity * localAI[1], (float)width * scale, DelegateMethods.CastLight);*/
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{

			if (projectile.velocity == Vector2.Zero)
			{
				return false;
			}
			Texture2D texture2D18 = (ModContent.GetTexture("CalValPlus/Projectiles/DeathRayTop"));
			Texture2D texture2D19 = (ModContent.GetTexture("CalValPlus/Projectiles/DeathRayMiddle"));
			Texture2D texture2D20 = (ModContent.GetTexture("CalValPlus/Projectiles/DeathRayBottom"));
			float num203 = projectile.localAI[1];
			Microsoft.Xna.Framework.Color color40 = new Microsoft.Xna.Framework.Color(255, 255, 255, 0) * 0.9f;
			spriteBatch.Draw(texture2D18, projectile.Center - Main.screenPosition, null, Color.White, projectile.rotation, texture2D18.Size() / 2f, projectile.scale, SpriteEffects.None, 0f);
			num203 -= (float)(texture2D18.Height / 2 + texture2D20.Height) * projectile.scale;
			Vector2 center3 = projectile.Center;
			center3 += projectile.velocity * projectile.scale * texture2D18.Height / 2f;
			if (num203 > 0f)
			{
				float num204 = 0f;
				Microsoft.Xna.Framework.Rectangle value21 = new Microsoft.Xna.Framework.Rectangle(0, 16 * (projectile.timeLeft / 3 % 5), texture2D19.Width, 16);
				while (num204 + 1f < num203)
				{
					if (num203 - num204 < (float)value21.Height)
					{
						value21.Height = (int)(num203 - num204);
					}
					spriteBatch.Draw(texture2D19, center3 - Main.screenPosition, value21, Color.White, projectile.rotation, new Vector2(value21.Width / 2, 0f), projectile.scale, SpriteEffects.None, 0f);
					num204 += (float)value21.Height * projectile.scale;
					center3 += projectile.velocity * value21.Height * projectile.scale;
					value21.Y += 16;
					if (value21.Y + value21.Height > texture2D19.Height)
					{
						value21.Y = 0;
					}
				}
			}
			spriteBatch.Draw(texture2D20, center3 - Main.screenPosition, null, Color.White, projectile.rotation, texture2D20.Frame().Top(), projectile.scale, SpriteEffects.None, 0f);
			return false;
		}

	}
}