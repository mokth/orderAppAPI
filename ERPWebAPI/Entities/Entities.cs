using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace galaCoreAPI.Entities
{
    [Table("Customer")]
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        public string ckey { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string Reseller { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }

    }

    [Table("Item")]
    public class Item
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        public string ckey { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemNameCN { get; set; }
        public string ItemType { get; set; }
        public string ItemCategory { get; set; }
        public string ItemUom { get; set; }
        public string ItemStatus { get; set; }
        public string ProcessItemCode { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public bool? isProcessItem { get; set; }
        public string imageUrl { get; set; }

    }

    [Table("NumberRunNO")]
    public class NumberRunNO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string NumCd { get; set; }
        public string NumDes { get; set; }
        public int TotLength { get; set; }
        public string Prefix { get; set; }
        public int Seq { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public string UserID { get; set; }
        [NotMapped] // dirty flag
        public bool isNewMode { get; set; }

    }

    [Table("OrderHdr")]
    public class OrderHdr
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        public string ckey { get; set; }
        public string OrderNo { get; set; }
        public string OrderStatus { get; set; }
        public DateTime? OrderDate { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string ResellerCode { get; set; }
        public string AccountStatus { get; set; }
        public string OrderType { get; set; }
        public string Remark { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
    }

    [Table("OrderDtl")]
    public class OrderDtl
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        public string ckey { get; set; }
        public string OrderNo { get; set; }
        public int? OrderLine { get; set; }
        public int? Line { get; set; }
        public string ItemCode { get; set; }
        public string ItemUom { get; set; }
        public string ItemName { get; set; }
        public decimal? Quantity { get; set; }
        public string Remark { get; set; }

    }

    [Table("FavouriteItem")]
    public class FavouriteItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        public string CustomerCode { get; set; }
        public string ItemCode { get; set; }
        public decimal? Qty { get; set; }
        public bool? isProcessItem { get; set; }

    }

    [Table("CustAcct")]
    public class CustAcct
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string ID { get; set; }
        public string Name { get; set; }
        public string PWord { get; set; }
        public string Status { get; set; }
        public string UserType { get; set; }
        public string CustomerCode { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public bool? chgpass { get; set; }

    }

    [Table("ItemCategory")]
    public class ItemCategory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]      
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
    }

    [Table("OrderSettings")]
    public class OrderSettings
    {
        public int id { get; set; }
        public TimeSpan beforeOrdertime { get; set; }
        public int maxorderperday { get; set; }
    }

    public class vItems
    {
        public int ID { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemNameCN { get; set; }
        public string ItemType { get; set; }
        public string ItemCategory { get; set; }
        public string ItemUom { get; set; }
        public string ItemStatus { get; set; }
        public string ProcessItemCode { get; set; }
        public bool? isProcessItem { get; set; }
        public string imageUrl { get; set; }
        public string imageUrlprc { get; set; }
        public string ItemNameprc { get; set; }
        public string ItemNameCNprc { get; set; }
        public string ItemUomprc { get; set; }


    }

    public class vOrderItems
    {
        public int ID { get; set; }
        public string OrderNo { get; set; }
        public int? OrderLine { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemNameCN { get; set; }
        public string ItemCategory { get; set; }
        public string ItemUom { get; set; }
        public string imageUrl { get; set; }
        public decimal? Quantity { get; set; }
    }

    public class vFavourite
    {
        public int ID { get; set; }
        public string CustomerCode { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemNameCN { get; set; }
        public string ItemUom { get; set; }
        public string imageUrl { get; set; }
        public decimal? Qty { get; set; }
        public string ProcessItemCode { get; set; }
        public string ItemNameprc { get; set; }
        public string ItemNameCNprc { get; set; }
        public string Uomprc { get; set; }
        public string imageUrlprc { get; set; }
        public decimal? Qtyprc { get; set; }
    }
}
