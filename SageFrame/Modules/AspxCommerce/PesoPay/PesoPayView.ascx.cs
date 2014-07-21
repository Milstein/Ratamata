using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using AspxCommerce.Core;
using SageFrame.Web;

public partial class PesoPayView : BaseAdministrationUserControl
{
     public string PathPesoPay = string.Empty;
    public string MainCurrency;
    public string couponApplied = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            StoreSettingConfig ssc = new StoreSettingConfig();
            MainCurrency = ssc.GetStoreSettingsByKey(StoreSetting.MainCurrency, GetStoreID, GetPortalID, GetCurrentCultureName);

            if (Session["CouponApplied"] != null)
            {
                couponApplied = Session["CouponApplied"].ToString();
            }
            else
            {
                couponApplied = "0";

            }
        }
    }

    protected void page_init(object sender, EventArgs e)
    {
        string modulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
        PathPesoPay = ResolveUrl(modulePath);
    }
}
