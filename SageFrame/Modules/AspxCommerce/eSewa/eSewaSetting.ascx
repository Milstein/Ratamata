<%@ Control Language="C#" AutoEventWireup="true" CodeFile="eSewaSetting.ascx.cs"
    Inherits="Modules_PaymentGatewayManagement_eSewaSetting" %>

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
        var aspxCommonObj = {
            StoreID: AspxCommerce.utils.GetStoreID(),
            PortalID: AspxCommerce.utils.GetPortalID(),
            UserName: AspxCommerce.utils.GetUserName()

        };
        Setting = {
            LoadPaymentGatewaySetting: function(id, PopUpID) {
                var paymentGatewayId = id;
                var param = JSON2.stringify({ paymentGatewayID: paymentGatewayId, storeId: storeId, portalId: portalId });
                $.ajax({
                    type: "POST",
                    url: '<%=aspxPaymentModulePath%>' + "Services/eSewaWebService.asmx/GetAlleSewaSetting",
                    data: param,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(msg) {
                        $.each(msg.d, function(index, item) {
                            $("#txtMerchantID").val(item.eSewaMerchantID);
                            $("#txtSuccessURL").val(item.eSewaSuccessURL);
                            $("#txtFailureURL").val(item.eSewaFailureURL);
                            $("#txtCurrency").val(item.eSewaCurrencyCode);
                            $("#txtCurrency").attr('disabled', 'disabled');
                            $("#chkIsTest").attr('checked', Boolean.parse(item.IsTesteSewa));
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
                $("#btnSaveeSewaSetting").bind("click", function() {
                    Setting.SaveUpdateeSewaSetting();
                });
            },
            SaveUpdateeSewaSetting: function() {
                var paymentGatewaySettingValueID = 0;
                var paymentGatewayID = $("#hdnPaymentGatewayID").val();

                var settingKey = '';
                settingKey += 'eSewaMerchantID' + "#" + 'eSewaSuccessURL' + "#" + 'eSewaFailureURL' + "#" + 'eSewaCurrencyCode' + "#" + 'IsTesteSewa';
                var settingValue = '';
                settingValue += $("#txtMerchantID").val() + "#" + $("#txtSuccessURL").val() + "#" + $("#txtFailureURL").val() + "#" + $("#txtCurrency").val() + "#" + $("#chkIsTest").attr('checked');
                var isActive = true;
                var param = JSON2.stringify({ paymentGatewaySettingValueID: paymentGatewaySettingValueID, paymentGatewayID: paymentGatewayID, settingKeys: settingKey, settingValues: settingValue, isActive: isActive, aspxCommonObj:aspxCommonObj });
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
            }
        }
    });


    //]]>
</script>

<div class="cssClassCloseIcon">
    <button type="button" class="cssClassClose">
        <span>Close</span></button>
</div>
<h2>
    <asp:Label ID="lblTitle" runat="server" Text="eSewa Setting Information" meta:resourcekey="lblTitleResource1"></asp:Label>
</h2>
<div class="cssClassFormWrapper">
    <table cellspacing="0" cellpadding="0" border="0" width="100%">
        <tr>
            <td>
                <asp:Label ID="lblMerchantID" runat="server" Text="Merchant ID:" CssClass="cssClassLabel"
                    meta:resourcekey="lblMerchantIDResource1"></asp:Label>
            </td>
            <td class="cssClassGridRightCol">
                <input type="text" class="cssClassNormalTextBox" id="txtMerchantID">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblSuccessUrl" runat="server" Text="Success URL:" CssClass="cssClassLabel"
                    meta:resourcekey="lblSuccessUrlResource1"></asp:Label>
            </td>
            <td class="cssClassGridRightCol">
                <input type="text" id="txtSuccessURL" class="cssClassNormalTextBox">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblFailureURL" runat="server" Text="Failure URL:" CssClass="cssClassLabel"
                    meta:resourcekey="lblFailureURLResource1"></asp:Label>
            </td>
            <td class="cssClassGridRightCol">
                <input type="text" class="cssClassNormalTextBox" id="txtFailureURL">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCurrency" runat="server" Text="Currency:" CssClass="cssClassLabel"
                    meta:resourcekey="lblCurrencyResource1"></asp:Label>
            </td>
            <td class="cssClassGridRightCol">
                <input type="text" class="cssClassNormalTextBox" id="txtCurrency">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblIsTest" runat="server" Text="Is Test eSewa:" CssClass="cssClassLabel"
                    meta:resourcekey="lblIsTestResource1"></asp:Label>
            </td>
            <td class="cssClassGridRightCol">
                <input type="checkbox" id="chkIsTest" class="cssClassCheckBox" />
            </td>
        </tr>
    </table>
    <div class="cssClassButtonWrapper">
        <p>
            <button id="btnSaveeSewaSetting" type="button">
                <span><span>Save</span></span></button>
        </p>
    </div>
</div>
