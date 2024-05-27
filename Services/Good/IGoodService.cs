using AdaptiveWebInterfaces_WebAPI.Models;

namespace AdaptiveWebInterfaces_WebAPI.Services.Good
{
    public interface IGoodService
    {
        Task<ResponseModel<GoodModel>> GetGoodAsync(int code);
        Task<ResponseModel<IEnumerable<GoodModel>>> GetAllGoodsAsync();
        Task<ResponseModel<GoodModel>> CreateGoodAsync(GoodModel good);
        Task<ResponseModel<GoodModel>> UpdateGoodAsync(int code, GoodModel good);
        Task<ResponseModel<bool>> DeleteGoodAsync(int code);
    }
}
