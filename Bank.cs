using System.Collections.Generic;

namespace FirstBankOfSuncoast
{
    public class Bank
    {
        public List<User> Users { get; set; } = new List<User>();

        public void CreateUser(string userName, string password)
        {
            var newUser = new User()
            {
                UserName = userName,
                Password = password
            };

            Users.Add(newUser);
        }
    }
}