using System.Data.Common;

namespace CustomLinqProvider
{
    public class Northwind
    {
        public Query<Customers> Customers;
        public Query<Orders> Orders;

        public Northwind(DbConnection connection)
        {
            QueryProvider provider = new DbQueryProvider(connection);
            this.Customers = new Query<Customers>(provider);
            this.Orders = new Query<Orders>(provider);
        }
    }
}