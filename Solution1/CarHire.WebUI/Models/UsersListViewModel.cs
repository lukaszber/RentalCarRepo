using CarHire.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarHire.WebUI.Models
{
    public class UsersListViewModel
    {
        public IEnumerable<User> Users { get; set; }
    }
}