using SouthBasement;

namespace TheRat.Characters.Stats
{
    public sealed class CharacterStaminaStats
    {
        public ObservableVariable<int> MaximumStamina { get; set; } = new(100);
        public ObservableVariable<int> Stamina { get; set; } = new(100);
        public float StaminaIncreaseRate { get; set; } = 0.1f;
    }
}