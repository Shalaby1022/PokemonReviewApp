using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Data.Interface;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class CountyRepository : ICountryRepository
    {
        private readonly PokemonDbContext _context;

        public CountyRepository(PokemonDbContext context)
        {
            _context = context;
        }

        public bool CountryExist(int id)
        {
            return _context.Countries.Any(c => c.Id == id);
        }

        public bool CreateCountry(Country country)
        {
            _context.Add(country);
            return save();
        }

        public ICollection<Country> GetAllCountries()
        {
            return _context.Countries.ToList();

        }

        public Country GetCountry(int id)
        {
            return _context.Countries.Where(c => c.Id == id).FirstOrDefault();

        }

        public Country GetCountryByOwner(int ownerid)
        {
           return _context.Owners.Where(p=>p.Id == ownerid).Select(p=>p.Country).FirstOrDefault();

        }

        public ICollection<Owner> GetownersfromAcountry(int countryId)
        {
            return _context.Owners.Where(c => c.Country.Id == countryId).ToList();
        }

        public bool save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
