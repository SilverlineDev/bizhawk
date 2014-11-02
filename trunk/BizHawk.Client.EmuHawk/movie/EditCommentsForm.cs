﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

using BizHawk.Client.Common;

namespace BizHawk.Client.EmuHawk
{
	public partial class EditCommentsForm : Form
	{
		private IMovie _selectedMovie;
		private string _lastHeaderClicked;
		private bool _sortReverse;

		public EditCommentsForm()
		{
			InitializeComponent();
			_lastHeaderClicked = string.Empty;
			_sortReverse = false;
		}

		private void EditCommentsForm_Load(object sender, EventArgs e)
		{
			if (Global.MovieSession.ReadOnly)
			{
				CommentGrid.Columns[0].ReadOnly = true;
				Text = "View Comments";
			}

			if (CommentGrid.Rows.Count > 8)
			{
				var x = Height + ((CommentGrid.Rows.Count - 8) * 21);
				Height = x < 600 ? x : 600;
			}
		}

		public void GetMovie(IMovie m)
		{
			_selectedMovie = m;
			if (!m.Comments.Any())
			{
				return;
			}

			for (int i = 0; i < m.Comments.Count; i++)
			{
				CommentGrid.Rows.Add();
				var c = CommentGrid.Rows[i].Cells[0];
				c.Value = m.Comments[i];
			}
		}

		private void Cancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void OK_Click(object sender, EventArgs e)
		{
			if (!Global.MovieSession.ReadOnly)
			{
				_selectedMovie.Comments.Clear();
				for (int i = 0; i < CommentGrid.Rows.Count - 1; i++)
				{
					var c = CommentGrid.Rows[i].Cells[0];
					_selectedMovie.Comments.Add("comment " + c.Value);
				}
				_selectedMovie.Save();
			}
			Close();
		}

		private void OnColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			SortColumn(CommentGrid.Columns[e.ColumnIndex]);
		}

		private void SortColumn(DataGridViewColumn e)
		{
			ListSortDirection _direction;
			DataGridViewColumn _column = e;
			if (_lastHeaderClicked != _column.Name)
			{
				_sortReverse = false;
			}

			if (!_sortReverse)
			{
				_direction = ListSortDirection.Ascending;
			}
			else
			{
				_direction = ListSortDirection.Descending;
			}

			CommentGrid.Sort(_column, _direction);
			_lastHeaderClicked = _column.Name;
			_sortReverse = !_sortReverse;
			CommentGrid.Refresh();
		}
	}
}
