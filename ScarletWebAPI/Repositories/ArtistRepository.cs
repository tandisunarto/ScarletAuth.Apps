using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ScarletShared.Models;
using ScarletWebAPI.Data;
using ScarletWebAPI.Interfaces;

namespace ScarletWebAPI.Repositories;

public class ArtistRepository : IArtistRepository
{
	private ChinookDbContext dbContext;

	public ArtistRepository(ChinookDbContext dbContext)
	{
		this.dbContext = dbContext;
	}

    public void Add(Artist value)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Artist> Get()
    {
        return dbContext.Artist.ToList();
    }

    public Artist GetById(int id)
    {
        return dbContext.Artist.Where(a => a.Id == id).FirstOrDefault();
    }

    public void Update(Artist artist)
    {
        dbContext.Entry(artist).State = EntityState.Modified;
        dbContext.SaveChanges();
    }

    // private bool disposed = false;

    // protected virtual void Dispose(bool disposing)
    // {
    //     if (!this.disposed)
    //     {
    //         if (disposing)
    //         {
    //             dbContext.Dispose();
    //         }
    //     }
    //     this.disposed = true;
    // }

    // public void Dispose()
    // {
    //     Dispose(true);
    //     GC.SuppressFinalize(this);
    // }
}