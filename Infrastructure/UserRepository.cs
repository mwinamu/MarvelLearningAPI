using Marten;
using MarvelLearningAPI.Domain;
using MarvelLearningAPI.Event;
using MarvelLearningAPI.Infrastructure.Interface;

namespace MarvelLearningAPI.Infrastructure;

public class UserRepository(IDocumentStore store) : IUserRepository
{
    public async Task CreateUser(CreateUserRequest create)
    {
        await using var session = store.LightweightSession();

        var user = new User
        {
            FirstName = create.FirstName,
            LastName = create.LastName,
            Internal = create.Internal
        };
        session.Store(user);
        await session.SaveChangesAsync();
    }

    public async Task<List<User>> GetUsers(bool internalOnly, CancellationToken ct)
    {
        await using var session = store.QuerySession();

        var users = await session.Query<User>()
            .Where(x => x.Internal == internalOnly)
            .ToListAsync(ct);

        return (List<User>)users;
    }

    public async Task<User?> GetUser(Guid id, CancellationToken ct)
    {
        await using var session = store.QuerySession();

        var user = await session.LoadAsync<User>(id, ct);

        return user;
    }
}
