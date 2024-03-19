namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;
        ResponseDTO responseDTO;

        public CouponAPIController(AppDbContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
            responseDTO = new ResponseDTO();
        }

        [HttpGet]
        public ResponseDTO Get()
        {
            try
            {
                IEnumerable<Coupon> Listobj = context.coupons.ToList();
                responseDTO.Result = mapper.Map<List<CouponDTO>>(Listobj);
                responseDTO.IsSuccess = true;
            }
            catch (Exception ex)
            {
                responseDTO.Message = ex.Message;

            }
            return responseDTO;

        }

        [HttpGet("{id:int}")]
        public ResponseDTO Get(int id)
        {
            try
            {
                Coupon obj = context.coupons.First(c=>c.CouponId == id);
                responseDTO.Result = mapper.Map<CouponDTO>(obj);
                responseDTO.IsSuccess = true;
            }
            catch (Exception ex)
            {
                responseDTO.Message = ex.Message;

            }
            return responseDTO;

        }

        [HttpGet("GetByCode/{code}")]
        public ResponseDTO Get(string code)
        {
            try
            {
                Coupon obj = context.coupons.First(c => c.CouponCode.ToLower()== code.ToLower());
                responseDTO.Result = mapper.Map<CouponDTO>(obj);
                responseDTO.IsSuccess = true;
            }
            catch (Exception ex)
            {
                responseDTO.Message = ex.Message;

            }
            return responseDTO;

        }

        [HttpPost("AddCoupon")]
        public ResponseDTO Post(CouponDTO couponDTO)
        {
            try
            {
                Coupon newCoupon = mapper.Map<Coupon>(couponDTO);
                context.coupons.Add(mapper.Map<Coupon>(newCoupon));
                context.SaveChanges();
                responseDTO.Result= mapper.Map<CouponDTO>(newCoupon);
                responseDTO.IsSuccess = true;
            }
            catch(Exception ex)
            {
              
                responseDTO.IsSuccess = false;
                responseDTO.Message = ex.Message;
            }

            return responseDTO;

        }

        [HttpPut("Update")]
        public ResponseDTO Put(CouponDTO couponDTO)
        {
            try
            {
                Coupon newCoupon = mapper.Map<Coupon>(couponDTO);
            
                context.coupons.Update(newCoupon);
                context.SaveChanges();
                responseDTO.Result = mapper.Map<CouponDTO>(newCoupon);
                responseDTO.IsSuccess = true;
            }
            catch (Exception ex)
            {

                responseDTO.IsSuccess = false;
                responseDTO.Message = ex.Message;
            }

            return responseDTO;

        }

        [HttpDelete("{id:int}")]
        public ResponseDTO delete(int id)
        {
            try
            {
                Coupon coupon = context.coupons.First(c=>c.CouponId==id);
                context.coupons.Remove(coupon);
                context.SaveChanges();
                responseDTO.Result = mapper.Map<CouponDTO>(coupon);
                responseDTO.IsSuccess = true;
            }
            catch (Exception ex)
            {

                responseDTO.IsSuccess = false;
                responseDTO.Message = ex.Message;
            }

            return responseDTO;

        }



    }

}
