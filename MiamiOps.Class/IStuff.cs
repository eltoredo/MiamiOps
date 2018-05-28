using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiamiOps
{
    public interface IStuff
    {
        void WalkOn();
    }

    public interface IStuffFactory
    {
        IStuff Create();
    }
}
