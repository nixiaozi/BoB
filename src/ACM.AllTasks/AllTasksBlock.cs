using ACM.MainDatabase;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACM.AllTasksEntities
{
    public class AllTasksBlock:BaseBlock<AllTasks,Guid>,IBaseBlock<AllTasks,Guid>,IAllTasksBlock
    {

    }
}
