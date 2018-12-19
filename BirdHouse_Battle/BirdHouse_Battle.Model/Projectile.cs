using Newtonsoft.Json;
using System;

namespace BirdHouse_Battle.Model
{
    public abstract class Projectile : IDisposable
    {
        Arena _arena;
        Vector _start;
        Vector _end;
        Vector _position;
        Vector _direction;
        Vector _mouvement;

        int _name;
        int _nbFram;
        int _damages;
        int _range;

        bool _arrived;

        protected Projectile(Arena arena, Vector start, Vector end, int nbFram, 
                             int damages, int range, int name)
        {
            _arena = arena;
            _start = start;
            _position = start;
            _end = end;
            _nbFram = nbFram;
            _damages = damages;
            _range = range;
            _arrived = false;
            _name = name;
        }

        [JsonIgnore]
        public Arena Arena { get { return _arena; } }

        public Vector Start { get { return _start; } }

        public Vector End { get { return _end; } }

        public Vector Position { get { return _position; } }

        public Vector Direction { get { return _direction; } }

        public Vector Mouvement { get { return _mouvement; } }

        public int Name { get { return _name; } }

        public int NbFram { get { return _nbFram; } }

        public int Damages { get { return _damages; } }

        public int Range { get { return _range; } }

        public bool Arrived { get { return _arrived; } }

        protected virtual void Dispose(bool disposing) { }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        public void SetDirection()
        {
            _direction = Vector.Soustract(Position, End);
            _mouvement = Vector.MoveProjectile(NbFram, Direction);
            _nbFram--;
        }

        public void ProjectileCharge()
        {
            _position = Vector.Add(Mouvement, Position);
        }

        public void DieNullContext()
        {
            _arena = null;
            _arrived = true;
        }

        public void GiveDamages()
        {
            Arena.IsUnitInRange(this, Damages);
        }

        public void GiveDamagesAoe()
        {
            Arena.IsUnitInRangeAoe(this, Damages);
        }

        public void FinalPosition()
        {
            _position = End;
        }

        public virtual void Update() { }
    }
}
