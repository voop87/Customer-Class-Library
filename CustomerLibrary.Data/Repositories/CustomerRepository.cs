using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CustomerClassLibrary
{
    public class CustomerRepository : BaseRepository
    {
        public void Create(Customer customer)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO [Customers] (FirstName, LastName, CustomerPhoneNumber, CustomerEmail, TotalPurchaseAmount) " +

                    "VALUES(@FirstName, @LastName, @CustomerPhoneNumber, @CustomerEmail, @TotalPurchaseAmount)", connection);

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

        public Customer Read(string customerLastName)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM [Customers] WHERE LastName = @LastName", connection);

                var LastNameParam = new SqlParameter("@LastName", SqlDbType.NVarChar, 50)
                {
                    Value = customerLastName
                };

                command.Parameters.Add(LastNameParam);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new Customer()
                        {
                            FirstName = reader["FirstName"]?.ToString()
                        };
                    }
                }
            }

            return null;
        }

        public void Update(Customer customer)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("UPDATE [Customers] SET FirstName = @FirstName " +
                    "WHERE LastName = @LastName", connection);

                var FirstNameParam = new SqlParameter("@FirstName", SqlDbType.NVarChar, 50)
                {
                    Value = customer.FirstName
                };
                var LastNameParam = new SqlParameter("@LastName", SqlDbType.NVarChar, 50)
                {
                    Value = customer.LastName
                };

                command.Parameters.Add(FirstNameParam);
                command.Parameters.Add(LastNameParam);


                command.ExecuteNonQuery();
            }
        }

        public void Delete(string customerLastName)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM [Customers] WHERE LastName = @LastName", connection);

                var LastNameParam = new SqlParameter("@LastName", SqlDbType.NVarChar, 50)
                {
                    Value = customerLastName
                };
                command.Parameters.Add(LastNameParam);

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
    }
}
