using Ardalis.Specification;
using Maintenance.Domain.Entity.ClientEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Application.Services.Client.Specification.AddressSpecification
{
    public class AddressSearchList : Specification<ClientAddress>
    {
        public AddressSearchList(Guid ClientId)
        {
            
            _ = Query.Where(x => x.ClientId == ClientId);
        }
    }
}
