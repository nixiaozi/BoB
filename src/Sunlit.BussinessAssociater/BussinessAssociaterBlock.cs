using Sunlit.BussinessAssociaterEntities.Model;
using Sunlit.MainDatabase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunlit.BussinessAssociaterEntities
{
    public class BussinessAssociaterBlock: BaseBlock<BussinessAssociater, Guid>, 
        IBaseBlock<BussinessAssociater, Guid>, IBussinessAssociaterBlock
    {

    }
}
