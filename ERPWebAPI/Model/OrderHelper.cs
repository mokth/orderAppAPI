using galaCoreAPI.Entities;
using galaOrderAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace galaOrderAPI.Model
{
    public class OrderHelper
    {
        private readonly DataDbContect _context;
        private Customer cust;
        private OrderHdr orderhdr;
        private NumberRunNO runnum;
        private List<OrderDtl> orderitem;
        private MakeOrder _makeorder;
        string _errmsg;

        public string Errmsg { get => _errmsg; set => _errmsg = value; }

        public OrderHelper(DataDbContect context)
        {
            _context = context;
        }

        public int MakeOrder(MakeOrder order)
        {
            _errmsg = "";
            _makeorder = order;
            cust = _context.Customers.Where(x => x.CustomerCode == order.custcode).FirstOrDefault();
            if (cust == null)
            {
                _errmsg = "Invalid customer code...";
                Console.WriteLine("Invalid customer code...{0}", order.custcode);
                return 1;
            }
            poplulateOrder();
            if (orderitem == null || orderitem.Count == 0)
            {
                _errmsg = "No item added...";
                Console.WriteLine("No item added...{0}", order.custcode);
                return 2;
            }

            var settings = _context.OrderSetting.FirstOrDefault();
            TimeSpan time = new TimeSpan(15, 0, 0);
            int count = 1;
            if (settings != null)
            {
                count = settings.maxorderperday;
                time = settings.beforeOrdertime;
            }
            TimeSpan ordertime = new TimeSpan(order.orderDate.Value.Hour, order.orderDate.Value.Minute, 0);

           
            if (ordertime > time) //> 3
            {
                _errmsg = "Client has exceeded timeout";
                Console.WriteLine("Client has exceeded timeout. CustCode {0}  Setting Time {1}  order time {2}", order.custcode, time, ordertime);
                return 3;
            }

            var oldorder = _context.OrderHdrs.Where(x => x.OrderDate.Value.Year == order.orderDate.Value.Year && 
                                                    x.OrderDate.Value.Month == order.orderDate.Value.Month &&
                                                    x.CustomerCode == order.custcode &&
                                                    x.OrderDate.Value.Day == order.orderDate.Value.Day).Count();
           // Console.WriteLine("{0} Db order count: {1} {2}", order.custcode);
            if (oldorder >=count)
            {
                _errmsg = "Only 1 order per day. Order reject.";
                Console.WriteLine("Only 1 order per day. Order reject....{0}", order.custcode);
                return 4;
            }

            string msg = "";
            bool success = _context.SaveNewOrder(orderhdr, orderitem, runnum, ref msg);
            if (success)
                _errmsg = orderhdr.OrderNo;
            else _errmsg = msg;
            return success?0:-1;
        }

       void poplulateOrder()
        {
            orderhdr = new OrderHdr();
            orderhdr.CreateBy = "CUST";
            orderhdr.CreateDate = DateTime.Now;
            orderhdr.AccountStatus = "";
            orderhdr.CustomerCode = cust.CustomerCode;
            orderhdr.CustomerName = cust.CustomerName;
            orderhdr.OrderDate = DateTime.Today;
            orderhdr.OrderNo = getSONO();
            orderhdr.OrderStatus = "NEW";
            orderhdr.OrderType = "";
            orderhdr.ResellerCode = cust.Reseller;
            
            poplulateItem();
        }

        void poplulateItem()
        {
            int lineno = 1;
            orderitem = new List<OrderDtl>();
            foreach (var itm in _makeorder.Items)
            {
                AddItem(itm, lineno);
            }
        }

        void AddItem(OrderItem itm,int lineno)
        {
            OrderDtl item = new OrderDtl();
            var masteritem =_context.Items.Where(x => x.ItemCode == itm.itemcode).FirstOrDefault();
            if (masteritem == null)
                return;

            item.OrderNo = orderhdr.OrderNo;
            item.OrderLine = lineno;
            item.ItemCode = itm.itemcode;
            item.ItemName = masteritem.ItemName;
            item.ItemUom = masteritem.ItemUom;
            item.Quantity = itm.qty;
            orderitem.Add(item);

        }

        string getSONO()
        {
            DateTime date = DateTime.Today;
            return GenerateAutoNumberWithDate("SO", date.Year, date.Month);
        }
        public string GenerateAutoNumberWithDate(String prefix, int year, int month)
        {
            string predelimeter = "/";

            string result = "";
             runnum = _context.NumberRunNOs.Where(x => x.NumCd == "SO" && x.Year == year && x.Month == month).FirstOrDefault();
            if (runnum == null)
            {
                runnum = new NumberRunNO();
                runnum.Created = DateTime.Now;
                runnum.Month = Convert.ToInt16(month);
                runnum.NumCd = "SO";
                runnum.NumDes = "SALE ORDRER";
                runnum.Prefix = "SO";
                runnum.Seq = 1;
                runnum.TotLength = 7;
                runnum.Year = Convert.ToInt16(year);
                runnum.isNewMode = true;
            }
            else
            {
                runnum.Seq = runnum.Seq + 1;
                runnum.isNewMode = false;
            }

            result = runnum.Prefix + year.ToString().Substring(2, 2) + month.ToString().PadLeft(2, '0') + predelimeter + runnum.Seq.ToString().PadLeft(Convert.ToInt32(runnum.TotLength), '0');


            return result;
        }
    }
}
