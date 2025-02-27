using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp18
{
    public class Organization : PhoneBookEntry
    {
        public string OrganizationName { get; set; }
        public string Fax { get; set; }
        public string ContactPerson { get; set; }

        public Organization(
            string organizationName,
            string address,
            string phoneNumber,
            string fax,
            string contactPerson)
            : base(address, phoneNumber)
        {
            OrganizationName = organizationName;
            Fax = fax;
            ContactPerson = contactPerson;
        }

        public override String ToString()
        {
            return("Тип: Организация"+$"\nНазвание: {OrganizationName}"+$"\nАдрес: {Address}"+$"\nТелефон: {PhoneNumber}"+$"\nФакс: {Fax}"+$"\nКонтактное лицо: {ContactPerson}"+'\n'+new string('-', 30));
        }

        public override bool MatchesCriteria(string lastName)
        {
            return ContactPerson.Equals(ContactPerson, StringComparison.OrdinalIgnoreCase);
        }
    }
}
