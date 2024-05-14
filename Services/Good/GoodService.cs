using AdaptiveWebInterfaces_WebAPI.Models;

namespace AdaptiveWebInterfaces_WebAPI.Services.Good
{
    public class GoodService : IGoodService
    {
        private readonly List<GoodModel> _goods;

        public GoodService()
        {
            _goods = new List<GoodModel>
            {
                new GoodModel { GoodId = 1, Name = "Brake Pads", Description = "High-quality brake pads for various car models", Price = 50.00m, Quantity = 100, ManufacturerId = 1, CategoryId = 1 },
                new GoodModel { GoodId = 2, Name = "Oil Filter", Description = "Premium oil filter for better engine performance", Price = 10.00m, Quantity = 200, ManufacturerId = 2, CategoryId = 2 },
                new GoodModel { GoodId = 3, Name = "Shock Absorbers", Description = "High-performance shock absorbers for smooth ride", Price = 150.00m, Quantity = 50, ManufacturerId = 3, CategoryId = 3 },
                new GoodModel { GoodId = 4, Name = "Air Filter", Description = "Premium air filter for improved air flow", Price = 20.00m, Quantity = 150, ManufacturerId = 4, CategoryId = 4 },
                new GoodModel { GoodId = 5, Name = "Spark Plugs", Description = "High-quality spark plugs for efficient combustion", Price = 5.00m, Quantity = 300, ManufacturerId = 5, CategoryId = 5 },
                new GoodModel { GoodId = 6, Name = "Transmission Fluid", Description = "Synthetic transmission fluid for smooth gear shifts", Price = 15.00m, Quantity = 100, ManufacturerId = 6, CategoryId = 6 },
                new GoodModel { GoodId = 7, Name = "Exhaust Muffler", Description = "Performance exhaust muffler for enhanced sound", Price = 200.00m, Quantity = 30, ManufacturerId = 7, CategoryId = 7 },
                new GoodModel { GoodId = 8, Name = "Radiator", Description = "Aluminum radiator for efficient cooling", Price = 100.00m, Quantity = 20, ManufacturerId = 8, CategoryId = 8 },
                new GoodModel { GoodId = 9, Name = "Floor Mats", Description = "Premium floor mats for interior protection", Price = 30.00m, Quantity = 80, ManufacturerId = 9, CategoryId = 9 },
                new GoodModel { GoodId = 10, Name = "Roof Rack", Description = "Universal roof rack for extra cargo space", Price = 150.00m, Quantity = 25, ManufacturerId = 10, CategoryId = 10 },
                new GoodModel { GoodId = 11, Name = "Alloy Wheels", Description = "Lightweight alloy wheels for improved performance", Price = 300.00m, Quantity = 40, ManufacturerId = 11, CategoryId = 11 },
                new GoodModel { GoodId = 12, Name = "Front Bumper", Description = "OEM front bumper for vehicle protection", Price = 250.00m, Quantity = 15, ManufacturerId = 12, CategoryId = 12 }
            };
        }

        public async Task<ResponseModel<GoodModel>> GetGoodAsync(int goodId)
        {
            var good = _goods.FirstOrDefault(g => g.GoodId == goodId);
            if (good != null)
            {
                return new ResponseModel<GoodModel>
                {
                    Data = good,
                    Success = true,
                    Message = "Good found."
                };
            }
            else
            {
                return new ResponseModel<GoodModel>
                {
                    Data = null,
                    Success = false,
                    Message = "Good not found."
                };
            }
        }

        public async Task<ResponseModel<IEnumerable<GoodModel>>> GetAllGoodsAsync()
        {
            return new ResponseModel<IEnumerable<GoodModel>>
            {
                Data = _goods,
                Success = true,
                Message = "All goods retrieved."
            };
        }

        public async Task<ResponseModel<GoodModel>> CreateGoodAsync(GoodModel good)
        {
            if (_goods.Any(g => g.GoodId == good.GoodId))
            {
                return new ResponseModel<GoodModel>
                {
                    Data = null,
                    Success = false,
                    Message = "Good with the same code already exists."
                };
            }

            _goods.Add(good);
            return new ResponseModel<GoodModel>
            {
                Data = good,
                Success = true,
                Message = "Good added successfully."
            };
        }

        public async Task<ResponseModel<GoodModel>> UpdateGoodAsync(int goodId, GoodModel good)
        {
            var existingGood = _goods.FirstOrDefault(g => g.GoodId == goodId);
            if (existingGood == null)
            {
                return new ResponseModel<GoodModel>
                {
                    Data = null,
                    Success = false,
                    Message = "Good not found."
                };
            }

            existingGood.Name = good.Name;
            existingGood.Description = good.Description;
            existingGood.Price = good.Price;
            existingGood.Quantity = good.Quantity;
            existingGood.ManufacturerId = good.ManufacturerId;
            existingGood.CategoryId = good.CategoryId;

            return new ResponseModel<GoodModel>
            {
                Data = existingGood,
                Success = true,
                Message = "Good updated successfully."
            };
        }

        public async Task<ResponseModel<bool>> DeleteGoodAsync(int goodId)
        {
            var existingGood = _goods.FirstOrDefault(g => g.GoodId == goodId);
            if (existingGood == null)
            {
                return new ResponseModel<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "Good not found."
                };
            }

            _goods.Remove(existingGood);
            return new ResponseModel<bool>
            {
                Data = true,
                Success = true,
                Message = "Good deleted successfully."
            };
        }
    }
}
