using System.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Data.SqlClient;
using WebAPI_ASPNET_Core.Models;

namespace WebAPI_ASPNET_Core.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public  DbSet<UserModel> Users { get; set; }
        
        
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
        
        public async Task<IEnumerable<UserModel>> GetUsersByProperty(string propertyName, string propertyValue)
        {
            var sqlQuery = FormattableStringFactory.Create($"SELECT * FROM `users` WHERE `{propertyName}` LIKE '{propertyValue}'");
            return Database.SqlQuery<UserModel>(sqlQuery).ToList();

            

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
    