using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Dtos.ResponseDtos
{
    public class ApiResponse <T> where T : class
    {
        public bool Status { get; set; }
        public T Data { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Info { get; set; }

    }
}
