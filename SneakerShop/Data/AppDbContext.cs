using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SneakerShop.Models.Data;
public class AppDbContext:IdentityDbContext<Users>
{ 
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
}
