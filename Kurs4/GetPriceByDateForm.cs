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
    public partial class GetPriceByDateForm : Form {

        private BaseSystem baseSystem;

        public GetPriceByDateForm(BaseSystem baseSystem) {
            this.baseSystem = baseSystem;
            InitializeComponent();
        }

        public void UpdateForm() {
            ListBox listLocalities = (ListBox)Controls.Find("listLocalities", true)[0];
            listLocalities.DataSource = null;
            listLocalities.DataSource = baseSystem.localities;
        }

        private void btnOk_Click(object sender, EventArgs e) {
            ListBox listLocalities = (ListBox)Controls.Find("listLocalities", true)[0];
            Label labelPrice = (Label)Controls.Find("labelPrice", true)[0];
            DateTimePicker dateTimePicker = (DateTimePicker)Controls.Find("dateTimePicker", true)[0];

            Locality locality = (Locality) listLocalities.SelectedItem;

            labelPrice.Text = locality.GetPriceByDate(dateTimePicker.Value).ToString();
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            UpdateForm();
        }
    }
}
