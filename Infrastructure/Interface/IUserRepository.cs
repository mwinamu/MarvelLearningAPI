using MarvelLearningAPI.Domain;
using MarvelLearningAPI.Event;

namespace MarvelLearningAPI.Infrastructure.Interface;

public interface IUserRepository
{
    Task CreateUser(CreateUserRequest create);
    Task<List<User>> GetUsers(bool internalOnly, CancellationToken ct);
    Task<User?> GetUser(Guid id, CancellationToken ct);
}