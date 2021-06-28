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
    public class AddressRepository : BaseRepository, IEntityRepository<Address>
    {
        public int Create(Address address)
        {
            int addressId;
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO [Addresses] (AddressLine, AddressLine2, AddressType, City, PostalCode, [state], Country, CustomerId) " +

                    "VALUES(@AddressLine, @AddressLine2, @AddressType, @City, @PostalCode, @state, @Country, CustomerId) " +
                    "SELECT CAST(scope_identity() AS int)", connection);

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
                var CustomerIdParam = new SqlParameter("@CustomerId", SqlDbType.Int)
                {
                    Value = address.CustomerId
                };

                command.Parameters.Add(AddressLineParam);
                command.Parameters.Add(AddressLine2Param);
                command.Parameters.Add(CityParam);
                command.Parameters.Add(PostalCodeParam);
                command.Parameters.Add(StateParam);
                command.Parameters.Add(AddressTypeParam);
                command.Parameters.Add(CountryParam);
                command.Parameters.Add(CustomerIdParam);


                addressId = (Int32)command.ExecuteScalar();
            }

            return addressId;
        }

        public Address Read(Address address)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM [Addresses] WHERE AddressId = @AddressId", connection);

                var AddressIdParam = new SqlParameter("@AddressId", SqlDbType.Int)
                {
                    Value = address.AddressId
                };

                command.Parameters.Add(AddressIdParam);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var addressType = reader["AddressType"]?.ToString();

                        if (addressType is not null)
                        {
                            return new Address()
                            {
                                CustomerId = (Int32)reader["CustomerId"],
                                AdressLine = reader["AdressLine"].ToString(),
                                AdressLine2 = reader["AdressLine2"].ToString(),
                                AddressType = (AddressType)Enum.Parse(typeof(AddressType), addressType),
                                City = reader["City"].ToString(),
                                PostalCode = reader["PostalCode"].ToString(),
                                State = reader["state"].ToString(),
                                Country = reader["Country"].ToString()
                            };
                        }
                    }
                }
            }

            return null;
        }

        public List<Address> ReadAll(int addressId)
        {
            List<Address> foundAddresses = new List<Address>();

            using (var connection = GetConnection())
            {
                connection.Open();

                var command = new SqlCommand("SELECT * FROM [Addresses] WHERE AddressId = @AddressId", connection);

                var addressIdParam = new SqlParameter("@AddressId", SqlDbType.Int)
                {
                    Value = addressId
                };

                command.Parameters.Add(addressIdParam);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var addressType = reader["AddressType"]?.ToString();

                        if (addressType is not null)
                        {
                            foundAddresses.Add(new Address()
                            {
                                CustomerId = (Int32)reader["CustomerId"],
                                AdressLine = reader["AdressLine"].ToString(),
                                AdressLine2 = reader["AdressLine2"].ToString(),
                                AddressType = (AddressType)Enum.Parse(typeof(AddressType), addressType),
                                City = reader["City"].ToString(),
                                PostalCode = reader["PostalCode"].ToString(),
                                State = reader["state"].ToString(),
                                Country = reader["Country"].ToString()
                            });
                        }


                    }
                }
            }

            return foundAddresses;
        }

        public void Update(Address address)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("UPDATE [Addresses] AdressLine = @AdressLine, AdressLine2 = @AdressLine2, AddressType = @AddressType, " +
                    "City = @City, PostalCode = @PostalCode, [state] = @state, Country = @Country " +
                    "WHERE AddressId = @AddressId", connection);

                command.Parameters.Add(new SqlParameter("@AdressLine", SqlDbType.NVarChar, 100) { Value = address.AdressLine });
                command.Parameters.Add(new SqlParameter("@AdressLine2", SqlDbType.NVarChar, 100) { Value = address.AdressLine2 });
                command.Parameters.Add(new SqlParameter("@AddressType", SqlDbType.VarChar, 8) { Value = address.AddressType.ToString() });
                command.Parameters.Add(new SqlParameter("@City", SqlDbType.NVarChar, 50) { Value = address.City });
                command.Parameters.Add(new SqlParameter("@PostalCode", SqlDbType.VarChar, 6) { Value = address.PostalCode });
                command.Parameters.Add(new SqlParameter("@state", SqlDbType.NVarChar, 20) { Value = address.State });
                command.Parameters.Add(new SqlParameter("@Country", SqlDbType.NVarChar, 255) { Value = address.Country });
                command.Parameters.Add(new SqlParameter("@AddressId", SqlDbType.Int) { Value = address.AddressId });

                command.ExecuteNonQuery();
            }
        }

        public void Delete(Address address)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM [Addresses] WHERE AddressId = @AddressId", connection);

                var AddressIdParam = new SqlParameter("@AddressId", SqlDbType.Int)
                {
                    Value = address.AddressId
                };
                command.Parameters.Add(AddressIdParam);

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

        public List<Address> ReadAll()
        {
            throw new NotImplementedException();
        }
    }
}
