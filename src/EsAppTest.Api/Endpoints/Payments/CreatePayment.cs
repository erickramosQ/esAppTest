using EsAppTest.Core.Services.Interfaces.Payments.Request;
using EsAppTest.Core.Services.Interfaces.Payments.Response;
using EsAppTest.Core.Services.Interfaces.Payments.Services;
using AutoMapperMapper = AutoMapper.IMapper;

namespace EsAppTest.Api.Endpoints.Payments;

public sealed class CreatePayment : FastEndpoints.Endpoint<CreatePaymentRequest, CreatePaymentResponse>
{
    private readonly PaymentsService _service;
    private readonly AutoMapperMapper _mapper;

    public CreatePayment(PaymentsService service, AutoMapperMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public override void Configure()
    {
        Post("/payments");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Registrar un pago";
            s.Description = "Crea un pago con estado pendiente. Rechaza montos > 1500 Bs y moneda distinta a BOB.";
        });
    }

    public override async Task HandleAsync(CreatePaymentRequest req, CancellationToken ct)
    {
        try
        {
            PCreatePaymentRequest coreReq = _mapper.Map<PCreatePaymentRequest>(req);
            PCreatePaymentResponse coreRes = await _service.CreateAsync(coreReq, ct);

            var httpRes = _mapper.Map<CreatePaymentResponse>(coreRes);
            HttpContext.Response.StatusCode = 201;
            await Send.OkAsync(httpRes, ct);
        }
        catch (ArgumentException ex)
        {
            AddError(ex.Message);
            await Send.ErrorsAsync(400, ct);
        }
    }
}
