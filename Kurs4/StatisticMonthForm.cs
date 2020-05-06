using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Kurs4.model;

namespace Kurs4 {
    public partial class StatisticMonthForm : Form {

        private BaseSystem baseSystem;

        public StatisticMonthForm(BaseSystem baseSystem) {
            this.baseSystem = baseSystem;
            InitializeComponent();
        }

        public void UpdateForm() {
            ListBox listLocalities = (ListBox) Controls.Find("listLocalities", true)[0];
            listLocalities.DataSource = null;
            listLocalities.DataSource = baseSystem.localities;
        }

        private void btnSet_Click(object sender, EventArgs e) {
            MonthCalendar monthCalendar = (MonthCalendar)Controls.Find("monthCalendar", true)[0];
            Chart chart = (Chart)Controls.Find("chart", true)[0];
            ListBox listLocalities = (ListBox)Controls.Find("listLocalities", true)[0];
            int countDays = DateTime.DaysInMonth(monthCalendar.SelectionStart.Year, monthCalendar.SelectionStart.Month);
            Locality locality = (Locality) listLocalities.SelectedItem;

            chart.Series[0].Points.Clear();

            DateTime selectionDateTime = new DateTime(monthCalendar.SelectionStart.Year, monthCalendar.SelectionStart.Month, 1);
            

            for (int i = 0; i < countDays; i++) {
                chart.Series[0].Points.AddXY(i + 1, baseSystem.GetCountClientsByMonthLocalityDay(selectionDateTime.AddDays(i), locality));
            }

            chart.Update();
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            UpdateForm();
        }
    }
}
