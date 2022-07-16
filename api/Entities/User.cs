using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Enums;

namespace api.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public UserRole Role { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public ICollection<Reservation>? Reservations { get; set; }
    }
}