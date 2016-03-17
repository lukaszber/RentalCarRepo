using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarHire.Domain.Abstract;
using CarHire.Domain.Entities;

namespace CarHire.Domain.Concrete
{
    public class EFRentalRepository : IRentalRepository
    {
        private EfDbContext context = new EfDbContext();

        public IEnumerable<Rental> Rent
        {
            get { return context.Rent; }
        }
        public void RentCar(Rental rent, int userId)
        {
            if (rent.RentalId == 0)
            {
                rent.UserId = userId;
                rent.Extension = rent.ReturnDate;
                rent.Status = "Oczekujacy";
                context.Rent.Add(rent);
            }
            else
            {
                Rental dbEntry = context.Rent.Find(rent.RentalId);
                if (dbEntry != null)
                {
                    dbEntry.CarId = rent.CarId;
                    dbEntry.UserId = rent.UserId;
                    dbEntry.RentalDate = rent.RentalDate;
                    dbEntry.ReturnDate = rent.ReturnDate;
                    dbEntry.Extension = rent.Extension;
                    dbEntry.Status = rent.Status;
                }

            }
            context.SaveChanges();
        }
        public Rental DeleteRent(int rentalID)
        {
            Rental dbEntry = context.Rent.Find(rentalID);

            if (dbEntry != null)
            {
                context.Rent.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public void RentalExtension(Rental rent)
        {
            Rental dbEntry = context.Rent.Find(rent.RentalId);
            dbEntry.CarId = rent.CarId;
            dbEntry.UserId = rent.UserId;
            dbEntry.Extension = rent.Extension;
            dbEntry.Status = "Oczekujacy";
            context.SaveChanges();
        }
        public void RentalApproval(Rental rent)
        {
            Rental dbEntry = context.Rent.Find(rent.RentalId);
            dbEntry.CarId = rent.CarId;
            dbEntry.UserId = rent.UserId;
            dbEntry.Status = "Wypozyczony";
            context.SaveChanges();
        }
        public void ExtensionApproval(Rental rent)
        {
            Rental dbEntry = context.Rent.Find(rent.RentalId);
            dbEntry.CarId = rent.CarId;
            dbEntry.UserId = rent.UserId;
            dbEntry.ReturnDate = rent.Extension;
            dbEntry.Status = "Wypozyczony";
            context.SaveChanges();

        }
        public void Hired(Rental rent)
        {
            Rental dbEntry = context.Rent.Find(rent.RentalId);
            dbEntry.CarId = rent.CarId;
            dbEntry.UserId = rent.UserId;
            dbEntry.Status = "Wypozyczony";
            context.SaveChanges();
        }
        public void Return(Rental rent)
        {
            Rental dbEntry = context.Rent.Find(rent.RentalId);
            dbEntry.CarId = rent.CarId;
            dbEntry.UserId = rent.UserId;
            dbEntry.Status = "Oddany";
            context.SaveChanges();
        }

    }
    }


