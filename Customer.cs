using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app
{
   
        public class Customer
        {
            public String FirstName { get; set; }
            public String LastName { get; set; }
            public String Address { get; set; }

            public Customer(String firstName, String lastName,
                String address)
            {
                this.FirstName = firstName;
                this.LastName = lastName;
                this.Address = address;
            }

            public static List<Customer> Customers()
            {
                return new List<Customer>(new Customer[4] {
            new Customer("A.", "Zero",
                "12 North Third Street, Apartment 45"
                ),
            new Customer("B.", "One",
                "34 West Fifth Street, Apartment 67"
                ),
            new Customer("C.", "Two",
                "56 East Seventh Street, Apartment 89"
                ),
            new Customer("D.", "Three",
                "78 South Ninth Street, Apartment 10")
        });
            }
        }
    
}
