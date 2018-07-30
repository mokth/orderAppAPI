using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace galaOrderAPI.Entities
{
    public class MakeOrder
    {
        public DateTime? orderDate { get; set; }
        public string custcode { get; set; }
        public OrderItem[] Items { get; set; }
    }

    public class OrderItem
    {
        public string itemcode { get; set; }
        public decimal? qty { get; set; }
    }

   

    public class SellItem
    {
        public int ID { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemNameCN { get; set; }
        public string ItemType { get; set; }
        public string ItemCategory { get; set; }
        public string ItemUom { get; set; }
        public string ItemUomprc { get; set; }
        public string ItemStatus { get; set; }
        public string ProcessItemCode { get; set; }
        public bool? isProcessItem { get; set; }
        public string imageUrl { get; set; }
        public string imageUrlprc { get; set; }
        public string ItemNameprc { get; set; }
        public string ItemNameCNprc { get; set; }

    }

    public class ReferencCode
    {
        public string code { get; set; }
        public string description { get; set; }
    }

    public class SalesOrder
    {
        public int id { get; set; }
        public string OrderNo { get; set; }
        public string OrderStatus { get; set; }
        public DateTime? OrderDate { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string ResellerCode { get; set; }
        public string AccountStatus { get; set; }
        public string OrderType { get; set; }
        public string Remark { get; set; }
        public DateTime? updateDate { get; set; }
        public DateTime? createDate { get; set; }

    }
}
