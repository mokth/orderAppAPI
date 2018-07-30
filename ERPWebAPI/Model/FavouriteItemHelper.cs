using galaCoreAPI.Entities;
using galaOrderAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace galaOrderAPI.Model
{
    public class FavouriteItemHelper
    {
        private readonly DataDbContect _context;
        private Customer cust;
        private List<FavouriteItem> favourites;
        private MakeOrder _makeorder;
        string _errmsg;

        public string Errmsg { get => _errmsg; set => _errmsg = value; }

        public FavouriteItemHelper(DataDbContect context)
        {
            _context = context;
        }

        public bool AddFavourites(MakeOrder order)
        {
            _errmsg = "";
            _makeorder = order;
            cust = _context.Customers.Where(x => x.CustomerCode == order.custcode).FirstOrDefault();
            if (cust == null)
            {
                _errmsg = "Invalid customer code...";
                return false;
            }
            poplulateItem();
            if (favourites == null || favourites.Count == 0)
            {
                _errmsg = "No item added...";
                return false;
            }

            string msg = "";
            bool success = _context.SaveFavouriteItems(cust.CustomerCode,favourites,ref msg);
            if (success)
                _errmsg = "Save!";
            else _errmsg = msg;
            return success;
        }


        void poplulateItem()
        {
            int lineno = 1;
            favourites = new List<FavouriteItem>();
            foreach (var itm in _makeorder.Items)
            {
                AddItem(itm, lineno);
            }
        }

        void AddItem(OrderItem itm,int lineno)
        {
            FavouriteItem item = new FavouriteItem();
            var masteritem =_context.Items.Where(x => x.ItemCode == itm.itemcode).FirstOrDefault();
            if (masteritem == null)
                return;
            item.CustomerCode = cust.CustomerCode;
            item.isProcessItem = masteritem.isProcessItem;
            item.ItemCode = masteritem.ItemCode;
            item.Qty = itm.qty;
            
            favourites.Add(item);

        }

       
    }
}
