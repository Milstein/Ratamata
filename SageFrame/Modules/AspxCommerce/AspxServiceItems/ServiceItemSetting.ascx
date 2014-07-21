<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ServiceItemSetting.ascx.cs" Inherits="Modules_AspxCommerce_AspxServiceItems_ServiceItemSetting" %>

<div class="cssServiceSetting">
    <table>
        <thead>
            <tr><td class="sfLocale">
                Service Settings</td></tr>
        </thead>
         <tr>
            <td>
                <asp:Label ID="lblEnableService" runat="server" Text="Enable Service Item"></asp:Label>
            </td>
            <td>
                <input type="checkbox" id="chkEnableService" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblServiceCount" runat="server" Text="Enter the Number of Products Displayed"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtServiceCount" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblServiceInARow" runat="server" Text="Enter the Number of Products Dispalyed In Row"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtServiceInARow" />
            </td>
        </tr>
         <tr>
            <td>
                <asp:Label ID="lblShowServiceRss" runat="server" Text="Enable Rss"></asp:Label>
            </td>
            <td>
                <input type="checkbox" id="chkEnableServiceRss" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblServiceRssCount" runat="server" Text="Number of Rss To Show"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtServiceRssCount" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblServiceRssPage" runat="server" Text="Service Rss Page"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtServiceRssPage" />
            </td>
        </tr>
         <tr>
            <td>
                <asp:Label ID="lblServiceDeatailPage" runat="server" Text="Service Detail Page:"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtServiceDetailPage" readonly="readonly" />
            </td>
        </tr>
         <tr>
            <td>
                <asp:Label ID="lblServiceItemDetailsPage" runat="server" Text="Service Item Details Page:"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtServiceItemDetailsPage" readonly="readonly" />
            </td>
        </tr>
         <tr>
            <td>
                <asp:Label ID="lblBookAnAppointmentPage" runat="server" Text="Book An Appointment Page:"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtBookAnAppointmentPage" readonly="readonly" />
            </td>
        </tr>
         <tr>
            <td>
                <asp:Label ID="lblAppointmentSuccessPage" runat="server" Text="Appointment Success Page:"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtAppointmentSuccessPage" readonly="readonly" />
            </td>
        </tr>
        <tr>
            <td>
                <input type="button" id="btnServiceSettingSave" class="sfLocale sfbtn" value="Save" />
            </td>
        </tr>
    </table>
</div>
<script type="text/javascript">
   
(function($) {
    $.ServiceSettingView = function(p) {
        p = $.extend
        ({
            isEnableService: '',
            serviceCategoryInARow: 0,
            serviceCategoryCount: 0,
            isEnableServiceRss: '',
            serviceRssCount: 0,
            serviceDetailsPage: '',
            serviceModuelPath: '',
            bookAnAppointmentPage: '',
            appointmemtSuccessPage: '',
            serviceRssPage: ''
        }, p);

        function aspxCommonObj() {
            var aspxCommonInfo = {
                StoreID: AspxCommerce.utils.GetStoreID(),
                PortalID: AspxCommerce.utils.GetPortalID(),
                CultureName: AspxCommerce.utils.GetCultureName()
            };
            return aspxCommonInfo;
        };

        var ServiceSetting = {
            config: {
                isPostBack: false,
                async: false,
                cache: false,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: 'json',
                baseURL: serviceModulePath + "ServiceHandler.ashx/",
                method: "",
                url: "",
                ajaxCallMode: ""
            },
            ajaxCall: function(config) {
                $.ajax({
                    type: ServiceSetting.config.type,
                    contentType: ServiceSetting.config.contentType,
                    cache: ServiceSetting.config.cache,
                    async: ServiceSetting.config.async,
                    url: ServiceSetting.config.url,
                    data: ServiceSetting.config.data,
                    dataType: ServiceSetting.config.dataType,
                    success: ServiceSetting.ajaxCallMode,
                    error: ServiceSetting.ajaxFailure
                });
            },

            BindServiceSetting: function(data) {
                $("chkEnableService").prop("checked", p.isEnableService);
                $("#txtServiceCount").val(p.serviceCategoryCount);
                $("#txtServiceInARow").val(p.serviceCategoryInARow);
                $("#chkEnableServiceRss").prop("checked", p.isEnableServiceRss);
                $("#txtServiceRssCount").val(p.serviceRssCount);
                $("#txtServiceItemDetailsPage").val(p.serviceDetailsPage);
                $("#txtBookAnAppointmentPage").val(p.bookAnAppointmentPage);
                $("#txtAppointmentSuccessPage").val(p.appointmentSuccessPage);
                $("#txtServiceRssPage").val(p.serviceRssPage);
            },
            ServiceSettingUpdate: function() {
                var isEnableService = $("chkEnableService").prop("checked");
                var serviceCategoryCount = $("#txtServiceCount").val();
                var serviceCategoryInARow = $("#txtServiceInARow").val();
                var isEnableServiceRss = $("#chkEnableServiceRss").prop("checked");
                var serviceRssCount = $("#txtServiceRssCount").val();
                var serviceDetailsPage = $("#txtServiceDetailPage").val();
                var serviceItemDetailsPage = $("#txtServiceItemDetailsPage").val();
                var bookAnAppointmentPage = $("#txtBookAnAppointmentPage").val();
                var appointmentSuccessPage = $("#txtAppointmentSuccessPage").val();
                var serviceRssPage = $("#txtServiceRssPage").val();
                var settingKeys = "IsEnableService*ServiceCategoryCount*ServiceCategoryInARow*IsEnableServiceRss*ServiceRssCount*ServiceDetailsPage*ServiceItemDetailsPage*BookAnAppointmentPage*AppointmentSuccessPage*ServiceRssPage";
                var settingValues = isEnableService + " * " + serviceCategoryCount + " * " + serviceCategoryInARow + " * " + isEnableServiceRss + " * " + serviceRssCount + " * " + serviceDetailsPage + " * " + serviceItemDetailsPage + " * " + bookAnAppointmentPage + " * " + appointmentSuccessPage + " * " + serviceRssPage;
                var param = JSON2.stringify({ SettingValues: settingValues, SettingKeys: settingKeys, aspxCommonObj: aspxCommonObj() });
                this.config.method = "ServiceSettingUpdate";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = param;
                this.config.ajaxCallMode = ServiceSetting.ServiceSettingSuccess;
                this.ajaxCall(this.config);
            },
            ServiceSettingSuccess: function(data) {
                SageFrame.messaging.show(getLocale(AspxServiceLocale, "Setting Saved Successfully"), "Success");
            },
            init: function() {
                ServiceSetting.BindServiceSetting();
                $("#btnServiceSettingSave").click(function() {
                    ServiceSetting.ServiceSettingUpdate();
                });
            }
        };
        ServiceSetting.init();
    };
    $.fn.ServiceSetting = function(p) {
    $.ServiceSettingView(p);
    };
})(jQuery);
$(function() {
    $(".sfLocale").localize({
        moduleKey: AspxServiceLocale
    });
    $(this).ServiceSetting({
        isEnableService: '<%=IsEnableService %>',
        serviceCategoryInARow: '<%=ServiceCategoryInARow %>',
        serviceCategoryCount: '<%=ServiceCategoryCount %>',
        isEnableServiceRss: '<%=IsEnableServiceRss %>',
        serviceRssCount: '<%=ServiceRssCount %>',
        serviceDetailsPage: '<%=ServiceDetailsPage %>',
        serviceModuelPath: '<%=ServiceModulePath %>',
        bookAnAppointmentPage: '<%=BookAnAppointmentPage %>',
        appointmemtSuccessPage: '<%=AppointmentSuccessPage %>',
        serviceRssPage: '<%=ServiceRssPage %>'
    });

    
});
</script>

