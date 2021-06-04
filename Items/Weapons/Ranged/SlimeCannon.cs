using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Terraria.ModLoader;

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
			item.damage = 10;
			item.useTime = 16;
			item.useAnimation = 16;
			item.rare = 3;
			item.value = 5000000;
			item.shoot = mod.ProjectileType("GunkBlast");
			item.autoReuse = true;
			item.useTurn = true;
			item.useStyle = 4;
			item.shootSpeed = 40f;
			item.UseSound = SoundID.Item111;
		}
	}
}
