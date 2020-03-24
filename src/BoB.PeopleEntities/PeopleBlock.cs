using BoB.AutoMapperManager;
using BoB.ContainManager;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using BoB.MainDatabase;
using System.Linq;
using System.Linq.Expressions;
using BoB.EFDbContext.Enums;

namespace BoB.PeopleEntities
{
    public class PeopleBlock : BaseBlock, IPeopleBlock
    {
        private IAutoMapperService _autoMapperService;

        protected override void Init()
        {
            _autoMapperService = CurrentServiceProvider.GetService<IAutoMapperService>();
        }

        public int Add(People people)
        {
            using (var context = new MainDbContext())
            {
                context.Add<People>(people);
                context.SaveChanges();

                return people.Key;
            }

        }

        public bool Delete(People people)
        {
            return Update(people, s =>
            {
                s.DataStatus = DataStatus.Delete;
                return s;
            });
        }

        public People Get(int key)
        {
            using (var context = new MainDbContext())
            {
                return context.Set<People>().FirstOrDefault(s => s.Key == key);
            }
        }

        public List<People> GetAllValidatePeople()
        {
            using (var context = new MainDbContext())
            {
                return context.Set<People>().Where(s=>s.DataStatus== DataStatus.Normal).ToList();
            }
        }

        public bool Update(People people,Func<People,People> func)
        {
            using (var context = new MainDbContext())
            {
                var data= context.Set<People>().FirstOrDefault(s => s.Key == people.Key);
                func?.Invoke(data);

                context.SaveChanges();

                return true;
            }
        }
    }
}
