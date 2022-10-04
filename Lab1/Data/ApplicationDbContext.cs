using Lab1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lab1.Data
{
    /// <summary>
    /// Application DB Context class
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        /// <summary>
        /// Reference to database for teams
        /// </summary>
        public DbSet<Lab1.Models.Team> Team { get; set; }
    }
}