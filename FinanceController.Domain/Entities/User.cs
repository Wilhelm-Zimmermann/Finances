﻿namespace FinanceController.Domain.Entities
{
    public class User : Base
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Role { get; private set; }
        public IList<Bill> Bills { get; private set; }

        public User(string name, string email, string password, string role)
        {
            Name = name;
            Email = email;
            Password = password;
            Role = role;
        }

        public void AddBill(Bill bill)
        {
            Bills.Add(bill);
        }

        public void RemoveBill(Bill bill) {  
            Bills.Remove(bill); 
        }
    }
}
