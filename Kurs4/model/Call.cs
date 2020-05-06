using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Kurs4.model {
    public class Call {
        public Client client = null;
        public DateTime dateTime;
        public Locality locality;
        public TimeSpan talkTime;
        public bool paid = true;

        public void Update(Client client, DateTime dateTime, Locality locality, TimeSpan talkTime) {
            this.client = client;
            this.dateTime = dateTime;
            this.locality = locality;
            this.talkTime = talkTime;
        }

        public double GetPrice() {
            return locality.GetPrice(dateTime) * Math.Round(talkTime.TotalMinutes);
        }

        public override string ToString() {
            return client.ToString() + " \n " +
                dateTime.ToString() + " \n " +
                locality.ToString() + " \n " +
                talkTime.ToString() + " \n " + (GetPrice());
        }
    }
}
