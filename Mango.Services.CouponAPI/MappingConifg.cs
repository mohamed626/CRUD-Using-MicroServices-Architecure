
namespace Mango.Services.CouponAPI
{
    public class MappingConifg
    {
        public static MapperConfiguration RegisterMap()
        {
            var mapperConfigration = new MapperConfiguration(config =>
            {
                config.CreateMap<Coupon,CouponDTO>();
                config.CreateMap<CouponDTO,Coupon>();   
            }); 

            return mapperConfigration;
        }
    }
}
