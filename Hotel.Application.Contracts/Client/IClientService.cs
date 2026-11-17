namespace Hotel.Application.Contracts.Client;

public interface IClientService :
    IService<ClientResponseDto, ClientCreateUpdateDto>
{ }