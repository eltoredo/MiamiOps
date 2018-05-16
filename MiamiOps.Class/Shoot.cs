﻿using System;

namespace MiamiOps
{
    public class Shoot
    {
        float _speed;
        float _timeShot;
        TimeSpan _lifeSpan;
        DateTime _creationDate;

        Vector _playerPosition;
        Vector _mousePosition;
        Vector _bulletPosition;
        
        public Shoot(float timeShot, TimeSpan lifeSpan, float speed, Vector playerPosition, Vector mousePosition)
        {
            _speed = speed;
            _timeShot = timeShot;
            _lifeSpan = lifeSpan;
            _creationDate = DateTime.UtcNow;

            _playerPosition = playerPosition;
            _mousePosition = mousePosition;
            _bulletPosition = new Vector();
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

        public Vector MousePosition => _mousePosition;
        public Vector BulletPosition
        {
            get { return _bulletPosition; }
            set { _bulletPosition = value; }
        }
    }
}
