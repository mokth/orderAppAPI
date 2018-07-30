using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using galaCoreAPI.Authentication;
using galaCoreAPI.Authentication.Model;
using galaCoreAPI.Model;
using galaOrderAPI.Entities;
using galaOrderAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace galaOrderAPI.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IJwtFactory _jwtFactory;
        private readonly DataDbContect _context;
      

        public OrderController(DataDbContect context, IHostingEnvironment hostingEnvironment, IJwtFactory jwtFactory)
        {
            _hostingEnvironment = hostingEnvironment;
            _jwtFactory = jwtFactory;            
            _context = context;           
        }

        // GET: api/<controller>
        [HttpGet]
        public JsonResult Get()
        {
            UserInfo user = _jwtFactory.DecodedRequestAuth(Request);
            var data = _context.OrderHdrs
                .Where(x=>x.CustomerCode ==user.companyCode)
                .Select(x =>
             new SalesOrder
             {
                 id = x.ID,
                 AccountStatus = x.AccountStatus,
                 CustomerCode = x.CustomerCode,
                 CustomerName = x.CustomerName,
                 OrderDate = x.OrderDate,
                 OrderNo = x.OrderNo,
                 OrderStatus = x.OrderStatus,
                 OrderType = x.OrderType,
                 Remark = x.Remark,
                 ResellerCode = x.ResellerCode,
                 updateDate=x.UpdateDate,
                 createDate =x.CreateDate

             }
           ).ToList();
            JsonResult dataJson = new JsonResult(data);
            return dataJson;
        }


        // GET api/<controller>/5
        [HttpGet("items/{code}")]
        public JsonResult Get(string code)
        {
            code = code.Replace('-', '/');
            var data = _context.vOrderItems.Where(x => x.OrderNo == code)
                .Select(x => new galaCoreAPI.Entities.vOrderItems()
                {
                    ID = x.ID,
                    ItemCategory = x.ItemCategory,
                    ItemCode = x.ItemCode,
                    ItemName = x.ItemName,
                    ItemNameCN = x.ItemNameCN,
                    ItemUom = x.ItemUom,
                    OrderLine = x.OrderLine,
                    OrderNo = x.OrderNo,
                    Quantity = x.Quantity,
                    imageUrl = "itemImages/" + x.imageUrl,

                });
            JsonResult dataJson = new JsonResult(data);
            return dataJson;
        }

        // POST api/<controller>
        [HttpPost, Route("makeorder")]
        public JsonResult Post([FromBody] MakeOrder order)
        {
            OrderHelper hlp = new OrderHelper(_context);
            int success = hlp.MakeOrder(order);

            JsonResult restultJson = Json(new
            {
                ok = (success==0) ? "yes" : "no",
                returncode = success,
                error = hlp.Errmsg
            });

            return restultJson;
        }

       
    }
}
