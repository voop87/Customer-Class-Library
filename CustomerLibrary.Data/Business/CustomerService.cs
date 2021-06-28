using CustomerClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CustomerLibrary.Data
{
    public class CustomerService
    {
        private readonly IEntityRepository<Customer> _customerRepository;
        private readonly IEntityRepository<Address> _addressRepository;
        private readonly IEntityRepository<Note> _noteRepository;

        public CustomerService()
        {
            _customerRepository = new CustomerRepository();
            _addressRepository = new AddressRepository();
            _noteRepository = new NotesRepository();
        }
        public CustomerService(IEntityRepository<Customer> customerRepository, IEntityRepository<Address> addressRepository,
            IEntityRepository<Note> customerNoteRepository)
        {
            _customerRepository = customerRepository;

            _addressRepository = addressRepository;

            _noteRepository = customerNoteRepository;
        }

        public int CreateCustomer(Customer customer)
        {
            int customerId;

            using (var transactionScope = new TransactionScope())
            {
                customerId = _customerRepository.Create(customer);

                foreach (var address in customer.AdressesList)
                {
                    address.CustomerId = customerId;
                    _addressRepository.Create(address);
                }

                foreach (var note in customer.Notes)
                {
                    note.CustomerId = customerId;
                    _noteRepository.Create(note);
                }

                transactionScope.Complete();

            }

            return customerId;
        }

        public Customer GetCustomer(Customer customer)
        {
            var readedCustomer = _customerRepository.Read(customer);
            readedCustomer.AdressesList = _addressRepository.ReadAll(customer.CustomerId);
            readedCustomer.Notes = _noteRepository.ReadAll(customer.CustomerId);
            return readedCustomer;

        }

        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = _customerRepository.ReadAll();

            foreach (var customer in customers)
            {
                customer.AdressesList = _addressRepository.ReadAll(customer.CustomerId);
                customer.Notes = _noteRepository.ReadAll(customer.CustomerId);
            }

            return customers;
        }

        public void ChangeCustomer(Customer customer)
        {
            using (var transactionScope = new TransactionScope())
            {
                _customerRepository.Update(customer);

                foreach (var address in customer.AdressesList)
                {
                    _addressRepository.Update(address);
                }

                foreach (var note in customer.Notes)
                {
                    _noteRepository.Update(note);
                }

                transactionScope.Complete();
            }

        }
    }
}
