using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIRecommender.Entities;

namespace AIRecommender.Dataloader
{
    public interface IDataLoader
    {
        public BookDetails Load();
    }
}
