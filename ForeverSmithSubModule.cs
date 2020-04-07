using TaleWorlds.CampaignSystem;
﻿using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace SmithForever
{
	public class ForeverSmithSubModule : MBSubModuleBase
	{
		protected static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

		protected override void OnSubModuleLoad()
		{
			NLog.Config.LoggingConfiguration logConfig = new NLog.Config.LoggingConfiguration();
			NLog.Targets.FileTarget logFile = new NLog.Targets.FileTarget(LogFileTarget()) { FileName = LogFilePath() };

			logConfig.AddRule(NLog.LogLevel.Debug, NLog.LogLevel.Fatal, logFile);
			NLog.LogManager.Configuration = logConfig;
		}

		protected virtual string LogFileTarget()
		{
			return "ForeverSmithLogFile";
		}

		protected virtual string LogFilePath()
		{
			// The default, relative path will place the log in $(GameFolder)\bin\Win64_Shipping_Client\
			return "ForeverSmithLog.txt";
		}

		protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
		{
			if (!(game.GameType is Campaign))
			{
				return;
			}
			Log.Debug("OnGameStart");
			AddModels(gameStarterObject);
		}

		protected virtual void AddModels(IGameStarter gameStarterObject)
		{
			gameStarterObject.AddModel(new ForeverSmithModel());
		}
	}
}
