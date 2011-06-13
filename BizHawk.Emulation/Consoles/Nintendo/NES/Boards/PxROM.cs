﻿using System;
using System.IO;
using System.Diagnostics;

namespace BizHawk.Emulation.Consoles.Nintendo
{
	//AKA MMC2 AKA Mike Tyson's Punch-Out!!
	class PxROM : NES.NESBoardBase
	{
		//configuration
		int prg_bank_mask_8k, chr_bank_mask_4k;

		//state
		IntBuffer prg_banks_8k = new IntBuffer(4);
		IntBuffer chr_banks_4k = new IntBuffer(4);
		IntBuffer chr_latches = new IntBuffer(2);

		public override void SyncState(Serializer ser)
		{
			base.SyncState(ser);
			ser.Sync("prg_banks_8k", ref prg_banks_8k);
			ser.Sync("chr_banks_4k", ref chr_banks_4k);
			ser.Sync("chr_latches", ref chr_latches);
		}

		public override void Dispose()
		{
			base.Dispose();
			prg_banks_8k.Dispose();
			chr_banks_4k.Dispose();
			chr_latches.Dispose();
		}

		public override bool Configure(NES.EDetectionOrigin origin)
		{
			switch (Cart.board_type)
			{
				case "NES-PNROM": //punch-out!!
				case "HVC-PEEOROM":
					AssertPrg(128); AssertChr(128); AssertWram(0); AssertVram(0);
					break;

				default:
					return false;
			}

			prg_bank_mask_8k = Cart.prg_size / 8 - 1;
			chr_bank_mask_4k = Cart.chr_size / 4 - 1;

			prg_banks_8k[0] = 0;
			prg_banks_8k[1] = 0xFD & prg_bank_mask_8k;
			prg_banks_8k[2] = 0xFE & prg_bank_mask_8k; ;
			prg_banks_8k[3] = 0xFF & prg_bank_mask_8k; ;

			return true;
		}


		public override void WritePRG(int addr, byte value)
		{
			switch (addr & 0xF000)
			{
				case 0x2000: //$A000:      PRG Reg
					prg_banks_8k[0] = value & prg_bank_mask_8k;
					break;
				case 0x3000: //$B000:      CHR Reg 0A
					chr_banks_4k[0] = value & chr_bank_mask_4k;
					break;
				case 0x4000: //$C000:      CHR Reg 0B
					chr_banks_4k[1] = value & chr_bank_mask_4k;
					break;
				case 0x5000: //$D000:      CHR Reg 1A
					chr_banks_4k[2] = value & chr_bank_mask_4k;
					break;
				case 0x6000: //$E000:      CHR Reg 1B
					chr_banks_4k[3] = value & chr_bank_mask_4k;
					break;
				case 0x7000: //$F000:  [.... ...M]   Mirroring:
					SetMirrorType(value.Bit(0) ? EMirrorType.Horizontal : EMirrorType.Vertical);
					break;
			}
		}

		public override byte ReadPPU(int addr)
		{
			int side = addr>>12;
			int tile = (addr>>4)&0xFF;
			if (addr < 0x2000)
			{
				switch (tile)
				{
					case 0xFD: chr_latches[side] = 0; break;
					case 0xFE: chr_latches[side] = 1; break;
				}
				int reg = side * 2 + chr_latches[side];
				int ofs = addr & ((1 << 12) - 1);
				int bank_4k = chr_banks_4k[reg];
				addr = (bank_4k << 12) | ofs;
				return VROM[addr];
			}
			else return base.ReadPPU(addr);
		}

		public override byte ReadPRG(int addr)
		{
			int bank_8k = addr >> 13;
			int ofs = addr & ((1 << 13) - 1);
			bank_8k = prg_banks_8k[bank_8k];
			addr = (bank_8k << 13) | ofs;
			return ROM[addr];
		}


	}
}