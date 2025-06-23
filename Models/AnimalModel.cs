using System.Reflection;

namespace LoginMVC.Models
{
    public class AnimalModel
    {
        public int idAnimal { get; set; }
        public string nombre { get; set; }
        public int idEspecie { get; set; }
        public int idTamaño { get; set; }
        public IFormFile imagen { get; set; }
        public int idRaza { get; set; }
        public int edad { get; set; }
        public int idEstado { get; set; }
        public DateTime fechaIngreso { get; set; }
    }
    public class AnimalEspecieModel
    {
    }
<<<<<<< HEAD
    public class AnimalEstadolModel
    {
        public int IdEstado { get; set; }
=======
    public class AnimalEstadoModel
    {
        public int idEstado { get; set; }
>>>>>>> origin/Rocio
        public string descripcion { get; set; }
    }
    public class AnimalRazaModel
    {
    }
    public class AnimalTamañoModel
    {
    }
  
}
