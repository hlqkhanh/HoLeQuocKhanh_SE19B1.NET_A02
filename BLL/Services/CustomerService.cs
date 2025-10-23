using BLL.IServices;
using BussinessObjects.Models;
using DAL.IRepositories;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository iCustomerRepository;

        public CustomerService()
        {
            iCustomerRepository = new CustomerRepository();
        }

        public void AddCustomer(Customer customer)
        {
            iCustomerRepository.AddCustomer(customer);
        }

        public Customer? CheckLogin(string email, string password)
        {
            return iCustomerRepository.CheckLogin(email, password);
        }

        public void DeleteCustomer(int customerID)
        {
            iCustomerRepository.DeleteCustomer(customerID);
        }

        public List<Customer> GetAllCustomer()
        {
            return iCustomerRepository.GetAllCustomer();
        }

        public void UpdateCustomer(Customer customer)
        {
            iCustomerRepository.UpdateCustomer(customer);
        }
    }
}
