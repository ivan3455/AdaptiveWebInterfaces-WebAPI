using AdaptiveWebInterfaces_WebAPI.Models;

namespace AdaptiveWebInterfaces_WebAPI.Services.Manufacturer
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly List<ManufacturerModel> _manufacturers;

        public ManufacturerService()
        {
            _manufacturers = new List<ManufacturerModel>
        {
            new ManufacturerModel { ManufacturerId = 1, Name = "Toyota", Contacts = "123 Main St, Japan" },
            new ManufacturerModel { ManufacturerId = 2, Name = "Honda", Contacts = "456 Oak St, Japan" }
        };
        }

        public async Task<ManufacturerModel> GetManufacturerAsync(int manufacturerId)
        {
            return await Task.FromResult(_manufacturers.FirstOrDefault(m => m.ManufacturerId == manufacturerId));
        }

        public async Task<IEnumerable<ManufacturerModel>> GetAllManufacturersAsync()
        {
            return await Task.FromResult(_manufacturers);
        }

        public async Task<ManufacturerModel> CreateManufacturerAsync(ManufacturerModel manufacturer)
        {
            if (_manufacturers.Any(m => m.ManufacturerId == manufacturer.ManufacturerId))
            {
                throw new Exception("Manufacturer with the same code already exists.");
            }

            _manufacturers.Add(manufacturer);
            return await Task.FromResult(manufacturer);
        }

        public async Task<ManufacturerModel> UpdateManufacturerAsync(int manufacturerId, ManufacturerModel manufacturer)
        {
            var existingManufacturer = _manufacturers.FirstOrDefault(m => m.ManufacturerId == manufacturerId);
            if (existingManufacturer == null)
            {
                throw new Exception("Manufacturer not found.");
            }

            existingManufacturer.Name = manufacturer.Name;
            existingManufacturer.Contacts = manufacturer.Contacts;

            return await Task.FromResult(existingManufacturer);
        }

        public async Task<bool> DeleteManufacturerAsync(int manufacturerId)
        {
            var existingManufacturer = _manufacturers.FirstOrDefault(m => m.ManufacturerId == manufacturerId);
            if (existingManufacturer == null)
            {
                throw new Exception("Manufacturer not found.");
            }

            _manufacturers.Remove(existingManufacturer);
            return await Task.FromResult(true);
        }
    }

}
