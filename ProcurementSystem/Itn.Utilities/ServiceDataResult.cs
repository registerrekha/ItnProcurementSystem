using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itn.Utilities
{
    public class ServiceDataResult<T> where T : class
    {
        public int TotalResults { get; set; }
        public List<T> Data { get; set; }
        public string Status { get; set; }

        public static ServiceDataResult<T> Create(List<T> data, string status)
        {
            return new ServiceDataResult<T>
            {
                Data = data,
                TotalResults = (data != null) ? data.Count : 0,
                Status = status
            };
        }
    }
}
