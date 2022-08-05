using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;
using static CalValPlus.NPCs.Andromeda.Andromeda;
using static Terraria.Projectile;
using CalamityMod.Projectiles.BaseProjectiles;
using ReLogic.Content;

namespace CalValPlus.Projectiles
{
    public class AndromedaDeahtray : BaseLaserbeamProjectile
    {
        public override string Texture => "CalValPlus/Projectiles/DeathRayTop";

        /*public float HueOffset
        {
            get => Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }*/
        public override float MaxScale => 1.1f;
        public override float MaxLaserLength => 2400f;
        public override float Lifetime => 180f;
        public override Color LaserOverlayColor => Color.Yellow;
        public override Color LightCastColor => LaserOverlayColor;
        public override Texture2D LaserBeginTexture => ModContent.Request<Texture2D>("CalValPlus/Projectiles/DeathRayTop", AssetRequestMode.ImmediateLoad).Value;
        public override Texture2D LaserMiddleTexture => ModContent.Request<Texture2D>("CalValPlus/Projectiles/DeathRayMiddle", AssetRequestMode.ImmediateLoad).Value;
        public override Texture2D LaserEndTexture => ModContent.Request<Texture2D>("CalValPlus/Projectiles/DeathRayBottom", AssetRequestMode.ImmediateLoad).Value;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Andromeda Deathray MK VI");
        }
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 22;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
        }
        public override bool PreAI()
        {
            Projectile.position.X = Main.npc[CalValPlusGlobalNPC.androalive].Center.X + 355;
            Projectile.position.Y = Main.npc[CalValPlusGlobalNPC.androalive].Center.Y + 600;
            return true;
        }
        public override bool ShouldUpdatePosition() => false;
    }
}