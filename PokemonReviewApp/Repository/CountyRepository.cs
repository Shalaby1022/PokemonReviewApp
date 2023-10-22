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
            _context = context ?? throw new ArgumentNullException(nameof(context));
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

        public bool DeleteCountry(Country country)
        {
            _context.Remove(country);
            return save();
        }

        public ICollection<Country> GetAllCountries()
        {
            return _context.Countries.ToList();

        }

        public  ICollection<Country> GetAllCountries(string? name, string? SearchQuery)
        {
                if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(SearchQuery)) return GetAllCountries();

                var collection = _context.Countries as IQueryable<Country>;

                if (!string.IsNullOrEmpty(name))
                {
                    name = name.Trim();
                    collection = collection.Where(c => c.Name == name);
                }

                if (!string.IsNullOrEmpty(SearchQuery))
                {
                    SearchQuery = SearchQuery.Trim();
                    collection = collection.Where(a => a.Name.Contains(SearchQuery));


                }

                return collection.ToList();
            }

        public Country GetCountryById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid country ID");
            }

            return _context.Countries.FirstOrDefault(c => c.Id == id);
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

        public bool UpdateCountry(Country country)
        {
            _context.Update(country);
            return save();
        }
    }
}
