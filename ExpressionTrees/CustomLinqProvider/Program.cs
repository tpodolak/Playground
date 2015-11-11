using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomLinqProvider
{
    class Program
    {
        static void Main(string[] args)
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                Northwind db = new Northwind(con);

                IQueryable<Customers> query =
                    db.Customers.Where(c => c.City == "London" && c.Country == "UK");

                Console.WriteLine("Query:\n{0}\n", query);

                var list = query.ToList();
                foreach (var item in list)
                {
                    Console.WriteLine("Name: {0}", item.ContactName);
                }

                Console.ReadLine();
            }
        }
    }
}
