using System;
using System.Collections.Generic;

namespace BackendApiReact.Domain.Entities
{
    public partial class States
    {
        public States()
        {
            Users = new HashSet<Users>();
        }

        public int Id { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
