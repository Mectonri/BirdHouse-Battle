namespace BirdHouse_Battle.Model
{
    public class BalisticAmmo : Projectile
    {
        public BalisticAmmo(Arena arena, Vector start, Vector end, int Name, int NbFram)
            : base(arena, start, end, NbFram, 15, 15, Name)
        {
        }

        public override void Update()
        {
            SetDirection();
            ProjectileCharge();

            if (NbFram == 0)
            {
                FinalPosition();
                GiveDamages();
                DieNullContext();
            }
        }
    }
}