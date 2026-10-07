using System.ComponentModel.DataAnnotations;

namespace Hospital_Manager.ViewModels.Patient
{
    public class PatientEditViewModel
    {
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

        [Required(ErrorMessage = "Please enter doctor")]
        public List<int> DoctorIds { get; set; } = new List<int>();
    }
}
