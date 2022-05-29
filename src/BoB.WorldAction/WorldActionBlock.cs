using BoB.AutoMapperManager;
using System.Collections.Generic;
using BoB.PeopleEntities;
using Autofac;
using BoB.BoBContainManager;

namespace BoB.WorldAction
{
    public class WorldActionBlock : InitBlockService, IWorldActionBlock
    {
        private IAutoMapperService _autoMapperService;
        private IPeopleBlock _peopleBlock;


        protected override void Init()
        {
            _autoMapperService = CurrentServiceContainer.Resolve<IAutoMapperService>();
            _peopleBlock= CurrentServiceContainer.Resolve<IPeopleBlock>();
        }



        public List<string> AllSayHello()
        {
            var peoples = _peopleBlock.GetAllValidatePeople();
            List<string> data = new List<string>();

            foreach(var item in peoples)
            {
                data.Add(item.FullName + " say Hello!");
            }
            return data;
        }
    }
}
