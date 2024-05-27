using AdaptiveWebInterfaces_WebAPI.Models;

namespace AdaptiveWebInterfaces_WebAPI.Services.Manufacturer
{
    public interface IManufacturerService
    {
        Task<ResponseModel<ManufacturerModel>> GetManufacturerAsync(int code);
        Task<ResponseModel<IEnumerable<ManufacturerModel>>> GetAllManufacturersAsync();
        Task<ResponseModel<ManufacturerModel>> CreateManufacturerAsync(ManufacturerModel manufacturer);
        Task<ResponseModel<ManufacturerModel>> UpdateManufacturerAsync(int code, ManufacturerModel manufacturer);
        Task<ResponseModel<bool>> DeleteManufacturerAsync(int code);
    }
}
