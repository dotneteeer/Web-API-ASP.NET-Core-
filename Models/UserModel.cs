using System.ComponentModel.DataAnnotations;

namespace WebAPI_ASPNET_Core.Models
{
    public class UserModel
    {
        public int id { get; set; }
        
        [Required(ErrorMessage = "Enter text")]
        [MaxLength(50, ErrorMessage = "Too long")]
        public string name { get; set; }

        [Range(0, 100, ErrorMessage = "unacceptable age")]
        public int age { get; set; }

        /*public User(int age, string name)
        {
            this.age = age;
            this.name = name;
        }    */

        public override string ToString()
        {
            return $"{{\"Name\":\"{name}\",\"age\":{age}}}";
        }
        
        

    }
}
