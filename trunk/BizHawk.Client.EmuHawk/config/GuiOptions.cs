﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using BizHawk.Client.Common;

namespace BizHawk.Client.EmuHawk
{
	public partial class EmuHawkOptions : Form
	{
		public EmuHawkOptions()
		{
			InitializeComponent();
		}

		private void GuiOptions_Load(object sender, EventArgs e)
		{
			StartFullScreenCheckbox.Checked = Global.Config.StartFullscreen;
			StartPausedCheckbox.Checked = Global.Config.StartPaused;
			PauseWhenMenuActivatedCheckbox.Checked = Global.Config.PauseWhenMenuActivated;
			EnableContextMenuCheckbox.Checked = Global.Config.ShowContextMenu;
			SaveWindowPositionCheckbox.Checked = Global.Config.SaveWindowPosition;
			RunInBackgroundCheckbox.Checked = Global.Config.RunInBackground;
			AcceptBackgroundInputCheckbox.Checked = Global.Config.AcceptBackgroundInput;
			NeverAskSaveCheckbox.Checked = Global.Config.SupressAskSave;
			SingleInstanceModeCheckbox.Checked = Global.Config.SingleInstanceMode;
			LogWindowAsConsoleCheckbox.Checked = Global.Config.WIN32_CONSOLE;


			BackupSRamCheckbox.Checked = Global.Config.BackupSaveram;
			FrameAdvSkipLagCheckbox.Checked = Global.Config.SkipLagFrame;

			if (LogConsole.ConsoleVisible)
			{
				LogWindowAsConsoleCheckbox.Enabled = false;
				toolTip1.SetToolTip(
					LogWindowAsConsoleCheckbox,
					"This can not be changed while the log window is open. I know, it's annoying.");
			}

			// Recent
			RecentRomsNumeric.Value = Global.Config.RecentRoms.MAX_RECENT_FILES;
			RecentMoviesNumeric.Value = Global.Config.RecentMovies.MAX_RECENT_FILES;
			RecentCheatsNumeric.Value = Global.Config.RecentCheats.MAX_RECENT_FILES;
			RecentTblNumeric.Value = Global.Config.RecentTables.MAX_RECENT_FILES;
			RecentLuaScriptNumeric.Value = Global.Config.RecentLua.MAX_RECENT_FILES;
			RecentLuaSessionsNumeric.Value = Global.Config.RecentLuaSession.MAX_RECENT_FILES;
			RecentWatchesNumeric.Value = Global.Config.RecentWatches.MAX_RECENT_FILES;
			RecentSearchesNumeric.Value = Global.Config.RecentSearches.MAX_RECENT_FILES;
		}

		private void OkBtn_Click(object sender, EventArgs e)
		{
			Global.Config.StartFullscreen = StartFullScreenCheckbox.Checked;
			Global.Config.StartPaused = StartPausedCheckbox.Checked;
			Global.Config.PauseWhenMenuActivated = PauseWhenMenuActivatedCheckbox.Checked;
			Global.Config.ShowContextMenu = EnableContextMenuCheckbox.Checked;
			Global.Config.SaveWindowPosition = SaveWindowPositionCheckbox.Checked;
			Global.Config.RunInBackground = RunInBackgroundCheckbox.Checked;
			Global.Config.AcceptBackgroundInput = AcceptBackgroundInputCheckbox.Checked;
			Global.Config.SupressAskSave = NeverAskSaveCheckbox.Checked;
			Global.Config.SingleInstanceMode = SingleInstanceModeCheckbox.Checked;
			Global.Config.WIN32_CONSOLE = LogWindowAsConsoleCheckbox.Checked;



			Global.Config.BackupSaveram = BackupSRamCheckbox.Checked;
			Global.Config.SkipLagFrame = FrameAdvSkipLagCheckbox.Checked;

			//Recent
			Global.Config.RecentRoms.MAX_RECENT_FILES = (int)RecentRomsNumeric.Value;
			Global.Config.RecentMovies.MAX_RECENT_FILES = (int)RecentMoviesNumeric.Value;
			Global.Config.RecentCheats.MAX_RECENT_FILES = (int)RecentCheatsNumeric.Value;
			Global.Config.RecentTables.MAX_RECENT_FILES = (int)RecentTblNumeric.Value;
			Global.Config.RecentLua.MAX_RECENT_FILES = (int)RecentLuaScriptNumeric.Value;
			Global.Config.RecentLuaSession.MAX_RECENT_FILES = (int)RecentLuaSessionsNumeric.Value;
			Global.Config.RecentWatches.MAX_RECENT_FILES = (int)RecentWatchesNumeric.Value;
			Global.Config.RecentSearches.MAX_RECENT_FILES = (int)RecentSearchesNumeric.Value;

			Close();
			DialogResult = DialogResult.OK;
			GlobalWin.OSD.AddMessage("Custom configurations saved.");
		}

		private void CancelBtn_Click(object sender, EventArgs e)
		{
			Close();
			DialogResult = DialogResult.Cancel;
			GlobalWin.OSD.AddMessage("Customizing aborted.");
		}

		private void DecreaseRecentBtn_Click(object sender, EventArgs e)
		{
			RecentGroupBox.Controls
				.OfType<NumericUpDown>()
				.ToList()
				.ForEach(n =>
				{
					if (n.Value > n.Minimum)
					{
						n.Value--;
					}
				});
		}

		private void IncreaseRecentBtn_Click(object sender, EventArgs e)
		{
			RecentGroupBox.Controls
				.OfType<NumericUpDown>()
				.ToList()
				.ForEach(n =>
				{
					if (n.Value < n.Maximum)
					{
						n.Value++;
					}
				});
		}


	}
}
