using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarHire.Domain.Entities;

namespace CarHire.Domain.Abstract
{
    public interface ICarRepository
    {
        IEnumerable<Car> Cars { get; }
        void SaveCar(Car car);
        void HireCar(int car);
        void ReturnCar(int car);
        Car DeleteCar(int carID);
    }
}

