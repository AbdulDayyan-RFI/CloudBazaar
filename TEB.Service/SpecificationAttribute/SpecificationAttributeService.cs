using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEB.Core.Common;
using TEB.Core.Domain;
using TEB.Data;

namespace TEB.Service
{
    public class SpecificationAttributeService : ISpecificationAttributeService
    {
        private readonly IGenericRepository<SpecificationAttribute> _specificationAttributeRepository;
        public SpecificationAttributeService(IGenericRepository<SpecificationAttribute> specificationAttributeRepository)
        {
            _specificationAttributeRepository = specificationAttributeRepository;
        }

        public Task<IEnumerable<SpecificationAttribute>> GetSpecificationAttributes(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = "select * from SpecificationAttribute order by DisplayOrder, Id";
            var param = new {  };
            var list = _specificationAttributeRepository.SqlQuery(query, param);
            return list;

        }
    }
}
