﻿#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace SageFrame.IpAccess
{
    public class IpRangeInfo
    {
        public int IpAccessId { get; set; }
        public string IpFrom { get; set; }
        public string IpTo { get; set; }
        public string Reason { get; set; }
        public bool IsActive { get; set; }
    }
}
