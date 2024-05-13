using AdaptiveWebInterfaces_WebAPI.Models;

namespace AdaptiveWebInterfaces_WebAPI.Services.Car
{
    public class CarService : ICarService
    {
        private readonly List<CarModel> _cars;

        public CarService()
        {
            _cars = new List<CarModel>
        {
            new CarModel { CarId = 1, Name = "Toyota Corolla", Country = "Japan" },
            new CarModel { CarId = 2, Name = "Honda Civic", Country = "Japan" }
        };
        }

        public async Task<CarModel> GetCarAsync(int carId)
        {
            return await Task.FromResult(_cars.FirstOrDefault(c => c.CarId == carId));
        }

        public async Task<IEnumerable<CarModel>> GetAllCarsAsync()
        {
            return await Task.FromResult(_cars);
        }

        public async Task<CarModel> CreateCarAsync(CarModel car)
        {
            if (_cars.Any(c => c.CarId == car.CarId))
            {
                throw new Exception("Car with the same code already exists.");
            }

            _cars.Add(car);
            return await Task.FromResult(car);
        }

        public async Task<CarModel> UpdateCarAsync(int carId, CarModel car)
        {
            var existingCar = _cars.FirstOrDefault(c => c.CarId == carId);
            if (existingCar == null)
            {
                throw new Exception("Car not found.");
            }

            existingCar.Name = car.Name;
            existingCar.Country = car.Country;

            return await Task.FromResult(existingCar);
        }

        public async Task<bool> DeleteCarAsync(int carId)
        {
            var existingCar = _cars.FirstOrDefault(c => c.CarId == carId);
            if (existingCar == null)
            {
                throw new Exception("Car not found.");
            }

            _cars.Remove(existingCar);
            return await Task.FromResult(true);
        }
    }
}
