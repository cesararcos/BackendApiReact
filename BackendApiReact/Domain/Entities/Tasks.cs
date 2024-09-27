using System;
using System.Collections.Generic;

namespace BackendApiReact.Domain.Entities
{
    public partial class Tasks
    {
        public Tasks()
        {
            Users = new HashSet<Users>();
        }

        public int Id { get; set; }
        public string? Description { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
