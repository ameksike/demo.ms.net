using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * author		Antonio Membrides Espinosa
 * update       13/08/2019
 * version    	1.0
 */
namespace desktop.crud.sqlite.src.db
{
    class PersonModel : KsModel
    {
        public String fullname {
            get {
                return $"{firstname} {lastname}";
            }
        }
        public String firstname { get; set; }
        public String lastname { get; set; }
        public String age { get; set; }
        public String user { get; set; }
        public long dni { get; set; }
        public int id { get; set; }

        public PersonModel()
        {
            this.table = "person";
        }

        public PersonModel(int id, long dni, String firstname, String lastname, String age, String user)
        {
            this.table = "person";
            this.age = age;
            this.dni = dni;
            this.id = id;
            this.firstname = firstname;
            this.lastname = lastname;
            this.user = user;                 
        }
    }
}
