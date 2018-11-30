namespace BirdHouse_Battle.Model
{
    public class Boulder : Projectile
    {
        public Boulder(Arena arena, Vector start, Vector end, int Name, int NbFram)
            : base(arena, start, end, NbFram, 20, 30, Name)
        {
        }

        public override void Update()
        {
            SetDirection();
            ProjectileCharge();

            if (NbFram == 0)
            {
                FinalPosition();
                GiveDamagesAoe();
                DieNullContext();
            }
        }
    }
}