var ABTest = "";
$(function () {
    var $accor = '';
    var rootPath = AspxCommerce.utils.GetAspxRootPath();
    var modulePath = rootPath + "Modules/AspxCommerce/AspxABTesting/";
       var ModuleServicePath = modulePath + "ABTestingHandler.ashx/";
    var aspxCommonObj = AspxCommerce.AspxCommonObj();
    var pageName = "";
    var isUserInRoles = false;
    var isValidated = false;
    ABTest = {
        SettingVars: [],
        SettingVar: {
            isActive: false,
            abTestID: "",
            opURL: "",
            v1URL: "",
            v2URL: "",
            v3URL: "",
            userInRoles: "",
            endsOnDate: ""
        },
        vcInfo: {
            ABTestID: "",
            VariationID: "",
            ABTestPageURL: "",
            Visit: 0,
            Conversion: 0
        },
        vcVar: {
            opCount: 0,
            v1Count: 0,
            v2Count: 0,
            v3Count: 0
        },
        config: {
            isPostBack: false,
            async: false,
            cache: false,
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
                type: ABTest.config.type,
                contentType: ABTest.config.contentType,
                cache: ABTest.config.cache,
                async: ABTest.config.async,
                url: ABTest.config.url,
                data: ABTest.config.data,
                dataType: ABTest.config.dataType,
                success: ABTest.config.ajaxCallMode,
                error: ABTest.config.error
            });
        },
        Init: function () {
                   },
        ABTestVisitOnPreviousPage: function (rPage) {
            var pNm = "/" + rPage.toLowerCase();
            var rPG = '';
            if (pNm.indexOf('.') > 0) {
                rPG = pNm.substr(0, pNm.indexOf("."));
            }
            else {
                rPG = pNm;
            }
            pageName = rPG;
            ABTest.ABTestValidate();
            if (isValidated) {
                if (rPG == ABTest.SettingVar.opURL || rPG == ABTest.SettingVar.v1URL || rPG == ABTest.SettingVar.v2URL || rPG == ABTest.SettingVar.v3URL) {
                    ABTest.ABTestVisitCount();
                    ABTest.ABTestRedirectToTarget();
                }
                else {
                    window.location.href = AspxCommerce.utils.GetAspxRedirectPath() + rPage;
                }
            }
            else {
                window.location.href = AspxCommerce.utils.GetAspxRedirectPath() + rPage;
            }
        },
        ABTestSaveVisitCount: function () {
            var currentPage = ABTest.GetCurrentPageName();
            pageName = currentPage;
            ABTest.ABTestValidate();
            if (isValidated) {
                               if (currentPage == ABTest.SettingVar.opURL) {
                    ABTest.vcInfo.VariationID = 0;
                    ABTest.vcInfo.ABTestPageURL = ABTest.SettingVar.opURL;
                }
                if (currentPage == ABTest.SettingVar.v1URL) {
                    ABTest.vcInfo.VariationID = 1;
                    ABTest.vcInfo.ABTestPageURL = ABTest.SettingVar.v1URL;
                }
                if (currentPage == ABTest.SettingVar.v2URL) {
                    ABTest.vcInfo.VariationID = 2;
                    ABTest.vcInfo.ABTestPageURL = ABTest.SettingVar.v2URL;
                }
                if (currentPage == ABTest.SettingVar.v3URL) {
                    ABTest.vcInfo.VariationID = 3;
                    ABTest.vcInfo.ABTestPageURL = ABTest.SettingVar.v3URL;
                }

                ABTest.vcInfo.ABTestID = ABTest.SettingVar.abTestID;
                ABTest.vcInfo.Visit = 1;
                ABTest.ABTestSaveVisitAndConversion();

            }
        },

        ABTestSaveConversion: function () {
            var currentPage = ABTest.GetCurrentPageName();
            pageName = currentPage;
            ABTest.ABTestValidate();
            if (isValidated) {
                               if (currentPage == ABTest.SettingVar.opURL) {
                    ABTest.vcInfo.VariationID = 0;
                    ABTest.vcInfo.ABTestPageURL = ABTest.SettingVar.opURL;
                }
                if (currentPage == ABTest.SettingVar.v1URL) {
                    ABTest.vcInfo.VariationID = 1;
                    ABTest.vcInfo.ABTestPageURL = ABTest.SettingVar.v1URL;
                }
                if (currentPage == ABTest.SettingVar.v2URL) {
                    ABTest.vcInfo.VariationID = 2;
                    ABTest.vcInfo.ABTestPageURL = ABTest.SettingVar.v2URL;
                }
                if (currentPage == ABTest.SettingVar.v3URL) {
                    ABTest.vcInfo.VariationID = 3;
                    ABTest.vcInfo.ABTestPageURL = ABTest.SettingVar.v3URL;
                }

                ABTest.vcInfo.ABTestID = ABTest.SettingVar.abTestID;
                ABTest.vcInfo.Conversion = 1;
                ABTest.ABTestSaveVisitAndConversion();

            }
        },
        ABTestValidate: function () {
            ABTest.ABTestGetSettingsAll();
            if (ABTest.SettingVar.isActive) {
                var endDate = Date.parse(ABTest.SettingVar.endsOnDate);
                var currentDate = Date.now();
                ABTest.ABTestIsUserInRoles(ABTest.SettingVar.userInRoles);
                if (endDate >= currentDate && isUserInRoles) {
                    isValidated = true;
                }
            }
        },
        ABTestRedirectToTarget: function () {
            var opCount = ABTest.vcVar.opCount;
            var v1Count = ABTest.vcVar.v1Count;
            var v2Count = ABTest.vcVar.v2Count;
            var v3Count = ABTest.vcVar.v3Count;

            if (opCount <= v1Count && opCount <= v2Count && opCount <= v3Count) {
                window.location.href = rootPath + ABTest.SettingVar.opURL.substring(1, ABTest.SettingVar.opURL.length) + pageExtension;
            }
            else if (v1Count <= v2Count && v1Count <= v3Count && v1Count <= opCount) {
                window.location.href = rootPath + ABTest.SettingVar.v1URL.substring(1, ABTest.SettingVar.v1URL.length) + pageExtension;
            }
            else if (v2Count <= v3Count && v2Count <= v1Count && v2Count <= opCount) {
                if (ABTest.SettingVar.v2URL != '') {
                    window.location.href = rootPath + ABTest.SettingVar.v2URL.substring(1, ABTest.SettingVar.v2URL.length) + pageExtension;
                }
                else if (opCount <= v1Count) {
                    window.location.href = rootPath + ABTest.SettingVar.opURL.substring(1, ABTest.SettingVar.opURL.length) + pageExtension;
                }
                else {
                    window.location.href = rootPath + ABTest.SettingVar.v1URL.substring(1, ABTest.SettingVar.v1URL.length) + pageExtension;
                }
            }
            else {
                if (ABTest.SettingVar.v3URL != '') {
                    window.location.href = rootPath + ABTest.SettingVar.v3URL.substring(1, ABTest.SettingVar.v3URL.length) + pageExtension;
                }
                else if (opCount <= v1Count && opCount <= v2Count) {
                    window.location.href = rootPath + ABTest.SettingVar.opURL.substring(1, ABTest.SettingVar.opURL.length) + pageExtension;
                }
                else if (v1Count <= v2Count && v1Count <= opCount) {
                    window.location.href = rootPath + ABTest.SettingVar.v1URL.substring(1, ABTest.SettingVar.v1URL.length) + pageExtension;
                }
                else {
                    window.location.href = rootPath + ABTest.SettingVar.v2URL.substring(1, ABTest.SettingVar.v2URL.length) + pageExtension;
                }
            }

            ABTest.vcVar.opCount = 0;
            ABTest.vcVar.v1Count = 0;
            ABTest.vcVar.v2Count = 0;
            ABTest.vcVar.v3Count = 0;
        },
        ABTestGetSettingsAll: function () {
            this.config.method = "ABTestGetSettingsAll";
            this.config.url = ModuleServicePath + this.config.method;
            this.config.data = JSON2.stringify({ offset: 1, limit: 100, abTestName: null, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = ABTest.ABTestGetSettingsAllSuccess;
            this.config.error = ABTest.ABTestGetSettingsAllError;
            this.ajaxCall(this.config);
        },
        ABTestGetSettingsAllSuccess: function (msg) {
            if (msg.d) {
                var pageNm = pageName;
                $.each(msg.d, function (index, item) {
                    var opUrl = item.OriginalPageURL.toLowerCase();
                    var v1Url = item.Variation1PageURL.toLowerCase();
                    var v2Url = '';
                    var v3Url = '';
                    if (item.Variation2PageURL != '') {
                        v2Url = item.Variation2PageURL.toLowerCase();
                    }
                    if (item.Variation3PageURL != '') {
                        v3Url = item.Variation3PageURL.toLowerCase();
                    }
                    if (opUrl == pageNm || v1Url == pageNm || v2Url == pageNm || v3Url == pageNm) {
                        if (item.IsActive) {
                            ABTest.SettingVar.abTestID = item.ABTestID;
                            ABTest.SettingVar.opURL = item.OriginalPageURL.toLowerCase();
                            ABTest.SettingVar.v1URL = item.Variation1PageURL.toLowerCase();
                            ABTest.SettingVar.isActive = item.IsActive;
                            ABTest.SettingVar.userInRoles = item.UsersInRole;
                            ABTest.SettingVar.endsOnDate = item.EndsOnDate;

                            if (item.Variation2PageURL != '') {
                                ABTest.SettingVar.v2URL = item.Variation2PageURL.toLowerCase();
                            }
                            else {
                                ABTest.SettingVar.v2URL = "";
                            }
                            if (item.Variation3PageURL != '') {
                                ABTest.SettingVar.v3URL = item.Variation3PageURL.toLowerCase();
                            }
                            else {
                                ABTest.SettingVar.v3URL = "";
                            }
                        }
                    }

                });

            }
        },
        ABTestGetSettingsAllError: function () {
            csscody.error("<h2>" + getLocale(AspxABTesting, "Error Message") + "</h2><p>" + getLocale(AspxABTesting, "Failed to load A/B test settings!") + "</p>");
        },
        ABTestIsUserInRoles: function (userInRoles) {
            this.config.method = "ABTestIsUserInRoles";
            this.config.url = ModuleServicePath + this.config.method;
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj, DefinedRoleNames: userInRoles });
            this.config.ajaxCallMode = ABTest.ABTestIsUserInRolesSuccess;
            this.config.error = ABTest.ABTestIsUserInRolesError;
            this.ajaxCall(this.config);
        },
        ABTestIsUserInRolesSuccess: function (msg) {
            if (msg.d) {
                isUserInRoles = msg.d;
            }
        },
        ABTestIsUserInRolesError: function () {
            csscody.error("<h2>" + getLocale(AspxABTesting, "Error Message") + "</h2><p>" + getLocale(AspxABTesting, "Failed to load A/B test Users in Role Checking!") + "</p>");
        },
        ABTestVisitCount: function () {
            this.config.method = "ABTestVisitCount";
            this.config.url = ModuleServicePath + this.config.method;
            this.config.data = JSON2.stringify({ abTestID: ABTest.SettingVar.abTestID, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = ABTest.ABTestVisitCountSuccess;
            this.config.error = ABTest.ABTestVisitCountError;
            this.ajaxCall(this.config);
        },
        ABTestVisitCountSuccess: function (msg) {
            if (msg.d) {
                ABTest.vcVar.opCount = msg.d.OriginalPageVisitCount;
                ABTest.vcVar.v1Count = msg.d.Variation1VisitCount;
                ABTest.vcVar.v2Count = msg.d.Variation2VisitCount;
                ABTest.vcVar.v3Count = msg.d.Variation3VisitCount;
            }
        },
        ABTestVisitCountError: function () {
            csscody.error("<h2>" + getLocale(AspxABTesting, "Error Message") + "</h2><p>" + getLocale(AspxABTesting, "Failed to load A/B test visit count!") + "</p>");
        },
        ABTestSaveVisitAndConversion: function () {
            this.config.method = "ABTestSaveVisitAndConversion";
            this.config.url = ModuleServicePath + this.config.method;
            this.config.data = JSON2.stringify({ visitConversionInfo: ABTest.vcInfo, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = ABTest.ABTestSaveVisitAndConversionSuccess;
            this.config.error = ABTest.ABTestSaveVisitAndConversionError;
            this.ajaxCall(this.config);
        },
        ABTestSaveVisitAndConversionSuccess: function () {
            ABTest.vcInfo.Conversion = 0;
            ABTest.vcInfo.Visit = 0;
            ABTest.vcInfo.ABTestID = '';
            ABTest.vcInfo.VariationID = '';
            ABTest.vcInfo.ABTestPageURL = '';
        },
        ABTestSaveVisitAndConversionError: function () {
            csscody.error("<h2>" + getLocale(AspxABTesting, "Error Message") + "</h2><p>" + getLocale(AspxABTesting, "Failed to save A/B test conversion!") + "</p>");
        },
        Boolean: function (str) {
            switch (str.toLowerCase()) {
                case "true":
                    return true;
                case "false":
                    return false;
                default:
                    return false;
            }
        },
        GetCurrentPageName: function () {
            var currurl = window.location.pathname;
            var index = currurl.lastIndexOf("/") + 1;
            var filenameWithExtension = currurl.substr(index);
            var filename = "/" + filenameWithExtension.split(".")[0];
            return filename.toString().toLowerCase();
        }

    };

    ABTest.Init();

});