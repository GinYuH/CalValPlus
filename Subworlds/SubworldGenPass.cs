using System;
using Terraria.WorldBuilding;
using Terraria.IO;

namespace CalValPlus.Subworlds
{
    public class SubworldGenPass : GenPass
	{
		private Action<GenerationProgress> method;

		public SubworldGenPass(Action<GenerationProgress> method)
			: base("", 1f)
		{
			this.method = method;
		}

		public SubworldGenPass(float weight, Action<GenerationProgress> method)
			: base("", weight)
		{
			this.method = method;
		}

		protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
		{
			method(progress);
		}
	}
}