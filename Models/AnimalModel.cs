using System.Reflection;

namespace LoginMVC.Models
{
    public class AnimalModel
    {
        public int idAnimal { get; set; }
        public string nombre { get; set; }
        public int idEspecie { get; set; }
        public int idTamaño { get; set; }
        public byte[] imagen { get; set; }
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
    public class AnimalEspecieModel
    {
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
