using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace LoginMVC.Models
{
    public class AnimalModel
    {
        public int idAnimal { get; set; }
        public string nombre { get; set; }
        public int idEspecie { get; set; }
        public int idTamaño { get; set; }
        public ImageFileMachine imagen { get; set; }
        public int idRaza { get; set; }
        public int edad { get; set; }
        public int idEstado { get; set; }
        public DateTime fechaIngreso { get; set; }
    }
    public class AnimalEspecieModel //PRII
    {
        public int idEspecie { get; set; }

        [Required(ErrorMessage = "Debe ingresar una descripción.")]
        public string descripcion { get; set; }

    }
    public class AnimaEstadolModel
    {
    }
    public class AnimalRazaModel
    {
    }
    public class AnimalTamañoModel
    {
    }
  
}
