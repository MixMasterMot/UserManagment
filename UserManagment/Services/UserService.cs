using Microsoft.Extensions.Options;
using MongoDB.Driver;
using UserManagment.Config;
using UserManagment.Entities;

namespace UserManagment.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _usersCollection;

        public UserService(IOptions<DatabaseSettings> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _usersCollection = mongoDatabase.GetCollection<User>(dbSettings.Value.CollectionName);
        }

        public async Task<List<User>> GetAsync(int pageSize = 100, int page = 0) =>
            await _usersCollection.Find(_ => true)
                .Skip(page * pageSize)
                .Limit(pageSize)
                .ToListAsync();

        public async Task<User?> GetByIDAsync(string id) =>
            await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<User?> GetByUserNameAsync(string name)=>
            await _usersCollection.Find(x => x.UserName.Equals(name)).FirstOrDefaultAsync();

        public async Task<List<User>> FindAsync(string term, int pageSize = 100, int page = 0)
        {
            //TODO: This can be improved by using indexes on mongodb
            term = term.ToLower();
            return await _usersCollection.Find(x => 
                x.UserName.ToLower().Contains(term) 
                || x.Email.ToLower().Contains(term)
                || x.FullName.ToLower().Contains(term))
                .ToListAsync();
        }
            

        public async Task<User?> CreateAsync(User newUser)
        {
            await _usersCollection.InsertOneAsync(newUser);
            return newUser;
        }

        public async Task UpdateAsync(User updatedUser) =>
            await _usersCollection.ReplaceOneAsync(x => x.Id == updatedUser.Id, updatedUser);

        public async Task RemoveAsync(string id) =>
            await _usersCollection.DeleteOneAsync(x => x.Id == id);
    }
}
