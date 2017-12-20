using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEB.Core.Common;
using TEB.Core.Domain;

namespace TEB.Service
{
    public interface ISpecificationAttributeService
    {
        Task<IEnumerable<SpecificationAttribute>> GetSpecificationAttributes(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
