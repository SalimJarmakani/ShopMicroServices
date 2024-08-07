using MediatR;

namespace Core.Services.CQRS;

public interface IQuery<out TResponse> : IRequest<TResponse>
	where TResponse : notnull
{
}
