using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.PeopleEntities
{
    public interface IPeopleBlock
    {
        public int Add(People people);


        public bool Update(People people, Func<People, People> func);


        public bool Delete(People people);


        public People Get(int key);


        public List<People> GetAllValidatePeople();

    }
}
