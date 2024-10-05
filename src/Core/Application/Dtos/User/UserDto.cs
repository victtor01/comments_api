using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tasks_api.src.Core.Application.Dtos.User
{
    public class UserDto
    {
        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }
    }
}
