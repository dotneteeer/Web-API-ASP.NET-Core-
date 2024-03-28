using System.ComponentModel.DataAnnotations;

namespace WebAPI_ASPNET_Core.Models
{
    public class UserModel
    {
        //[Range(1, int.MaxValue, ErrorMessage = "Id can not be below one ")]
        public int id { get; set; }
        
        [Required(ErrorMessage = "Enter name")]
        [MaxLength(50, ErrorMessage = "Too long")]
        public string name { get; set; }
        
        [Required(ErrorMessage = "Enter age")]   
        [Range(0, 115, ErrorMessage = "unacceptable age")]
        public int? age { get; set; }

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
