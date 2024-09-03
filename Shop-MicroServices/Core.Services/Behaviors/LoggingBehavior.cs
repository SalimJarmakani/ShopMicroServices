using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Core.Services.Behaviors;

public class LoggingBehavior<TRequest, TResponse> (ILogger<LoggingBehavior<TRequest,TResponse>> logger)
	: IPipelineBehavior<TRequest, TResponse>

	where TRequest: notnull, IRequest<TResponse>
	where TResponse : notnull
{
	public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	{
		logger.LogInformation($"Request: {typeof(TRequest).Name} - Response: {typeof(TResponse).Name}, \n request: {request}");

		var timer = new Stopwatch();
		timer.Start();

		var response = await next();

		timer.Stop();

		var timerPeriod = timer.Elapsed;

		if (timerPeriod.Seconds > 5)
		{
			logger.LogWarning($"PERFORMANCE ISSUE => the request {request} took {timerPeriod}");
		}


		logger.LogInformation($"END of request: {typeof(TRequest).Name} with Response: {typeof(TResponse).Name} ");

		return response;
	}
}

