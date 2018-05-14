using System;

namespace MiamiOps
{
    public class Shoot
    {
        float _speed;
        float _timeShot;
        TimeSpan _lifeSpan;
        DateTime _creationDate;

        public Shoot(float timeShot, TimeSpan lifeSpan, float speed)
        {
            _speed = speed;
            _timeShot = timeShot;
            _lifeSpan = lifeSpan;
            _creationDate = DateTime.UtcNow;
        }

        public TimeSpan LifeSpan
        {
            get { return _lifeSpan; }
            set { _lifeSpan = value; }
        }

        public bool IsAlive
        {
            get
            {
                TimeSpan span = DateTime.UtcNow - _creationDate;
                return span < _lifeSpan;
            }
        }
    }
}
