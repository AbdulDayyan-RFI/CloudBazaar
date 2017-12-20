using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEB.Core.Domain;

namespace TEB.Service
{
    public interface ISpecificationAttributeOptionService
    {
        Task<IEnumerable<SpecificationAttributeOption>> GetSpecificationAttributeOptionsBySpecificationAttribute(int specificationAttributeId);
    }
}
