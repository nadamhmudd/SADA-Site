using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADA.Service
{
    //Static - Constants Details
    public partial class SD
    {
        public const string Ascending       = "ASC";
        public const string Descending      = "DESC";
        public const string Role_Client     = "Client";
        public const string Role_Admin      = "Admin";

        public const string StatusPending   = "Pending";
        public const string StatusApproved  = "Approved";
        public const string StatusInProcess = "Processing";
        public const string StatusShipped   = "Shipped";
        public const string StatusDelivered = "Delivered";
        public const string StatusCancelled = "Cancelled";
        public const string StatusRefunded  = "Refunded";

        public enum PaymentOptions { Cash = 0, Visa = 1}

    }
}
