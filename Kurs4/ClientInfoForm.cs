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
    public partial class ClientInfoForm : Form {

        public Client client;

        public ClientInfoForm() {
            InitializeComponent();
        }

        public void UpdateForm() {
            Label labelNumber = (Label) Controls.Find("labelNumber", true)[0];
            Label labelName = (Label) Controls.Find("labelName", true)[0];
            Label labelSurname = (Label) Controls.Find("labelSurname", true)[0];
            Label labelPatronymic = (Label) Controls.Find("labelPatronymic", true)[0];
            Label labelAddress = (Label) Controls.Find("labelAddress", true)[0];

            Label labelBalance = (Label)Controls.Find("labelBalance", true)[0];

            labelNumber.Text = client.telephoneNumber;
            labelName.Text = client.fullName.name;
            labelSurname.Text = client.fullName.surname;
            labelPatronymic.Text = client.fullName.patronymic;
            labelAddress.Text = client.address;

            labelBalance.Text = "" + client.money;

            UpdateListCalls();
        }

        public void UpdateListCalls() {
            ComboBox comboBoxCalls = (ComboBox) Controls.Find("comboBoxCalls", true)[0];
            ListBox listCalls = (ListBox)Controls.Find("listCalls", true)[0];

            comboBoxCalls.SelectedItem = "Всі";
            switch(comboBoxCalls.SelectedItem) {
                case "Всі":
                    listCalls.DataSource = null;
                    listCalls.DataSource = client.calls;
                    break;
                case "Оплачені":
                    listCalls.DataSource = null;
                    listCalls.DataSource = client.GetPaidCalls();
                    break;
                case "Неоплачені":
                    listCalls.DataSource = null;
                    listCalls.DataSource = client.GetNotPaidCalls();
                    break;
            }
        }

        private void label8_Click(object sender, EventArgs e) {

        }

        private void comboBoxCalls_SelectedIndexChanged(object sender, EventArgs e) {
            UpdateListCalls();
        }
    }
}
