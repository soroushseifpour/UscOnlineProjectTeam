using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace UscProject.Areas.KarFarma.ViewModel
{
    public class OrderDetails
    {
        public int OrderID;
        public string boxname;
        public string UserName;
        public string Email;
        public string ComapnyName;
        public DateTime? datebuy;
        public int? UnitPrice;
        public int Price;
        public Int64? PhoneNumber;
        public string webSite;
        public string address;
        public bool status;
    }
}