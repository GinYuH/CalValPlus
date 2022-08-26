using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Terraria.ModLoader;
using CalValPlus.Projectiles;

namespace CalValPlus.Items.Weapons.Ranged
{
	public class SlimeCannon : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Slime Cannon");
			Tooltip.SetDefault("Shoots a ball of sludge that detonates into a colossal explosion on impact");
		}
		public override void SetDefaults()
        {
			Item.damage = 10;
			Item.useTime = 16;
			Item.useAnimation = 16;
			Item.rare = 3;
			Item.value = 5000000;
			Item.shoot = ModContent.ProjectileType<GunkBlast>();
			Item.autoReuse = true;
			Item.useTurn = true;
			Item.useStyle = 4;
			Item.shootSpeed = 40f;
			Item.UseSound = SoundID.Item111;
		}
	}
}
