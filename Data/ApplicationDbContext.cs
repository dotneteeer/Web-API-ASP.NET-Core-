
using System.Collections;
using System.Reflection;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<UserModel> Users { get; set; }
        
        
        /// <summary>
        /// GET
        /// </summary>/
        public async Task<IEnumerable<UserModel>>GetAllUsers()
        {
            return await Users.ToListAsync();
        }

        public async Task<UserModel> GetUserById(int id)
        {
            return Users.ToList().Find(user => user.id == id);
        }
        
        public async Task<IEnumerable<UserModel>> GetUsersByProperty(string propertyName, object propertyValue)
        {
            PropertyInfo property = typeof(UserModel).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (property == null)
            {
                throw new ArgumentException($"Property named '{propertyName}' is not found in User model.");
            }

            return  Users.ToList().FindAll(user =>
                property.GetValue(user)?.Equals(propertyValue) == true);

        }

        /// <summary>
        /// OTHER
        /// </summary>/

        public async Task UpdateUserById(int id, UserModel newUser)
        {
             int newUserIndex=Users.ToList().IndexOf(await this.GetUserById(id));
             Users.ToList()[newUserIndex] = newUser;
             this.SaveChangesAsync();

        }


        public async Task DeleteUserById(int id)
        {
            Users.Remove(await this.GetUserById(id));
            this.SaveChangesAsync();
        }

        public async Task Insert(UserModel newUser)
        {
            Users.Add(newUser);
            this.SaveChangesAsync();
        }

        public async Task InsertSomeValues(IEnumerable<UserModel> collection)
        {
            foreach (var user in collection)
            {
                Users.Add(user);
            }

            this.SaveChangesAsync();
        }
    }
}
    