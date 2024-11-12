using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrivateBlogCathe.web.Data.Entities
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Titulo")]
        [MaxLength(64, ErrorMessage ="El Campo {0} debe tener un maximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public string Title { get; set; }

        [Display(Name = "Contenido")]
        [Column(TypeName= "VARCHAR(MAX)")]
        [Required(ErrorMessage = "El campo '{0}' es requerido.")]
        public string Content { get; set; }

        public bool IsPublished { get; set; }=false;

        public Section Section { get; set; }
        public int SectionId { get; set; }

        //TOdo: crear user
        //public User Author { get; set; }

        //public int AuthorID { get; set; }
    }
}
