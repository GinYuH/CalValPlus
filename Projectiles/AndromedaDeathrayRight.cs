using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;
using CalValPlus.NPCs.Andromeda;
using static Terraria.Projectile;
using CalamityMod.Projectiles.BaseProjectiles;

namespace CalValPlus.Projectiles
{
    public class AndromedaDeathrayRight : BaseLaserbeamProjectile
    {
        int hostiletimer = 0;
        float laserscale = 0.2f;
        public override string Texture => "CalValPlus/Projectiles/DeathRayTop";

        public override float MaxScale => laserscale;
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
            projectile.hostile = false;
            projectile.penetrate = -1;
            projectile.alpha = 100;
            projectile.tileCollide = false;
        }
        public override bool PreAI()
        {
            hostiletimer++;
            if (hostiletimer >= 60)
            {
                projectile.hostile = true;
                if (laserscale < 1f)
                {
                    laserscale *= 1.5f;
                }
                projectile.alpha = 255;
            }
            else
            {
                projectile.hostile = false;
                laserscale = 0.2f;
                projectile.alpha = 100;
            }
            if (hostiletimer == 60)
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/LaserCannon"));
            }
            projectile.position.X = Main.npc[CalValPlusGlobalNPC.androalive].Center.X + 355;
            projectile.position.Y = Main.npc[CalValPlusGlobalNPC.androalive].Center.Y + 240;
            if (!NPC.AnyNPCs(ModContent.NPCType<Andromeda>()))
            {
                projectile.active = false;
            }
            return true;
        }
        public override bool ShouldUpdatePosition() => false;
    }
}