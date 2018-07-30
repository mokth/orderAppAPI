using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace galaCoreAPI.Model
{
    public class UserInfo
    {
        public string name;
        public string fullname;
        public string password;
        public string role;
        public string access;
        public string companyCode { get; set; }
      
    }

   
}
