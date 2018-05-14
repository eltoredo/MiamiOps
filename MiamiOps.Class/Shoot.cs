using System;

namespace MiamiOps
{
    public class Shoot
    {
        float _speed;
        float _timeShot;
        float _lifeTime;

        public Shoot(float timeShot, float lifeTime = 0.5f, float speed = 0.1f)
        {
            _speed = speed;
            _timeShot = timeShot;
        }

        public float LifeTime
        {
            get { return _lifeTime; }
            set { _lifeTime = value; }
        }
    }
}
