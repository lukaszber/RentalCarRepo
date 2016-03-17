using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarHire.Domain.Entities;
namespace CarHire.Domain.Abstract
{
    public interface IRentalRepository
    {
        IEnumerable<Rental> Rent { get; }
        void RentCar(Rental rent,int userId);
        Rental DeleteRent(int rentalID);
        void RentalExtension(Rental rent);
        void Return(Rental rent);
        void RentalApproval(Rental rent);
        void Hired(Rental rent);
        void ExtensionApproval(Rental rent);
    }
}
