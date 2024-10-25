using System.ComponentModel.DataAnnotations;

namespace PrivateBlogCathe.web.Data.Entities
{
    public class Section
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Sección")]
        [Required(ErrorMessage ="El campo '{0}' es requerido.")]
        public string Name { get; set; }

        [Display(Name = "Descripción")]
        public string? Description { get; set; }

        [Display(Name = "¿Esta OCulta?")]
        public bool IsHidden { get; set; }
    }
}
