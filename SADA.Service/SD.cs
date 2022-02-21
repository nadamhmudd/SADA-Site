using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADA.Service
{
    //Static - Constants Details
    public static class SD
    {
        public const string Ascending   = "ASC";
        public const string Descending  = "DESC";
        public const string Role_Client = "Client";
        public const string Role_Admin  = "Admin";

        public enum Status
        {
            Pending, Approved, Processing, Shipped, Delivered, Cancelled, Refunded
        };

        public enum PaymentOptions { Cash = 0, Visa = 1 }

    }
}
