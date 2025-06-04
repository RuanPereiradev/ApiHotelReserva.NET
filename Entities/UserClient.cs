using System;

namespace WebApplication1.Entities
{
    public class UserClient
    {

        public int Id { get; set; }
        public required string UserName { get; set; }

        public required string Password { get; set; }
     

    }
}
