
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
        public List<UserModel> GetAllUsers()
        {
            return Users.ToList();
        }

        public UserModel GetUserById(int id)
        {
            return Users.ToList().Find(user => user.id == id);
        }
        
        public List<UserModel> GetUserByProperty(string propertyName, object propertyValue)
        {
            PropertyInfo property = typeof(UserModel).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (property == null)
            {
                throw new ArgumentException($"Property named '{propertyName}' is not found in User model.");
            }

            return Users.ToList().FindAll(user =>
                property.GetValue(user)?.Equals(propertyValue) == true);
        }

        /// <summary>
        /// OTHER
        /// </summary>/

        public void UpdateUserById(int id, UserModel newUser)
        {
             int newUserIndex=Users.ToList().IndexOf(this.GetUserById(id));
             Users.ToList()[newUserIndex] = newUser;
             this.SaveChangesAsync();

        }


        public void DeleteUserById(int id)
        {
            Users.Remove(this.GetUserById(id));
            this.SaveChangesAsync();
        }
        
    }
}
    