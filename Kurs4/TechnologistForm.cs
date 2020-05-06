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
using Newtonsoft.Json;

namespace Kurs4 {
    public partial class TechnologistForm : Form {

        private BaseSystem baseSystem;

        private CallSimulation callSimulation;
        private ClientInfoForm clientInfoForm;
        private ReplenishmentSimulation replenishmentSimulation;
        private DebtorsForm debtorsForm;
        private StatisticMonthForm statisticMonthForm;
        private CurrentTimeSimulation currentTimeSimulation;
        private GetPriceByDateForm getPriceByDateForm;

        public TechnologistForm(BaseSystem baseSystem) {
            this.baseSystem = baseSystem;
            callSimulation = new CallSimulation(baseSystem);
            clientInfoForm = new ClientInfoForm();
            replenishmentSimulation = new ReplenishmentSimulation(baseSystem);
            debtorsForm = new DebtorsForm(baseSystem);
            statisticMonthForm = new StatisticMonthForm(baseSystem);
            currentTimeSimulation = new CurrentTimeSimulation();
            getPriceByDateForm = new GetPriceByDateForm(baseSystem);
            InitializeComponent();
            UpdateForm();
        }

        public void UpdateForm() {
            ListBox listClients = (ListBox) Controls.Find("listClients", true)[0];
            listClients.DataSource = null;
            listClients.DataSource = baseSystem.clients;

            ListBox listLocalities = (ListBox)Controls.Find("listLocalities", true)[0];
            listLocalities.DataSource = null;
            listLocalities.DataSource = baseSystem.localities;
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e) {

        }

        private void tableLayoutPanel2_Paint_1(object sender, PaintEventArgs e) {

        }

        private void btnRegister_Click(object sender, EventArgs e) {
            TextBox tBoxNumber = (TextBox) Controls.Find("tBoxNumber", true)[0];
            TextBox tBoxName = (TextBox) Controls.Find("tBoxName", true)[0];
            TextBox tBoxSurname = (TextBox) Controls.Find("tBoxSurname", true)[0];
            TextBox tBoxPatronymic = (TextBox) Controls.Find("tBoxPatronymic", true)[0];
            TextBox tBoxAddress = (TextBox) Controls.Find("tBoxAddress", true)[0];

            bool success;

            success = baseSystem.RegisterClient(new Client.FullName(tBoxName.Text, tBoxSurname.Text, tBoxPatronymic.Text),
                tBoxNumber.Text, tBoxAddress.Text);

            UpdateForm();
            if (success)
                MessageBox.Show("Клієнт був успішно зареєстрований");
            else
                MessageBox.Show("Помилка реєстрації, спробуйте ще раз");
        }

        private void btnRegisterLocality_Click(object sender, EventArgs e) {
            TextBox tBoxLocalityName = (TextBox)Controls.Find("tBoxLocalityName", true)[0];

            bool success;

            success = baseSystem.RegisterLocality(tBoxLocalityName.Text);

            UpdateForm();
            if (success)
                MessageBox.Show("Місцевість була успішно зареєстрована");
            else
                MessageBox.Show("Помилка реєстрації, спробуйте ще раз");
        }

        private void callToolStripMenuItem_Click(object sender, EventArgs e) {
            callSimulation.UpdateForm();
            callSimulation.ShowDialog();
        }

        private void listClients_DoubleClick(object sender, EventArgs e) {
            ListBox listClients = (ListBox) sender;

            if (listClients.SelectedItem != null) {
                clientInfoForm.client = (Client) listClients.SelectedItem;
                clientInfoForm.UpdateForm();

                clientInfoForm.ShowDialog();
            }
        }

        private void replenishmentToolStripMenuItem_Click(object sender, EventArgs e) {
            replenishmentSimulation.ShowDialog();
        }

        private void listLocalities_DoubleClick(object sender, EventArgs e) {

        }

        private void listLocalities_Click(object sender, EventArgs e) {
            ListBox listLocalities = (ListBox) sender;

            TextBox tBoxPrice = (TextBox) Controls.Find("tBoxPrice", true)[0];
            TextBox tBoxPriceP = (TextBox) Controls.Find("tBoxPriceP", true)[0];

            Locality locality = (Locality) listLocalities.SelectedItem;

            if(listLocalities.SelectedItem != null) {
                tBoxPrice.Text = "" + locality.GetLastPrice().price1m;
                tBoxPriceP.Text = "" + locality.GetLastPrice().price1mP;
            } else {
                tBoxPrice.Text = "" + 0;
                tBoxPriceP.Text = "" + 0;
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            ListBox listLocalities = (ListBox) Controls.Find("listLocalities", true)[0];

            TextBox tBoxPrice = (TextBox)Controls.Find("tBoxPrice", true)[0];
            TextBox tBoxPriceP = (TextBox)Controls.Find("tBoxPriceP", true)[0];

            Locality locality = (Locality) listLocalities.SelectedItem;

            if (listLocalities.SelectedItem != null) {
                bool success = locality.AddPrice(new Locality.Price(float.Parse(tBoxPrice.Text), float.Parse(tBoxPriceP.Text), BaseSystem.currentDateTime));

                if (success)
                    MessageBox.Show("Ціна змінена успішно");
                else
                    MessageBox.Show("Помилка зміни ціни");
            }

            UpdateForm();
        }

        private void debtorsToolStripMenuItem_Click(object sender, EventArgs e) {
            debtorsForm.ShowDialog();
        }

        private void statisticToolStripMenuItem_Click(object sender, EventArgs e) {
        }

        private void currentTimeToolStripMenuItem_Click(object sender, EventArgs e) {
            currentTimeSimulation.ShowDialog();
        }

        private void diagramToolStripMenuItem_Click(object sender, EventArgs e) {
            statisticMonthForm.ShowDialog();
        }

        private void priceByDateToolStripMenuItem_Click(object sender, EventArgs e) {
            getPriceByDateForm.ShowDialog();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == DialogResult.OK) {
                System.IO.File.WriteAllText(openFileDialog.FileName, JsonConvert.SerializeObject(baseSystem, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects }));
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e) {

        }
    }
}
