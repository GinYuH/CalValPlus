using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;
using static CalValPlus.NPCs.Andromeda.Andromeda;
using static Terraria.Projectile;
using CalamityMod.Projectiles.BaseProjectiles;

namespace CalValPlus.Projectiles
{
    public class AndromedaDeahtray : BaseLaserbeamProjectile
    {
        public override string Texture => "CalValPlus/Projectiles/DeathRayTop";

        /*public float HueOffset
        {
            get => projectile.ai[1];
            set => projectile.ai[1] = value;
        }*/
        public override float MaxScale => 1.1f;
        public override float MaxLaserLength => 2400f;
        public override float Lifetime => 180f;
        public override Color LaserOverlayColor => Color.Yellow;
        public override Color LightCastColor => LaserOverlayColor;
        public override Texture2D LaserBeginTexture => ModContent.GetTexture("CalValPlus/Projectiles/DeathRayTop");
        public override Texture2D LaserMiddleTexture => ModContent.GetTexture("CalValPlus/Projectiles/DeathRayMiddle");
        public override Texture2D LaserEndTexture => ModContent.GetTexture("CalValPlus/Projectiles/DeathRayBottom");
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Andromeda Deathray MK VI");
        }
        public override void SetDefaults()
        {
            projectile.width = projectile.height = 22;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.alpha = 255;
        }
        public override bool PreAI()
        {
            projectile.position.X = Main.npc[CalValPlusGlobalNPC.androalive].Center.X + 355;
            projectile.position.Y = Main.npc[CalValPlusGlobalNPC.androalive].Center.Y + 600;
            return true;
        }
        public override bool ShouldUpdatePosition() => false;
    }
}