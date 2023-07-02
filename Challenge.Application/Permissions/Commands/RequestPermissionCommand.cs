using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Application.Permissions.Commands
{
    public class RequestPermissionCommand: IRequest<long>
    {
        public string Name { get; set; } = string.Empty;
        public long EmployeeId { get; set; }
        public long PermissionTypeId { get; set; }
    }
}
