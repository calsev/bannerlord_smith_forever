using TaleWorlds.CampaignSystem;
﻿using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace SmithForever
{
	public class ForeverSmithSubModule : MBSubModuleBase
	{
		protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
		{
			if (!(game.GameType is Campaign))
				return;
			gameStarterObject.AddModel(new ForeverSmithModel());
		}
	}
}
