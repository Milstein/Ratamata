<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PersonalizationSetting.ascx.cs" Inherits="Modules_AspxCommerce_AspxPersonalization_PersonalizationSetting" %>
<script type="text/javascript">
    //<!<[CDATA[
    var modulePath = '<%=ModulePath %>';
 

    var PersonalizationSetting = "";
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
        PersonalizationSetting = {
            config: {
                isPostBack: false,
                async: false,
                cache: false,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: "json",
                baseURL: modulePath + "AspxPersonalizationWebService.asmx/",
                url: "",
                method: "",
                ajaxCallMode: 0
            },
            ajaxCall: function (config) {
                $.ajax({
                    type: PersonalizationSetting.config.type,
                    contentType: PersonalizationSetting.config.contentType,
                    cache: PersonalizationSetting.config.cache,
                    async: PersonalizationSetting.config.async,
                    data: PersonalizationSetting.config.data,
                    dataType: PersonalizationSetting.config.dataType,
                    url: PersonalizationSetting.config.url,
                    success: PersonalizationSetting.config.ajaxSuccess,
                    error: PersonalizationSetting.config.ajaxFailure
                });
            },

            SavePersonalizationSettingSuccessful: function () {
                SageFrame.messaging.show(getLocale(PersonalizationLanguage, 'Setting Saved Successfully'), "Success");
            },

            SaveAndUpdatePersonalizationSetting: function () {
                var settingKey = "AspxPersonalization*PersonalizeLatestItemPercent*PersonalizeFeatureItemCount*PersonalizeSpecialItemCount*PersonalizeRecentlyViewItemCount";
                var boolPersonalize = $('#chkEnablePersonalization').prop('checked');
                var pLatestItemPercent = $("#txtPLatestItemPercent").val();
                var pFeatureItemCount = $("#txtPFeatureItemCount").val();
                var PSpecialItemCount = $("#txtPSpecialItemCount").val();
                var pRecentItemCount = $("#txtPRecentlyViewItemCount").val();
                var settingValue = boolPersonalize + "*" + pLatestItemPercent + "*" + pFeatureItemCount + "*" + PSpecialItemCount + "*" + pRecentItemCount;

                var settingKeyValuePairObj = {
                    SettingKey: settingKey,
                    SettingValue: settingValue
                };

                this.config.method = "SaveUpdatePersonalizationSetting";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({
                    aspxCommonObj: AspxCommonObj(),
                    pSettingList: settingKeyValuePairObj
                });
                this.config.ajaxSuccess = PersonalizationSetting.SavePersonalizationSettingSuccessful;
                this.config.ajaxFailure = PersonalizationSetting.PersonalizationSettingError;
                this.ajaxCall(this.config);
            },
            BindPersonalizationSetting: function (data) {
                var item = data.d;
                $('#chkEnablePersonalization').prop('checked', $.parseJSON(item.AspxPersonalization.toLowerCase()));
                $("#txtPLatestItemPercent").val(item.PersonalizeLatestItemPercent);
                $("#txtPFeatureItemCount").val(item.PersonalizeFeatureItemCount);
                $("#txtPSpecialItemCount").val(item.PersonalizeSpecialItemCount);
                $("#txtPRecentlyViewItemCount").val(item.PersonalizeRecentlyViewItemCount);
            },
            GetPersonalizationSetting: function () {
                this.config.method = "GetPersonalizeSettingList";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({
                    aspxCommonObj: AspxCommonObj()
                });
                this.config.ajaxSuccess = PersonalizationSetting.BindPersonalizationSetting;
                this.config.ajaxFailure = PersonalizationSetting.PersonalizationSettingError;
                this.ajaxCall(this.config);
            },
            Init: function () {
                PersonalizationSetting.GetPersonalizationSetting();
                $("#btnPSettingSave").click(function () {
                    PersonalizationSetting.SaveAndUpdatePersonalizationSetting();
                });
            }
        };
        PersonalizationSetting.Init();
    });

//]]>
</script>

<div class="classRecommendedCategoryCountSettings">
    <h2 class="sfLocale">Personalization Settings</h2>
    <table>
        <tr>
            <td>
                <label id="lblEnablePersonalization" class="sfLocale">
                  Enable Personalization:</label>
            </td>
            <td>
                <input type="checkbox"   id="chkEnablePersonalization" />                 
            </td>
        </tr>
        <tr>
            <td>
                <label id="lblPersonalizeLatestItemPercent" class="sfLocale">
                   Personalize Latest Item Percent:</label>
            </td>
            <td>
                <input type="text"  class="sfInputbox" id="txtPLatestItemPercent" />                 
            </td>
        </tr>
        <tr style="display:none">
            <td>
                <label id="lblPFeatureItemCount" class="sfLocale">
                   Personalize Feature Item Count:</label>
            </td>
            <td>
                <input type="text"  class="sfInputbox" id="txtPFeatureItemCount" />                 
            </td>
        </tr>
        <tr style="display:none">
            <td>
                <label id="lblPersonalizeSpecialItemCount" class="sfLocale">
                   Personalize Special Item Count:</label>
            </td>
            <td>
                <input type="text"  class="sfInputbox" id="txtPSpecialItemCount" />                 
            </td>
        </tr>
        <tr style="display:none">
            <td>
                <label id="lblRecentlyViewItemCount" class="sfLocale">
                   Personalize Recently View Item Count:</label>
            </td>
            <td>
                <input type="text"  class="sfInputbox" id="txtPRecentlyViewItemCount" />                 
            </td>
        </tr>
    </table>
    <input type="button" id="btnPSettingSave" class="sfLocale" value="Save"/>
</div>