using System_uznawania_przychodow.DTOs.Contracts;

namespace System_uznawania_przychodow.Services;

public interface IContractService
{
    Task<int> CreateContractAsync(CreateContractDto dto);
    Task DeleteContractAsync(int id);
}