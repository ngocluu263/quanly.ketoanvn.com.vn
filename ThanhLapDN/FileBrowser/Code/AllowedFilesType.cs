using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThanhLapDN.FileBrowser.Code
{
    public class AllowedFilesType
    {
        const string types = "jpg,jpeg,doc,docx,zip,gif,png,pdf,rar,svg,svgz,xls,xlsx,ppt,pps,pptx";
        public static string[] GetAllowed()
        {
            string myTypes = (String.IsNullOrEmpty(MagicSession.Current.AllowedFileTypes)) ? types : MagicSession.Current.AllowedFileTypes;
            return myTypes.Split(new char[] { ',' });

        }
    }
}