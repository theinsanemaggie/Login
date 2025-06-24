using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LoginMVC.Models
{
    public class LoginContext : DbContext
    {
        public LoginContext(DbContextOptions<LoginContext> options)
            : base(options)
        {
        }

        public DbSet<AnimalModel> AnimalesModel { get; set; }
        public DbSet<AnimalEspecieModel> Especies { get; set; }
        public DbSet<AnimalEstadoModel> Estados { get; set; }
        public DbSet<AnimalRazaModel> Razas { get; set; }
        public DbSet<AnimalTamañoModel> Tamaños { get; set; }
    }
}
