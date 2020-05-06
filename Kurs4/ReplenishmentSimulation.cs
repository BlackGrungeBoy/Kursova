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
    public partial class ReplenishmentSimulation : Form {

        public BaseSystem baseSystem;

        public ReplenishmentSimulation(BaseSystem baseSystem) {
            this.baseSystem = baseSystem;
            InitializeComponent();
        }

        private void btnReplenish_Click(object sender, EventArgs e) {
            TextBox tBoxNumber = (TextBox) Controls.Find("tBoxNumber", true)[0];
            TextBox tBoxSum = (TextBox) Controls.Find("tBoxSum", true)[0];

            bool success = false;

            Client client = baseSystem.GetClientByNumber(tBoxNumber.Text);

            if (client != null) {
                client.money += int.Parse(tBoxSum.Text);
                success = true;
            }

            if (success)
                MessageBox.Show("Поповнення пройшло успішно");
            else
                MessageBox.Show("Помилка поповнення");

        }
    }
}
