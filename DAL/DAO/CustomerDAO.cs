using BussinessObjects.Models;
using Microsoft.EntityFrameworkCore; 
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.DAO { 
    public class CustomerDAO
    {

        public static List<Customer> GetAll()
        {
            List<Customer> list = new List<Customer>();
            try
            {
                using var context = new FuminiHotelManagementContext(); 
                list = context.Customers.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy tất cả khách hàng: {ex.Message}");
            }
            return list;
        }

        public static void Add(Customer customer)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                if (context.Customers.Any(c => c.EmailAddress == customer.EmailAddress))
                {
                    throw new Exception($"Email '{customer.EmailAddress}' đã tồn tại.");
                }
                context.Customers.Add(customer);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm khách hàng: {ex.Message}");
            }
        }

        public static void Update(Customer customer)
        {
            try
            {
                using var context = new FuminiHotelManagementContext(); 
                context.Entry(customer).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex) 
            {
                throw new Exception($"Lỗi tương tranh khi cập nhật khách hàng ID {customer.CustomerId}. Dữ liệu có thể đã bị thay đổi. {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật khách hàng ID {customer.CustomerId}: {ex.Message}");
            }
        }

        public static void Delete(int customerId) 
        {
            try
            {
                using var context = new FuminiHotelManagementContext(); 
                var customerToDelete = context.Customers.Find(customerId);
                if (customerToDelete != null)
                {
                    bool hasBookings = context.BookingReservations.Any(br => br.CustomerId == customerId);
                    if (hasBookings)
                    {
                        throw new Exception($"Không thể xóa khách hàng ID {customerId} vì có lịch sử đặt phòng.");
                    }

                    context.Customers.Remove(customerToDelete);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception($"Không tìm thấy khách hàng với ID {customerId} để xóa.");
                }
            }
            catch (DbUpdateException ex) 
            {
                throw new Exception($"Lỗi khi xóa khách hàng ID {customerId} (có thể do ràng buộc khóa ngoại): {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa khách hàng ID {customerId}: {ex.Message}");
            }
        }

        public static Customer? CheckLogin(string email, string password)
        {
            Customer? customer = null;
            try
            {
                using var context = new FuminiHotelManagementContext(); 
                customer = context.Customers.FirstOrDefault(c => c.EmailAddress == email && c.Password == password && c.CustomerStatus == 1); // Chỉ cho phép user active đăng nhập
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi kiểm tra đăng nhập: {ex.Message}");
            }
            return customer;
        }

        public static Customer? GetCustomer(int customerID)
        {
            Customer? customer = null;
            try
            {
                using var context = new FuminiHotelManagementContext(); 
                customer = context.Customers.Find(customerID);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy khách hàng theo ID {customerID}: {ex.Message}");
            }
            return customer;
        }

        public static Customer? GetCustomerByEmail(string email)
        {
            Customer? customer = null;
            try
            {
                using var context = new FuminiHotelManagementContext();
                customer = context.Customers.FirstOrDefault(c => c.EmailAddress == email);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy khách hàng theo Email '{email}': {ex.Message}");
            }
            return customer;
        }

        public static List<Customer> SearchCustomerByName(string name)
        {
            List<Customer> list = new List<Customer>();
            try
            {
                using var context = new FuminiHotelManagementContext();
                list = context.Customers
                               .Where(c => c.CustomerFullName.Contains(name))
                               .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi tìm khách hàng theo tên '{name}': {ex.Message}");
            }
            return list;
        }
    }
}