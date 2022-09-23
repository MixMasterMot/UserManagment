using Microsoft.Extensions.Options;
using MongoDB.Driver;
using UserManagment.Entities;
using UserManagment.Models;

namespace UserManagment.Repositories
{
    public class UserRepository
    {
        private readonly IMongoCollection<User> _usersCollection;

        public UserRepository(IOptions<DatabaseSettings> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _usersCollection = mongoDatabase.GetCollection<User>(dbSettings.Value.CollectionName);
        }

        public async Task<List<User>> GetAsync() =>
            await _usersCollection.Find(_ => true).ToListAsync();

        public async Task<User?> GetByIDAsync(string id) =>
            await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<User?> GetByUserNameAsync(string name) =>
            await _usersCollection.Find(x => x.UserName.Equals(name)).FirstOrDefaultAsync();

        public async Task<List<User>> FindAsync(string term) =>
            await _usersCollection.Find(x => x.UserName.Contains(term, StringComparison.InvariantCultureIgnoreCase)).ToListAsync();

        public async Task<User?> CreateAsync(User newUser)
        {
            await _usersCollection.InsertOneAsync(newUser);
            return newUser;
        }

        public async Task UpdateAsync(string id, User updatedUser) =>
            await _usersCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);

        public async Task RemoveAsync(string id) =>
            await _usersCollection.DeleteOneAsync(x => x.Id == id);
    }
}
