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
                new CarModel { CarId = 2, Name = "Honda Civic", Country = "Japan" },
                new CarModel { CarId = 3, Name = "BMW X5", Country = "Germany" },
                new CarModel { CarId = 4, Name = "Mersedes Vivo", Country = "Germany" },
                new CarModel { CarId = 5, Name = "Renault Kangoo", Country = "France" },
                new CarModel { CarId = 6, Name = "Shkoda Octavia", Country = "Chezh" },
                new CarModel { CarId = 7, Name = "Opel Astra", Country = "Germany" },
                new CarModel { CarId = 8, Name = "Opel Kadet", Country = "Germany" },
                new CarModel { CarId = 9, Name = "Toyota Land Cruiser 200", Country = "Japan" },
                new CarModel { CarId = 10, Name = "Lexus Ls", Country = "Japan" },
                new CarModel { CarId = 11, Name = "Mazda 6", Country = "Japan" },
                new CarModel { CarId = 12, Name = "Audi A6", Country = "Germany" }
            };
        }

        public async Task<ResponseModel<CarModel>> GetCarAsync(int carId)
        {
            var car = _cars.FirstOrDefault(c => c.CarId == carId);
            if (car != null)
            {
                return new ResponseModel<CarModel>
                {
                    Data = car,
                    Success = true,
                    Message = "Car found."
                };
            }
            else
            {
                return new ResponseModel<CarModel>
                {
                    Data = null,
                    Success = false,
                    Message = "Car not found."
                };
            }
        }

        public async Task<ResponseModel<IEnumerable<CarModel>>> GetAllCarsAsync()
        {
            return new ResponseModel<IEnumerable<CarModel>>
            {
                Data = _cars,
                Success = true,
                Message = "All cars retrieved."
            };
        }

        public async Task<ResponseModel<CarModel>> CreateCarAsync(CarModel car)
        {
            if (_cars.Any(c => c.CarId == car.CarId))
            {
                return new ResponseModel<CarModel>
                {
                    Data = null,
                    Success = false,
                    Message = "Car with the same code already exists."
                };
            }

            _cars.Add(car);
            return new ResponseModel<CarModel>
            {
                Data = car,
                Success = true,
                Message = "Car added successfully."
            };
        }

        public async Task<ResponseModel<CarModel>> UpdateCarAsync(int carId, CarModel car)
        {
            var existingCar = _cars.FirstOrDefault(c => c.CarId == carId);
            if (existingCar == null)
            {
                return new ResponseModel<CarModel>
                {
                    Data = null,
                    Success = false,
                    Message = "Car not found."
                };
            }

            existingCar.Name = car.Name;
            existingCar.Country = car.Country;

            return new ResponseModel<CarModel>
            {
                Data = existingCar,
                Success = true,
                Message = "Car updated successfully."
            };
        }

        public async Task<ResponseModel<bool>> DeleteCarAsync(int carId)
        {
            var existingCar = _cars.FirstOrDefault(c => c.CarId == carId);
            if (existingCar == null)
            {
                return new ResponseModel<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "Car not found."
                };
            }

            _cars.Remove(existingCar);
            return new ResponseModel<bool>
            {
                Data = true,
                Success = true,
                Message = "Car deleted successfully."
            };
        }
    }
}
