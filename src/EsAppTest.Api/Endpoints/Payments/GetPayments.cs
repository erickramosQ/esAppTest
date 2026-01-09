using System.Linq;
using FastEndpoints;
using AutoMapperMapper = AutoMapper.IMapper;

using EsAppTest.Core.Services.Interfaces.Payments.Request;
using EsAppTest.Core.Services.Interfaces.Payments.Services;

namespace EsAppTest.Api.Endpoints.Payments;

public sealed class GetPayments : FastEndpoints.Endpoint<GetPaymentsRequest, List<GetPaymentsResponse>>
{
    private readonly PaymentsService _service;
    private readonly AutoMapperMapper _mapper;

    public GetPayments(PaymentsService service, AutoMapperMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public override void Configure()
    {
        Get("/payments");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Devuelve pagos";
            s.Description = "Devuelve una lista de pagos pendientes.";
        });
    }

    public override async Task HandleAsync(GetPaymentsRequest req, CancellationToken ct)
    {
        try
        {
            var coreReq = _mapper.Map<PGetPaymentsRequest>(req);
            var coreList = await _service.GetByCustomerIdAsync(coreReq, ct);

            var httpList = coreList.Select(x => _mapper.Map<GetPaymentsResponse>(x)).ToList();
            await Send.OkAsync(httpList, ct);
        }
        catch (ArgumentException ex)
        {
            AddError(ex.Message);
            await Send.ErrorsAsync(400, ct);
        }
    }
}
