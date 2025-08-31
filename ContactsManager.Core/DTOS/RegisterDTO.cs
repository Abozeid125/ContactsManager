using ContactsManager.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.Core.DTOS
{
    public class RegisterDTO
    {
        [Required(ErrorMessage ="Name Can't be blank")]
        public string? PersonName { get; set; }

        [Required(ErrorMessage = "Email Can't be blank")]
        [EmailAddress]
        public string? Email { get; set; }


        [Required(ErrorMessage = "Password Can't be blank")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        

        [Required(ErrorMessage = "CanfirmPassword Can't be blank")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Phone Can't be blank")]

        public string? Phone { get; set; }
        public RoleOptions RoleOptions { get; set; } = RoleOptions.User;



    }
}
