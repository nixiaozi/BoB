using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.EFDbContext
{
    public interface IModelCreator
    {
        void CreateModel(ModelBuilder builder);
    }
}
