<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TPSLSetting.ascx.cs"
    Inherits="Modules_PaymentGatewayManagement_TPSLSetting" %>

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
                 var param = JSON2.stringify({ paymentGatewayID: paymentGatewayId, storeId: storeId, portalId: portalId });
                 $.ajax({
                     type: "POST",
                     url: '<%=aspxPaymentModulePath%>' + "Services/TPSLWebService.asmx/GetAllTPSLSetting",
                     data: param,
                     contentType: "application/json; charset=utf-8",
                     dataType: "json",
                     success: function(msg) {
                         $.each(msg.d, function(index, item) {
                             $("#txtBillerID").val(item.BillerID);
                             $("#txtResponseUrl").val(item.ResponseUrl);
                             $("#txtCurrency").val(item.Currency);
                             $("#txtCurrency").attr('disabled', 'disabled');
                             $("#txtCheckSumKey").val(item.CheckSumKey);
                             $("#txtLogfileName").val(item.LogfileName);
                             $("#txtLogfileName").attr('disabled', 'disabled');
                             $("#chkIsTest").attr('checked', Boolean.parse(item.IsTestTPSL));
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
                 $("#btnSaveTPSLSetting").bind("click", function() {
                     Setting.SaveUpdateTPSLSetting();
                 });
             },
             SaveUpdateTPSLSetting: function() {
                 var paymentGatewaySettingValueID = 0;
                 var paymentGatewayID = $("#hdnPaymentGatewayID").val();

                 var settingKey = '';
                 settingKey += 'BillerID' + "#" + 'ResponseUrl' + "#" + 'Currency' + "#" + 'CheckSumKey' + "#" + 'LogfileName' + "#" + 'IsTestTPSL';
                 var settingValue = '';
                 settingValue += $("#txtBillerID").val() + '#' + $("#txtResponseUrl").val() + "#" + $("#txtCurrency").val() + "#" + $("#txtCheckSumKey").val() + "#" + $("#txtLogfileName").val() + "#" + $("#chkIsTest").attr('checked');
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
             }
         }
     });

   
    //]]>
</script>


    <p>
        </p>


    <div class="cssClassCloseIcon">
        <button type="button" class="cssClassClose">
            <span>Close</span></button>
    </div>
    <h2>
        <asp:Label ID="lblTitle" runat="server" Text="TPSL Setting Information" 
            meta:resourcekey="lblTitleResource1"></asp:Label>
    </h2>
    <div class="cssClassFormWrapper">
    <table cellspacing="0" cellpadding="0" border="0" width="100%">
        <tr>
            <td>
                <asp:Label ID="lblBillerID" runat="server" Text="Biller ID:" 
                    CssClass="cssClassLabel" meta:resourcekey="lblBillerIDResource1"></asp:Label>
            </td>
            <td class="cssClassGridRightCol">
                <input type="text" id="txtBillerID" class="cssClassNormalTextBox">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblResponseUrl" runat="server" Text="Response Url:" 
                    CssClass="cssClassLabel" meta:resourcekey="lblResponseUrlResource1"></asp:Label>
            </td>
            <td class="cssClassGridRightCol">
                <input type="text" class="cssClassNormalTextBox" id="txtResponseUrl">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCurrencyCode" runat="server" Text="Currency:" 
                    CssClass="cssClassLabel" meta:resourcekey="lblCurrencyCodeResource1"></asp:Label>
            </td>
            <td class="cssClassGridRightCol">
                <input type="text" class="cssClassNormalTextBox" id="txtCurrency">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCheckSumKey" runat="server" Text="CheckSum Key:" 
                    CssClass="cssClassLabel" meta:resourcekey="lblCheckSumKeyResource1"></asp:Label>
            </td>
            <td class="cssClassGridRightCol">
                <input type="text" class="cssClassNormalTextBox" id="txtCheckSumKey">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblLogfileName" runat="server" Text="Log File Name:" 
                    CssClass="cssClassLabel" meta:resourcekey="lblLogfileNameResource1"></asp:Label>
            </td>
            <td class="cssClassGridRightCol">
                <input type="text" class="cssClassNormalTextBox" id="txtLogfileName">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblIsTest" runat="server" Text="Is Test TPSL:" 
                    CssClass="cssClassLabel" meta:resourcekey="lblIsTestResource1"></asp:Label>
            </td>
            <td class="cssClassGridRightCol">
                <input type="checkbox" id="chkIsTest" class="cssClassCheckBox" />
            </td>
        </tr>
    </table>
    <div class="cssClassButtonWrapper">
        <p>
            <button id="btnSaveTPSLSetting" type="button">
                <span><span>Save</span></span></button>
        </p>
    </div>
</div>
