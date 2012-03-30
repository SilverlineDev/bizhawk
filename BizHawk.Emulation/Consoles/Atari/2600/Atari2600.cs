﻿using System;
using System.Collections.Generic;
using System.IO;

namespace BizHawk
{
	public partial class Atari2600 : IEmulator, IVideoProvider, ISoundProvider
	{
		public string SystemId { get { return "A26"; } }
		public GameInfo game;

		public int[] frameBuffer = new int[320 * 262];
		public CoreInputComm CoreInputComm { get; set; }
		public CoreOutputComm CoreOutputComm { get; private set; }
		public IVideoProvider VideoProvider { get { return this; } }
		public ISoundProvider SoundProvider { get { return this; } }

		public Atari2600(GameInfo game, byte[] rom)
		{
			var domains = new List<MemoryDomain>(1);
			domains.Add(new MemoryDomain("Main RAM", 128, Endian.Little, addr => ram[addr & 127], (addr, value) => ram[addr & 127] = value));
			memoryDomains = domains.AsReadOnly();
			CoreOutputComm = new CoreOutputComm();
			CoreInputComm = new CoreInputComm();
			this.rom = rom;
			this.game = game;
			Console.WriteLine("Game uses mapper " + game.GetOptionsDict()["m"]);
			HardReset();
		}
		public void ResetFrameCounter()
		{
			_frame = 0;
		}

		public static readonly ControllerDefinition Atari2600ControllerDefinition = new ControllerDefinition
		{
			Name = "Atari 2600 Basic Controller",
			BoolButtons =
			{
				"P1 Up", "P1 Down", "P1 Left", "P1 Right", "P1 Button", 
				"P2 Up", "P2 Down", "P2 Left", "P2 Right", "P2 Button", 
				"Reset",
			}
		};

		void SyncState(Serializer ser)
		{
			cpu.SyncState(ser);
			ser.Sync("ram", ref ram, false);
			ser.Sync("Lag", ref _lagcount);
			ser.Sync("Frame", ref _frame);
			//TODO - you need to sync your m6532 and tia
			mapper.SyncState(ser);
		}

		public ControllerDefinition ControllerDefinition { get { return Atari2600ControllerDefinition; } }
		public IController Controller { get; set; }

		public int Frame { get { return _frame; } set { _frame = value; } }
		public int LagCount { get { return _lagcount; } set { _lagcount = value; } }
		public bool IsLagFrame { get { return _islag; } }
		private bool _islag = true;
		private int _lagcount = 0;
		private int _frame = 0;

		public byte[] SaveRam { get { return new byte[0]; } }
		public bool DeterministicEmulation { get; set; }
		public bool SaveRamModified { get; set; }
		public void SaveStateText(TextWriter writer) { SyncState(Serializer.CreateTextWriter(writer)); }
		public void LoadStateText(TextReader reader) { SyncState(Serializer.CreateTextReader(reader)); }
		public void SaveStateBinary(BinaryWriter bw) { SyncState(Serializer.CreateBinaryWriter(bw)); }
		public void LoadStateBinary(BinaryReader br) { SyncState(Serializer.CreateBinaryReader(br)); }

		public byte[] SaveStateBinary()
		{
			MemoryStream ms = new MemoryStream();
			BinaryWriter bw = new BinaryWriter(ms);
			SaveStateBinary(bw);
			bw.Flush();
			return ms.ToArray();
		}
		public int[] GetVideoBuffer() { return frameBuffer; }
		public int BufferWidth { get { return 320; } }
		public int BufferHeight { get { return 262; } }
		public int BackgroundColor { get { return 0; } }
		public void GetSamples(short[] samples) 
		{
			/*
			int freqDiv = 0;
			byte myP4 = 0x00;

			short[] moreSamples = new short[1000];
			for (int i = 0; i < 1000; i++)
			{
				if (++freqDiv == (tia.audioFreqDiv * 2))
				{
					freqDiv = 0;
					myP4 = (byte)(((myP4 & 0x0f) != 0) ? ((myP4 << 1) | ((((myP4 & 0x08) != 0) ? 1 : 0) ^ (((myP4 & 0x04) != 0) ? 1 : 0))) : 1);
				}

				moreSamples[i] = (short)(((myP4 & 0x08) != 0) ? 32767 : 0);

			}

			for (int i = 0; i < samples.Length/2; i++)
			{
				//samples[i] = 0;
				if (tia.audioEnabled)
				{
					samples[i*2] = moreSamples[(int)(((double)moreSamples.Length / (double)(samples.Length/2)) * i)];
					//samples[i * 2 + 1] = moreSamples[(int)((moreSamples.Length / (samples.Length / 2)) * i)];
					//samples[i] = (short)(Math.Sin(((((32000.0 / (tia.audioFreqDiv+1)) / 60.0) * Math.PI) / samples.Length) * i) * MaxVolume + MaxVolume);
				}
				else
				{
					samples[i] = 0;
				}
			}
			//samples = tia.samples; 
			 * */
		}
		public void DiscardSamples() { }
		public int MaxVolume { get; set; }
		private IList<MemoryDomain> memoryDomains;
		public IList<MemoryDomain> MemoryDomains { get { return memoryDomains; } }
		public MemoryDomain MainMemory { get { return memoryDomains[0]; } }
		public void Dispose() { }
	}

}
