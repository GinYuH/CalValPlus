using MonoMod.Cil;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent;
using CalamityMod;
using CalamityMod.DataStructures;
using Microsoft.Xna.Framework.Graphics;

namespace CalValPlus.NPCs.Orthocera
{
	internal class Orthocera : ModNPC
	{
        bool afterimages = false;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Orthoceron");
			Main.npcFrameCount[NPC.type] = 1;
		}

		public override void SetDefaults()
		{

			NPC.noGravity = true;
			NPC.lavaImmune = true;
			NPC.aiStyle = -1;
			NPC.npcSlots = 25f;
			NPC.lifeMax = 400000;
			NPC.damage = 0;
			NPC.HitSound = SoundID.NPCHit49;
			NPC.DeathSound = SoundID.NPCDeath51;
			NPC.knockBackResist = 0f;
			NPC.noTileCollide = true;
			NPC.behindTiles = true;
			NPC.width = 266;
			NPC.height = 612;
			NPC.boss = true;
		}

		public override void AI()
		{
			//CalamityUtils.SmoothMovement(NPC, 500, new Vector2( Main.LocalPlayer.Center.X - NPC.Center.X, Main.LocalPlayer.Center.Y - NPC.Center.Y - 250), 5, 1, true);
		}

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
			#region main
			SpriteEffects spriteEffects = SpriteEffects.None;
            if (NPC.spriteDirection == 1)
                spriteEffects = SpriteEffects.FlipHorizontally;


            Texture2D texture = TextureAssets.Npc[NPC.type].Value;
            Texture2D glowmask =ModContent.Request<Texture2D>("CalValPlus/NPCs/Orthocera/OrthoceraGlow").Value;

            Vector2 origin = new Vector2((float)(texture.Width / 2), (float)(texture.Height / 2));
            Color white = Color.White;
            float colorLerpAmt = 0.5f;
            int afterimageAmt = 7;


            if (CalamityMod.CalamityConfig.Instance.Afterimages && afterimages)
            {
                for (int i = 1; i < afterimageAmt; i += 2)
                {
                    Color color1 = drawColor;
                    color1 = Color.Lerp(color1, white, colorLerpAmt);
                    color1 = NPC.GetAlpha(color1);
                    color1 *= (float)(afterimageAmt - i) / 15f;
                    Vector2 offset = NPC.oldPos[i] + new Vector2((float)NPC.width, (float)NPC.height) - screenPos;
                    offset -= new Vector2((float)texture.Width, (float)(texture.Height / Main.npcFrameCount[NPC.type])) * NPC.scale;
                    offset += origin * NPC.scale + new Vector2(0f, NPC.gfxOffY);
                    spriteBatch.Draw(texture, offset, NPC.frame, color1, NPC.rotation, origin, NPC.scale, spriteEffects, 0f);
                }
            }

            Vector2 npcOffset = NPC.Center - screenPos;
            npcOffset -= new Vector2((float)texture.Width, (float)(texture.Height / Main.npcFrameCount[NPC.type])) * NPC.scale;
            npcOffset += origin * NPC.scale + new Vector2(0f, NPC.gfxOffY);
            spriteBatch.Draw(texture, npcOffset, null, drawColor, NPC.rotation, Vector2.Zero, NPC.scale, spriteEffects, 0f);
            spriteBatch.Draw(glowmask, npcOffset, null, Color.White, NPC.rotation, Vector2.Zero, NPC.scale, spriteEffects, 0f);
			#endregion
			//Thank you Iban
			for (int k = 0; k < 2; k++)
			{
				int sign = k % 2 == 0 ? 1 : -1;

				int basex = 400;
				int extrax = 0;
				float pos = basex * sign;
				float posy = -200;
				if (k > 1)
                {
					pos += 600 * sign;
					extrax += -600 * sign;
                }

				pos += (float)Math.Sin(Main.GlobalTimeWrappedHourly * 2) * 200 * sign - (300 * sign);
				posy += (float)Math.Sin(Main.GlobalTimeWrappedHourly * 2) * 200;

				Vector2 start = new Vector2(NPC.Center.X + basex * -sign + extrax, NPC.Center.Y + 500);
				Vector2 dest = new Vector2(Main.LocalPlayer.position.X * -sign + extrax + extrax, NPC.Center.Y + posy);

				float curvatureneg = MathHelper.Clamp(Math.Abs(dest.X) / 50f * 90, 30, 80);
				float curvature = MathHelper.Clamp(Math.Abs(dest.X) / 50f * -90, 30, 80);

				Vector2 controlPoint1 = NPC.Center + Vector2.UnitY * curvature;
				Vector2 controlPoint2 = NPC.Center + Vector2.UnitY * curvatureneg;
				controlPoint1.X -= 800 * sign;
				controlPoint2.X += -800 * sign;
				controlPoint1.Y += 300;
				controlPoint2.Y -= 300;

				BezierCurve curve = new BezierCurve(new Vector2[] { start, controlPoint1, controlPoint2, Main.LocalPlayer.Center });
				int numPoints = 6; //"Should make dynamic based on curve length, but I'm not sure how to smoothly do that while using a bezier curve" -Graydee, from the code i referenced. I do agree.
				Vector2[] chainPositions = curve.GetPoints(numPoints).ToArray();

				//Draw each chain segment bar the very first one
				for (int i = 1; i < numPoints; i++)
				{
					Texture2D chainTexture = ModContent.Request<Texture2D>("CalValPlus/NPCs/Orthocera/OrthoceraTentacle" + i).Value;
					Vector2 position = chainPositions[i];
					float rotation = (chainPositions[i] - chainPositions[i - 1]).ToRotation() + MathHelper.PiOver2; //Calculate rotation based on direction from last point
					float yScale = Vector2.Distance(chainPositions[i], chainPositions[i - 1]) / chainTexture.Height; //Calculate how much to squash/stretch for smooth chain based on distance between points
					Vector2 scale = new Vector2(1, yScale);
					Color chainLightColor = Lighting.GetColor((int)position.X / 16, (int)position.Y / 16); //Lighting of the position of the chain segment
					Vector2 origine = new Vector2(chainTexture.Width / 2, chainTexture.Height); //Draw from center bottom of texture
					Main.spriteBatch.Draw(chainTexture, position - Main.screenPosition, null, chainLightColor, rotation, Vector2.Zero, scale, SpriteEffects.None, 0);
				}
			}
			return false;
        }
    }
}