using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TableStorage.Commands
{
    public class CreatePolicy
    {
        public Guid Id { get; set; }
        public Guid ParentId { get; set; } 
        public string PolicyName { get; set; }
        public string Description { get; set; } 
    }
}
