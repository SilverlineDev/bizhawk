﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LuaInterface;

namespace BizHawk.Client.EmuHawk
{
	public partial class LuaWinform : Form
	{
		public List<LuaEvent> ControlEvents = new List<LuaEvent>();

		public LuaWinform()
		{
			InitializeComponent();
			StartPosition = FormStartPosition.CenterParent;
			Closing += (o, e) => CloseThis();
		}

		private void LuaWinform_Load(object sender, EventArgs e)
		{

		}

		public void CloseThis()
		{
			GlobalWin.Tools.LuaConsole.LuaImp.WindowClosed(Handle);
		}

		public void DoLuaEvent(IntPtr handle)
		{
			foreach (LuaEvent l_event in ControlEvents)
			{
				if (l_event.Control == handle)
				{
					l_event.Event.Call();
				}
			}
		}

		public class LuaEvent
		{
			public LuaFunction Event;
			public IntPtr Control;

			public LuaEvent() { }
			public LuaEvent(IntPtr handle, LuaFunction lfunction)
			{
				Event = lfunction;
				Control = handle;
			}
		}
	}
}
