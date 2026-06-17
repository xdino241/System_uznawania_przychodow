using System_uznawania_przychodow.DTOs.Payments;

namespace System_uznawania_przychodow.Services.ContractPaymentServices;

public interface IContractPaymentService
{
    Task<int> AddPaymentAsync(AddPaymentDto dto);
}