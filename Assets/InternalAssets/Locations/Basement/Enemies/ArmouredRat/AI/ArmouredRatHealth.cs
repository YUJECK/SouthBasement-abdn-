using SouthBasement.AI;

namespace SouthBasement.Basement.Enemies.ArmouredRat.AI
{
    public sealed class ArmouredRatHealth : DefaultEnemyHealth
    {
        public bool CurrentDefends { get; set; }

        public override void Damage(int damage, string[] args)
        {
            if(!CurrentDefends)
                base.Damage(damage, args);
        }
    }
}