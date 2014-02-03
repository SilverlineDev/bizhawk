﻿using System;
using System.Drawing;
using System.Windows.Forms;

using BizHawk.Client.Common;

namespace BizHawk.Client.EmuHawk
{
	/// <summary>
	/// A simple form that prompts the user for a single line of input
	/// </summary>
	public partial class InputPrompt : Form
	{
		public enum InputType { HEX, UNSIGNED, SIGNED, TEXT };
		public bool UserOK;    //Will be true if the user selects Ok
		public string UserText = "";   //What the user selected
		public Point _Location = new Point(-1, -1);
		private InputType itype = InputType.TEXT;

		public InputType TextInputType
		{
			get { return itype; }
			set { itype = value; }
		}

		public InputPrompt()
		{
			InitializeComponent();
		}

		public void SetMessage(string message)
		{
			PromptLabel.Text = message;
		}

		public void SetCasing(CharacterCasing casing)
		{
			PromptBox.CharacterCasing = casing;
		}

		public void SetInitialValue(string value)
		{
			PromptBox.Text = value;
		}

		public void SetTitle(string value)
		{
			Text = value;
		}

		private void InputPrompt_Load(object sender, EventArgs e)
		{
			if (_Location.X > 0 && _Location.Y > 0)
			{
				Location = _Location;
			}
		}

		private void OK_Click(object sender, EventArgs e)
		{
			UserOK = true;
			UserText = PromptBox.Text;
			Close();
		}

		private void Cancel_Click(object sender, EventArgs e)
		{
			UserOK = false;
			Close();
		}

		private void PromptBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			switch (itype)
			{
				default:
				case InputType.TEXT:
					break;
				case InputType.HEX:
					if (e.KeyChar == '\b' || e.KeyChar == 22 || e.KeyChar == 1 || e.KeyChar == 3)
					{
						return;
					}
					else if (!InputValidate.IsHex(e.KeyChar))
					{
						e.Handled = true;
					}
					break;
				case InputType.SIGNED:
					if (e.KeyChar == '\b' || e.KeyChar == 22 || e.KeyChar == 1 || e.KeyChar == 3)
					{
						return;
					}
					else if (!InputValidate.IsUnsigned(e.KeyChar))
					{
						e.Handled = true;
					}
					break;
				case InputType.UNSIGNED:
					if (e.KeyChar == '\b' || e.KeyChar == 22 || e.KeyChar == 1 || e.KeyChar == 3)
					{
						return;
					}
					else if (!InputValidate.IsSigned(e.KeyChar))
					{
						e.Handled = true;
					}
					break;
			}
		}
	}
}
