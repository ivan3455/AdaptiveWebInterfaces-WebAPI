using AdaptiveWebInterfaces_WebAPI.Models;

namespace AdaptiveWebInterfaces_WebAPI.Services.Car
{
    public interface ICarService
    {
        Task<ResponseModel<CarModel>> GetCarAsync(int code);
        Task<ResponseModel<IEnumerable<CarModel>>> GetAllCarsAsync();
        Task<ResponseModel<CarModel>> CreateCarAsync(CarModel car);
        Task<ResponseModel<CarModel>> UpdateCarAsync(int code, CarModel car);
        Task<ResponseModel<bool>> DeleteCarAsync(int code);
    }
}
