using Anipat.Models;

namespace Anipat.Services
{
    public class PetDescriptionService
    {
        // Bura diqqət: Parametr olaraq "EnergyLevel energy" qəbul edirik
        public string GenerateDescription(PetSpecies species, int age, EnergyLevel energy, bool isKidsFriendly)
        {
            string speciesName = species switch
            {
                PetSpecies.İt => "it",
                PetSpecies.Pişik => "pişik",
                PetSpecies.Dovşan => "dovşan",
                PetSpecies.Quş => "quş",
                _ => "sevimli dost"
            };

            string energyText = energy switch
            {
                EnergyLevel.Yüksək => "çox enerjili",
                EnergyLevel.Orta => "orta aktivlikdə",
                EnergyLevel.Aşağı => "sakit təbiətli",
                _ => "mehriban"
            };

            string kidsText = isKidsFriendly ? "uşaqlarla əla yola gedir" : "sakit mühiti daha çox sevir";

            return $"{age} yaşlı bu {speciesName}, {energyText} bir xarakterə malikdir. O, {kidsText}.";
        }
    }
}