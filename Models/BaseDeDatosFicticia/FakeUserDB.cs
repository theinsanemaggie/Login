namespace LoginMVC.Models.BaseDeDatosFicticia
{
    public class FakeUserDB
    {
        //permite llamadas externas
        //lista de usuarios que se van a loguear
        //hacemos una instancia de LoginModel
        public static List<LoginModel> User = new List<LoginModel> {

            new LoginModel
            {
                Id = 1,
                User = "Maggie",
                Password = "1234",
            },

            new LoginModel
            {
                Id = 2,
                User = "Juan",
                Password = "123465",
            }
        };


    }
}
