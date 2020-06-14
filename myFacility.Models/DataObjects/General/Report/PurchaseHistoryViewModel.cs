using System;
using myFacility.Models.DataObjects.User;

namespace myFacility.Models.DataObjects.Report
{
    public class PurchaseHistoryViewModel : UserViewModel
    {
        public long regid { get; set; }
        public DateTime purchasedate { get; set; }
        public DateTime trainingdate { get; set; }
        public decimal amount { get; set; }
        public bool isattended { get; set; }
        public string coursename { get; set; }
        public bool pytconfirmed { get; set; }
    }
}
