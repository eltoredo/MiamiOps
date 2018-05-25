using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiamiOps
{
    public interface IStuff
    {


    }

    public class IStuffFactory
    {
        public IStuffFactory()
        {

        }


        public IStuff CreateIStuff()
        {
            throw new NotImplementedException();
        }

    }
}
