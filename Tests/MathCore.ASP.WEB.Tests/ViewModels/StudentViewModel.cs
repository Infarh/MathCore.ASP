using System.ComponentModel.DataAnnotations;

namespace MathCore.ASP.WEB.Tests.ViewModels
{
    public class StudentViewModel
    {
        [Required, StringLength(150, MinimumLength = 2)]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [MaxLength(150)]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [MaxLength(150)]
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Range(minimum: 16, maximum: 150)]
        [Display(Name = "Возраст")]
        public int Age { get; set; }
    }
}
