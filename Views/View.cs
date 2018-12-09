using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MutableDataGridSync.Views
{
    public partial class View : Form
    {
        public DataTable DataSource
        {
            get
            {
                return dataGridView1.DataSource as DataTable;
            }
            set
            {
                dataGridView1.DataSource = value;
            }
        }
        public event EventHandler UpdateTable;
        public event EventHandler<DataRow> RowChanged;

        public View()
        {
            InitializeComponent();
            var presenter = new Presenter.Presenter(this);
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            UpdateTable?.Invoke(sender, EventArgs.Empty);
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var row = ((DataRowView) dataGridView1.Rows[e.RowIndex].DataBoundItem).Row;
            RowChanged?.Invoke(this, row);
        }
    }
}
