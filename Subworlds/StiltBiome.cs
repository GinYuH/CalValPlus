using CalamityMod.CalPlayer;
using CalamityMod.Events;
using CalamityMod.Systems;
using CalamityMod.World;
using CalamityMod;
using CalamityMod.BiomeManagers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SubworldLibrary;

namespace CalValPlus.Subworlds
{
    public class StiltBiome : ModBiome
    {
        public override ModWaterStyle WaterStyle => ModContent.Find<ModWaterStyle>("CalamityMod/SulphuricWater");
        public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle => ModContent.Find<ModSurfaceBackgroundStyle>("CalamityMod/SulphurSeaSurfaceBGStyle");
        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
        public override string BestiaryIcon => "CalamityMod/BiomeManagers/SulphurousSeaIcon";
        public override string BackgroundPath => "CalamityMod/Backgrounds/MapBackgrounds/SulphurBG";
        public override string MapBackground => "CalamityMod/Backgrounds/MapBackgrounds/SulphurBG";

        public override int Music => MusicID.Desert;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Styx");
        }

        public override bool IsBiomeActive(Player player)
        {
            return SubworldSystem.IsActive<Subworlds.StiltVillage>();
        }
    }
}
