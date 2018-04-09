using System;

namespace MiamiOps
{
public struct Vector
    {
        private double _abs;
        private double _ord;

        public Vector(double abs, double ord)
        {
            this._abs = abs;
            this._ord = ord;
        }

        public double X
        {
            get {return this._abs;}
            set {this._abs = value;}
        }
        public double Y
        {
            get {return this._ord;}
            set {this._ord = value;}
        }

        public Vector Sub(Vector v)
        {
            return new Vector(this._abs - v._abs, this._ord - v._ord);
        }
        public double Magnitude => Math.Sqrt(this._abs * this._abs + this._ord * this._ord);
        public Vector Mul(double n) => new Vector(this._abs * n, this._ord * n);
        public Vector Add(Vector v) => new Vector(this._abs + v._abs, this._ord + v._ord);

    }
}
