using PokemonReviewApp.Models;

namespace PokemonReviewApp.Data.Interface
{
    public interface IOwnerRepository
    {
        ICollection<Owner> GetAllOwners();
        Owner GetOwnerById(int ownerId);
        ICollection<Owner> GetOwnerofaPokemon(int pokieId);
        ICollection<Pokemon> GetPOkemonFromOwner(int ownerId);
        bool OwnerExist(int ownerId);
        bool CreateOwner(Owner owner);
        bool UpdateOwner(Owner owner);
        bool DeleteOwner(Owner owner);
        bool save();

    }
}
