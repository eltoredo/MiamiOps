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

        public Package(Round ctx, string name, TimeSpan lifeSpan,float width=0, float height=0)
        {
            _ctx = ctx;

            this._name = name;
            this._place = CreateRandomPosition();
            this._width = width;
            this._height = height;
            this._lifeSpan = lifeSpan;
            this._name = name;
            this._creationDate = DateTime.UtcNow;
        }

        internal float GetNextRandomFloat()
        {
            return ((float)this.random.NextDouble() * 2) - 1;
        }

        Vector CreateRandomPosition()
        {
            return new Vector(GetNextRandomFloat(), GetNextRandomFloat());
        }

        public void WalkOn(Round Ctx)
        {
            int count = -1;
            bool exist = false;
            foreach (var item in Ctx.PackageEffectList)
            {
                count++;
                if (item.Name == this.Name)
                {
                    exist = true;
                }
            }
            if (this.Name == "health")
            {
                Ctx.Player.LifePlayer += 20;
                if (Ctx.Player.LifePlayer > Ctx.Player.LifePlayerMax) Ctx.Player.LifePlayer = Ctx.Player.LifePlayerMax;
                Ctx.StuffList.Remove(this);
            }

            if(this.Name == "point")
            {
                Ctx.Player.Points += 200;
                Ctx.StuffList.Remove(this);
            }

            if(this.Name == "speed")
            {
                if (exist == true)
                {
                    Ctx.PackageEffectList[count].LifeSpan = TimeSpan.FromSeconds(5);
                    Ctx.PackageEffectList[count].CreationDate = DateTime.UtcNow;
                }
                else
                {
                    Ctx.Player.Speed += 0.005f;
                    this.LifeSpan = TimeSpan.FromSeconds(5);
                    this.CreationDate = DateTime.UtcNow;
                    Ctx.PackageEffectList.Add(this);
                }
                Ctx.StuffList.Remove(this);
            }

            if(this.Name == "brute")
            {
                if (exist == true)
                {
                    Ctx.PackageEffectList[count].LifeSpan = TimeSpan.FromSeconds(10);
                    Ctx.PackageEffectList[count].CreationDate = DateTime.UtcNow;
                }
                else
                {
                    Ctx.Player.Effect = "brute";
                    this.LifeSpan = TimeSpan.FromSeconds(10);
                    this.CreationDate = DateTime.UtcNow;
                    Ctx.PackageEffectList.Add(this);
                }
                Ctx.StuffList.Remove(this);
            }
            if (this.Name == "pyro_fruit")
            {
                if (exist == true)
                {
                    Ctx.PackageEffectList[count].LifeSpan = TimeSpan.FromSeconds(15);
                    Ctx.PackageEffectList[count].CreationDate = DateTime.UtcNow;
                }
                else
                {
                    Ctx.Player.Effect = "pyro_fruit";
                    this.LifeSpan = TimeSpan.FromSeconds(15);
                    this.CreationDate = DateTime.UtcNow;
                    Ctx.PackageEffectList.Add(this);
                }
                Ctx.StuffList.Remove(this);
            }
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
        public DateTime CreationDate
        {
            get { return _creationDate; }
            set { _creationDate = value; }
        }

        public string Name => _name;

        public Vector Position => _place;
    }

    public class PackageFactory : IStuffFactory
    {
        Round _ctx;
        readonly string _name;
        readonly TimeSpan _lifeSpan;

        public PackageFactory(Round ctx, string name, TimeSpan lifeSpan)
        {
            _ctx = ctx;
            _name = name;
            _lifeSpan = lifeSpan;
        }

        public IStuff Create()
        {
            return new Package(_ctx, _name, _lifeSpan);
        }
    }
}

