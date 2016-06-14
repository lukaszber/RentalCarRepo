using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarHire.Domain.Entities;
using CarHire.Domain.Abstract;
using System.Data.SqlClient;

namespace CarHire.Domain.Concrete
{
    public class EFUserRepository: IUserRepository
    {
        private EfDbContext context = new EfDbContext();

        public IEnumerable<User> Users
        {
            get { return context.Users; }
        }
        public void SaveUser(User user)
        {
            if (user.UserID == 0)
            {
                context.Users.Add(user);
            }
            else
            {
                User dbEntry = context.Users.Find(user.UserID);
                if (dbEntry != null)
                {
                    dbEntry.Name = user.Name;
                    dbEntry.Surname = user.Surname;
                    dbEntry.Username = user.Username;
                    dbEntry.Password = user.Password;
                    dbEntry.Phone = user.Phone;
                    dbEntry.Mail = user.Mail;
                    dbEntry.Category = user.Category;
                    dbEntry.Adress = user.Adress;
                    dbEntry.Pesel = user.Pesel;
                }
            }
            try
            {
                context.SaveChanges();
            }
            catch (SqlException sqlExc)
            {
                foreach (SqlError error in sqlExc.Errors)
                {
                    string msg = string.Format("{0}: {1}", error.Number, error.Message);
                }
            }
            
        }
        public void NewUser(User user, string category)
        {
            if (user.UserID == 0)
            {
                user.Category = category;
                context.Users.Add(user);
            }
            context.SaveChanges();
        }
        public User FindUser(int userID)
        {
            User dbEntry = context.Users.Find(userID);
            return dbEntry;
        }
        public User DeleteUser(int userID)
        {
            User dbEntry = context.Users.Find(userID);
            if (dbEntry != null)
            {
                context.Users.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
