using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace galaEatAPI.Services
{
    public class CommonService : ICommonService
    {
        IConfiguration _configuration;

       
        public CommonService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration GetConfiguration()
        {
            return _configuration;
        }
    }
}
