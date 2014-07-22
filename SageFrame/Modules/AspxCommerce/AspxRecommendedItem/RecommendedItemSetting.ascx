<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RecommendedItemSetting.ascx.cs" Inherits="Modules_AspxCommerce_AspxRecommendedItem_RecommendedItemSetting" %>
<script type="text/javascript">
 //<![CDATA[
    var modulePath = '<%=ModulePath %>';
    var modulePath = '<%=ModulePath %>';
    var itemCount = '<%=ItemCount %>';
    var RecommendedItemSetting = "";
    $(function () {
        var AspxCommonObj = function () {
            var aspxCommonObj = {
                StoreID: AspxCommerce.utils.GetStoreID(),
                PortalID: AspxCommerce.utils.GetPortalID(),
                CultureName: AspxCommerce.utils.GetCultureName(),
                UserName: AspxCommerce.utils.GetUserName(),
                SessionCode: AspxCommerce.utils.GetSessionCode(),
                CustomerID: AspxCommerce.utils.GetCustomerID()
            };
            return aspxCommonObj;
        };
        RecommendedItemSetting = {
            config: {
                isPostBack: false,
                async: false,
                cache: false,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: "json",
                baseURL: modulePath + "RecommendedItemWebService.asmx/",
                url: "",
                method: "",
                ajaxCallMode: 0
            },
            ajaxCall: function (config) {
                $.ajax({
                    type: RecommendedItemSetting.config.type,
                    contentType: RecommendedItemSetting.config.contentType,
                    cache: RecommendedItemSetting.config.cache,
                    async: RecommendedItemSetting.config.async,
                    data: RecommendedItemSetting.config.data,
                    dataType: RecommendedItemSetting.config.dataType,
                    url: RecommendedItemSetting.config.url,
                    success: RecommendedItemSetting.config.ajaxSuccess,
                    error: RecommendedItemSetting.config.ajaxFailure
                });
            },

            SaveRecommendedItemSuccessful: function () {
                SageFrame.messaging.show(getLocale(RecommendedItemLanguage, 'Setting Saved Successfully'), "Success");
            },
            SaveAndUpdateItemSetting: function () {
                var noOfCategoryDisplay = $.trim(parseInt($("#txtNoOfRecommendedItems").val()));
                this.config.method = "SaveRecommendedItemSetting";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({
                    aspxCommonObj: AspxCommonObj(),
                    noOfItemDisplay: noOfCategoryDisplay
                });
                this.config.ajaxSuccess = RecommendedItemSetting.SaveRecommendedItemSuccessful;
                this.config.ajaxFailure = RecommendedItemSetting.RecommendedItemSettingError;
                this.ajaxCall(this.config);
            },
            Init: function () {
                $("#txtNoOfRecommendedItems").val(itemCount);
                $("#btnRecommendItemSave").click(function () {
                    var noOfCategoryDisplay = $("#txtNoOfRecommendedItems").val();
                    RecommendedItemSetting.SaveAndUpdateItemSetting();
                });
            }
        };
        RecommendedItemSetting.Init();
    });
//]]>
</script>
<div class="classRecommendedItemsCountSettings">
    <h2 class="sfLocale">Recommended Items Display Settings</h2>
    <table>
        <tr>
            <td>
                <label id="lblNoOfRecommendedItemsDisplay" class="sfLocale">
                    No Of Recommended Items Display</label>
            </td>
            <td>
                <input type="text" id="txtNoOfRecommendedItems" />
            </td>
        </tr>
    </table>
    <input type="button" id="btnRecommendItemSave" class="sfLocale" value="Save"/>
</div>