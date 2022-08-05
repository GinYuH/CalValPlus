using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.ModLoader;
using CalValPlus.NPCs.Andromeda;
using static Terraria.Projectile;
using CalamityMod.Projectiles.BaseProjectiles;
using ReLogic.Content;

namespace CalValPlus.Projectiles
{
    public class AndromedaDeathrayLeft : BaseLaserbeamProjectile
    {
        int hostiletimer = 0;
        float laserscale = 0.2f;
        public override string Texture => "CalValPlus/Projectiles/DeathRayTop";

        public override float MaxScale => laserscale;
        public override float MaxLaserLength => 2400f;
        public override float Lifetime => 300f;
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
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.alpha = 100;
            Projectile.tileCollide = false;
        }
        public override bool PreAI()
        {
            hostiletimer++;
            if (hostiletimer >= 60)
            {
                Projectile.hostile = true;
                if (laserscale < 1f)
                {
                    laserscale *= 1.5f;
                }
                Projectile.alpha = 255;
            }
            else
            {
                Projectile.hostile = false;
                laserscale = 0.2f;
                Projectile.alpha = 100;
            }
            if (hostiletimer == 60)
            {
                SoundEngine.PlaySound(new Terraria.Audio.SoundStyle("CalValPlus/Sounds/LaserCannon"), Projectile.Center);
            }
            Projectile.position.X = Main.npc[CalValPlusGlobalNPC.androalive].Center.X - 355;
            Projectile.position.Y = Main.npc[CalValPlusGlobalNPC.androalive].Center.Y + 240;
            if (!NPC.AnyNPCs(ModContent.NPCType<Andromeda>()))
            {
                Projectile.active = false;
            }
            return true;
        }
        public override bool ShouldUpdatePosition() => false;
    }
}