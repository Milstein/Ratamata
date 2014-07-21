<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PesoPaySetting.ascx.cs"
    Inherits="PesoPay_PesoPaySetting" %>

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


         var clientPostChoice = '';
         var directChoice = '';

         clientPostChoice += '<option value="ALL">All the available payment method</option>';

         directChoice += '<option value="CC">Credit Card Payment</option>';
         // directChoice += '<option value="GCash">Globe GCash Mobile Payment</option>';
         // directChoice += ' <option value="BancNet">BancNet Debit Card Payment</option>';
         Setting = {
             LoadPaymentGatewaySetting: function(id, PopUpID) {
                 var paymentGatewayId = id;
                 var param = JSON2.stringify({ storeId: storeId, paymentGatewayId: paymentGatewayId, portalId: portalId });
                 $.ajax({
                     type: "POST",
                     url: '<%=aspxPaymentModulePath%>' + "Services/PesoPayWebService.asmx/GetPesoPaySettings",
                     data: param,
                     contentType: "application/json; charset=utf-8",
                     dataType: "json",
                     success: function(data) {
                         var value = eval(data.d);
                         if (value.PesoPayPaymentType == 1) {
                             $("#ddlPayMethod").html(clientPostChoice);
                             // $("#txtErrorUrl").attr("disabled", "disabled");
                         } else {
                             $("#ddlPayMethod").html(directChoice);
                             //$("#txtErrorUrl").removeAttr("disabled");
                         }
                         $("input[type='radio'][name='pptype'][value='" + value.PesoPayPaymentType + "']").attr("checked", "checked");
                         $("#ddlPPLang>option[value='" + value.PesoPayLanguage + "']").attr("selected", "selected");
                         //  $("#txtOrderReference").val(value.PesoPayOrderReference);

                         $("#ddlCurrencyCode>option[value='" + value.PesoPayCurrencyCode + "']").attr("selected", "selected");

                         $("#txtMerchantID").val(value.PesoPayMerchantID);
                         $("#ddlPayMethod>option[value='" + value.PesoPayPaymentMethod + "']").attr("selected", "selected");
                         $("#txtCancelUrl").val(value.PesoPayCancelURL);
                         // $("#txtFailUrl").val(value.PesoPayFailURL);
                         $("#txtSuccessUrl").val(value.PesoPaySuccessURL);
                         $("#txtErrorUrl").val(value.PesoPayErrorURL);
                         $("input[type='radio'][name='paytype'][value='" + value.PesoPayPayType + "']").attr("checked", "checked");
                         // $("#txtRemark").val(value.PesoPayRemark);
                         $("#txtRedirect").val(value.PesoPayRedirectTime);
                         $("#txtDataFeed").val(value.PesoPayDataFeed);
                         $("#chkIsTestPeso").attr('checked', $.parseJSON(value.IsTestPesoPay.toLowerCase()));
                         $("#ddlMspMode>option[value='" + value.PesoPayMpsMode + "']").attr("selected", "selected");
                         $("#chkIsSecureHashEnabled").attr('checked', $.parseJSON(value.PesoPayIsSecureHashEnabled.toLowerCase()));
                         $("#txtSecureHashSecretKey").val(value.PesoPaySecureHashSecret);
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

                 $("input[type='radio'][name='pptype']").bind("change", function() {
                     if ($(this).attr("value") == 1) {
                         $("#ddlPayMethod").html(clientPostChoice);
                         // $("#txtErrorUrl").attr("disabled", "disabled");
                     } else {
                         $("#ddlPayMethod").html(directChoice);
                         // $("#txtErrorUrl").removeAttr("disabled");
                     }
                 });

                 $("#btnSaveUpdatePeso").bind("click", function() {
                     var paymentGatewaySettingValueID = 0;
                     var paymentGatewayID = $("#hdnPaymentGatewayID").val();
                     var settingKey = '';
                     var settingValue = '';
                     if ($("#chkIsSecureHashEnabled").attr('checked')) {
                         if ($.trim($("#txtSecureHashSecretKey").val()) == "") {
                             csscody.alert('<h2>Information Alert</h2><p>Please input secure hash secret key provided by gateway.</p>');
                             return false;
                         }
                     }
                     settingKey += 'PesoPayPaymentType' +
                     // "#" + 'PesoPayOrderReference' + 
                         "#" + 'PesoPayCurrencyCode' + "#" + 'PesoPayLanguage' + "#" + 'PesoPayMerchantID' + "#";
                     settingKey += 'PesoPayPaymentMethod' + "#" + 'PesoPayCancelURL' + "#" + 'PesoPaySuccessURL' + "#" + 'PesoPayErrorURL' + "#";
                     settingKey += 'PesoPayPayType' + "#" + 'PesoPayRedirectTime' + "#" + 'PesoPayDataFeed' + "#" + 'IsTestPesoPay' + "#" + "PesoPayMpsMode";
                     settingKey += '#' + 'PesoPaySecureHashSecret' + '#' + 'PesoPayIsSecureHashEnabled';

                     settingValue += $("input[type='radio'][name='pptype']:checked").attr("value") +
                     //  '#' + $("#txtOrderReference").val() +
                             "#" + $("#ddlCurrencyCode>option:selected").val() + "#" + $("#ddlPPLang>option:selected").val() + "#" + $("#txtMerchantID").val() + "#";
                     settingValue += $("#ddlPayMethod>option:selected").val() + "#" + $("#txtCancelUrl").val() + "#" + $("#txtSuccessUrl").val() + "#" + $("#txtErrorUrl").val() + "#";
                     settingValue += $("input[type='radio'][name='paytype']:checked").attr("value") + "#" + $("#txtRedirect").val() + "#";
                     settingValue += $("#txtDataFeed").val() + "#" + $("#chkIsTestPeso").attr("checked") + "#" + $("#ddlMspMode").val();
                     settingValue += '#' + $.trim($("#txtSecureHashSecretKey").val()) + '#' + $("#chkIsSecureHashEnabled").attr('checked');
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
        <asp:Label ID="lblPesoPayTitle" runat="server" Text="PesoPay Setting Information"></asp:Label>
    </h2>
   <div class="cssClassFormWrapper">

<table>
    <tr>
        <td>
            <asp:Label ID="lblPPType" runat="server" CssClass="cssClassLabel" Text="Payment Type:"></asp:Label>
        </td>
        <td>
            <label> <input type="radio" name="pptype" value="1"  class="cssClassRadioButton" />Client Post Through Browser</label><br />
       <label>  <input type="radio" name="pptype" value="2" />Direct Client Side Connection</label><br />
       <label><input type="radio" name="pptype" value="3" />Server Side Connection</label>
       
        
           
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblPesoPayLang"  CssClass="cssClassLabel" Text="Language:" runat="server">
            </asp:Label>
        </td>
        <td>
            <select id="ddlPPLang">
                  <option value="C">Traditional Chinese</option>
                <option value="E">English</option>
                <option value="X">Simplified Chinese</option>
                <option value="K">Korean</option>
                <option value="J">Japanese</option>
                <option value=""></option>
                <option value="T">Thai</option>
                <option value="F">French</option>
                <option value="G">German</option>
                <option value="R">Russian</option>
                <option value="S">Spanish</option>
            </select>
        </td>
    </tr>
 <%--   <tr>
    <td><asp:Label ID="lblOrderReference" runat="server" Text="Order reference:"></asp:Label></td>
    <td><input type="text" id="txtOrderReference" /></td>
    </tr>--%>
    <tr>
        <td>
            <asp:Label ID="lblPPCancelUrl" CssClass="cssClassLabel" Text="Cancel Url:" runat="server"></asp:Label>
        </td>
        <td>
            <input type="text" id="txtCancelUrl"  class="cssClassNormalTextBox" />
        </td>
    </tr>
  
    <tr>
        <td>
            <asp:Label CssClass="cssClassLabel" ID="lblSuccessUrl" Text="Success Url:" runat="server">
            </asp:Label>
        </td>
        <td>
            <input type="text" id="txtSuccessUrl" class="cssClassNormalTextBox"/>
        </td>
        </tr>
        <tr>
        <td><asp:Label ID="lblDataFeed" runat="server" CssClass="cssClassLabel"  Text="Data Feed:"></asp:Label></td>
        <td><input type="text" id="txtDataFeed"  class="cssClassNormalTextBox" /></td>
        </tr>
         <tr>
        <td><asp:Label ID="lblErrorURL" Text="Error Url:" CssClass="cssClassLabel" runat="server"></asp:Label></td>
        <td><input type="text" id="txtErrorUrl"  class="cssClassNormalTextBox" /></td>
        </tr>     
       
        <tr>
            <td>
                <asp:Label ID="lblMerchantID" CssClass="cssClassLabel" Text="Merchant ID:" runat="server"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtMerchantID"  class="cssClassNormalTextBox" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPayType" Text="Pay Type:" CssClass="cssClassLabel" runat="server"></asp:Label>
            </td>
            <td>
            <input type="radio" name="paytype" value="N" />Normal Payment (Sales)
            <input type="radio" name="paytype" value="H" />Hold Payment(Authorize Only)
               
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblpayMethod" Text="Payment Method:" CssClass="cssClassLabel" runat="server"></asp:Label>
            </td>
            <td>
                <select id="ddlPayMethod">
                
                </select>
            </td>
        </tr>
       
        <tr>
            <td>
                <asp:Label ID="lblRedirect" Text="Redirect Time(in secs):" CssClass="cssClassLabel" runat="server"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtRedirect"  class="cssClassNormalTextBox" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCurrencyCode" Text="CurrencyCode:"  CssClass="cssClassLabel" runat="server"></asp:Label>
            </td>
            <td>
                <select id="ddlCurrencyCode">
                  <option value="608">PHP</option>
                <option value="344">HKD</option>
                <option value="840">USD</option>
                <option value="702">SGD</option>
                <option value="156">CNY(RMB)</option>
                <option value="392">JPY</option>
                <option value="901">TWD</option>
                <option value="036">AUD</option>
                <option value="978">EUR</option>
                <option value="826">GBP</option>
                <option value="124">CAD</option>
                <option value="446">MOP</option>
                <option value="764">THB</option>
                <option value="458">MYR</option>
                <option value="360">IDR</option>
                <option value="410">KRW</option>
                <option value="682">SAR</option>
                <option value="554">NZD</option>
                <option value="784">AED</option>
                <option value="096">BND</option>
                </select>
            </td>
        </tr>
        <tr>
        <td>
            <asp:Label ID="lblMspMode" runat="server" CssClass="cssClassLabel" Text="MSP Mode:"></asp:Label>
        </td>
        <td>
            <select id="ddlMspMode" disabled="disabled">
                <option value="NIL">Disable MPS (merchant not using MPS)</option>
                <option value="SCP">Enable MPS with ‘Simple Currency Conversion’</option>
                <option value="DCC">Enable MPS with ‘Dynamic Currency Conversion’</option>
                <option value="MCP">Enable MPS with ‘Multi Currency Pricing’</option>
            </select>
        </td>
    </tr>
    <tr>
        <td><asp:Label ID="lblIsSecureHashEnabled" CssClass="cssClassLabel" Text="Is Secure Hash Enabled:" runat="server"></asp:Label></td>
        <td><input type="checkbox" id="chkIsSecureHashEnabled" /></td>
    </tr>
     <tr>
        <td><asp:Label ID="lblSecureHashSecretKey" CssClass="cssClassLabel" Text="Secure Hash Secret Key:" runat="server"></asp:Label></td>
        <td><input type="text" id="txtSecureHashSecretKey"  class="cssClassNormalTextBox" /></td>
    </tr>
        <tr>
        <td><asp:Label ID="lblIstest" CssClass="cssClassLabel" Text="Is Test PesoPay:" runat="server"></asp:Label></td>
        <td><input type="checkbox" id="chkIsTestPeso" /></td>
        </tr>
        <tr>
        <td></td>
        </tr>
</table>
<div class="cssClassButtonWrapper">
        <p>
            <button id="btnSaveUpdatePeso" type="button">
                <span><span>Save</span></span></button>
        </p>
    </div>
</div>

