﻿using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.DataObject.SysAdmin.MessageObject
{
    public class SmsTemplateViewModel
    {
        public int smstemplateid { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string sender { get; set; }
        public string message { get; set; }
    }
}
