using NicholasHalmagyiFilip.DataModelCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicholasHalmagyiFilip.DomainCore.CorrelationService
{
    public abstract class BaseCorrelationService<T> 
    {
        protected readonly AppDbContext DataSet;

        protected BaseCorrelationService(AppDbContext dataSet)
        {
            this.DataSet = dataSet;
        }

        public abstract T CorrelateData();
    }
}