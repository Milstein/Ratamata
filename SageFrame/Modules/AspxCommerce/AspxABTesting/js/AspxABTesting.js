var AspxABTesting;
$(function () {

    var modulePath = '<%=AspxABTestingModulePath%>';
       var ModuleServicePath = aspxABTestingModulePath + "ABTestingHandler.ashx/";
    var aspxCommonObj = AspxCommerce.AspxCommonObj();
    var AbTestingGlobalVars = {
        countAddVariation: 2,
        isPageExists: false
    };
    var shortBy = '';
    var settings = [];
    var settingsInfo = {
        ABTestID: 0,
        ABTestName: '',
        OriginalPageURL: '',
        Variation1PageURL: '',
        Variation2PageURL: '',
        Variation3PageURL: '',
        TrafficPercentage: '',
        EmailNotification: '',
        EndsOnDate: '',
        EndsOnMaxVisit: 0,
        UsersInRole: '',
        IsActive: false
    };
    AspxABTesting = {
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
                type: AspxABTesting.config.type,
                contentType: AspxABTesting.config.contentType,
                cache: AspxABTesting.config.cache,
                async: AspxABTesting.config.async,
                url: AspxABTesting.config.url,
                data: AspxABTesting.config.data,
                dataType: AspxABTesting.config.dataType,
                success: AspxABTesting.config.ajaxCallMode,
                error: AspxABTesting.config.error
            });
        },
        Init: function () {
            $("#divABTestingHome").show();
            AspxABTesting.ABTestLoadStaticImage();
            AspxABTesting.gdvABTestsBind();

            var form2 = $("#form1").validate({
                ignore: ":hidden",
                messages: {
                    ABTestName: {
                        required: '*'
                    },
                    UsersInRole: {
                        required: '*'
                    },
                    OriginalPage: {
                        required: '*'
                    },
                    Variant1: {
                        required: '*'
                    }
                }
            });
            $("#btnAddNewTest").click(function () {
                AspxABTesting.HideAll();
                $("#divABTestingAdd").show();
                $("#lblAddVariation").show();
                AspxABTesting.ABTestRoleList();
                $("select#ddlUsersInRole").val('0');
                $("#txtEndsOnDate").datepicker({ dateFormat: 'mm/dd/yy', minDate: 1 });

            });
            $("#lblAddVariation").click(function () {
                AspxABTesting.AddNewRowTable();
            });
            $("#lblBackNewABTest").click(function () {
                AspxABTesting.ClearForm();
                AspxABTesting.HideAll();
                $("#ddlUsersInRole option").removeAttr('selected');
                $("#divABTestingHome").show();

            });
            $("#lblSaveNewABTest").click(function () {
                if (form2.form()) {
                    if ($('#ddlUsersInRole option:selected').val() != 0) {
                        var op = $("#txtOriginalPage").val().toLowerCase();
                        var v1 = $("#txtVariant1").val().toLowerCase();
                        var v2 = 'a';
                        var v3 = 'b';
                        if (typeof $("#txtVariant2").val() != 'undefined') {
                            v2 = $("#txtVariant2").val().toLowerCase();
                        }
                        if (typeof $("#txtVariant3").val() != 'undefined') {
                            v3 = $("#txtVariant3").val().toLowerCase();
                        }

                        if (op != v1 && op != v2 && op != v3 && v1 != v2 && v1 != v3 && v2 != v3) {
                            AspxABTesting.ABTestSaveUpdateSettings();
                        }
                        else {
                            SageFrame.messaging.show(getLocale(AspxABTestingLocale, "Provided URL for the pages should not be same!"), "alert");
                        }

                    }
                    else {
                        SageFrame.messaging.show(getLocale(AspxABTestingLocale, "Select atleast one user role by unselecting (--Select One--)"), "alert");
                        return false;
                    }
                }
            });
            $('#txtSearchABTestName').keypress(function (e) {
                if (e.keyCode == 13)
                    AspxABTesting.ABTestLoadStaticImage();
                AspxABTesting.gdvABTestsBind();
            });
            $("#btnSearchABTestName").click(function () {
                AspxABTesting.ABTestLoadStaticImage();
                AspxABTesting.gdvABTestsBind();
            });
            $("#ADay").click(function () {
                shortBy = 'day';
                AspxABTesting.ABTestDetails();
                AspxABTesting.ABTestChartShow();
                $(this).siblings().removeClass('active');
                $(this).addClass('active');

            });
            $("#AMonth").click(function () {
                shortBy = 'month';
                AspxABTesting.ABTestDetails();
                AspxABTesting.ABTestChartShow();
                $(this).siblings().removeClass('active');
                $(this).addClass('active');
            });
            $("#AYear").click(function () {
                shortBy = 'year';
                AspxABTesting.ABTestDetails();
                AspxABTesting.ABTestChartShow();
                $(this).siblings().removeClass('active');
                $(this).addClass('active');
            });

            $("#lblCancelFromDetails").click(function () {
                AspxABTesting.ClearForm();
                AspxABTesting.HideAll();
                $("#divABTestingHome").show();
                shortBy = '';
            });
        },
        ABTestRoleList: function () {
            this.config.method = "AspxCoreHandler.ashx/GetAllRoles";
            this.config.url = aspxservicePath + this.config.method;
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = AspxABTesting.ABTestRoleListSuccess;
            this.config.error = AspxABTesting.ABTestRoleListError;
            this.ajaxCall(this.config);
        },
        ABTestRoleListSuccess: function (msg) {
            if (msg.d) {
                $("#ddlUsersInRole").append('<option value="0" class="sfLocale">--Select One--</option>');
                $.each(msg.d, function (index, item) {
                    $("#ddlUsersInRole").append("<option value=" + item.RoleID + ">" + item.RoleName + "</option>");
                });
            }
        },
        ABTestRoleListError: function () {
            SageFrame.messaging.show(getLocale(AspxABTestingLocale, "Failed to load data!"), "error");
        },
        ABTestGetSettings: function () {
            this.config.method = "ABTestGetSettingsAll";
            this.config.url = ModuleServicePath + this.config.method;
            this.config.data = JSON2.stringify({ offset: 1, limit: 100, abTestName: null, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = AspxABTesting.ABTestGetSettingsSuccess;
            this.config.error = AspxABTesting.ABTestGetSettingsError;
            this.ajaxCall(this.config);
        },
        ABTestGetSettingsSuccess: function (msg) {
            if (msg.d) {
                $.each(msg.d, function (index, item) {
                    settingsInfo.OriginalPageURL = item.OriginalPageURL.toLowerCase();
                    settingsInfo.Variation1PageURL = item.Variation1PageURL.toLowerCase();
                    settingsInfo.Variation2PageURL = item.Variation2PageURL.toLowerCase();
                    settingsInfo.Variation3PageURL = item.Variation3PageURL.toLowerCase();
                    settingsInfo.IsActive = item.IsActive;
                    settings.push(settingsInfo);
                });
            }
        },
        ABTestGetSettingsError: function () {
            SageFrame.messaging.show(getLocale(AspxABTestingLocale, "Failed to load data!"), "error");
        },
        lblVariantOnClick: function (lblVID) {
            if (lblVID == 'lblV2') {
                AbTestingGlobalVars.countAddVariation = 2;
                $("#txtVariant2").val('');
                $('table#tblNewABTest tr#trVariant2').remove();
                if ($('#trVariant3').length > 0) {
                    var txt3value = $("#txtVariant3").val();
                    $('table#tblNewABTest tr#trVariant3').remove();
                    AspxABTesting.AddNewRowTable();
                    $("#txtVariant2").val(txt3value);
                }
                $("#lblAddVariation").show();
            }
            if (lblVID == 'lblV3') {
                $("#txtVariant3").val('');
                $('table#tblNewABTest tr#trVariant3').remove();
                AbTestingGlobalVars.countAddVariation = 3;
                $("#lblAddVariation").show();
            }
        },
        gdvABTestsBind: function () {
            var abTestName = $("#txtSearchABTestName").val().trim();
            this.config.method = "ABTestGetSettingsAll";
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvABTests_pagesize").length > 0) ? $("#gdvABTests_pagesize :selected").text() : 10;
            $("#gdvABTests").sagegrid({
                url: ModuleServicePath,
                functionMethod: this.config.method,
                colModel: [
                    { display: getLocale(AspxABTestingLocale, 'ABTest ID'), name: 'abTestID', cssclass: 'cssClassABTestLabel', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxABTestingLocale, 'A/B Test Name'), name: 'abTestName', cssclass: 'cssClassABTestLabel', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxABTestingLocale, 'Original Page URL'), name: 'orginalPageURL', cssclass: 'cssClassABTestLabel', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxABTestingLocale, 'Variation1 Page URL'), name: 'variation1PageURL', cssclass: 'cssClassABTestLabel', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxABTestingLocale, 'Variation2 Page URL'), name: 'variation2PageURL', cssclass: 'cssClassABTestLabel', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxABTestingLocale, 'Variation3 Page URL'), name: 'variation3PageURL', cssclass: 'cssClassABTestLabel', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxABTestingLocale, 'Traffic Percentage'), name: 'trafficPercentage', cssclass: 'cssClassABTestLabel', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxABTestingLocale, 'Email Notification'), name: 'emailNotification', cssclass: 'cssClassABTestLabel', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'True/False', hide: true },
                    { display: getLocale(AspxABTestingLocale, 'Start Date'), name: 'startDate', cssclass: 'cssClassABTestLabel', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxABTestingLocale, 'End Date'), name: 'EndsOnDate', cssclass: 'cssClassABTestLabel', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxABTestingLocale, 'Ends On Max Visit'), name: 'EndsOnMaxVisit', cssclass: 'cssClassABTestLabel', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: getLocale(AspxABTestingLocale, 'Users Role'), name: 'UsersInRole', cssclass: 'cssClassABTestLabel', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxABTestingLocale, 'Active'), name: 'isActive', cssclass: 'cssClassABTestLabel', coltype: 'label', align: 'left', type: 'boolean', format: 'True/False', hide: true },
                    { display: getLocale(AspxABTestingLocale, 'Status'), name: 'Status', cssclass: 'cssClassABTestLabel', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxABTestingLocale, 'Actions'), name: 'action', cssclass: 'cssClassABTestLabel', coltype: 'label', align: 'center' }
                ],
                buttons: [
                    { display: getLocale(AspxABTestingLocale, 'View'), name: 'view', enable: true, _event: 'click', trigger: '3', callMethod: 'AspxABTesting.ABTestViewSetting', arguments: '1,2,3,4,5,6,7,8,9,10,11,12,13,14' },
                    { display: getLocale(AspxABTestingLocale, 'Edit'), name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'AspxABTesting.ABTestEditSettings', arguments: '1,2,3,4,5,6,7,8,9,10,11,12,13,14' },
                    { display: getLocale(AspxABTestingLocale, 'Delete'), name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'AspxABTesting.ABTestDeleteSettings', arguments: '1' }

                ],
                rp: perpage,
                nomsg: getLocale(AspxABTestingLocale, "No Records Found!"),
                param: { abTestName: abTestName, aspxCommonObj: aspxCommonObj },
                current: current_,
                pnew: offset_,
                sortcol: { 10: { sorter: false }, 11: { sorter: false }, 12: { sorter: false }, 13: { sorter: false }, 14: { sorter: false} }

            });
        },
        ABTestViewSetting: function (tblID, argus) {
            switch (tblID) {
                case "gdvABTests":
                    AspxABTesting.HideAll();
                    $("#divTestDetailForm").show();
                    if (argus != undefined) {
                        $("#hdnABTestIDforView").val(argus[0]);
                        $("#hdnABTestNameforView").val(argus[3]);
                    }
                    AspxABTesting.ABTestDetails();
                    AspxABTesting.ABTestChartShow($("#hdnABTestIDforView").val());
                    break;
            }

        },
        ABTestDetails: function () {
            this.config.method = "ABTestResultByID";
            this.config.url = ModuleServicePath + "ABTestResultByID";
            this.config.data = JSON2.stringify({ abTestID: $("#hdnABTestIDforView").val(), shortBy: shortBy, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = AspxABTesting.ABTestViewSettingSuccess;
            this.config.error = AspxABTesting.ABTestViewSettingError;
            this.ajaxCall(this.config);
        },
        ABTestViewSettingSuccess: function (data) {
            if (data.d.length > 0) {
                $("#spanTestName").html($("#hdnABTestNameforView").val());
                var c1 = 0;
                var c2 = 0;
                var c3 = 0;
                var var1 = '';
                var var2 = '';
                var var3 = '';
                var tableElements = '';
                $("#spanStatus").html(getLocale(AspxABTestingLocale, 'No winner yet - Testing still running')).css({ "font-weight": "normal", "color": "white", "font-size": "12px" });
                $.each(data.d, function (index, value) {
                    $("#spanVisits").html(value.TotalVisit);
                    $("#spanDaysOfData").html(value.DaysOfData);
                    tableElements += '<tr>';
                    tableElements += '<td>' + value.Variation + '</td>';
                    tableElements += '<td>' + value.Visit + '</td>';
                    tableElements += '<td>' + value.Conversion + '</td>';
                    tableElements += '<td>' + value.ConversionRate + '</td>';
                    if (index == 0) {
                        var1 = value.Variation;
                        tableElements += '<td>' + value.CompareToOriginal + '</td>';
                    }
                    var img = '';
                    if (index == 1) {
                        c1 = value.CompareToOriginal.substring(0, value.CompareToOriginal.length - 1);
                        var1 = value.Variation;
                        if (c1 > c2 && c1 > c3 && c1 >= 80 && value.Status == '1') {
                            $("#spanStatus").html('Winner - ' + var1).css({ "font-weight": "bold", "color": "orange", "font-size": "10px" });
                            img = '<input type="image" alt="" width="10" height="10"  src="' + modulePath + 'Images/winner.png" />' + ' ';
                        }
                        tableElements += '<td>' + img + value.CompareToOriginal + '</td>';
                    }
                    if (index == 2) {
                        c2 = value.CompareToOriginal.substring(0, value.CompareToOriginal.length - 1);
                        var2 = value.Variation;
                        if (c2 > c3 && c2 > c1 && c2 >= 80 && value.Status == '1') {
                            $("#spanStatus").html('Winner - ' + var2).css({ "font-weight": "bold", "color": "green", "font-size": "10px" });
                            img = '<input type="image" alt="" width="10" height="10"  src="' + modulePath + 'Images/winner.png" />' + ' ';
                        }
                        tableElements += '<td>' + img + value.CompareToOriginal + '</td>';
                    }
                    if (index == 3) {
                        c3 = value.CompareToOriginal.substring(0, value.CompareToOriginal.length - 1);
                        var3 = value.Variation;
                        if (c3 > c1 && c3 > c2 && c3 >= 80 && value.Status == '1') {
                            $("#spanStatus").html('Winner - ' + var3).css({ "font-weight": "bold", "color": "green", "font-size": "10px" });
                            img = '<input type="image" alt="" width="10" height="10"  src="' + modulePath + 'Images/winner.png" />' + ' ';
                        }
                        tableElements += '<td>' + img + value.CompareToOriginal + '</td>';
                    }
                    tableElements += '<td>' + value.ChancesToBeatOriginal + '</td>';
                });
                tableElements += '</tr>';
                $("#tblTestDetails").find('tbody').html(tableElements);

            }
            else {
                var tableElements = '';
                tableElements += '<tr>';
                tableElements += '<td colspan="6"> No Data Found </td>';
                tableElements += '</tr>';
                $("#tblTestDetails").find('tbody').html(tableElements);
            }

        },
        ABTestViewSettingError: function () {
            SageFrame.messaging.show(getLocale(AspxABTestingLocale, "Failed to load test details!"), "error");
        },

        ABTestChartShow: function () {
            this.config.method = "ABTestConversionRateForChart";
            this.config.url = ModuleServicePath + this.config.method;
            this.config.data = JSON2.stringify({ abTestID: $("#hdnABTestIDforView").val(), shortBy: shortBy, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = AspxABTesting.ABTestChartShowSuccess;
            this.config.error = AspxABTesting.ABTestChartShowError;
            this.ajaxCall(this.config);
        },
        ABTestChartShowSuccess: function (data) {

            if (data.d.First.length) {
                $("#divABTestDetail").show();
                               var DailyVisit = [];
                var DailyVisit1 = [];
                var DailyVisit2 = [];
                var DailyVisit3 = [];
                if (data.d.First.length > 0) {
                    var arrVisit = [];
                    $.each(data.d.First, function (index, item) {
                        arrVisit = [];
                        arrVisit.push(item.VisitedDate);
                        arrVisit.push(item.ConversionRate);
                        DailyVisit.push(arrVisit);
                    });
                }
                if (data.d.Second.length > 0) {
                    var arrVisit1 = [];
                    $.each(data.d.Second, function (index, item) {
                        arrVisit1 = [];
                        arrVisit1.push(item.VisitedDate);
                        arrVisit1.push(item.ConversionRate);
                        DailyVisit1.push(arrVisit1);
                    });
                }
                if (data.d.Third.length > 0) {
                    var arrVisit2 = [];
                    $.each(data.d.Third, function (index, item) {
                        arrVisit2 = [];
                        arrVisit2.push(item.VisitedDate);
                        arrVisit2.push(item.ConversionRate);
                        DailyVisit2.push(arrVisit2);
                    });
                }
                if (data.d.Fourth.length > 0) {
                    var arrVisit3 = [];
                    $.each(data.d.Fourth, function (index, item) {
                        arrVisit3 = [];
                        arrVisit3.push(item.VisitedDate);
                        arrVisit3.push(item.ConversionRate);
                        DailyVisit3.push(arrVisit3);
                    });
                }
                AspxABTesting.DateWiseVisitMulti(DailyVisit, DailyVisit1, DailyVisit2, DailyVisit3);
            }
            else {
                $("#divABTestDetail").hide();
               
            }
        },
        ABTestChartShowError: function () {
            SageFrame.messaging.show(getLocale(AspxABTestingLocale, "Failed to load test chart!"), "error");
        },
        DateWiseVisitMulti: function (DailyVisit, DailyVisit1, DailyVisit2, DailyVisit3) {
            $('#divABTestVisit').html('');
            var a1 = [];
            var a2 = [];
            var a3 = [];
            var a4 = [];
            a1 = DailyVisit;            a2 = DailyVisit1;            a3 = DailyVisit2;            a4 = DailyVisit3;
            var sr = [];
            sr = [a1, a2];
            jQuery.jqplot.config.enablePlugins = true;
            if (a3.length != 0) {
                sr = [a1, a2, a3];
            }
            if (a4.length != 0) {
                sr = [a1, a2, a3, a4];
            }

            var plot1 = $.jqplot('divABTestVisit', sr, {
                title: 'Date wise Convesion',
                seriesDefaults: {
                    pointLabels: {
                        show: true
                    }
                },
                axes: {
                    xaxis: {
                        pad: 1,
                        renderer: $.jqplot.DateAxisRenderer,
                        tickOptions: {
                            formatString: '%b %#d'
                        },
                        showLabel: true,
                        label: 'Date Ranges'
                    },
                    yaxis: {
                        labelRenderer: $.jqplot.CanvasAxisLabelRenderer,
                        tickOptions: {
                            formatString: '%#.2f'
                        },
                        showLabel: true,
                        label: 'Conversions'
                    }
                },
                highlighter: {
                    sizeAdjust: 7.5
                },
                cursor: {
                    show: true,
                    zoom: true,
                    tooltipLocation: 'ne'
                },
                legend: {
                    renderer: $.jqplot.EnhancedLegendRenderer,
                    show: true,
                    location: 'se',
                    labels: ['Original Page', 'Variation1', 'Variation2', 'variation3']
                },
                series: [
                    { color: 'red' },
                    { color: 'blue' },
                    { color: 'violet' },
                    { color: 'orange' }
                ]
            });
        },
        ABTestEditSettings: function (tblID, argus) {
            switch (tblID) {
                case "gdvABTests":
                    $("#txtEndsOnDate").datepicker({ dateFormat: 'mm/dd/yy', minDate: 1 });
                    $('#lblABTestingHead').html('Edit');
                    $('#trABTestEnd').show();
                    $("#hdnABTestID").val(argus[0]);
                    $("#txtABTestName").val(argus[3]);
                    $("#txtOriginalPage").val(argus[4]);
                    $("#txtVariant1").val(argus[5]);
                    if (argus[6] != '') {
                        AspxABTesting.AddNewRowTable();
                        $("#txtVariant2").val(argus[6]);
                    }
                    if (argus[7] != '') {
                        AspxABTesting.AddNewRowTable();
                        $("#txtVariant3").val(argus[7]);
                    }
                                       $('#ddlABTestEmailNotification').val('' + AspxABTesting.Boolean(argus[9]) + '');
                    $('#txtEndsOnDate').val(argus[11].trim());
                    AspxABTesting.ABTestRoleList();
                    var str = argus[13];
                    var array = str.split(',');
                    $.each(array, function (index, value) {
                        $("#ddlUsersInRole option").each(function () {
                            if ($(this).text() == array[index]) {
                                $(this).prop("selected", "selected");
                            }
                        });
                    });
                    $('#ddlABTestEnd').val('' + AspxABTesting.Boolean(argus[14]) + '');
                    AspxABTesting.HideAll();
                    $("#divABTestingAdd").show();
                    break;
            }
        },
        AddNewRowTable: function () {
            if (AbTestingGlobalVars.countAddVariation <= 3) {
                var tblElements = '';
                tblElements += '<tr id="trVariant' + AbTestingGlobalVars.countAddVariation + '">';
                tblElements += '<td>';
                tblElements += '<label id="lblVariant' + AbTestingGlobalVars.countAddVariation + '" class="sfFormlabel sfLocale">Variant' + AbTestingGlobalVars.countAddVariation + ' :</label>';
                tblElements += '</td>';
                tblElements += '<td>';
                tblElements += '<input type="text" id="txtVariant' + AbTestingGlobalVars.countAddVariation + '" name="Variant' + AbTestingGlobalVars.countAddVariation + '" class="sfInputbox required" onchange="AspxABTesting.OnChangeEventPageURL(this.value, this.id);"/>'
                tblElements += '&nbsp;<label id="lblV' + AbTestingGlobalVars.countAddVariation + '" class="sfBtn icon-close" onclick="AspxABTesting.lblVariantOnClick(this.id);"></label>'
                tblElements += '</td>';
                tblElements += '</tr>';
                $("#tblNewABTest").append(tblElements);
                AbTestingGlobalVars.countAddVariation = AbTestingGlobalVars.countAddVariation + 1;
                if (AbTestingGlobalVars.countAddVariation == 4) {
                    $("#lblAddVariation").hide();
                }

            }
            else {
                SageFrame.messaging.show(getLocale(AspxABTestingLocale, "Maximum allowed variation has been already added."), "alert");
            }
        },
        ABTestSaveUpdateSettings: function () {
            settingsInfo.ABTestID = $("#hdnABTestID").val();
            settingsInfo.ABTestName = $("#txtABTestName").val();
            settingsInfo.OriginalPageURL = $("#txtOriginalPage").val().toLowerCase();
            settingsInfo.Variation1PageURL = $("#txtVariant1").val().toLowerCase();
            if (typeof $("#txtVariant2").val() != 'undefined') {
                settingsInfo.Variation2PageURL = $("#txtVariant2").val().toLowerCase();
            }
            else {
                settingsInfo.Variation2PageURL = "";
            }
            if (typeof $("#txtVariant3").val() != 'undefined') {
                settingsInfo.Variation3PageURL = $("#txtVariant3").val().toLowerCase();
            }
            else {
                settingsInfo.Variation3PageURL = "";
            }
            settingsInfo.TrafficPercentage = 0;
            settingsInfo.EmailNotification = AspxABTesting.Boolean($("#ddlABTestEmailNotification option:selected").val());
            settingsInfo.IsActive = AspxABTesting.Boolean($("#ddlABTestEnd option:selected").val());
            var str1 = '';
            var strRoleName = '';
            $("#ddlUsersInRole option:selected").each(function () {
                str1 += $(this).text() + ',';
                strRoleName = str1.substr(0, str1.length - 1)
            });
            settingsInfo.UsersInRole = strRoleName;
            settingsInfo.EndsOnDate = $("#txtEndsOnDate").val();

            this.config.method = "ABTestSaveUpdateSettings";
            this.config.url = ModuleServicePath + this.config.method;
            this.config.data = JSON2.stringify({ settingsInfo: settingsInfo, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = AspxABTesting.SaveUpdateSettingsSuccess;
            this.config.error = AspxABTesting.SaveUpdateSettingsError;
            this.ajaxCall(this.config);
        },
        SaveUpdateSettingsSuccess: function (msg) {
            SageFrame.messaging.show(getLocale(AspxABTestingLocale, "Settings saved successfully!"), "success");
            AspxABTesting.ClearForm();
            AspxABTesting.HideAll();
            $("#divABTestingHome").show();
            AspxABTesting.gdvABTestsBind();
        },
        SaveUpdateSettingsError: function () {
            SageFrame.messaging.show(getLocale(AspxABTestingLocale, "Failed to save settings!"), "error");
        },
        ABTestDeleteSettings: function (tblID, argus) {
            switch (tblID) {
                case "gdvABTests":
                    var properties = {
                        onComplete: function (e) {
                            AspxABTesting.ABTestDeleteSettingsByID(argus[0], e);
                        }
                    };
                    csscody.confirm("<h2>" + getLocale(AspxABTestingLocale, 'Delete Confirmation') + '</h2><p>' + getLocale(AspxABTestingLocale, 'Are you sure you want to delete?') + "</p>", properties);
                    break;
                default:
                    break;
            }
        },
        ABTestDeleteSettingsByID: function (testID, event) {
            if (event) {
                this.config.method = "ABTestDeleteSettings";
                this.config.url = ModuleServicePath + this.config.method;
                this.config.data = JSON2.stringify({ abTestID: testID, aspxCommonObj: aspxCommonObj });
                this.config.ajaxCallMode = AspxABTesting.ABTestDeleteSettingsByIDSuccess;
                this.config.error = AspxABTesting.ABTestDeleteSettingsByIDError;
                this.ajaxCall(this.config);
            }
            return false;
        },
        ABTestDeleteSettingsByIDSuccess: function (msg) {
            AspxABTesting.ABTestLoadStaticImage();
            AspxABTesting.gdvABTestsBind();
            SageFrame.messaging.show(getLocale(AspxABTestingLocale, "Settings deleted successfully!"), "success");
        },
        ABTestDeleteSettingsByIDError: function () {
            SageFrame.messaging.show(getLocale(AspxABTestingLocale, "Failed to delete settings!"), "error");
        },
        OnChangeEventPageURL: function (textBoxValue, textBoxID) {
            if (textBoxValue != '') {
                AspxABTesting.CheckPageExists(textBoxValue);
                if (AbTestingGlobalVars.isPageExists == false) {
                    $("#" + textBoxID).val('');
                }
                AbTestingGlobalVars.isPageExists = false;
            }
        },
        CheckPageExists: function (pageTabPath) {
            this.config.method = "ABTestCheckPageExists";
            this.config.url = ModuleServicePath + this.config.method;
            this.config.data = JSON2.stringify({ pageTabPath: pageTabPath, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = AspxABTesting.CheckPageExistsSuccess;
            this.config.error = AspxABTesting.CheckPageExistsError;
            this.ajaxCall(this.config);
        },
        CheckPageExistsSuccess: function (msg) {
            if (msg.d) {
                AbTestingGlobalVars.isPageExists = true;
            }
            else {
                SageFrame.messaging.show(getLocale(AspxABTestingLocale, "Provided URL for the page does not exists!"), "alert");
            }
        },
        CheckPageExistsError: function () {
            SageFrame.messaging.show(getLocale(AspxABTestingLocale, "Failed to load page satatus!"), "error");
        },
        ABTestLoadStaticImage: function () {
            $('#ajaxABTestImage').prop('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
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
        HideAll: function () {
            $("#divABTestingHome").hide();
            $("#divABTestingAdd").hide();
            $("#divTestDetailForm").hide();
        },
        ClearForm: function () {
            $("#txtABTestName").val('');
            $("#txtEndsOnDate").val('');
            $("#ddlUsersInRole").empty();
            $("#txtOriginalPage").val('');
            $("#txtVariant1").val('');
            $("#txtVariant2").val('');
            $("#txtVariant3").val('');
            $('table#tblNewABTest tr#trVariant2').remove();
            $('table#tblNewABTest tr#trVariant3').remove();
            AbTestingGlobalVars.countAddVariation = 2;
            $("#txtSearchABTestName").val('');
            $('#trABTestEnd').hide();
            $("#hdnABTestIDforView").val(0);
            $("#hdnABTestID").val(0);

        }
    };

    AspxABTesting.Init();



});