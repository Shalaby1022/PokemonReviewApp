﻿using PokemonReviewApp.Models;

namespace PokemonReviewApp.Data.Interface
{
    public interface ICountryRepository
    {
        ICollection<Country> GetAllCountries();
        Country GetCountry(int id);
        Country GetCountryByOwner(int ownerid);
        ICollection<Owner> GetownersfromAcountry(int countryId);
        bool CountryExist(int id);

    }
}