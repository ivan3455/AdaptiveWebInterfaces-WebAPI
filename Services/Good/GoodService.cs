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
            new GoodModel { GoodId = 2, Name = "Oil Filter", Description = "Premium oil filter for better engine performance", Price = 10.00m, Quantity = 200, ManufacturerId = 2, CategoryId = 2 }
        };
        }

        public async Task<GoodModel> GetGoodAsync(int goodId)
        {
            return await Task.FromResult(_goods.FirstOrDefault(g => g.GoodId == goodId));
        }

        public async Task<IEnumerable<GoodModel>> GetAllGoodsAsync()
        {
            return await Task.FromResult(_goods);
        }

        public async Task<GoodModel> CreateGoodAsync(GoodModel good)
        {
            if (_goods.Any(g => g.GoodId == good.GoodId))
            {
                throw new Exception("Good with the same code already exists.");
            }

            _goods.Add(good);
            return await Task.FromResult(good);
        }

        public async Task<GoodModel> UpdateGoodAsync(int goodId, GoodModel good)
        {
            var existingGood = _goods.FirstOrDefault(g => g.GoodId == goodId);
            if (existingGood == null)
            {
                throw new Exception("Good not found.");
            }

            existingGood.Name = good.Name;
            existingGood.Description = good.Description;
            existingGood.Price = good.Price;
            existingGood.Quantity = good.Quantity;
            existingGood.ManufacturerId = good.ManufacturerId;
            existingGood.CategoryId = good.CategoryId;

            return await Task.FromResult(existingGood);
        }

        public async Task<bool> DeleteGoodAsync(int goodId)
        {
            var existingGood = _goods.FirstOrDefault(g => g.GoodId == goodId);
            if (existingGood == null)
            {
                throw new Exception("Good not found.");
            }

            _goods.Remove(existingGood);
            return await Task.FromResult(true);
        }
    }

}
