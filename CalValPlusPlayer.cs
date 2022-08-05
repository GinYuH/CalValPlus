using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CalValPlus
{
    public class CalValPlusPlayer : ModPlayer
    {
        public bool ZonePerma;
        /*public override void UpdateBiomes()
        {
            Player player2 = Main.LocalPlayer;
            CalValPlusPlayer calPlayer = Player.GetModPlayer<CalValPlusPlayer>();
            Tile wallado = Framing.GetTileSafely(player2.Center);
            ZonePerma = (wallado.WallType == ModLoader.GetMod("CalValEX").Find<ModWall>("FrostflakeWallPlaced").Type || wallado.WallType == ModLoader.GetMod("CalamityMod").Find<ModWall>("CryonicBrickWall").Type || wallado.WallType == ModLoader.GetMod("CalValEX").Find<ModWall>("AstralBrickWallPlaced").Type || wallado.WallType == WallID.Shadewood || wallado.WallType == WallID.SnowBrick || wallado.WallType == WallID.Ebonwood || wallado.WallType == WallID.IronFence || wallado.WallType == WallID.BorealWood || wallado.WallType == ModLoader.GetMod("CalamityMod").Find<ModWall>("PlaguedPlateWall").Type || wallado.WallType == ModLoader.GetMod("CalamityMod").Find<ModWall>("ProfanedSlabWall").Type || wallado.WallType == ModLoader.GetMod("CalamityMod").Find<ModWall>("ProfanedCrystalWall").Type) && CalValPlusWorld.permaTiles > 50;
            if (calPlayer.ZonePerma && !Main.hardMode && !player2.setNebula)
            {
                player2.velocity.X = 0;
                player2.ClearBuff(BuffID.Warmth);
                player2.AddBuff(BuffID.Frozen, 20);
                player2.AddBuff(BuffID.Frostburn, 20);
                player2.AddBuff(BuffID.Cursed, 20);
                player2.AddBuff(BuffID.Stoned, 20);
                player2.AddBuff(ModLoader.GetMod("CalamityMod").Find<ModBuff>("GlacialState").Type, 20);
                player2.AddBuff(ModLoader.GetMod("CalamityMod").Find<ModBuff>("ExoFreeze").Type, 20);
                if (player2.lifeRegen > 0)
                {
                    player2.lifeRegen = 0;
                }
                player2.lifeRegenTime = 0;
                player2.lifeRegen -= 10;
            }
        }
        public override bool CustomBiomesMatch(Player other)
        {
            CalValPlusPlayer modOther = other.GetModPlayer<CalValPlusPlayer>();
            return ZonePerma == modOther.ZonePerma;
        }

        public override void CopyCustomBiomesTo(Player other)
        {
            CalValPlusPlayer modOther = other.GetModPlayer<CalValPlusPlayer>();
            modOther.ZonePerma = ZonePerma;
        }

        public override void SendCustomBiomes(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = ZonePerma;
            writer.Write(flags);
        }
        public override void ReceiveCustomBiomes(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            ZonePerma = flags[0];
        }*/
    }
}