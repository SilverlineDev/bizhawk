﻿using System;
using System.IO;
using System.Collections.Generic;

using BizHawk.Emulation.Common;
using BizHawk.Emulation.Cores.Components.CP1610;

namespace BizHawk.Emulation.Cores.Intellivision
{
	[CoreAttributes(
		"IntelliHawk",
		"BrandonE",
		isPorted: false,
		isReleased: false
		)]
	[ServiceNotApplicable(typeof(ISaveRam))]
	public sealed partial class Intellivision : IEmulator
	{
		byte[] Rom;
		GameInfo Game;

		CP1610 Cpu;
		ICart Cart;
		STIC Stic;
		PSG Psg;

		public void Connect()
		{
			Cpu.SetIntRM(Stic.GetSr1());
			Cpu.SetBusRq(Stic.GetSr2());
			Stic.SetSst(Cpu.GetBusAk());
		}

		public void LoadExecutiveRom(byte[] erom)
		{
			if (erom.Length != 8192)
			{
				throw new ApplicationException("EROM file is wrong size - expected 8192 bytes");
			}
			int index = 0;
			// Combine every two bytes into a word.
			while (index + 1 < erom.Length)
			{
				ExecutiveRom[index / 2] = (ushort)((erom[index++] << 8) | erom[index++]);
			}
		}

		public void LoadGraphicsRom(byte[] grom)
		{
			if (grom.Length != 2048)
			{
				throw new ApplicationException("GROM file is wrong size - expected 2048 bytes");
			}
			GraphicsRom = grom;
		}

		[CoreConstructor("INTV")]
		public Intellivision(CoreComm comm, GameInfo game, byte[] rom)
		{
			ServiceProvider = new BasicServiceProvider(this);
			CoreComm = comm;

			Rom = rom;
			Game = game;
			Cart = new Intellicart();
			if (Cart.Parse(Rom) == -1)
			{
				Cart = new Cartridge();
				Cart.Parse(Rom);
			}

			Cpu = new CP1610();
			Cpu.ReadMemory = ReadMemory;
			Cpu.WriteMemory = WriteMemory;
			Cpu.Reset();

			Stic = new STIC();
			Stic.ReadMemory = ReadMemory;
			Stic.WriteMemory = WriteMemory;
			Stic.Reset();
			(ServiceProvider as BasicServiceProvider).Register<IVideoProvider>(Stic);

			Psg = new PSG();
			Psg.ReadMemory = ReadMemory;
			Psg.WriteMemory = WriteMemory;

			Connect();

			Cpu.LogData();

			LoadExecutiveRom(CoreComm.CoreFileProvider.GetFirmware("INTV", "EROM", true, "Executive ROM is required."));
			LoadGraphicsRom(CoreComm.CoreFileProvider.GetFirmware("INTV", "GROM", true, "Graphics ROM is required."));
		}

		public IEmulatorServiceProvider ServiceProvider { get; private set; }

		public void FrameAdvance(bool render, bool rendersound)
		{
			Frame++;
			Cpu.AddPendingCycles(14934);
			while (Cpu.GetPendingCycles() > 0)
			{
				int cycles = Cpu.Execute();
				Stic.Execute(cycles);
				Connect();
				Cpu.LogData();
			}
		}

		public ISoundProvider SoundProvider { get { return NullSound.SilenceProvider; } }
		public ISyncSoundProvider SyncSoundProvider { get { return new FakeSyncSound(NullSound.SilenceProvider, 735); } }
		public bool StartAsyncSound() { return true; }
		public void EndAsyncSound() { }

		public static readonly ControllerDefinition IntellivisionController =
			new ControllerDefinition
			{
				Name = "Intellivision Controller",
				BoolButtons = {
					"P1 Up", "P1 Down", "P1 Left", "P1 Right",
					"P1 L", "P1 R",
					"P1 Key 0", "P1 Key 1", "P1 Key 2", "P1 Key 3", "P1 Key 4", "P1 Key 5",
					"P1 Key 6", "P1 Key 7", "P1 Key 8", "P1 Key 9", "P1 Enter", "P1 Clear",

					"P2 Up", "P2 Down", "P2 Left", "P2 Right",
					"P2 L", "P2 R",
					"P2 Key 0", "P2 Key 1", "P2 Key 2", "P2 Key 3", "P2 Key 4", "P2 Key 5",
					"P2 Key 6", "P2 Key 7", "P2 Key 8", "P2 Key 9", "P2 Enter", "P2 Clear"
				}
			};

		public ControllerDefinition ControllerDefinition
		{
			get { return IntellivisionController; }
		}

		public IController Controller { get; set; }
		public int Frame { get; set; }

		public string SystemId
		{
			get { return "INTV"; }
		}

		[FeatureNotImplemented]
		public string BoardName { get { return null; } }

		public bool DeterministicEmulation { get { return true; } }

		public void ResetCounters()
		{
			Frame = 0;
		}

		public CoreComm CoreComm { get; private set; }

		public void Dispose()
		{
		}
	}
}