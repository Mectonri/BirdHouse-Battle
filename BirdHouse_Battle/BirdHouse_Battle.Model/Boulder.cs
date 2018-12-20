using System;

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
                if (Position.X < Arena.Height && Position.X > - Arena.Height && 
                    Position.Y < Arena.Height && Position.Y > - Arena.Height)
                {
                    Arena.SpawnRock(int.Parse($"{Math.Round(Position.X, 0)}"), int.Parse($"{Math.Round(Position.Y, 0)}"));
                }
                DieNullContext();
            }
        }
    }
}