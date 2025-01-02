using Discount.Grpc;
using NetTopologySuite.Index.HPRtree;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

public record StoreBasketResult(string UserName);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{

    public StoreBasketCommandValidator()
    {
        RuleFor(x=> x.Cart).NotNull().WithMessage("Cart can not be null");
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("Username Is Not Empty");

    }
}



public class StoreBasketHandler(IBasketRepository repository,DiscountProtoService.DiscountProtoServiceClient discountProto ) : 
    ICommandHandler<StoreBasketCommand,StoreBasketResult>
{
    
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command,CancellationToken cancellationToken)
    {
        //TODO: communicate with Discount microservices
        ShoppingCart cart = command.Cart;

        await ApplyCoupon(discountProto, cart, cancellationToken);

        await repository.StoreBasket(command.Cart, cancellationToken);

        return new StoreBasketResult(command.Cart.UserName);


    }

    private static async Task ApplyCoupon(DiscountProtoService.DiscountProtoServiceClient discountProto, ShoppingCart cart, CancellationToken cancellationToken)
    {
        foreach (var product in cart.Items)
        {

            var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = product.ProductName }, cancellationToken: cancellationToken);

            product.Price -= coupon.Amount;

        }
    }
}