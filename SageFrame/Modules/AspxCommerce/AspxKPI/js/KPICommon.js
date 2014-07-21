var KPICommon;
$(function () {
    var $accor = '';
    var rootPath = AspxCommerce.utils.GetAspxRootPath();
    var modulePath = rootPath + "Modules/AspxCommerce/AspxKPI/";
    var ModuleServicePath = modulePath + "AspxKPIHandler.ashx/";
    var aspxCommonObj = AspxCommerce.AspxCommonObj();
   
    KPICommon = {
        KPISettingsGetAllInfo: {
            KPISettingsID: 0,
            IsActive: false,
            EmailNotification: false,
            StartDate: '',
            EndDate: ''
        },
        KPISaveVisitAndConversionInfo: {
            TabPath: '',
            SubTabPath: '',
            Visit: 0,
            Conversion: 0           
        },
        KPIIPDetailsInfo: {
            IPAddress: '',
            CountryName: '',
            CountryCode: '',
            CityName: '',
            RegionName: '',
            Latitude: '',
            Longitude: ''
        },
        config: {
            isPostBack: false,
            async: true,
            cache: true,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: 'json',
            baseURL: AspxCommerce.utils.GetAspxServicePath(),
            method: "",
            url: "",
            ajaxCallMode: "",
            error: "",
            sessionValue: ""
        },
        ajaxCall: function (config) {
            $.ajax({
                type: KPICommon.config.type,
                contentType: KPICommon.config.contentType,
                cache: KPICommon.config.cache,
                async: KPICommon.config.async,
                url: KPICommon.config.url,
                data: KPICommon.config.data,
                dataType: KPICommon.config.dataType,
                success: KPICommon.config.ajaxCallMode,
                error: KPICommon.config.error
            });
        },
        Init: function () {

        },

        KPISaveVisit: function (subTabPath) {
            var currentPage = KPICommon.GetCurrentPageName();
            var isValidated = KPICommon.KPIValidate();
            if (isValidated) {
                KPICommon.KPISaveVisitAndConversionInfo.TabPath = currentPage;
                KPICommon.KPISaveVisitAndConversionInfo.SubTabPath = subTabPath;
                KPICommon.KPISaveVisitAndConversionInfo.Visit = 1;
                KPICommon.KPISaveVisitAndConversion();
            }
        },
        KPISaveConversion: function (subTabPath) {
            var currentPage = KPICommon.GetCurrentPageName();
            var isValidated = KPICommon.KPIValidate();
            if (isValidated) {
                KPICommon.KPISaveVisitAndConversionInfo.TabPath = currentPage;
                KPICommon.KPISaveVisitAndConversionInfo.SubTabPath = subTabPath;
                KPICommon.KPISaveVisitAndConversionInfo.Conversion = 1;
                KPICommon.KPISaveVisitAndConversion();

            }
        },
        KPISaveVisitAndConversion: function () {
            this.config.method = "KPISaveVisitAndConversion";
            this.config.url = ModuleServicePath + this.config.method;
            this.config.data = JSON2.stringify({ vcInfo: KPICommon.KPISaveVisitAndConversionInfo, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = KPICommon.KPISaveVisitAndConversionSuccess;
            this.config.error = KPICommon.KPISaveVisitAndConversionError;
            this.config.async = false;
            this.ajaxCall(this.config);
        },
        KPISaveVisitAndConversionSuccess: function () {
            KPICommon.KPISaveVisitAndConversionInfo.TabPath = '';
            KPICommon.KPISaveVisitAndConversionInfo.Visit = 0;
            KPICommon.KPISaveVisitAndConversionInfo.Conversion = 0;
        },
        KPISaveVisitAndConversionError: function () {
            csscody.error("<h2>" + getLocale(AspxKPILanguage, "Error Message") + "</h2><p>" + getLocale(AspxKPILanguage, "Failed to save KPI visit and conversion!") + "</p>");
        },
        KPIValidate: function () {
            KPICommon.KPISettingsGetAll();
            var endDate = Date.parse(KPICommon.KPISettingsGetAllInfo.EndDate);
            var currentDate = Date.now();
            if (KPICommon.KPISettingsGetAllInfo.IsActive && endDate >= currentDate) {
                return true;
            }
            else {
                return false;
            }

        },
        KPISettingsGetAll: function () {
            this.config.method = "KPISettingsGetAll";
            this.config.url = ModuleServicePath + this.config.method;
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = KPICommon.KPISettingsGetAllSuccess;
            this.config.error = KPICommon.KPISettingsGetAllError;
            this.config.async = false;
            this.ajaxCall(this.config);
        },
        KPISettingsGetAllSuccess: function (msg) {
            if (msg.d) {
                KPICommon.KPISettingsGetAllInfo.KPISettingsID = msg.d.KPISettingsID;
                KPICommon.KPISettingsGetAllInfo.IsActive = msg.d.IsActive;
                KPICommon.KPISettingsGetAllInfo.EmailNotification = msg.d.EmailNotification;
                KPICommon.KPISettingsGetAllInfo.StartDate = msg.d.StartDate;
                KPICommon.KPISettingsGetAllInfo.EndDate = msg.d.EndDate;
            }
        },
        KPISettingsGetAllError: function () {
            SageFrame.messaging.show("Failed to get settings information!", "error");
        },
        GetCurrentPageName: function () {
            var currurl = window.location.pathname;
            var index = currurl.lastIndexOf("/") + 1;
            var filenameWithExtension = currurl.substr(index);
            var filename = "/" + filenameWithExtension.split(".")[0];
            return filename.toString().toLowerCase();
        }


    };

    KPICommon.Init();

});