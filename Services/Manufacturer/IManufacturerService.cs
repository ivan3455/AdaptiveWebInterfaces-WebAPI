using AdaptiveWebInterfaces_WebAPI.Models;

namespace AdaptiveWebInterfaces_WebAPI.Services.Manufacturer
{
    public interface IManufacturerService
    {
        Task<ManufacturerModel> GetManufacturerAsync(int code);
        Task<IEnumerable<ManufacturerModel>> GetAllManufacturersAsync();
        Task<ManufacturerModel> CreateManufacturerAsync(ManufacturerModel manufacturer);
        Task<ManufacturerModel> UpdateManufacturerAsync(int code, ManufacturerModel manufacturer);
        Task<bool> DeleteManufacturerAsync(int code);
    }
}
