
using MediatR;

using System.Collections.Generic;

namespace Crm.Application.Contacts.GetContactTypes
{
    public class GetContactTypesQuery : IRequest<List<ContactTypesDto>>
    {
        public GetContactTypesQuery()
        {

        }
    }
}
