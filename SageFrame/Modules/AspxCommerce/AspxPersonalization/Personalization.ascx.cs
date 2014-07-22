using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using System.Security.Cryptography;
using System.Web.Security;
using System.Text;
using System.IO;
using AspxCommerce.Core;
using AspxCommerce.Personalization;
using SageFrame.Framework;
using SageFrame.Common;
using System.Web.Configuration;
using System.Configuration;
using System.Xml;

public partial class Modules_AspxCommerce_AspxPersonalization_Personalization : SageFrameStartUpControl
{
    public string ModulePath = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ModulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
            }
            IncludeLanguageJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
}