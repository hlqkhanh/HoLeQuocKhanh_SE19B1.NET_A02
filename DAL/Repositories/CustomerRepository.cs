using BussinessObjects.Models;
using DAL.DAO;
using DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public void AddCustomer(Customer customer)
        {
            CustomerDAO.Add(customer);
        }

        public Customer? CheckLogin(string email, string password)
        {
            return CustomerDAO.CheckLogin(email, password);
        }


        public void DeleteCustomer(int customerID)
        {
            CustomerDAO.Delete(customerID);
        }

        public List<Customer> GetAllCustomer()
        {
            return CustomerDAO.GetAll();
        }

        public void UpdateCustomer(Customer customer)
        {
            CustomerDAO.Update(customer);
        }
    }
}
