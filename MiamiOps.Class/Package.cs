﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiamiOps
{
    public class Package : IStuff
    {

        Vector _place;
        float _width;
        float _height;
        TimeSpan _lifeSpan;
        DateTime _creationDate;
        private Random random = new Random();
        Round _ctx;
        string _name;
        int _id;

        public Package()
        {
           
        }

        public Package(string name, TimeSpan lifeSpan, int id,float width=0, float height=0)
        {
            this._place = CreateRandomPosition();
            this._width = width;
            this._height = height;
            this._lifeSpan = lifeSpan;
            this._name = name;
            this._id = id;

        }

        internal float GetNextRandomFloat()
        {
            return ((float)this.random.NextDouble() * 2) - 1;
        }

        Vector CreateRandomPosition()
        {
            return new Vector(GetNextRandomFloat(), GetNextRandomFloat());
        }

        public void WalkOn()
        {
            throw new NotImplementedException();
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

    public class PackageFactory : IStuffFactory
    {
        readonly string _name;
       readonly TimeSpan _lifeSpan;
       readonly int _id;



        public PackageFactory(string name, TimeSpan lifeSpan, int id)
        {
            _name = name;
            _lifeSpan = lifeSpan;
            _id = id;
        }

        public IStuff Create()
        {
            return new Package(_name, _lifeSpan, _id);
        }

        public void WalkOn()
        {
            throw new NotImplementedException();
        }
    }
}

