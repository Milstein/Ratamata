<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DragonPaySetting.ascx.cs"
    Inherits="DragonPay_DragonPaySetting" %>

<script type="text/javascript">
    //<![CDATA[
    var Setting = "";
    $(function() {

        var storeId = AspxCommerce.utils.GetStoreID();
        var portalId = AspxCommerce.utils.GetPortalID();
        var userName = AspxCommerce.utils.GetUserName();
        var cultureName = AspxCommerce.utils.GetCultureName();
        var customerId = AspxCommerce.utils.GetCustomerID();
        var userIP = AspxCommerce.utils.GetClientIP();
        var countryName = AspxCommerce.utils.GetAspxClientCoutry();
        var sessionCode = AspxCommerce.utils.GetSessionCode();

        Setting = {
            LoadPaymentGatewaySetting: function(id, PopUpID) {
                var paymentGatewayId = id;
                var param = JSON2.stringify({ storeId: storeId, paymentGatewayId: paymentGatewayId, portalId: portalId });
                $.ajax({
                    type: "POST",
                    url: '<%=aspxPaymentModulePath%>' + "Services/DragonPayWebService.asmx/GetDragonPaySettings",
                    data: param,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(data) {
                        $.each(data.d, function(index, item) {
                            $("#txtMerchantID").val(item.DragonPayMerchantID);
                            $("#txtSecretKey").val(item.DragonPaySecretKey);
                            $("#txtPostBackUrl").val(item.DragonPayPostBackURL);
                            $("#txtReturnUrl").val(item.DragonPayReturnURL);
                            $("#ddlCurrencyCode>option[value='" + item.DragonPayCurrencyCode + "']").attr("selected", "selected");
                            $("#ddlCurrencyCode").attr('disabled', 'disabled');
                            $("#chkIsTestDragonPay").attr('checked', Boolean.parse(item.IsTestDragonPay.toLowerCase()));
                        });
                        ShowPopupControl(PopUpID);
                        $(".cssClassClose").click(function() {
                            $('#fade, #popuprel2').fadeOut();
                        });
                    },
                    error: function() {
                        csscody.error('<h2>Error Message</h2><p>Failed to load</p>');
                    }
                });
            },
            Init: function() {
                $("#btnSaveUpdateDragonPay").bind("click", function() {
                    var paymentGatewaySettingValueID = 0;
                    var paymentGatewayID = $("#hdnPaymentGatewayID").val();
                    var settingKey = '';
                    var settingValue = '';
                    settingKey += 'DragonPayMerchantID' + "#" + 'DragonPaySecretKey' + "#" + 'DragonPayPostBackURL' + "#" + 'DragonPayReturnURL' + "#";
                    settingKey += 'DragonPayCurrencyCode' + "#" + 'IsTestDragonPay';
                    settingValue += $("#txtMerchantID").val() + "#" + $("#txtSecretKey").val() + "#" + $("#txtPostBackUrl").val() + "#" + $("#txtReturnUrl").val() + "#";
                    settingValue += $("#ddlCurrencyCode>option:selected").text() + "#" + $("#chkIsTestDragonPay").attr("checked");

                    var isActive = true;
                    var param = JSON2.stringify({ paymentGatewaySettingValueID: paymentGatewaySettingValueID, paymentGatewayID: paymentGatewayID, settingKeys: settingKey, settingValues: settingValue, isActive: isActive, storeId: storeId, portalId: portalId, updatedBy: userName, addedBy: userName });
                    $.ajax({
                        type: "POST",
                        url: aspxservicePath + "AspxCommerceWebService.asmx/AddUpdatePaymentGateWaySettings",
                        data: param,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function() {
                            csscody.info('<h2>Successful Message</h2><p>Setting has been saved successfully.</p>');
                            $('#fade, #popuprel2').fadeOut();
                        },
                        error: function() {
                            csscody.error('<h2>Error Message</h2><p>Failed to save!</p>');
                        }
                    });
                });
            }
        };
        Setting.Init();
    });
  
              
  
   
    

</script>

<div class="cssClassCloseIcon">
    <button type="button" class="cssClassClose">
        <span>Close</span></button>
</div>
<h2>
    <asp:Label ID="lblDragonPayTitle" runat="server" Text="DragonPay Setting Information"
        meta:resourcekey="lblDragonPayTitleResource1"></asp:Label>
</h2>
<div class="cssClassFormWrapper">
    <table>
        <tr>
            <td>
                <asp:Label ID="lblMerchantID" CssClass="cssClassLabel" Text="Merchant ID:" runat="server"
                    meta:resourcekey="lblMerchantIDResource1"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtMerchantID" class="cssClassNormalTextBox" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblSecretKey" CssClass="cssClassLabel" Text="Secret Key:" runat="server"
                    meta:resourcekey="lblSecretKeyResource1"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtSecretKey" class="cssClassNormalTextBox" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label CssClass="cssClassLabel" ID="lblPostBackUrl" Text="Postback Url:" runat="server"
                    meta:resourcekey="lblPostBackUrlResource1"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtPostBackUrl" class="cssClassNormalTextBox" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label CssClass="cssClassLabel" ID="lblReturnUrl" Text="Return Url:" runat="server"
                    meta:resourcekey="lblReturnUrlResource1"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtReturnUrl" class="cssClassNormalTextBox" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCurrencyCode" Text="Currency Code:" CssClass="cssClassLabel" runat="server"
                    meta:resourcekey="lblCurrencyCodeResource1"></asp:Label>
            </td>
            <td>
                <select id="ddlCurrencyCode">
                    <option value="1">PHP</option>
                    <option value="2">USD</option>
                </select>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblIstest" CssClass="cssClassLabel" Text="Is Test DragonPay:" runat="server"
                    meta:resourcekey="lblIstestResource1"></asp:Label>
            </td>
            <td>
                <input type="checkbox" id="chkIsTestDragonPay" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
    <div class="cssClassButtonWrapper">
        <p>
            <button id="btnSaveUpdateDragonPay" type="button">
                <span><span>Save</span></span></button>
        </p>
    </div>
</div>
