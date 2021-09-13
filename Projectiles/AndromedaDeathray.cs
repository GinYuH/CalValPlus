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
    public class AndromedaDeathray : BaseLaserbeamProjectile
    {
        public override string Texture => "CalValPlus/Projectiles/DeathRayTop";

        public override float MaxScale => 0.5f;
        public override float MaxLaserLength => 2400f;
        public override float Lifetime => 180f;
        public override Color LaserOverlayColor => Color.Yellow;
        public override Color LightCastColor => LaserOverlayColor;
        public override Texture2D LaserBeginTexture => ModContent.GetTexture("CalValPlus/Projectiles/AndromedaDeathrayTop");
        public override Texture2D LaserMiddleTexture => ModContent.GetTexture("CalValPlus/Projectiles/AndromedaDeathrayMiddle");
        public override Texture2D LaserEndTexture => ModContent.GetTexture("CalValPlus/Projectiles/AndromedaDeathrayBottom");
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
            projectile.tileCollide = false;
        }
        public override bool PreAI()
        {
            return true;
        }
        public override bool ShouldUpdatePosition() => false;
    }
}