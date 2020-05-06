using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurs4.model {

    public class BaseSystem {

        public static DateTime currentDateTime = DateTime.Now;

        public List<Client> clients = new List<Client>();
        public List<Locality> localities = new List<Locality>();
        
        public BaseSystem() {
        }

        public int GetCallsByMonthAndLocality(Client client, Locality locality) {
            return 0;
        }

        public bool RegisterClient(Client.FullName fullName, string telephoneNumber, string address) {
            if(fullName.name != "" && fullName.surname != "" && fullName.patronymic != "" &&
                telephoneNumber != "" && address != "" && GetClientByNumber(telephoneNumber) == null) {

                Client client = new Client();
                client.Update(fullName, telephoneNumber, address);
                clients.Add(client);

                return true;
            }

            return false;
        }

        public bool RegisterLocality(string localityName) {
            if(localityName != "" && GetLocalityByName(localityName) == null) {
                localities.Add(new Locality(localityName));
                return true;
            }

            return false;
        }

        public Locality GetLocalityByName(string localityName) {
            Locality localityResult = null;

            foreach(Locality locality in localities) {
                if(locality.localityName == localityName) {
                    localityResult = locality;
                    break;
                }
            }

            return localityResult;
        }

        public Client GetClientByNumber(string number) {
            Client clientResult = null;

            foreach(Client client in clients) {
                if(client.telephoneNumber == number) {
                    clientResult = client;
                    break;
                }
            }

            return clientResult;
        }

        public List<Client> GetDebtClients() {
            List<Client> clients = new List<Client>();

            foreach(Client client in this.clients) {
                if(client.HasDebt()) {
                    clients.Add(client);
                }
            }

            return clients;
        }

        public int GetCountClientsByMonthLocality(DateTime dateTime, Locality locality) {
            int count = 0;

            foreach(Client client in clients) {
                if(client.GetCallsByMonthLocality(dateTime, locality).Count > 0) {
                    count++;
                }
            }

            return count;
        }

        public int GetCountClientsByMonthLocalityDay(DateTime dateTime, Locality locality) {
            int count = 0;

            foreach (Client client in clients) {
                if (client.GetCallsByMonthLocalityDay(dateTime, locality).Count > 0) {
                    count++;
                }
            }

            return count;
        }
    }
}
