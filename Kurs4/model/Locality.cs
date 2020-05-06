using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurs4.model {
    public class Locality {
        public string localityName;

        public struct Price {
            public float price1m;
            public float price1mP;
            public DateTime dateTime;

            public Price(float price1m, float price1mP, DateTime dateTime) {
                this.price1m = price1m;
                this.price1mP = price1mP;
                this.dateTime = dateTime;
            }

            public override string ToString() {
                return price1m + "/" + price1mP;
            }
        }

        public List<Price> prices = new List<Price>();

        public Locality(string localityName) {
            this.localityName = localityName;
            prices.Add(new Price(0, 0, BaseSystem.currentDateTime));
        }

        public bool AddPrice(Price price) {
            if(HasDatePrice(price.dateTime)) {
                return false;
            } else {
                prices.Add(price);
                return true;
            }
        }

        public bool HasDatePrice(DateTime dateTime) {
            foreach(Price price in prices) {
                DateTime dateT = price.dateTime;
                if(dateT.Day == dateTime.Day && dateT.Month == dateTime.Month && dateT.Year == dateTime.Year) {
                    return true;
                }
            }

            return false;
        }

        public int GetPriceIndexLowerByDate(DateTime date) {

            int index = prices.Count - 1;

            if(prices.Count > 1) {
                for(int i = 0; i < prices.Count; i++) {
                    if(IsDateLowerThan(date, prices[i].dateTime)) {
                        index = i - 1;
                        break;
                    }
                }
            } else {
                
            }

            return index;
        }

        public Price GetPriceByDate(DateTime date) {
            return prices[GetPriceIndexLowerByDate(date)];
        }

        public static bool IsDateLowerThan(DateTime date, DateTime than) {
            if(date.Day < than.Day) {
                return true;
            } else {
                return false;
            }
        }

        public Price GetLastPrice() {
            return prices[prices.Count - 1];
        }

        public double GetPrice(DateTime dateTime) {
            if(dateTime.Hour >= 20 && dateTime.Hour <= 0) {
                return GetLastPrice().price1mP;
            } else {
                return GetLastPrice().price1m;
            }
        }

        public override string ToString() {
            return localityName + " | " + GetLastPrice().price1m + "/" + GetLastPrice().price1mP;
        }
    }
}
