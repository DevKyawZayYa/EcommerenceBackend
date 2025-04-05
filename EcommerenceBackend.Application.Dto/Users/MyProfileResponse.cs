using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.Dto.Users
{
    namespace EcommerenceBackend.Application.Dto.Response
    {
        public class MyProfileResponse
        {
            public string Id { get; set; } = default!;
            public string Name { get; set; } = default!;
        }
    }

}
