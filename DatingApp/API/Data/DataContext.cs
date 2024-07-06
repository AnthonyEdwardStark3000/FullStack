using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.DATA;

public class DataContext(DbContextOptions options):DbContext{
    public UInt32 Id {get;set;}
    public DbSet<AppUser>Users{get;set;}    
}