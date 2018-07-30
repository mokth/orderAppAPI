using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using galaCoreAPI.Authentication;
using galaCoreAPI.Entities;
using galaCoreAPI.Model;
using galaOrderAPI.Entities;
using galaOrderAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MRP.BL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace galaOrderAPI.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Route("api/[controller]")]
    public class ItemController : Controller
    {
        private readonly DataDbContect _context;
       
        private readonly IJwtFactory _jwtFactory;

        public ItemController(DataDbContect context, IJwtFactory jwtFactory)
        {
            _context = context;
            _jwtFactory = jwtFactory;

        }
        // GET: api/<controller>
        [HttpGet]
        public JsonResult Get()
        {
            var data=  _context.vItems
                 .Select(x =>
              new SellItem
              {
                  ID = x.ID,
                  imageUrl = "itemImages/" + ((string.IsNullOrEmpty(x.imageUrl)) ? "empty.png" : x.imageUrl) ,
                  imageUrlprc = "itemImages/" + ((string.IsNullOrEmpty(x.imageUrlprc))?"empty.png": x.imageUrlprc),
                  isProcessItem = x.isProcessItem,
                  ItemCategory = x.ItemCategory,
                  ItemCode = x.ItemCode,
                  ItemName = x.ItemName,
                  ItemNameCN = x.ItemNameCN,
                  ItemNameprc = x.ItemNameprc,
                  ItemNameCNprc = x.ItemNameCNprc,
                  ItemStatus = x.ItemStatus,
                  ItemType = x.ItemType,
                  ItemUom = x.ItemUom,
                  ItemUomprc =x.ItemUomprc,
                  ProcessItemCode = x.ProcessItemCode
              }
            ).ToList();
            JsonResult dataJson = new JsonResult(data);
            return dataJson;
        }

        [HttpGet, Route("refs")]
        public JsonResult GetRefs()
        {
            var list = _context.ItemCategorys.Select(x=>
                 new ReferencCode()
                 {
                      code = x.Code,
                      description =x.Description
                 }
                );
            JsonResult dataJson = new JsonResult(list);
            return dataJson;
        }

        [HttpGet, Route("favouriteitems/{code}")]
        public JsonResult GetFavuoriteItems(string code)
        {
            var list = _context.vFavourites.Where(x => x.CustomerCode == code)
                       .Select(x =>
                           new vFavourite()
                           {
                               CustomerCode = x.CustomerCode,
                               Qty = x.Qty,
                               ID = x.ID,
                               ItemCode = x.ItemCode,
                               imageUrl = "itemImages/"  +((string.IsNullOrEmpty(x.imageUrl)) ? "empty.png" : x.imageUrl),
                               ItemName = x.ItemName,
                               ItemNameCN = x.ItemNameCN,
                               ItemUom = x.ItemUom,
                               imageUrlprc = "itemImages/" + ((string.IsNullOrEmpty(x.imageUrlprc)) ? "empty.png" : x.imageUrlprc),
                               ItemNameCNprc = x.ItemNameCNprc,
                               ItemNameprc = x.ItemNameprc,
                               ProcessItemCode = x.ProcessItemCode,
                               Qtyprc = x.Qtyprc,
                               Uomprc = x.Uomprc
                           }
                       );                    
                        
                
            JsonResult dataJson = new JsonResult(list);
            return dataJson;
        }

        // POST api/<controller>
        [HttpPost, Route("favourites")]
        public JsonResult FavuoriteItems([FromBody] MakeOrder order)
        {
            FavouriteItemHelper hlp = new FavouriteItemHelper(_context);
            bool success = hlp.AddFavourites(order);

            JsonResult restultJson = Json(new
            {
                ok = (success) ? "yes" : "no",
                error = hlp.Errmsg
            });

            return restultJson;
        }


    }
}
