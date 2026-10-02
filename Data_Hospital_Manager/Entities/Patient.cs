using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Hospital_Manager.Entities
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Моля, въведете име.")]
        [MaxLength(30, ErrorMessage = "Името не може да бъде повече от 30 символа.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Моля, въведете фамилия.")]
        [MaxLength(30, ErrorMessage = "Фамилията не може да бъде повече от 30 символа.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Моля, въведете email.")]
        [EmailAddress(ErrorMessage = "Моля, въведете валиден email адрес.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Моля, въведете телефонен номер.")]
        [Phone(ErrorMessage = "Моля, въведете валиден телефонен номер.")]
        public string PhoneNumber { get; set; }

        public List<DoctorPatient> DoctorPatients { get; set; } = new List<DoctorPatient>();
    }
}
