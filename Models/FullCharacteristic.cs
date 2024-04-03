using Microsoft.EntityFrameworkCore;

namespace DiscountElectronicsApi.Models
{
    [Keyless]
    public class FullCharacteristic
    {
        public int? IdCharacteristics { get; set; }
        public string? Values { get; set; }
        public string? CharacteristicName { get; set; }
        public int? IdCategory { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Characteristic characteristic &&
                   IdCharacteristics == characteristic.IdCharacteristics;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdCharacteristics);
        }
    }
}
