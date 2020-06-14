namespace myFacility.DataObject.SysAdmin.MessageObject
{
    public class BroadcastScheduleViewModel
    {
        public string broadcastscheduleid { get; set; }
        public long groupid { get; set; }
        public string broadcasttype { get; set; }
        public long templateid { get; set; }
        public string receivertypeid { get; set; }
        public string receivertype { get; set; }
        public string frequencyid { get; set; }
        public string frequency { get; set; }
    }
}
