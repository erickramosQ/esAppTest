using AutoMapper;
using EsAppTest.Core.Services.Interfaces.Payments.Request;
using EsAppTest.Core.Services.Interfaces.Payments.Response;

namespace EsAppTest.Api.Endpoints.Payments.Mapper
{
    public sealed class PaymentsProfile : Profile
    {
        public PaymentsProfile()
        {
            CreateMap<CreatePaymentRequest, PCreatePaymentRequest>();
            CreateMap<GetPaymentsRequest, PGetPaymentsRequest>();

            CreateMap<PCreatePaymentResponse, CreatePaymentResponse>();
            CreateMap<PGetPaymentsResponse, GetPaymentsResponse>();
        }
    }
}
