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
                new ManufacturerModel { ManufacturerId = 2, Name = "Honda", Contacts = "456 Oak St, Japan" },
                new ManufacturerModel { ManufacturerId = 3, Name = "BMW", Contacts = "789 Elm St, Germany" },
                new ManufacturerModel { ManufacturerId = 4, Name = "Ford", Contacts = "101 Pine St, USA" },
                new ManufacturerModel { ManufacturerId = 5, Name = "Mercedes-Benz", Contacts = "111 Oak St, Germany" },
                new ManufacturerModel { ManufacturerId = 6, Name = "Audi", Contacts = "222 Maple St, Germany" },
                new ManufacturerModel { ManufacturerId = 7, Name = "Chevrolet", Contacts = "333 Cedar St, USA" },
                new ManufacturerModel { ManufacturerId = 8, Name = "Volkswagen", Contacts = "444 Birch St, Germany" },
                new ManufacturerModel { ManufacturerId = 9, Name = "Nissan", Contacts = "555 Pine St, Japan" },
                new ManufacturerModel { ManufacturerId = 10, Name = "Hyundai", Contacts = "666 Elm St, South Korea" },
                new ManufacturerModel { ManufacturerId = 11, Name = "Mazda", Contacts = "777 Oak St, Japan" },
                new ManufacturerModel { ManufacturerId = 12, Name = "Subaru", Contacts = "888 Maple St, Japan" }
            };
        }

        public async Task<ResponseModel<ManufacturerModel>> GetManufacturerAsync(int manufacturerId)
        {
            var manufacturer = _manufacturers.FirstOrDefault(m => m.ManufacturerId == manufacturerId);
            if (manufacturer != null)
            {
                return new ResponseModel<ManufacturerModel>
                {
                    Data = manufacturer,
                    Success = true,
                    Message = "Manufacturer found."
                };
            }
            else
            {
                return new ResponseModel<ManufacturerModel>
                {
                    Data = null,
                    Success = false,
                    Message = "Manufacturer not found."
                };
            }
        }

        public async Task<ResponseModel<IEnumerable<ManufacturerModel>>> GetAllManufacturersAsync()
        {
            return new ResponseModel<IEnumerable<ManufacturerModel>>
            {
                Data = _manufacturers,
                Success = true,
                Message = "All manufacturers retrieved."
            };
        }

        public async Task<ResponseModel<ManufacturerModel>> CreateManufacturerAsync(ManufacturerModel manufacturer)
        {
            if (_manufacturers.Any(m => m.ManufacturerId == manufacturer.ManufacturerId))
            {
                return new ResponseModel<ManufacturerModel>
                {
                    Data = null,
                    Success = false,
                    Message = "Manufacturer with the same code already exists."
                };
            }

            _manufacturers.Add(manufacturer);
            return new ResponseModel<ManufacturerModel>
            {
                Data = manufacturer,
                Success = true,
                Message = "Manufacturer added successfully."
            };
        }

        public async Task<ResponseModel<ManufacturerModel>> UpdateManufacturerAsync(int manufacturerId, ManufacturerModel manufacturer)
        {
            var existingManufacturer = _manufacturers.FirstOrDefault(m => m.ManufacturerId == manufacturerId);
            if (existingManufacturer == null)
            {
                return new ResponseModel<ManufacturerModel>
                {
                    Data = null,
                    Success = false,
                    Message = "Manufacturer not found."
                };
            }

            existingManufacturer.Name = manufacturer.Name;
            existingManufacturer.Contacts = manufacturer.Contacts;

            return new ResponseModel<ManufacturerModel>
            {
                Data = existingManufacturer,
                Success = true,
                Message = "Manufacturer updated successfully."
            };
        }

        public async Task<ResponseModel<bool>> DeleteManufacturerAsync(int manufacturerId)
        {
            var existingManufacturer = _manufacturers.FirstOrDefault(m => m.ManufacturerId == manufacturerId);
            if (existingManufacturer == null)
            {
                return new ResponseModel<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "Manufacturer not found."
                };
            }

            _manufacturers.Remove(existingManufacturer);
            return new ResponseModel<bool>
            {
                Data = true,
                Success = true,
                Message = "Manufacturer deleted successfully."
            };
        }
    }
}
