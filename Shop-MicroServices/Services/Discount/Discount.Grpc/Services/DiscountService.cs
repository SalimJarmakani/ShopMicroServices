using Grpc.Core;

namespace Discount.Grpc.Services;

public class DiscountService(DiscountContext dbContext,ILogger<DiscountService> logger) 
    : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons.Where(c=> c.ProductName == request.ProductName).FirstOrDefaultAsync();

        if (coupon is null)
            coupon = new Coupon {  ProductName = "No Discount" , Amount=0,Description="No Discount Coupon available" };

        logger.LogInformation("Discount is retrieved for product {productname} with amount {amount}", coupon.ProductName, coupon.Amount);

        var couponModel = coupon.Adapt<CouponModel>();

        return couponModel;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();

        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request"));

        dbContext.Coupons.Add(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount is Created for Product: {productname}", coupon.ProductName);

        var couponModel = coupon.Adapt<CouponModel>();

        return couponModel;

    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons.
                           Where(c=> c.ProductName==request.ProductName)
                           .FirstOrDefaultAsync();

        if (coupon is null)
            throw new RpcException(new Status(StatusCode.NotFound, "Coupon does not exist"));

        dbContext.Coupons.Remove(coupon);

        await dbContext.SaveChangesAsync();

        logger.LogInformation("Coupon is deleted for Product: {product}", coupon.ProductName);

        return new DeleteDiscountResponse { Success = true };
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();

        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request"));

        dbContext.Coupons.Update(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Coupon is Updated for Product: {productname}", coupon.ProductName);

        var couponModel = coupon.Adapt<CouponModel>();

        return couponModel;
    }
}
