using Microsoft.EntityFrameworkCore;
using ScarletShared.Models;

namespace ScarletWebAPI.Data;

public class ChinookDbContext : DbContext
{
	public DbSet<Artist> Artist { get; set; }

	public ChinookDbContext(DbContextOptions<ChinookDbContext> options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Artist>(
			b =>
			{
				b.Property(a => a.Id).HasColumnName("ArtistId");
			});
	}
}