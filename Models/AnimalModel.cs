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
        //public IFormFile imagen { get; set; }
        public int idRaza { get; set; }
        public int edad { get; set; }
        public int idEstado { get; set; }
        public DateTime fechaIngreso { get; set; }

        /*se agregan para que en ligar de leer el ID lea la descricón */
        public string especie { get; set; }
        public string tamaño { get; set; }
        public string raza { get; set; }
        public string estado { get; set; }
    }
    public class AnimalEspecieModel //PRII
    {
        public int idEspecie { get; set; }

        [Required(ErrorMessage = "Debe ingresar una descripción.")]
        public string descripcion { get; set; }

    }
    public class AnimalEstadoModel
    {
        public int idEstado { get; set; }
        // origin/Rocio
        public string descripcion { get; set; }
    }
    public class AnimalRazaModel
    {
        public int idRaza { get; set; }
        public string descripcion { get; set; }
    }
    public class AnimalTamañoModel
    {
        public int idTamaño { get; set; }
        public string descripcion { get; set; }
    }

}

  

