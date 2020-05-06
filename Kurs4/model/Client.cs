using System;
using System.Collections.Generic;

namespace Kurs4.model {

    public class Client {

        public struct FullName {
            public string name;
            public string surname;
            public string patronymic;

            public FullName(string name, string surname, string patronymic) {
                this.name = name;
                this.surname = surname;
                this.patronymic = patronymic;
            }

            public override string ToString() {
                return name + " " + surname + " " + patronymic;
            }
            
        }

        public FullName fullName;
        public string address;
        public DateTime registerDate = new DateTime();
        public string telephoneNumber;
        public float money = 100;
        public List<Call> calls = new List<Call>();

        public void Update(FullName fullName, string telephoneNumber, string address) {
            this.fullName = fullName;
            this.telephoneNumber = telephoneNumber;
            this.address = address;
            registerDate = BaseSystem.currentDateTime;
        }

        public bool Call(DateTime dateTime, Locality locality, TimeSpan talkTime) {

            if (money > 0) {
                Call call = new Call();
                call.Update(this, dateTime, locality, talkTime);

                calls.Add(call);
                money -= (float) (locality.GetPrice(dateTime) * talkTime.TotalMinutes);

                if(money < 0) {
                    call.paid = false;
                }

                return true;
            }

            return false;

        }

        public List<Call> GetCallsByMonth(DateTime dateTime) {
            List<Call> calls = new List<Call>();
            
            foreach(Call call in this.calls) {
               if(call.dateTime.Month == dateTime.Month && dateTime.Year == dateTime.Year) {
                    calls.Add(call);
                }
            }

            return calls;
        }

        public List<Call> GetCallsByMonthLocality(DateTime dateTime, Locality locality) {
            List<Call> calls = new List<Call>();

            foreach(Call call in GetCallsByMonth(dateTime)) {
                if(call.locality == locality) {
                    calls.Add(call);
                }
            }

            return calls;
        }

        public List<Call> GetCallsByMonthLocalityDay(DateTime dateTime, Locality locality) {
            List<Call> calls = new List<Call>();

            foreach (Call call in GetCallsByMonthLocality(dateTime, locality)) {
                if (call.dateTime.Day == dateTime.Day) {
                    calls.Add(call);
                }
            }

            return calls;
        }

        public List<Call> GetPaidCalls() {
            List<Call> callsResult = new List<Call>();

            foreach(Call call in calls) {
                if(call.paid) {
                    callsResult.Add(call);
                }
            }

            return callsResult;
        }

        public List<Call> GetNotPaidCalls() {
            List<Call> callsResult = new List<Call>();

            foreach(Call call in calls) {
                if(!call.paid) {
                    callsResult.Add(call);
                }
            }

            return callsResult;
        }

        public bool HasDebt() {
            return money < 0;
        }

        public override string ToString() {
            return fullName.ToString();
        }
    }
}
