﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BizHawk.Emulation.Computers.Commodore64.MOS
{
	// the functions on this port are at the point of
	// view of an external device.

	public class SerialPort
	{
		public Func<bool> DeviceReadAtn;
		public Func<bool> DeviceReadClock;
		public Func<bool> DeviceReadData;
		public Func<bool> DeviceReadReset;
		public Action<bool> DeviceWriteAtn;
		public Action<bool> DeviceWriteClock;
		public Action<bool> DeviceWriteData;
		public Action<bool> DeviceWriteSrq;

		public Func<bool> SystemReadAtn;
		public Func<bool> SystemReadClock;
		public Func<bool> SystemReadData;
		public Func<bool> SystemReadSrq;
		public Action<bool> SystemWriteAtn;
		public Action<bool> SystemWriteClock;
		public Action<bool> SystemWriteData;
		public Action<bool> SystemWriteReset;

		// Connect() needs to set System functions above

		public SerialPort()
		{
			DeviceReadAtn = (() => { return true; });
			DeviceReadClock = (() => { return true; });
			DeviceReadData = (() => { return true; });
			DeviceReadReset = (() => { return true; });
			DeviceWriteAtn = ((bool val) => { });
			DeviceWriteClock = ((bool val) => { });
			DeviceWriteData = ((bool val) => { });
			DeviceWriteSrq = ((bool val) => { });
			SystemReadAtn = (() => { return true; });
			SystemReadClock = (() => { return true; });
			SystemReadData = (() => { return true; });
			SystemReadSrq = (() => { return true; });
			SystemWriteAtn = ((bool val) => { });
			SystemWriteClock = ((bool val) => { });
			SystemWriteData = ((bool val) => { });
			SystemWriteReset = ((bool val) => { });
		}

		public void HardReset()
		{
		}
	}
}
