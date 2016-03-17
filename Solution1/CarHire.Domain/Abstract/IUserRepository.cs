using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarHire.Domain.Entities;

namespace CarHire.Domain.Abstract
{
    public interface IUserRepository
    {
        IEnumerable<User> Users { get; }
        void SaveUser(User user);
        User DeleteUser(int userID);
        User FindUser(int userID);
        void NewUser(User user, string category);

    }
}
