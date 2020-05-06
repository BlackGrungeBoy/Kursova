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
    public partial class CurrentTimeSimulation : Form {
        public CurrentTimeSimulation() {
            InitializeComponent();
        }

        public void UpdateForm() {
            Label labelDate = (Label)Controls.Find("labelDate", true)[0];
            DateTimePicker dateTimePicker = (DateTimePicker)Controls.Find("dateTimePicker", true)[0];

            labelDate.Text = "" + dateTimePicker.Value.Day + "." + dateTimePicker.Value.Month + "." + dateTimePicker.Value.Year;
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            UpdateForm();
        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e) {
            UpdateForm();
        }

        private void btnChangeDateTime_Click(object sender, EventArgs e) {
            DateTimePicker dateTimePicker = (DateTimePicker)Controls.Find("dateTimePicker", true)[0];
            BaseSystem.currentDateTime = dateTimePicker.Value;

            Hide();
            if(BaseSystem.currentDateTime.Hour >= 20 || (BaseSystem.currentDateTime.Hour <= 6))
                MessageBox.Show(BaseSystem.currentDateTime.ToString());
        }
    }
}
