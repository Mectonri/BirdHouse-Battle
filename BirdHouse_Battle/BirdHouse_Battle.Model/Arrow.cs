﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdHouse_Battle.Model
{
    class Arrow : Projectile
    {
        public Arrow(Arena arena, Vector start, Vector end, int Name, int NbFram)
            : base(arena, start, end, NbFram, 8, 10, Name)
        {
        }

        public override void Update()
        {
            SetDirection();
            ProjectileCharge();

            if(NbFram == 0)
            {
                FinalPosition();
                GiveDamages();
                DieNullContext();
            }
        }
    }
}
