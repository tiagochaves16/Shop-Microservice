using GeekShop.Models;

namespace GeekShop.Services.IService
{
    public interface ICouponService
    {
        Task<CouponViewModel> GetCoupon(string code, string token);
    }
}
