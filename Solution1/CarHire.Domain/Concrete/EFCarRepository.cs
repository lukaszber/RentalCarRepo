using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarHire.Domain.Abstract;
using CarHire.Domain.Entities;
namespace CarHire.Domain.Concrete
{
    public class EFCarRepository : ICarRepository 
    {
        private EfDbContext context = new EfDbContext();

        public IEnumerable<Car> Cars
        {
            get { return context.Cars; }
        }
        public void SaveCar(Car car)
        {
            if (car.CarID == 0)
            {
                context.Cars.Add(car);
            }
            else
            {
                var dbEntry = context.Cars.Find(car.CarID);
                if (dbEntry != null)
                {
                    dbEntry.Brand = car.Brand;
                    dbEntry.Model = car.Model;
                    dbEntry.Capacity = car.Capacity;
                    dbEntry.Mileage = car.Mileage;
                    dbEntry.PricePerDay = car.PricePerDay;
                    dbEntry.RegistrationNumber = car.RegistrationNumber;
                    dbEntry.Hired = car.Hired;
                    dbEntry.Year = car.Year;
                    dbEntry.ImageData = car.ImageData;
                    dbEntry.ImageMimeType = car.ImageMimeType;
                }
            }
            context.SaveChanges();
        }
        public Car DeleteCar(int CarID)
        {
            Car dbEntry = context.Cars.Find(CarID);

            if (dbEntry != null)
            {
                context.Cars.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
        public void HireCar(int CarID)
        {
            Car dbEntry = context.Cars.Find(CarID);
            dbEntry.Hired = true;
            context.SaveChanges();
        }
        public void ReturnCar(int CarID)
        {
            Car dbEntry = context.Cars.Find(CarID);
            dbEntry.Hired = false;
            context.SaveChanges();
        }
    }
}
