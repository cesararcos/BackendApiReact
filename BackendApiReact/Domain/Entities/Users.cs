using System;
using System.Collections.Generic;

namespace BackendApiReact.Domain.Entities
{
    public partial class Users
    {
        public int Id { get; set; }
        public string? Usuario { get; set; }
        public string? Contrasena { get; set; }
        public string? Rol { get; set; }
        public int? TaskId { get; set; }
        public int? State { get; set; }

        public virtual States? StateNavigation { get; set; }
        public virtual Tasks? Task { get; set; }
    }
}
