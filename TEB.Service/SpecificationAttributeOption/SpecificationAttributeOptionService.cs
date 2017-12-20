using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEB.Core.Domain;
using TEB.Data;

namespace TEB.Service
{
    public class SpecificationAttributeOptionService : ISpecificationAttributeOptionService
    {
        private readonly IGenericRepository<SpecificationAttributeOption> _specificationAttributeOptionRepository;
        public SpecificationAttributeOptionService(IGenericRepository<SpecificationAttributeOption> specificationAttributeOptionRepository)
        {
            _specificationAttributeOptionRepository = specificationAttributeOptionRepository;
        }

        public Task<IEnumerable<SpecificationAttributeOption>> GetSpecificationAttributeOptionsBySpecificationAttribute(int specificationAttributeId)
        {
            var query = "select * SpecificationAttributeOption where SpecificationAttributeId=@specificationAttributeId order by DisplayOrder, Id";
            var param = new { specificationAttributeId = specificationAttributeId };
            var list = _specificationAttributeOptionRepository.SqlQuery(query, param);
            return list;
        }

    }
}
