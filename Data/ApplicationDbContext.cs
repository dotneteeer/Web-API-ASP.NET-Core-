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
        
        
        public async Task<IEnumerable<UserModel>>GetAllUsers()
        {
            return Users.ToList().OrderBy(user => user.id);;
        }

        public async Task<UserModel> GetUserById(int id)
        {
            var user = Users.ToList().FirstOrDefault(user => user.id == id);
            if (user != null)
            {
                return user;
            }

            return default;
            //throw new NullReferenceException($"User({{\"id\":{id}}}) is not found");
        }
        
        public async Task<IEnumerable<UserModel>> GetUsersByProperty(string propertyName, string propertyValue)
        {
            try
            {
                var sqlQuery =
                    FormattableStringFactory.Create(
                        $"SELECT * FROM `users` WHERE `{propertyName}` LIKE '{propertyValue}'");
                return Database.SqlQuery<UserModel>(sqlQuery).ToList().OrderBy(user=>user.id);
            }
            catch (MySqlConnector.MySqlException e)
            {
                if (e.Number == 1054)
                {
                    throw new ArgumentException($"Column({{\"propertyName\":\"{propertyName}\"}}) name is incorrect");
                }
                

                throw e;
            }
        }


        public async Task<bool> UpdateUserById(int id, UserModel newUser)
        {
            var user = await GetUserById(id);
            if (user is UserModel)
            {
                try
                {
                    user.name = newUser.name;
                    user.age = newUser.age;
                    await SaveChangesAsync();
                    return true;
                }
                catch (Exception e)
                {
                    throw e;
                }
                
            }

            return false;
        }


        public async Task<bool> DeleteUserById(int id)
        {

            var user = await GetUserById(id);
            if (user is UserModel)
            {
                Users.Remove(user);
                await SaveChangesAsync();
                return true;
            }

            return false;

        }

        public async Task<int> DeleteUserByIdRange(int startId, int endId)
        {
            var usersToDelete = Users.Where(u => u.id >= startId && u.id <= endId);
            int counter = usersToDelete.Count();
            Users.RemoveRange(usersToDelete);
            await SaveChangesAsync();

            return counter;
        }

        public async Task<int> DeleteUsersByProperty(string propertyName, string propertyValue)
        {
            
            var usersByProperty = await GetUsersByProperty(propertyName, propertyValue);
            if (usersByProperty.Count() != 0)
            {
                foreach (var user in usersByProperty)
                {
                    Users.Remove(user);
                }
            }

            await SaveChangesAsync();
            return usersByProperty.Count();//return users deleted quantity
        }

        public async Task Insert(UserModel newUser)
        {
            await Users.AddAsync(newUser);
            await SaveChangesAsync();
        }

        public async Task InsertSomeValues(IEnumerable<UserModel> collection)
        {
            foreach (var user in collection)
            {
                await Users.AddAsync(user);
            }

            await SaveChangesAsync();
        }
    }
}
    