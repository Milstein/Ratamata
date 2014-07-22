<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RecommendedCategorySetting.ascx.cs" Inherits="Modules_AspxCommerce_AspxRecommendedCategory_RecommendedCategorySetting" %>
<script type="text/javascript">
 //<![CDATA[
    var modulePath = '<%=ModulePath %>';
    var Count = '<%=Count %>';    
    var RecommendedCategorySetting = "";
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
        RecommendedCategorySetting = {
            config: {
                isPostBack: false,
                async: false,
                cache: false,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: "json",
                baseURL: modulePath + "RecommendedCategoryHandler.ashx/",
                url: "",
                method: "",
                ajaxCallMode: 0
            },
            ajaxCall: function (config) {
                $.ajax({
                    type: RecommendedCategorySetting.config.type,
                    contentType: RecommendedCategorySetting.config.contentType,
                    cache: RecommendedCategorySetting.config.cache,
                    async: RecommendedCategorySetting.config.async,
                    data: RecommendedCategorySetting.config.data,
                    dataType: RecommendedCategorySetting.config.dataType,
                    url: RecommendedCategorySetting.config.url,
                    success: RecommendedCategorySetting.config.ajaxSuccess,
                    error: RecommendedCategorySetting.config.ajaxFailure
                });
            },

            SaveRecommendedCategorySuccessful: function () {
                SageFrame.messaging.show(getLocale(RecommendedCategoryLanguage, 'Setting Saved Successfully'), "Success");
            },
            SaveAndUpdateCategorySetting: function () {
                var noOfCategoryDisplay = $.trim(parseInt($("#txtNoOfRecommendedCategory").val()));
                this.config.method = "SaveRecommendedCategorySetting";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({
                    aspxCommonObj: AspxCommonObj(),
                    NoOfCategoryDisplay: noOfCategoryDisplay
                });
                this.config.ajaxSuccess = RecommendedCategorySetting.SaveRecommendedCategorySuccessful;
                this.config.ajaxFailure = RecommendedCategorySetting.RecommendedCategorySettingError;
                this.ajaxCall(this.config);
            },
            Init: function () {
                $("#txtNoOfRecommendedCategory").val(Count);
                $("#btnRecommendCategorySave").click(function () {
                    var noOfCategoryDisplay = $("#txtNoOfRecommendedCategory").val();
                    RecommendedCategorySetting.SaveAndUpdateCategorySetting();
                });
            }
        };
        RecommendedCategorySetting.Init();
    });
    //]]>
</script>
<div class="classRecommendedCategoryCountSettings">
    <h2 class="sfLocale">Recommended Category Settings</h2>
    <table>
        <tr>
            <td>
                <label id="lblNoOfRecommendedCategoryDisplay" class="sfLocale">
                    No Of Recommended Category Display</label>
            </td>
            <td>
                <input type="text"  class="sfInputbox" id="txtNoOfRecommendedCategory" />                 
            </td>
        </tr>
    </table>
    <input type="button" id="btnRecommendCategorySave" class="sfLocale" value="Save"/>
</div>