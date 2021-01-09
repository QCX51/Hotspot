using System;
using System.Drawing;
using System.Windows.Forms;

namespace Classes
{
    internal class GridList : DataGridView
    {
        internal GridList()
        {
            this.BorderStyle = BorderStyle.FixedSingle;
            this.TopLeftHeaderCell.Style.BackColor = SystemColors.Control;
            this.AdjustedTopLeftHeaderBorderStyle.All = DataGridViewAdvancedCellBorderStyle.Single;
            this.AdvancedColumnHeadersBorderStyle.All = DataGridViewAdvancedCellBorderStyle.Single;
            this.AdvancedRowHeadersBorderStyle.All = DataGridViewAdvancedCellBorderStyle.Single;
            this.AdvancedCellBorderStyle.All = DataGridViewAdvancedCellBorderStyle.Single;
            this.ScrollBars = ScrollBars.Both;
            this.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.EditMode = DataGridViewEditMode.EditOnEnter;
            this.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            this.AutoSize = false;
            this.AllowDrop = false;
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.AllowUserToResizeRows = false;
            this.AllowUserToResizeColumns = true;
            this.AllowUserToOrderColumns = false;
            this.ColumnHeadersVisible = true;
            this.ColumnHeadersHeight = 35;
            this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.RowHeadersVisible = true;
            this.RowHeadersWidth = 30;
            this.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.Margin = new Padding(0);
            this.ReadOnly = false;
            this.BackgroundColor = SystemColors.Control;
            this.EnableHeadersVisualStyles = false;
            this.GridColor = SystemColors.ControlText;
            this.MultiSelect = true;
            this.ShowCellToolTips = true;
            this.Dock = DockStyle.Fill;
            this.TabIndex = 1024;

        }
        internal void AddColumn(string HeaderText, bool Readonly, bool Frozen, bool AutoSize, bool Resizeble, int Width)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action(() => AddColumn(HeaderText, ReadOnly, Frozen, AutoSize, Resizeble, Width)));
                return;
            }
            DataGridViewTextBoxColumn Column = new DataGridViewTextBoxColumn()
            {
                HeaderText = HeaderText,
                Name = HeaderText,
                SortMode = DataGridViewColumnSortMode.Programmatic,
                ReadOnly = ReadOnly,
                Frozen = Frozen
            };
            this.Columns.Add(Column);
            Column.HeaderCell.Style = ColumnHeaderStyle;
            Column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Column.Width = Width;
            Column.MinimumWidth = Width > 10 ? 10 : Width;
            Column.FillWeight = Width;
            Column.DividerWidth = 0;
            Column.Resizable = Resizeble ? DataGridViewTriState.True : DataGridViewTriState.False;
            //Column.AutoSizeMode = AutoSize ? DataGridViewAutoSizeColumnMode.AllCells : DataGridViewAutoSizeColumnMode.Fill;
            Column.HeaderCell.SortGlyphDirection = SortOrder.None;
        }
        internal void AddRow(DataGridViewCell[] Cells, bool ReadOnly, int Height)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action(() => AddRow(Cells, ReadOnly, Height)));
                return;
            }
            DataGridViewRow Row = new DataGridViewRow()
            {
                DefaultHeaderCellType = typeof(DataGridViewRowHeaderCell),
                MinimumHeight = Height
            };
            Row.Cells.AddRange(Cells);
            Rows.Add(Row);
            Row.HeaderCell.Style = RowHeaderStyle;
            Row.HeaderCell.ToolTipText = Row.Index.ToString();
            Row.DefaultCellStyle = CellStyle;
            Row.Frozen = Row.HeaderCell.Frozen;
            Row.ReadOnly = ReadOnly;
            Row.Height = Height;
            Row.MinimumHeight = Height;
            Row.DividerHeight = 0;
            Row.HeaderCell.Value = Row.Index;
            Row.Resizable = DataGridViewTriState.False;
            Row.Selected = false;
            Row.Tag = string.Empty;

            foreach (DataGridViewCell item in Row.Cells)
            {
                item.Selected = false;
                if (item.ColumnIndex.Equals(4))
                {
                    item.ReadOnly = false;
                }
                else
                {
                    item.ReadOnly = true;
                }
            }
        }

        private DataGridViewCellStyle CellStyle
        {
            get
            {
                DataGridViewCellStyle style = new DataGridViewCellStyle
                {
                    Padding = new Padding(0),
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    BackColor = SystemColors.Control,
                    ForeColor = Color.Black,
                    Font = new Font(SystemFonts.DefaultFont.FontFamily, 8F, FontStyle.Regular),
                    Format = null,
                    NullValue = null,
                    SelectionBackColor = SystemColors.Highlight,
                    SelectionForeColor = Color.Black
                };
                return style;
            }
        }

        private DataGridViewCellStyle RowHeaderStyle
        {
            get
            {
                DataGridViewCellStyle style = new DataGridViewCellStyle
                {
                    Padding = new Padding(0),
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    BackColor = Color.Orange,
                    Font = new Font(SystemFonts.DefaultFont.FontFamily, 8F, FontStyle.Regular),
                    ForeColor = Color.Black,
                    Format = null,
                    NullValue = null,
                    SelectionBackColor = Color.Orange,
                    SelectionForeColor = Color.Black,
                    WrapMode = DataGridViewTriState.False
                };
                return style;
            }
        }

        private DataGridViewCellStyle ColumnHeaderStyle
        {
            get
            {
                DataGridViewCellStyle style = new DataGridViewCellStyle
                {
                    Padding = new Padding(0),
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    BackColor = SystemColors.Control,
                    Font = new Font(SystemFonts.DefaultFont.FontFamily, 9F, FontStyle.Bold),
                    ForeColor = Color.Black,
                    Format = null,
                    NullValue = null,
                    SelectionBackColor = SystemColors.Highlight,
                    SelectionForeColor = Color.Black,
                    WrapMode = DataGridViewTriState.False
                };
                return style;
            }
        }
        
    }
}
