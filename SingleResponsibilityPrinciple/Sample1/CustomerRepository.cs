using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple
{
    public class CustomerRepository
    {
        public void Add(Customer customer)
        {

            try
            {
               //Add Customer To DB
            }
            catch (Exception e)
            {
                SystemLog.Log(e.Message);
            }

        }

    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
    }
}
