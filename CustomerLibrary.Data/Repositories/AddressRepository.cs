using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerClassLibrary
{
    public class AddressRepository : BaseRepository
    {
        public void Create(Address address)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO [Addresses] (AddressLine, AddressLine2, AddressType, City, PostalCode, [state], Country, CustomerId) " +

                    "VALUES(@AddressLine, @AddressLine2, @AddressType, @City, @PostalCode, @state, @Country, " +
                    "(SELECT TOP 1 Customers.CustomerId FROM Customers))", connection);

                var AddressLineParam = new SqlParameter("@AddressLine", SqlDbType.NVarChar, 100)
                {
                    Value = address.AdressLine
                };
                var AddressLine2Param = new SqlParameter("@AddressLine2", SqlDbType.NVarChar, 100)
                {
                    Value = address.AdressLine2
                };
                var CityParam = new SqlParameter("@City", SqlDbType.NVarChar, 50)
                {
                    Value = address.City
                };
                var PostalCodeParam = new SqlParameter("@PostalCode", SqlDbType.NVarChar, 6)
                {
                    Value = address.PostalCode
                };
                var StateParam = new SqlParameter("@state", SqlDbType.NVarChar, 20)
                {
                    Value = address.State
                };
                var AddressTypeParam = new SqlParameter("@AddressType", SqlDbType.NVarChar, 8)
                {
                    Value = address.AddressType
                };
                var CountryParam = new SqlParameter("@Country", SqlDbType.NVarChar, 30)
                {
                    Value = address.Country
                };

                command.Parameters.Add(AddressLineParam);
                command.Parameters.Add(AddressLine2Param);
                command.Parameters.Add(CityParam);
                command.Parameters.Add(PostalCodeParam);
                command.Parameters.Add(StateParam);
                command.Parameters.Add(AddressTypeParam);
                command.Parameters.Add(CountryParam);


                command.ExecuteNonQuery();
            }

        }

        public Address Read(string addressState)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM [Addresses] WHERE [state] = @state", connection);

                var StateParam = new SqlParameter("@state", SqlDbType.NVarChar, 20)
                {
                    Value = addressState
                };

                command.Parameters.Add(StateParam);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new Address()
                        {
                            City = reader["City"]?.ToString(),
                            State=reader["state"]?.ToString()
                        };
                    }
                }
            }

            return null;
        }

        public void Update(Address address)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("UPDATE [Addresses] SET City = @City " +
                    "WHERE [state] = @state", connection);

                var CityParam = new SqlParameter("@City", SqlDbType.NVarChar, 50)
                {
                    Value = address.City
                };
                var StateParam = new SqlParameter("@state", SqlDbType.NVarChar, 20)
                {
                    Value = address.State
                };

                command.Parameters.Add(CityParam);
                command.Parameters.Add(StateParam);

                command.ExecuteNonQuery();
            }
        }

        public void Delete(string addressState)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM [Addresses] WHERE [state] = @state", connection);

                var StateParam = new SqlParameter("@state", SqlDbType.NVarChar, 20)
                {
                    Value = addressState
                };
                command.Parameters.Add(StateParam);

                command.ExecuteNonQuery();
            }
        }

        public void DeleteAll()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM [Addresses]", connection);

                command.ExecuteNonQuery();
            }
        }
    }
}
