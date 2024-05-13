using AdaptiveWebInterfaces_WebAPI.Models;

namespace AdaptiveWebInterfaces_WebAPI.Services.Car
{
    public interface ICarService
    {
        Task<CarModel> GetCarAsync(int code);
        Task<IEnumerable<CarModel>> GetAllCarsAsync();
        Task<CarModel> CreateCarAsync(CarModel car);
        Task<CarModel> UpdateCarAsync(int code, CarModel car);
        Task<bool> DeleteCarAsync(int code);
    }
}
