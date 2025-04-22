using System.ComponentModel.DataAnnotations;

namespace Demo.presentation.ViewModels.User
{
    public class UpdatedUserDto
    {
        public string Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
