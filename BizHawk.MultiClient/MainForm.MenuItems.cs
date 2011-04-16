using System;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using BizHawk.Core;
using BizHawk.Emulation.Consoles.Sega;
using BizHawk.Emulation.Consoles.TurboGrafx;
using BizHawk.Emulation.Consoles.Calculator;
using BizHawk.Emulation.Consoles.Gameboy;
using BizHawk.Emulation.Consoles.Nintendo;

namespace BizHawk.MultiClient
{
	partial class MainForm
	{
		private void RAMPokeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			RamPoke r = new RamPoke();
			r.Show();
		}

		private void saveWindowPositionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Global.Config.SaveWindowPosition ^= true;
		}

		private void startPausedToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Global.Config.StartPaused ^= true;
		}

		private void luaConsoleToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var window = new BizHawk.MultiClient.tools.LuaWindow();
			window.Show();
		}

		private void miLimitFramerate_Click(object sender, EventArgs e)
		{
			Global.Config.LimitFramerate ^= true;
		}

		private void miDisplayVsync_Click(object sender, EventArgs e)
		{
			Global.Config.DisplayVSync ^= true;
			Global.RenderPanel.Resized = true;
		}

		private void miAutoMinimizeSkipping_Click(object sender, EventArgs e)
		{
			Global.Config.AutoMinimizeSkipping ^= true;
		}

		private void miFrameskip0_Click(object sender, EventArgs e) { Global.Config.FrameSkip = 0; }
		private void miFrameskip1_Click(object sender, EventArgs e) { Global.Config.FrameSkip = 1; }
		private void miFrameskip2_Click(object sender, EventArgs e) { Global.Config.FrameSkip = 2; }
		private void miFrameskip3_Click(object sender, EventArgs e) { Global.Config.FrameSkip = 3; }
		private void miFrameskip4_Click(object sender, EventArgs e) { Global.Config.FrameSkip = 4; }
		private void miFrameskip5_Click(object sender, EventArgs e) { Global.Config.FrameSkip = 5; }
		private void miFrameskip6_Click(object sender, EventArgs e) { Global.Config.FrameSkip = 6; }
		private void miFrameskip7_Click(object sender, EventArgs e) { Global.Config.FrameSkip = 7; }
		private void miFrameskip8_Click(object sender, EventArgs e) { Global.Config.FrameSkip = 8; }
		private void miFrameskip9_Click(object sender, EventArgs e) { Global.Config.FrameSkip = 9; }

		private void miSpeed50_Click(object sender, EventArgs e) { SetSpeedPercent(50); }
		private void miSpeed75_Click(object sender, EventArgs e) { SetSpeedPercent(75); }
		private void miSpeed100_Click(object sender, EventArgs e) { SetSpeedPercent(100); }
		private void miSpeed150_Click(object sender, EventArgs e) { SetSpeedPercent(150); }
		private void miSpeed200_Click(object sender, EventArgs e) { SetSpeedPercent(200); }

		private void pauseWhenMenuActivatedToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Global.Config.PauseWhenMenuActivated ^= true;
		}

		private void soundToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SoundConfig s = new SoundConfig();
			s.ShowDialog();
		}

		private void zoomMenuItem_Click(object sender, EventArgs e)
		{
			if (sender == x1MenuItem) Global.Config.TargetZoomFactor = 1;
			if (sender == x2MenuItem) Global.Config.TargetZoomFactor = 2;
			if (sender == x3MenuItem) Global.Config.TargetZoomFactor = 3;
			if (sender == x4MenuItem) Global.Config.TargetZoomFactor = 4;
			if (sender == x5MenuItem) Global.Config.TargetZoomFactor = 5;
			if (sender == mzMenuItem) Global.Config.TargetZoomFactor = 10;

			x1MenuItem.Checked = Global.Config.TargetZoomFactor == 1;
			x2MenuItem.Checked = Global.Config.TargetZoomFactor == 2;
			x3MenuItem.Checked = Global.Config.TargetZoomFactor == 3;
			x4MenuItem.Checked = Global.Config.TargetZoomFactor == 4;
			x5MenuItem.Checked = Global.Config.TargetZoomFactor == 5;
			mzMenuItem.Checked = Global.Config.TargetZoomFactor == 10;

			FrameBufferResized();
		}

		private void enableFMChipToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Global.Config.SmsEnableFM ^= true;
		}

		private void overclockWhenKnownSafeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Global.Config.SmsAllowOverlock ^= true;
		}

		private void forceStereoSeparationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Global.Config.SmsForceStereoSeparation ^= true;
		}

		private void recordMovieToolStripMenuItem_Click(object sender, EventArgs e)
		{
			RecordMovie r = new RecordMovie();
			r.ShowDialog();
		}

		private void playMovieToolStripMenuItem_Click(object sender, EventArgs e)
		{
			PlayMovie p = new PlayMovie();
			p.ShowDialog();
		}

		private void stopMovieToolStripMenuItem_Click(object sender, EventArgs e)
		{
            InputLog.StopMovie();   //TODO: stop user movie if it exists, and start InputLog logging, else do nothing
		}

		private void playFromBeginningToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}


		private void RAMWatchToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LoadRamWatch();
		}

		private void rAMSearchToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LoadRamSearch();
		}


		private void autoloadMostRecentToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UpdateAutoLoadRecentRom();
		}

		private void clearToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Global.Config.RecentRoms.Clear();
		}


		private void selectSlot1ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveSlot = 1;
			SaveSlotSelectedMessage();
		}

		private void selectSlot2ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveSlot = 2;
			SaveSlotSelectedMessage();
		}

		private void selectSlot3ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveSlot = 3;
			SaveSlotSelectedMessage();
		}

		private void selectSlot4ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveSlot = 4;
			SaveSlotSelectedMessage();
		}

		private void selectSlot5ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveSlot = 5;
			SaveSlotSelectedMessage();
		}

		private void selectSlot6ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveSlot = 6;
			SaveSlotSelectedMessage();
		}

		private void selectSlot7ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveSlot = 7;
			SaveSlotSelectedMessage();
		}

		private void selectSlot8ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveSlot = 8;
			SaveSlotSelectedMessage();
		}

		private void selectSlot9ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveSlot = 9;
			SaveSlotSelectedMessage();
		}

		private void selectSlot10ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveSlot = 0;
			SaveSlotSelectedMessage();
		}

		private void previousSlotToolStripMenuItem_Click(object sender, EventArgs e)
		{
            PreviousSlot();
		}

		private void nextSlotToolStripMenuItem_Click(object sender, EventArgs e)
		{
            NextSlot();
		}

		private void saveToCurrentSlotToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveState("QuickSave" + SaveSlot.ToString());
		}

		private void loadCurrentSlotToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LoadState("QuickSave" + SaveSlot.ToString());
		}

		private void closeROMToolStripMenuItem_Click(object sender, EventArgs e)
		{
            CloseROM();
		}

		private void saveStateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Global.Sound.StopSound();

			var frm = new NameStateForm();
			frm.ShowDialog(this);

			if (frm.OK)
				SaveState(frm.Result);

			Global.Sound.StartSound();
		}

		private void powerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LoadRom(CurrentlyOpenRom);
		}

		private void resetToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Global.Emulator.ControllerDefinition.BoolButtons.Contains("Reset"))
				Global.Emulator.Controller.ForceButton("Reset");
		}

		private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (EmulatorPaused == true)
				UnpauseEmulator();
			else
				PauseEmulator();
		}

		private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
		{

		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new AboutBox().ShowDialog();
		}

		private void controllersToolStripMenuItem_Click(object sender, EventArgs e)
		{
			InputConfig i = new InputConfig();
			i.ShowDialog();
			//re-initialize controls in case anything was changed
			if (i.DialogResult == DialogResult.OK)
			{
				InitControls();
				SyncControls();
			}
		}

		private void hotkeysToolStripMenuItem_Click(object sender, EventArgs e)
		{
			BizHawk.MultiClient.tools.HotkeyWindow h = new BizHawk.MultiClient.tools.HotkeyWindow();
			h.ShowDialog();
            if (h.DialogResult == DialogResult.OK)
            {
                InitControls();
                SyncControls();
            }
		}

		private void displayFPSToolStripMenuItem_Click(object sender, EventArgs e)
		{
            ToggleFPS();
		}

		private void displayFrameCounterToolStripMenuItem_Click(object sender, EventArgs e)
		{
            ToggleFrameCounter();
		}

		private void displayInputToolStripMenuItem_Click(object sender, EventArgs e)
		{
            ToggleInputDisplay();
		}

		private void displayLagCounterToolStripMenuItem_Click(object sender, EventArgs e)
		{
            ToggleLagCounter();
		}

		private void screenshotF12ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TakeScreenshot();
		}

		private void savestate1toolStripMenuItem_Click(object sender, EventArgs e) { SaveState("QuickSave1"); }
		private void savestate2toolStripMenuItem_Click(object sender, EventArgs e) { SaveState("QuickSave2"); }
		private void savestate3toolStripMenuItem_Click(object sender, EventArgs e) { SaveState("QuickSave3"); }
		private void savestate4toolStripMenuItem_Click(object sender, EventArgs e) { SaveState("QuickSave4"); }
		private void savestate5toolStripMenuItem_Click(object sender, EventArgs e) { SaveState("QuickSave5"); }
		private void savestate6toolStripMenuItem_Click(object sender, EventArgs e) { SaveState("QuickSave6"); }
		private void savestate7toolStripMenuItem_Click(object sender, EventArgs e) { SaveState("QuickSave7"); }
		private void savestate8toolStripMenuItem_Click(object sender, EventArgs e) { SaveState("QuickSave8"); }
		private void savestate9toolStripMenuItem_Click(object sender, EventArgs e) { SaveState("QuickSave9"); }
		private void savestate0toolStripMenuItem_Click(object sender, EventArgs e) { SaveState("QuickSave0"); }

		private void loadstate1toolStripMenuItem_Click(object sender, EventArgs e) { LoadState("QuickSave1"); }
		private void loadstate2toolStripMenuItem_Click(object sender, EventArgs e) { LoadState("QuickSave2"); }
		private void loadstate3toolStripMenuItem_Click(object sender, EventArgs e) { LoadState("QuickSave3"); }
		private void loadstate4toolStripMenuItem_Click(object sender, EventArgs e) { LoadState("QuickSave4"); }
		private void loadstate5toolStripMenuItem_Click(object sender, EventArgs e) { LoadState("QuickSave5"); }
		private void loadstate6toolStripMenuItem_Click(object sender, EventArgs e) { LoadState("QuickSave6"); }
		private void loadstate7toolStripMenuItem_Click(object sender, EventArgs e) { LoadState("QuickSave7"); }
		private void loadstate8toolStripMenuItem_Click(object sender, EventArgs e) { LoadState("QuickSave8"); }
		private void loadstate9toolStripMenuItem_Click(object sender, EventArgs e) { LoadState("QuickSave9"); }
		private void loadstate0toolStripMenuItem_Click(object sender, EventArgs e) { LoadState("QuickSave0"); }

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (RamWatch1.AskSave())
				Close();
		}

		private void openROMToolStripMenuItem_Click(object sender, EventArgs e)
		{
            OpenROM();
		}

		private void replayInputLogToolStripMenuItem_Click(object sender, EventArgs e)
		{
			InputLog.StopMovie();
			InputLog.StartPlayback();
			LoadRom(CurrentlyOpenRom);
		}

		private void PPUViewerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LoadNESPPU();
		}

		private void enableRewindToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Global.Config.RewindEnabled ^= true;
		}

		private void hexEditorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LoadHexEditor();
		}

		private void MainForm_Shown(object sender, EventArgs e)
		{
			HandlePlatformMenus();
		}

		private void gameGenieCodesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LoadGameGenieEC();
		}

		private void cheatsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LoadCheatsWindow();
		}

		private void forceGDIPPresentationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Global.Config.ForceGDI ^= true;
			SyncPresentationMode();
		}

        private void debuggerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadNESDebugger();
        }

        private void saveStateToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            savestate1toolStripMenuItem.ShortcutKeyDisplayString = Global.Config.SaveSlot1;
            savestate2toolStripMenuItem.ShortcutKeyDisplayString = Global.Config.SaveSlot2;
            savestate3toolStripMenuItem.ShortcutKeyDisplayString = Global.Config.SaveSlot3;
            savestate4toolStripMenuItem.ShortcutKeyDisplayString = Global.Config.SaveSlot4;
            savestate5toolStripMenuItem.ShortcutKeyDisplayString = Global.Config.SaveSlot5;
            savestate6toolStripMenuItem.ShortcutKeyDisplayString = Global.Config.SaveSlot6;
            savestate7toolStripMenuItem.ShortcutKeyDisplayString = Global.Config.SaveSlot7;
            savestate8toolStripMenuItem.ShortcutKeyDisplayString = Global.Config.SaveSlot8;
            savestate9toolStripMenuItem.ShortcutKeyDisplayString = Global.Config.SaveSlot9;
            savestate0toolStripMenuItem.ShortcutKeyDisplayString = Global.Config.SaveSlot0;
            saveNamedStateToolStripMenuItem.ShortcutKeyDisplayString = Global.Config.SaveNamedState;
        }

        private void loadStateToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            loadstate1toolStripMenuItem.ShortcutKeyDisplayString = Global.Config.LoadSlot0;
            loadstate2toolStripMenuItem.ShortcutKeyDisplayString = Global.Config.LoadSlot1;
            loadstate3toolStripMenuItem.ShortcutKeyDisplayString = Global.Config.LoadSlot2;
            loadstate4toolStripMenuItem.ShortcutKeyDisplayString = Global.Config.LoadSlot3;
            loadstate5toolStripMenuItem.ShortcutKeyDisplayString = Global.Config.LoadSlot4;
            loadstate6toolStripMenuItem.ShortcutKeyDisplayString = Global.Config.LoadSlot5;
            loadstate7toolStripMenuItem.ShortcutKeyDisplayString = Global.Config.LoadSlot6;
            loadstate8toolStripMenuItem.ShortcutKeyDisplayString = Global.Config.LoadSlot7;
            loadstate9toolStripMenuItem.ShortcutKeyDisplayString = Global.Config.LoadSlot8;
            loadstate0toolStripMenuItem.ShortcutKeyDisplayString = Global.Config.LoadSlot9;
            loadNamedStateToolStripMenuItem.ShortcutKeyDisplayString = Global.Config.LoadNamedState;
        }

        private void nametableViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadNESNameTable();
        }
        
        private void saveNamedStateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveStateAs();
        }

        private void loadNamedStateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadStateAs();
        }

        private void toolBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadToolBox();
        }

        private void toolsToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            toolBoxToolStripMenuItem.ShortcutKeyDisplayString = Global.Config.ToolBox;
            if (!ToolBox1.IsHandleCreated || ToolBox1.IsDisposed)
                toolBoxToolStripMenuItem.Enabled = true;
            else
                toolBoxToolStripMenuItem.Enabled = false;

            rAMWatchToolStripMenuItem.ShortcutKeyDisplayString = Global.Config.RamWatch;
            rAMSearchToolStripMenuItem.ShortcutKeyDisplayString = Global.Config.RamSearch;
            rAMPokeToolStripMenuItem.ShortcutKeyDisplayString = Global.Config.RamPoke;
            hexEditorToolStripMenuItem.ShortcutKeyDisplayString = Global.Config.HexEditor;
            luaConsoleToolStripMenuItem.ShortcutKeyDisplayString = Global.Config.LuaConsole;
            cheatsToolStripMenuItem.ShortcutKeyDisplayString = Global.Config.Cheats;
        }

        private void saveSlotToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            selectSlot10ToolStripMenuItem.ShortcutKeyDisplayString = Global.Config.SelectSlot0;
            selectSlot1ToolStripMenuItem.ShortcutKeyDisplayString = Global.Config.SelectSlot1;
            selectSlot2ToolStripMenuItem.ShortcutKeyDisplayString = Global.Config.SelectSlot2;
            selectSlot3ToolStripMenuItem.ShortcutKeyDisplayString = Global.Config.SelectSlot3;
            selectSlot4ToolStripMenuItem.ShortcutKeyDisplayString = Global.Config.SelectSlot4;
            selectSlot5ToolStripMenuItem.ShortcutKeyDisplayString = Global.Config.SelectSlot5;
            selectSlot6ToolStripMenuItem.ShortcutKeyDisplayString = Global.Config.SelectSlot6;
            selectSlot7ToolStripMenuItem.ShortcutKeyDisplayString = Global.Config.SelectSlot7;
            selectSlot8ToolStripMenuItem.ShortcutKeyDisplayString = Global.Config.SelectSlot8;
            selectSlot9ToolStripMenuItem.ShortcutKeyDisplayString = Global.Config.SelectSlot9;
            previousSlotToolStripMenuItem.ShortcutKeyDisplayString = Global.Config.PreviousSlot;
            nextSlotToolStripMenuItem.ShortcutKeyDisplayString = Global.Config.NextSlot;
            saveToCurrentSlotToolStripMenuItem.ShortcutKeyDisplayString = Global.Config.QuickSave;
            loadConfigToolStripMenuItem.ShortcutKeyDisplayString = Global.Config.QuickLoad;
        }

        private void switchToFullscreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleFullscreen();
        }
	}
}