using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogCore.Domain.DomainServices.Dto
{
    public class UserStore
    {
        private static List<User> _users = new List<User>() {
            new User {  Id=1, Name="alice", Password="alice", Email="alice@gmail.com", PhoneNumber="18800000001", Birthday=DateTime.Now },
            new User {  Id=1, Name="bob", Password="bob", Email="bob@gmail.com", PhoneNumber="18800000002", Birthday=DateTime.Now.AddDays(1)}
        };

        public User FindUser(string userName, string password)
        {
            return _users.FirstOrDefault(_ => _.Name == userName && _.Password == password);
        }
    }

    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public DateTime Birthday { get; set; }
    }
}
