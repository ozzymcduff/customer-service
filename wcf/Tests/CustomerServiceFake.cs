﻿using System;
using Customers;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Tests
{
    public class CustomerServiceFake : ICustomerService
    {
        public CustomerServiceFake ()
        {
            AllCustomers = new List<Customer>();
        }
        public List<Customer> AllCustomers;
        public ArrayOfCustomer GetAllCustomers()
        {
            return new ArrayOfCustomer
            {
                Customer = AllCustomers.ToArray()
            };
        }

        public Customer GetCustomerByAccountNumber(int accountNumber)
        {
            return AllCustomers
                .SingleOrDefault(c => c.AccountNumber == accountNumber);
        }

        public ArrayOfCustomer GetCustomers(string lastName)
        {
            return new ArrayOfCustomer
            { 
                Customer= AllCustomers
                    .Where(c => c.LastName == lastName).ToArray()
            };
        }

        public bool SaveCustomer(Customer editedCustomer)
        {
            var customer = AllCustomers
                .SingleOrDefault(c => c.AccountNumber == editedCustomer.AccountNumber);
            if (customer != null)
            {
                var properties = typeof(Customer).GetProperties(
                    BindingFlags.Instance | BindingFlags.Public);
                foreach (var property in properties)
                {
                    property.SetValue(customer, 
                        property.GetValue(editedCustomer));
                }
                return true;
            }
            return false;
        }

        public bool SaveCustomerLastName(string accountNumber, string newName)
        {
            var number = Int32.Parse(accountNumber);
            var customer = AllCustomers
                .SingleOrDefault(c => c.AccountNumber == number);
            if (customer != null)
            {
                customer.LastName = newName;
                return true;
            }
            return false;
        }

        public Task<ArrayOfCustomer> GetCustomersAsync(string lastName) => Task.FromResult(GetCustomers(lastName));

        public Task<Customer> GetCustomerByAccountNumberAsync(int accountNumber) => Task.FromResult(GetCustomerByAccountNumber(accountNumber));

        public Task<ArrayOfCustomer> GetAllCustomersAsync() => Task.FromResult(GetAllCustomers());

        public Task<bool> SaveCustomerAsync(Customer editedCustomer) => Task.FromResult(SaveCustomer(editedCustomer));

        public Task<bool> SaveCustomerLastNameAsync(string accountNumber, string newName) => Task.FromResult(SaveCustomerLastName(accountNumber, newName));
    }
}

