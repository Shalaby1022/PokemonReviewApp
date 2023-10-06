using PokemonReviewApp.Models;

namespace PokemonReviewApp.Data.Interface
{
    public interface ICountryRepository
    {
        ICollection<Country> GetAllCountries();
        Country GetCountryById(int id);
        Country GetCountryByOwner(int ownerid);
        ICollection<Owner> GetownersfromAcountry(int countryId);
        bool CountryExist(int id);
        bool CreateCountry(Country country);
        bool UpdateCountry(Country country);
        bool DeleteCountry(Country country);
        bool save();


    }
}
