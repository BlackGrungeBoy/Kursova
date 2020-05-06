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
    public partial class CallSimulation : Form {

        public BaseSystem baseSystem;

        public CallSimulation(BaseSystem baseSystem) {
            this.baseSystem = baseSystem;
            InitializeComponent();
        }

        public void UpdateForm() {
            ListBox listLocalities = (ListBox)Controls.Find("listLocalities", true)[0];
            listLocalities.DataSource = null;
            listLocalities.DataSource = baseSystem.localities;
        }

        private void btnCall_Click(object sender, EventArgs e) {
            TextBox tBoxNumber = (TextBox) Controls.Find("tBoxNumber", true)[0];
            ListBox listLocalities = (ListBox) Controls.Find("listLocalities", true)[0];
            DateTimePicker dtpCallDuration = (DateTimePicker) Controls.Find("dtpCallDuration", true)[0];

            bool success = false;

            Client client = baseSystem.GetClientByNumber(tBoxNumber.Text);

            if(client != null)
                success = client.Call(BaseSystem.currentDateTime, (Locality) listLocalities.SelectedItem, new TimeSpan(dtpCallDuration.Value.Hour, dtpCallDuration.Value.Minute, dtpCallDuration.Value.Second));

            if (success)
                MessageBox.Show("Дзвінок виконаний успішно");
            else
                MessageBox.Show("Дзвінок не виконано");
        }

        private void listLocalities_Click(object sender, EventArgs e) {
            Label lblLocality = (Label)Controls.Find("lblLocality", true)[0];

            lblLocality.Text = ((ListBox) sender).SelectedItem.ToString();
        }
    }
}
