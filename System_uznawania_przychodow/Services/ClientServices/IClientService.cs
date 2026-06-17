using System_uznawania_przychodow.DTOs.Clients;

namespace System_uznawania_przychodow.Services;

public interface IClientService
{
    Task<int> AddIndividualClientAsync(CreateIndividualClientDto dto);
    Task<int> AddCompanyClientAsync(CreateCompanyClientDto dto);
    Task DeleteClientAsync(int id);
    Task UpdateIndividualClientAsync(int id, UpdateIndividualClientDto dto);
    Task UpdateCompanyClientAsync(int id, UpdateCompanyClientDto dto);
}