using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int Role { get; set; }
        public string Token { get; set; }
    }
}