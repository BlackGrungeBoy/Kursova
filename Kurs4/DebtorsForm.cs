using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kurs4.model;


namespace Kurs4 {
    public partial class DebtorsForm : Form {

        private BaseSystem baseSystem;

        public DebtorsForm(BaseSystem baseSystem) {
            this.baseSystem = baseSystem;
            InitializeComponent();
        }

        public void UpdateForm() {
            ListBox listDebtors = (ListBox) Controls.Find("listDebtors", true)[0];

            listDebtors.DataSource = null;
            listDebtors.DataSource = baseSystem.GetDebtClients();
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            UpdateForm();
            MessageBox.Show("asd");
        }
    }
}
