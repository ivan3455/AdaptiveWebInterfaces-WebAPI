using AdaptiveWebInterfaces_WebAPI.Models;

namespace AdaptiveWebInterfaces_WebAPI.Services.Good
{
    public interface IGoodService
    {
        Task<GoodModel> GetGoodAsync(int code);
        Task<IEnumerable<GoodModel>> GetAllGoodsAsync();
        Task<GoodModel> CreateGoodAsync(GoodModel good);
        Task<GoodModel> UpdateGoodAsync(int code, GoodModel good);
        Task<bool> DeleteGoodAsync(int code);
    }
}
