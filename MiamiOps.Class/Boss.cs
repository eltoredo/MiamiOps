using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiamiOps
{

    public class Boss : Enemies
    {

        public Boss(Round context, int name, Vector place, float life, float speed, float attack,float width = 0, float height = 0 ,int pattern = 0) : base(context, name, place, life, speed, attack, width, height)
        {
    
            if (pattern == 0 && _life < _maxLife / 3)
            {
                _life = _maxLife;
                this._speed = this._speed * 2;

            }

        }

    }
}
 