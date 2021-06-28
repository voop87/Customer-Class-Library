using CustomerLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CustomerClassLibrary
{
    public class CustomerRepository : BaseRepository, IEntityRepository<Customer>
    {
        public int Create(Customer customer)
        {
            int customerId;

            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO [Customers] (FirstName, LastName, CustomerPhoneNumber, CustomerEmail, TotalPurchaseAmount) " +

                    "VALUES(@FirstName, @LastName, @CustomerPhoneNumber, @CustomerEmail, @TotalPurchaseAmount) " +
                    " SELECT CAST(scope_identity() AS int)", connection);

                var FirstNameParam = new SqlParameter("@FirstName", SqlDbType.NVarChar, 50)
                {
                    Value = customer.FirstName
                };
                var LastNameParam = new SqlParameter("@LastName", SqlDbType.NVarChar, 50)
                {
                    Value = customer.LastName
                };
                var PhoneNumberParam = new SqlParameter("@CustomerPhoneNumber", SqlDbType.NVarChar, 15)
                {
                    Value = customer.PhoneNumber
                };
                var EmailParam = new SqlParameter("@CustomerEmail", SqlDbType.NVarChar, 50)
                {
                    Value = customer.Email
                };
                var TotalPurchaseAmountParam = new SqlParameter("@TotalPurchaseAmount", SqlDbType.Money)
                {
                    Value = customer.TotalPurshasesAmount
                };

                command.Parameters.Add(FirstNameParam);
                command.Parameters.Add(LastNameParam);
                command.Parameters.Add(PhoneNumberParam);
                command.Parameters.Add(EmailParam);
                command.Parameters.Add(TotalPurchaseAmountParam);


                customerId = (Int32)command.ExecuteScalar();
            }

            return customerId;
        }

        public Customer Read(Customer customer)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM [Customers] WHERE CustomerId = @CustomerId", connection);

                var customerIdParam = new SqlParameter("@CustomerId", SqlDbType.Int)
                {
                    Value = customer.CustomerId
                };

                command.Parameters.Add(customerIdParam);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new Customer()
                        {
                            CustomerId = (Int32)reader["CustomerId"],
                            FirstName = reader["FirstName"]?.ToString(),
                            LastName = reader["LastName"]?.ToString(),
                            PhoneNumber = reader["CustomerPhoneNumber"]?.ToString(),
                            Email = reader["CustomerEmail"]?.ToString(),
                            TotalPurshasesAmount = (decimal)reader["TotalPurchaseAmount"]
                        };
                    }
                }
            }

            return null;
        }

        public List<Customer> ReadAll()
        {
            List<Customer> customers = new List<Customer>();

            using (var connection = GetConnection())
            {
                connection.Open();

                var command = new SqlCommand("SELECT * FROM [Customers]", connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customers.Add(new Customer()
                        {
                            CustomerId = (Int32)reader["CustomerId"],
                            FirstName = reader["FirstName"]?.ToString(),
                            LastName = reader["LastName"]?.ToString(),
                            PhoneNumber = reader["CustomerPhoneNumber"]?.ToString(),
                            Email = reader["CustomerEmail"]?.ToString(),
                            TotalPurshasesAmount = (decimal)reader["TotalPurchaseAmount"]
                        });
                    }
                }
            }
            return customers;
        }

        public void Update(Customer customer)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("UPDATE [Customers] SET FirstName=@FirstName, LastName=@LastName, CustomerPhoneNumber=@CustomerPhoneNumber, " +
                    "CustomerEmail=@CustomerEmail, TotalPurchaseAmount=@TotalPurchaseAmount" +
                    " WHERE CustomerId=@CustomerId", connection);

                var FirstNameParam = new SqlParameter("@FirstName", SqlDbType.NVarChar, 50)
                {
                    Value = customer.FirstName
                };
                var LastNameParam = new SqlParameter("@LastName", SqlDbType.NVarChar, 50)
                {
                    Value = customer.LastName
                };
                var PhoneNumberParam = new SqlParameter("@CustomerPhoneNumber", SqlDbType.NVarChar, 15)
                {
                    Value = customer.PhoneNumber
                };
                var EmailParam = new SqlParameter("@CustomerEmail", SqlDbType.NVarChar, 50)
                {
                    Value = customer.Email
                };
                var TotalPurchaseAmountParam = new SqlParameter("@TotalPurchaseAmount", SqlDbType.Money)
                {
                    Value = customer.TotalPurshasesAmount
                };

                command.Parameters.Add(FirstNameParam);
                command.Parameters.Add(LastNameParam);
                command.Parameters.Add(PhoneNumberParam);
                command.Parameters.Add(EmailParam);
                command.Parameters.Add(TotalPurchaseAmountParam);


                command.ExecuteNonQuery();
            }
        }

        public void Delete(Customer customer)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM [Customers] WHERE CustomerId = @CustomerId", connection);

                var CustomerIdParam = new SqlParameter("@CustomerId", SqlDbType.Int)
                {
                    Value = customer.CustomerId
                };
                command.Parameters.Add(CustomerIdParam);

                command.ExecuteNonQuery();
            }
        }

        public void DeleteAll()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM [Customers]", connection);

                command.ExecuteNonQuery();
            }
        }

        public List<Customer> ReadAll(int entityId)
        {
            throw new NotImplementedException();
        }
    }
}
