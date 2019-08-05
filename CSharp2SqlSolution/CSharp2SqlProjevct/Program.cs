using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CSharp2SqlProjevct {
    class Program {
        static void Main(string[] args) {
            var ord = GetOrderById(2038);
            if(ord == null) {
                Console.WriteLine("Order not found");
            } else {
                Console.WriteLine($"{ord.Id} | {ord.Date}");
            }

            var cust = GetCustomerById(1028);
            if(cust == null) {
                Console.WriteLine("Customer not found");
            } else {
                Console.WriteLine(cust.Name);
            }

            //var sql = "SELECT * from Orders;";
            //var orders = SelectOrder(sql);
            //foreach(var order in orders) {
            //    Console.WriteLine($"{order.Id} | {order.Date}");
            //}
            //sql = "SELECT * from Customers Where State = 'OH';";
            //var customers = SelectCustomer(sql);
            //foreach(var customer in customers) {
            //    Console.WriteLine(customer.Name);
            //}
        }

        static Order GetOrderById(int pid) {
            var connStr =
                @"server=localhost\sqlexpress;database=CustomerOrderDb;trusted_connection=true;";
            var connection = new SqlConnection(connStr);
            connection.Open();
            if(connection.State != System.Data.ConnectionState.Open) {
                throw new Exception("Connection did not open!");
            }
            var orderList = new List<Order>();
            var sql = "SELECT * from Orders where Id = @pid";
            var cmd = new SqlCommand(sql, connection);
            var parm = new SqlParameter("@pid", pid);
            cmd.Parameters.Add(parm);
            var reader = cmd.ExecuteReader();
            Order order = null;
            if(reader.Read()) {
                var id = (int)reader["Id"];
                var date = (DateTime)reader["Date"];
                var note = reader.IsDBNull(reader.GetOrdinal("Note"))
                    ? null : reader["Note"].ToString();
                int custId;
                if(reader.IsDBNull(reader.GetOrdinal("CustomerId"))) {
                    custId = 0;
                } else {
                    custId = (int)reader["CustomerId"];
                }
                order = new Order(id, date, note, custId);
            }
            reader.Close();

            connection.Close();
            return order;
        }

        static List<Order> SelectOrder(string sql) {
            var connStr =
                @"server=localhost\sqlexpress;database=CustomerOrderDb;trusted_connection=true;";
            var connection = new SqlConnection(connStr);
            connection.Open();
            if(connection.State != System.Data.ConnectionState.Open) {
                throw new Exception("Connection did not open!");
            }
            var orderList = new List<Order>();
            var cmd = new SqlCommand(sql, connection);
            var reader = cmd.ExecuteReader();
            while(reader.Read()) {
                var id = (int)reader["Id"];
                var date = (DateTime)reader["Date"];
                var note = reader.IsDBNull(reader.GetOrdinal("Note")) 
                    ? null : reader["Note"].ToString();
                int custId;
                if(reader.IsDBNull(reader.GetOrdinal("CustomerId"))) {
                    custId = 0;
                } else {
                    custId = (int)reader["CustomerId"];
                }
                var order = new Order(id, date, note, custId);
                orderList.Add(order);
            }
            reader.Close();

            connection.Close();
            return orderList;
        }
        static List<Customer> SelectCustomer(string sql) { 

            var connStr =
                @"server=localhost\sqlexpress;database=CustomerOrderDb;trusted_connection=true;";
            var connection = new SqlConnection(connStr);
            connection.Open();
            if(connection.State != System.Data.ConnectionState.Open) {
                throw new Exception("Connection did not open!");
            }
            var customerList = new List<Customer>();
            var cmd = new SqlCommand(sql, connection);
            var reader = cmd.ExecuteReader();
            while(reader.Read()) {
                var id = (int)reader["Id"];
                var name = reader["Name"].ToString();
                var city = reader["City"].ToString();
                var state = reader["State"].ToString();
                var active = (bool)reader["Active"];
                var code = reader.IsDBNull(reader.GetOrdinal("Code"))
                    ? null
                    : reader["Code"].ToString();
                var customer = new Customer(id, name, city, state, active, code);
                customerList.Add(customer);
            }
            reader.Close();

            connection.Close();
            return customerList;
        }
        static Customer GetCustomerById(int pid) {
            var connStr =
                @"server=localhost\sqlexpress;database=CustomerOrderDb;trusted_connection=true;";
            var connection = new SqlConnection(connStr);
            connection.Open();
            if(connection.State != System.Data.ConnectionState.Open) {
                throw new Exception("Connection did not open!");
            }
            var sql = "SELECT * From Customers Where Id = @myid;";
            var cmd = new SqlCommand(sql, connection);
            var theId = new SqlParameter("@myid", pid);
            cmd.Parameters.Add(theId);
            var reader = cmd.ExecuteReader();
            Customer cust = null; 
            if(reader.Read()) {
                var id = (int)reader["Id"];
                var name = reader["Name"].ToString();
                var city = reader["City"].ToString();
                var state = reader["State"].ToString();
                var active = (bool)reader["Active"];
                var code = reader.IsDBNull(reader.GetOrdinal("Code"))
                    ? null
                    : reader["Code"].ToString();
                cust = new Customer(id, name, city, state, active, code);
            }
            reader.Close();
            connection.Close();

            return cust;

        }
    }
}
