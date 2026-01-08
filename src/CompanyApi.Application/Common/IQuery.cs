using MediatR;

namespace CompanyApi.Application.Common;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
