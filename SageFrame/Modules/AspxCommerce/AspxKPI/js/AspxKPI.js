var AspxKPI;
$(function () {
    var $accor = '';
     var ModuleServicePath = modulePath + "AspxKPIHandler.ashx/";
    var aspxCommonObj = AspxCommerce.AspxCommonObj();
    var shortBy = null;
    var metricShort = null;
    var metricNameShort = 'all';
    var metrics = null;
    var SettingsInfo = {
        KPISettingsID: 0,
        IsActive: false,
        EmailNotification: false,
        StartDate: '',
        EndDate: '',
        IPInfoDBAPIkey:''
    };

    AspxKPI = {
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
                type: AspxKPI.config.type,
                contentType: AspxKPI.config.contentType,
                cache: AspxKPI.config.cache,
                async: AspxKPI.config.async,
                url: AspxKPI.config.url,
                data: AspxKPI.config.data,
                dataType: AspxKPI.config.dataType,
                success: AspxKPI.config.ajaxCallMode,
                error: AspxKPI.config.error
            });
        },
        Init: function () {        
            $('.block, .funnel').addClass('animate');
            $("#txtEndDate").datepicker({ dateFormat: 'mm/dd/yy', minDate: 1 });
            var formVal = $("#form1").validate({
                ignore: ":hidden",
                messages: {
                    EndDate: {
                        required: getLocale(AspxKPILanguage,'* select a date')
                    }
                }
            });
            AspxKPI.HideAll();
            AspxKPI.InitializeTabs();
            AspxKPI.InitializeTabs2();
            AspxKPI.KPIFunnelCartGetAll('all');
            $("#btnSaveKPISettings").click(function () {
                if (formVal.form()) {
                    AspxKPI.KPISaveUpdateSettings();
                }
            });
            $("#btnRefrershKPISettings").click(function () {
                AspxKPI.KPISettingsGetAll();
            });
            $("#ADay").click(function () {
                AspxKPI.KPIFunnelCartGetAll('day');
            });
            $("#AWeek").click(function () {
                AspxKPI.KPIFunnelCartGetAll('week');
            });
            $("#AMonth").click(function () {
                AspxKPI.KPIFunnelCartGetAll('month');
            });
            $("#AYear").click(function () {
                AspxKPI.KPIFunnelCartGetAll('year');
            });
            $("#AAll").click(function () {
                AspxKPI.KPIFunnelCartGetAll('all');
            });
            $('#ui-id-1').click(function () {
                AspxKPI.KPIFunnelCartGetAll('all');
                $('.sfTabInterface a').removeClass("active");
                $("#AAll").addClass("active");

                $('.sfTabInterface a').click(function () {
                    $(this).siblings().removeClass("active");
                    $(this).addClass("active");
                });
            });
            $('#ui-id-2').click(function () {
                AspxKPI.KPISalesConversionsGetAll('all');
                $('.sfTabInterface a').removeClass("active");
                $("#AAllOS").addClass("active");

                $('.sfTabInterface a').click(function () {
                    $(this).siblings().removeClass("active");
                    $(this).addClass("active");
                });

            });
            $('#ui-id-3').click(function () {
                metricShort = getLocale(AspxKPILanguage,'country');
                metricNameShort = getLocale(AspxKPILanguage,'all');
                shortBy = null;
                metrics = getLocale(AspxKPILanguage,"All countries");
                AspxKPI.KPILocationsVisitGetAll(metricShort);
                AspxKPI.KPIVisitDetailsGetAll(metricNameShort);
                $('.sfTabInterface a').removeClass("active");
                $("#AVisitAll, #AVisitCountry").addClass("active");

                $('.sfTabInterface a').click(function () {
                    $(this).siblings().removeClass("active");
                    $(this).addClass("active");
                });
            });
            $('#ui-id-4').click(function () {                
                AspxKPI.KPISettingsGetAll();   
            });
            $("#ADayOS").click(function () {
                AspxKPI.KPISalesConversionsGetAll('day');
            });
            $("#AWeekOS").click(function () {
                AspxKPI.KPISalesConversionsGetAll('week');
            });
            $("#AMonthOS").click(function () {
                AspxKPI.KPISalesConversionsGetAll('month');
            });
            $("#AYearOS").click(function () {
                AspxKPI.KPISalesConversionsGetAll('year');
            });
            $("#AAllOS").click(function () {
                AspxKPI.KPISalesConversionsGetAll('all');
            });
            $("#AVisitCountry").click(function () {
                metricShort = getLocale(AspxKPILanguage,'country');
                metricNameShort = getLocale(AspxKPILanguage,'all');
                metrics = getLocale(AspxKPILanguage,'All countries');
                $("#lblMetrics").html(getLocale(AspxKPILanguage,'Country'));
                AspxKPI.KPILocationsVisitGetAll(metricShort);
                AspxKPI.KPIVisitDetailsGetAll(metricNameShort);
            });
            $("#AVisitCity").click(function () {
                metricShort = getLocale(AspxKPILanguage,'city');
                metricNameShort = getLocale(AspxKPILanguage,'all');
                metrics = getLocale(AspxKPILanguage,'All cities');
                $("#lblMetrics").html('City');
                AspxKPI.KPILocationsVisitGetAll(metricShort);
                AspxKPI.KPIVisitDetailsGetAll(metricNameShort);
            });
            $("#AVisitDay").click(function () {
                shortBy = 'day';
                AspxKPI.KPILocationsVisitGetAll(metricShort);
                AspxKPI.KPIVisitDetailsGetAll(metricNameShort);
            });
            $("#AVisitWeek").click(function () {
                shortBy = 'week';
                AspxKPI.KPILocationsVisitGetAll(metricShort);
                AspxKPI.KPIVisitDetailsGetAll(metricNameShort);
            });
            $("#AVisitMonth").click(function () {
                shortBy = 'month';
                AspxKPI.KPILocationsVisitGetAll(metricShort);
                AspxKPI.KPIVisitDetailsGetAll(metricNameShort);
            });
            $("#AVisitYear").click(function () {
                shortBy = 'year';
                AspxKPI.KPILocationsVisitGetAll(metricShort);
                AspxKPI.KPIVisitDetailsGetAll(metricNameShort);

            });
            $("#AVisitAll").click(function () {
                shortBy = 'all';
                AspxKPI.KPILocationsVisitGetAll(metricShort);
                AspxKPI.KPIVisitDetailsGetAll(metricNameShort);
            });
        },
        KPIVisitDetailsGetAll: function (mtCSShort) {
            metricNameShort = mtCSShort;
            var shrtBy = shortBy;
            var location = metricNameShort;
            if (shortBy == null) {
                shrtBy = 'all';
            }
            if (metricNameShort == 'all') {
                location = metrics;
            }
            if (metricNameShort == '(Not Set)' || metricNameShort == '') {
                metricNameShort = null;
                location = 'Not set';
            }
            $(".cssClassspanVisitMetric").empty();
            var txtSpan = '';
            txtSpan += '<label class="cssClassShortBy">'+getLocale(AspxKPILanguage,"Sort By :")+'</label>&nbsp;&nbsp;';
            txtSpan += '<label>' + location + '</label>,&nbsp;&nbsp;';
            txtSpan += '<label>' + shrtBy + '</label>';
            $(".cssClassspanVisitMetric").append(txtSpan);

            $(".cssClassspanLocationMetric").empty();
            var txtSpanLocation = '';
            txtSpanLocation += '<label  class="cssClassShortBy">'+getLocale(AspxKPILanguage,"Sort By :")+' </label>&nbsp;&nbsp;';
            txtSpanLocation += '<label>' + metrics + '</label>,&nbsp;&nbsp;';
            txtSpanLocation += '<label>' + shrtBy + '</label>';
            $(".cssClassspanLocationMetric").append(txtSpanLocation);

            this.config.method = "KPIVisitDetailsGetAll";
            this.config.url = ModuleServicePath + this.config.method;
            this.config.data = JSON2.stringify({ metric: metricNameShort, shortBy: shortBy, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = AspxKPI.KPIVisitDetailsGetAllSuccess;
            this.config.error = AspxKPI.KPIVisitDetailsGetAllError;
            this.ajaxCall(this.config);
        },
        KPIVisitDetailsGetAllSuccess: function (data) {
            $("#spanTotalVisit").html(0);
            var tblVisitElements = '';
            var tblViewElements = '';
            var newVisitor = 0;
            var retVisitor = 0;
            if (data.d.Visitor.length > 0) {
                $.each(data.d.Visitor, function (index, value) {
                    $("#spanNewVisitors").html(value.NewVisitor);
                    $("#spanReturningVisitors").html(value.ReturningVisitor);
                    newVisitor = value.NewVisitor;
                    retVisitor = value.ReturningVisitor;
                    $("#spanTotalVisitor").html(value.TotalVisitor);
                });
            }
            if (newVisitor > 0 || retVisitor > 0) {
                AspxKPI.ShowVisitorsChart(newVisitor, retVisitor);
            }
            else {
                $("#divVisitorsChart").empty();
                $("#divVisitorsChart").append('<label>'+getLocale(AspxKPILanguage,"No status yet, check back later.")+'</label>');
            }
            if (data.d.PageViews.length > 0) {
                $.each(data.d.PageViews, function (index, value) {
                    tblViewElements += '<tr>';
                    tblViewElements += '<td>' + value.SubTabPath + '</td>';
                    tblViewElements += '<td>' + value.Visits + '</td>';
                    tblViewElements += '<td>';
                    tblViewElements += '<span style="width:' + value.VisitsPer + '%;" class="sfPositive ">&nbsp;</span>';
                    tblViewElements += '<span style="width:' + parseFloat(parseFloat(100.00).toFixed(2) - parseFloat(value.VisitsPer).toFixed(2)).toFixed(2) + '%;" class="sfNegative ">&nbsp;</span>';
                    tblViewElements += '</td>';
                    tblViewElements += '<td>' + value.VisitsPer + '%' + '</td>';
                    tblViewElements += '<td>' + value.AverageDuration + '</td>';
                    tblViewElements += '</tr>';
                    $("#spanTotalVisit").html(value.TotalVisits);
                });
            }
            else {
                tblViewElements += '<tr><td colspan="5"><label>'+getLocale(AspxKPILanguage,"No status yet, check back later.")+'</label></td></tr>';
            }

            $("#tblViews").find('tbody').html(tblViewElements);
        },
        KPIVisitDetailsGetAllError: function () {
            SageFrame.messaging.show("Failed to get visit information!", "error");
        },
        ShowVisitorsChart: function (newVisitor, retVisitor) {
            $("#divVisitorsChart").empty();
            var s1 = [['New', newVisitor], ['Returning', retVisitor]];
            $.jqplot.config.enablePlugins = false;
            var plot1 = $.jqplot('divVisitorsChart', [s1], {
                seriesDefaults: {                    
                    renderer: $.jqplot.DonutRenderer,
                    rendererOptions: {
                        sliceMargin: 3,
                        startAngle: 180,
                        showDataLabels: true,
                        dataLabelThreshold: 1,
                        dataLabelCenterOn: true
                    }
                },
                legend: {
                    renderer: $.jqplot.DonutLegendRenderer,
                    show: false,
                    location: 'se',
                    labels: ['New', 'Returning']
                },
                grid: {
                    background: 'transparent',
                    borderColor: 'transparent',
                    shadow: false
                },
                seriesColors: ["#008080", "#084B8A"],
            });
        },
        KPILocationsVisitGetAll: function (metricShort) {
            this.config.method = "KPILocationsVisitGetAll";
            this.config.url = ModuleServicePath + this.config.method;
            this.config.data = JSON2.stringify({ metric: metricShort, shortBy: shortBy, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = AspxKPI.KPILocationsVisitGetAllSuccess;
            this.config.error = AspxKPI.KPILocationsVisitGetAllError;
            this.ajaxCall(this.config);
        },
        KPILocationsVisitGetAllSuccess: function (data) {
            var tblElements = '';
            if (data.d.length > 0) {
                $.each(data.d, function (index, value) {
                    tblElements += '<tr>';
                    tblElements += '<td><a href="#" onclick="AspxKPI.KPIVisitDetailsGetAll(\'' + value.MetricName + '\');">' + value.MetricName + '</a></td>';
                    tblElements += '<td>' + value.Visits + '</td>';
                    tblElements += '<td>';
                    tblElements += '<span style="width:' + value.VisitPer + '%;" class="sfPositive ">&nbsp;</span>';
                    tblElements += '<span style="width:' + parseFloat(parseFloat(100.00).toFixed(2) - parseFloat(value.VisitPer).toFixed(2)).toFixed(2) + '%;" class="sfNegative ">&nbsp;</span>';
                    tblElements += '</td>';
                    tblElements += '<td>' + value.VisitPer + '%' + '</td>';
                    tblElements += '</tr>';
                });
            }
            else {
                tblElements += '<tr><td colspan="4"><label>'+getLocale(AspxKPILanguage,"No status yet, check back later.")+'</label></td></tr>';
            }
            $("#tblLocationsDetails").find('tbody').html(tblElements);
        },
        KPILocationsVisitGetAllError: function () {
            SageFrame.messaging.show("Failed to get visit location information!", "error");
        },
        KPISalesConversionsGetAll: function (shortBy) {
            this.config.method = "KPISalesConversionsGetAll";
            this.config.url = ModuleServicePath + this.config.method;
            this.config.data = JSON2.stringify({ shortBy: shortBy, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = AspxKPI.KPISalesConversionsGetAllSuccess;
            this.config.error = AspxKPI.KPISalesConversionsGetAllError;
            this.ajaxCall(this.config);
        },
        KPISalesConversionsGetAllSuccess: function (data) {
            var tableElements = '';
            var totalOrders = 0;
            var orderByNewAccounts = 0;
            var orderByExistingAccount = 0;
            var orderByGuest = 0;
            var totalSales = 0;
            var salesByNewAccounts = 0;
            var salesByExistingAccount = 0;
            var salesByGuest = 0;

            if (data.d) {
                totalOrders = data.d.TotalOrders;
                $("#spanTotalOrders").html(totalOrders);
                $("#spanTotalOrders2").html(totalOrders);

                totalSales = data.d.TotalSales;
                var totSales = data.d.TotalSales;
                $("#spanTotalSales2Unit").empty();
                $("#spanTotalSalesUnit").empty();
                if (eval(totSales) >= 1000 && eval(totSales) < 1000000) {
                    totSales = (eval(totSales) / 1000).toFixed(2);
                    $("#spanTotalSales2Unit").html(' K');
                    $("#spanTotalSalesUnit").html(' K');
                }
                if (eval(totSales) >= 1000000 && eval(totSales) < 1000000000) {
                    totSales = (eval(totSales) / 1000000).toFixed(2);
                    $("#spanTotalSales2Unit").html(' M');
                    $("#spanTotalSalesUnit").html(' M');
                }
                if (eval(totSales) >= 1000000000) {
                    totSales = (eval(totSales) / 1000000000).toFixed(2);
                    $("#spanTotalSales2Unit").html(' B');
                    $("#spanTotalSalesUnit").html(' B');
                }
                $("#spanTotalSales").html(totSales);
                $("#spanTotalSales2").html(totSales);
                
                var totalAvgOrdeValue = data.d.TotalAverageOrderValue;
                $("#spanTotalAverageOrderValueUnit").empty();
                if (eval(totalAvgOrdeValue) >= 1000 && eval(totalAvgOrdeValue) < 1000000) {
                    totalAvgOrdeValue = (eval(totalAvgOrdeValue) / 1000).toFixed(2);
                    $("#spanTotalAverageOrderValueUnit").html(' K');
                }
                if (eval(totalAvgOrdeValue) >= 1000000 && eval(totalAvgOrdeValue) < 1000000000) {
                    totalAvgOrdeValue = (eval(totalAvgOrdeValue) / 1000000).toFixed(2);
                    $("#spanTotalAverageOrderValueUnit").html(' M');
                }
                if (eval(totalAvgOrdeValue) >= 1000000000) {
                    totalAvgOrdeValue = (eval(totalAvgOrdeValue) / 1000000000).toFixed(2);
                    $("#spanTotalAverageOrderValueUnit").html(' B');
                }
                $("#spanTotalAverageOrderValue").html(totalAvgOrdeValue);

                var avgOrdeValueByNew = data.d.AverageOrderValueByNewAccount;
                $("#spanAverageOrderValueByNewAccountUnit").empty();
                if (eval(avgOrdeValueByNew) >= 1000 && eval(avgOrdeValueByNew) < 1000000) {
                    avgOrdeValueByNew = (eval(avgOrdeValueByNew) / 1000).toFixed(2);
                    $("#spanAverageOrderValueByNewAccountUnit").html(' K');
                }
                if (eval(avgOrdeValueByNew) >= 1000000 && eval(avgOrdeValueByNew) < 1000000000) {
                    avgOrdeValueByNew = (eval(avgOrdeValueByNew) / 1000000).toFixed(2);
                    $("#spanAverageOrderValueByNewAccountUnit").html(' M');
                }
                if (eval(avgOrdeValueByNew) >= 1000000000) {
                    avgOrdeValueByNew = (eval(avgOrdeValueByNew) / 1000000000).toFixed(2);
                    $("#spanAverageOrderValueByNewAccountUnit").html(' B');
                }
                $("#spanAverageOrderValueByNewAccount").html(avgOrdeValueByNew);

                var avgOrdeValueByExt = data.d.AverageOrderValueByExistingAccount;
                $("#spanAverageOrderValueByExistingAccountUnit").empty();
                if (eval(avgOrdeValueByExt) >= 1000 && eval(avgOrdeValueByExt) < 1000000) {
                    avgOrdeValueByExt = (eval(avgOrdeValueByExt) / 1000).toFixed(2);
                    $("#spanAverageOrderValueByExistingAccountUnit").html(' K');
                }
                if (eval(avgOrdeValueByExt) >= 1000000 && eval(avgOrdeValueByExt) < 1000000000) {
                    avgOrdeValueByExt = (eval(avgOrdeValueByExt) / 1000000).toFixed(2);
                    $("#spanAverageOrderValueByExistingAccountUnit").html(' M');
                }
                if (eval(avgOrdeValueByExt) >= 1000000000) {
                    avgOrdeValueByExt = (eval(avgOrdeValueByExt) / 1000000000).toFixed(2);
                    $("#spanAverageOrderValueByExistingAccountUnit").html(' B');
                }
                $("#spanAverageOrderValueByExistingAccount").html(avgOrdeValueByExt);

                var avgOrdeValueByGst = data.d.AverageOrderValueByGuest;
                $("#spanAverageOrderValueByGuestUnit").empty();
                if (eval(avgOrdeValueByGst) >= 1000 && eval(avgOrdeValueByGst) < 1000000) {
                    avgOrdeValueByGst = (eval(avgOrdeValueByGst) / 1000).toFixed(2);
                    $("#spanAverageOrderValueByGuestUnit").html(' K');
                }
                if (eval(avgOrdeValueByGst) >= 1000000 && eval(avgOrdeValueByGst) < 1000000000) {
                    avgOrdeValueByGst = (eval(avgOrdeValueByGst) / 1000000).toFixed(2);
                    $("#spanAverageOrderValueByGuestUnit").html(' M');
                }
                if (eval(avgOrdeValueByGst) >= 1000000000) {
                    avgOrdeValueByGst = (eval(avgOrdeValueByGst) / 1000000000).toFixed(2);
                    $("#spanAverageOrderValueByGuestUnit").html(' B');
                }
                $("#spanAverageOrderValueByGuest").html(avgOrdeValueByGst);

                $("#spanItemsSoldPerOrder").html(data.d.ItemsSoldPerOrder);
                $("#spanTotalSKUSold").html(data.d.TotalSKUSold);
                $("#spanTotlaItemsSold").html(data.d.TotlaItemsSold);

                var totDiscount = data.d.TotalDiscount;
                $("#spanTotalDiscountUnit").empty();
                if (eval(totDiscount) >= 1000 && eval(totDiscount) < 1000000) {
                    totDiscount = (eval(totDiscount) / 1000).toFixed(2);
                    $("#spanTotalDiscountUnit").html(' K');
                }
                if (eval(totDiscount) >= 1000000 && eval(totDiscount) < 1000000000) {
                    totDiscount = (eval(totDiscount) / 1000000).toFixed(2);
                    $("#spanTotalDiscountUnit").html(' M');
                }
                if (eval(totDiscount) >= 1000000000) {
                    totDiscount = (eval(totDiscount) / 1000000000).toFixed(2);
                    $("#spanTotalDiscountUnit").html(' B');
                }
                $("#spanTotalDiscount").html(totDiscount);

                var avgDiscount = data.d.AverageDiscount;
                $("#spanAverageDiscountUnit").empty();
                if (eval(avgDiscount) >= 1000 && eval(avgDiscount) < 1000000) {
                    avgDiscount = (eval(avgDiscount) / 1000).toFixed(2);
                    $("#spanAverageDiscountUnit").html(' K');
                }
                if (eval(avgDiscount) >= 1000000 && eval(avgDiscount) < 1000000000) {
                    avgDiscount = (eval(avgDiscount) / 1000000).toFixed(2);
                    $("#spanAverageDiscountUnit").html(' M');
                }
                if (eval(avgDiscount) >= 1000000000) {
                    avgDiscount = (eval(avgDiscount) / 1000000000).toFixed(2);
                    $("#spanAverageDiscountUnit").html(' B');
                }
                $("#spanAverageDiscount").html(avgDiscount);

                $("#spanAverageDiscountPer").html(data.d.AverageDiscountPer + '%');
                $("#spanOrderDiscountedPer").html(data.d.OrderDiscountedPer + '%');

                var totShipCost = data.d.TotalShippingCost;
                $("#spanTotalShippingCostUnit").empty();
                if (eval(totShipCost) >= 1000 && eval(totShipCost) < 1000000) {
                    totShipCost = (eval(totShipCost) / 1000).toFixed(2);
                    $("#spanTotalShippingCostUnit").html(' K');
                }
                if (eval(totShipCost) >= 1000000 && eval(totShipCost) < 1000000000) {
                    totShipCost = (eval(totShipCost) / 1000000).toFixed(2);
                    $("#spanTotalShippingCostUnit").html(' M');
                }
                if (eval(totShipCost) >= 1000000000) {
                    totShipCost = (eval(totShipCost) / 1000000000).toFixed(2);
                    $("#spanTotalShippingCostUnit").html(' B');
                }
                $("#spanTotalShippingCost").html(totShipCost);

                var avgShipCost = data.d.AverageShipping;
                $("#spanAverageShippingUnit").empty();
                if (eval(avgShipCost) >= 1000 && eval(avgShipCost) < 1000000) {
                    avgShipCost = (eval(avgShipCost) / 1000).toFixed(2);
                    $("#spanAverageShippingUnit").html(' K');
                }
                if (eval(avgShipCost) >= 1000000 && eval(avgShipCost) < 1000000000) {
                    avgShipCost = (eval(avgShipCost) / 1000000).toFixed(2);
                    $("#spanAverageShippingUnit").html(' M');
                }
                if (eval(avgShipCost) >= 1000000000) {
                    avgShipCost = (eval(avgShipCost) / 1000000000).toFixed(2);
                    $("#spanAverageShippingUnit").html(' B');
                }
                $("#spanAverageShipping").html(avgShipCost);

                $("#spanShippingPer").html(data.d.ShippingPer + '%');

                var totTax = data.d.TotalTax;
                $("#spanTotalTaxUnit").empty();
                if (eval(totTax) >= 1000 && eval(totTax) < 1000000) {
                    totTax = (eval(totTax) / 1000).toFixed(2);
                    $("#spanTotalTaxUnit").html(' K');
                }
                if (eval(totTax) >= 1000000 && eval(totTax) < 1000000000) {
                    totTax = (eval(totTax) / 1000000).toFixed(2);
                    $("#spanTotalTaxUnit").html(' M');
                }
                if (eval(totTax) >= 1000000000) {
                    totTax = (eval(totTax) / 1000000000).toFixed(2);
                    $("#spanTotalTaxUnit").html(' B');
                }
                $("#spanTotalTax").html(totTax);

                $("#spanTaxedOrderPer").html(data.d.TaxedOrderPer + '%');

                var avgTax = data.d.AverageTax;
                $("#spanAverageTaxUnit").empty();
                if (eval(avgTax) >= 1000 && eval(avgTax) < 1000000) {
                    avgTax = (eval(avgTax) / 1000).toFixed(2);
                    $("#spanAverageTaxUnit").html(' K');
                }
                if (eval(avgTax) >= 1000000 && eval(avgTax) < 1000000000) {
                    avgTax = (eval(avgTax) / 1000000).toFixed(2);
                    $("#spanAverageTaxUnit").html(' M');
                }
                if (eval(avgTax) >= 1000000000) {
                    avgTax = (eval(avgTax) / 1000000000).toFixed(2);
                    $("#spanAverageTaxUnit").html(' B');
                }
                $("#spanAverageTax").html(avgTax);

                $("#spanAverageTaxPer").html(data.d.AverageTaxPer + '%');
                orderByNewAccounts = data.d.OrdersByNewAccount;
                $("#spanOrdersByNewAccount").html(data.d.OrdersByNewAccount);
                orderByExistingAccount = data.d.OrdersByExistingAccount;
                $("#spanOrdersByExistingAccount").html(data.d.OrdersByExistingAccount);
                orderByGuest = data.d.OrdersByGuestAccount;
                $("#spanOrdersByGuestAccount").html(data.d.OrdersByGuestAccount);

                salesByNewAccounts = data.d.SalesByNewAccount;
                var slsByNew = data.d.SalesByNewAccount;
                $("#spanSalesByNewAccountUnit").empty();
                if (eval(slsByNew) >= 1000 && eval(slsByNew) < 1000000) {
                    slsByNew = (eval(slsByNew) / 1000).toFixed(2);
                    $("#spanSalesByNewAccountUnit").html(' K');
                }
                if (eval(slsByNew) >= 1000000 && eval(slsByNew) < 1000000000) {
                    slsByNew = (eval(slsByNew) / 1000000).toFixed(2);
                    $("#spanSalesByNewAccountUnit").html(' M');
                }
                if (eval(slsByNew) >= 1000000000) {
                    slsByNew = (eval(slsByNew) / 1000000000).toFixed(2);
                    $("#spanSalesByNewAccountUnit").html(' B');
                }
                $("#spanSalesByNewAccount").html(slsByNew);

                salesByExistingAccount = data.d.SalesByExistingAccount;
                $("#spanSalesByExistingAccountUnit").empty();
                var slsByExt = data.d.SalesByExistingAccount;
                if (eval(slsByExt) >= 1000 && eval(slsByExt) < 1000000) {
                    slsByExt = (eval(slsByExt) / 1000).toFixed(2);
                    $("#spanSalesByExistingAccountUnit").html(' K');

                }
                if (eval(slsByExt) >= 1000000 && eval(slsByExt) < 1000000000) {
                    slsByExt = (eval(slsByExt) / 1000000).toFixed(2);
                    $("#spanSalesByExistingAccountUnit").html(' M');
                }
                if (eval(slsByExt) >= 1000000000) {
                    slsByExt = (eval(slsByExt) / 1000000000).toFixed(2);
                    $("#spanSalesByExistingAccountUnit").html(' B');
                }
                $("#spanSalesByExistingAccount").html(slsByExt);

                salesByGuest = data.d.SalesByGuestAccount;
                var slsByGuest = data.d.SalesByGuestAccount;
                $("#spanSalesByGuestAccountUnit").empty();
                if (eval(slsByGuest) >= 1000 && eval(slsByGuest) < 1000000) {
                    slsByGuest = (eval(slsByGuest) / 1000).toFixed(2);
                    $("#spanSalesByGuestAccountUnit").html(' K');

                }
                if (eval(slsByGuest) >= 1000000 && eval(slsByGuest) < 1000000000) {
                    slsByGuest = (eval(slsByGuest) / 1000000).toFixed(2);
                    $("#spanSalesByGuestAccountUnit").html(' M');
                }
                if (eval(slsByGuest) >= 1000000000) {
                    slsByGuest = (eval(slsByGuest) / 1000000000).toFixed(2);
                    $("#spanSalesByGuestAccountUnit").html(' B');
                }
                $("#spanSalesByGuestAccount").html(slsByGuest);

                $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });

                if (totalOrders > 0) {
                    AspxKPI.ShowOrdersChart(totalOrders, orderByNewAccounts, orderByExistingAccount, orderByGuest);
                    AspxKPI.ShowSalesChart(totalSales, salesByNewAccounts, salesByExistingAccount, salesByGuest);
                }
                else {
                    $("#divOrdersChart").empty();
                    $("#divSalesChart").empty();
                    $("#divOrdersChart").append('<label>'+getLocale(AspxKPILanguage,"No status yet, check back later.")+'</label>');
                    $("#divSalesChart").append('<label>'+getLocale(AspxKPILanguage,"No status yet, check back later.")+'</label>');
                }
            }
        },
        KPISalesConversionsGetAllError: function () {
            SageFrame.messaging.show("Failed to to load sales conversion!", "error");
        },
        ShowOrdersChart: function (totalOrders, orderByNewAccounts, orderByExistingAccount, orderByGuest) {
            $("#divOrdersChart").empty();
            var s1 = [['New', orderByNewAccounts], ['Existing', orderByExistingAccount], ['Guest', orderByGuest]];
            $.jqplot.config.enablePlugins = false;
            var plot1 = $.jqplot('divOrdersChart', [s1], {
                seriesDefaults: {                    
                    renderer: $.jqplot.DonutRenderer,
                    rendererOptions: {                        
                        sliceMargin: 3,                        
                        startAngle: -90,
                        showDataLabels: true,
                        dataLabelThreshold: 1,
                        dataLabelCenterOn: true
                    }
                },
                legend: {
                    renderer: $.jqplot.DonutLegendRenderer,
                    show: false,
                    location: 'se',
                    labels: ['New', 'Existing', 'Guest']
                },
                grid: {
                    background: 'transparent',
                    borderColor: 'transparent',
                    shadow: false
                },
                seriesColors: ["#008080", "#084B8A", "#C35817"],
            });

        },
        ShowSalesChart: function (totalSales, salesByNewAccounts, salesByExistingAccount, salesByGuest) {
            $("#divSalesChart").empty();
            var s1 = [['New', salesByNewAccounts], ['Existing', salesByExistingAccount], ['Guest', salesByGuest]];
            $.jqplot.config.enablePlugins = false;
            var plot1 = $.jqplot('divSalesChart', [s1], {
                seriesDefaults: {                    
                    renderer: $.jqplot.DonutRenderer,
                    rendererOptions: {                       
                        sliceMargin: 3,                        
                        startAngle: -90,
                        showDataLabels: true,
                        dataLabelThreshold: 1,
                        dataLabelCenterOn: true
                    }
                },
                legend: {
                    renderer: $.jqplot.DonutLegendRenderer,
                    show: false,
                    location: 'se',
                    labels: ['New', 'Existing', 'Guest']
                },
                grid: {
                    background: 'transparent',
                    borderColor: 'transparent',
                    shadow: false
                },
                seriesColors: ["#008080", "#084B8A", "#C35817"],
            });
        },
        KPIFunnelCartGetAll: function (shortBy) {
            this.config.method = "KPIFunnelCartGetAll";
            this.config.url = ModuleServicePath + this.config.method;
            this.config.data = JSON2.stringify({ shortBy: shortBy, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = AspxKPI.KPIFunnelCartGetAllSuccess;
            this.config.error = AspxKPI.KPIFunnelCartGetAllError;
            this.ajaxCall(this.config);
        },
        KPIFunnelCartGetAllSuccess: function (data) {
            $("#divKPIFunnelVisualization").empty();
            var tableElements = '';
            var fElmt = '';
            fElmt += '<h1>'+getLocale(AspxKPILanguage,"Funnel Visualization")+'</h1>';
            if (data.d) {
                var funnelVisit = 0;
                var funnelConversion = 0;
                $.each(data.d, function (index, value) {
                    if (index < 5) {
                        tableElements += '<tr>';
                        tableElements += '<td>' + value.PageName + '</td>';
                        tableElements += '<td>' + value.Visit + '</td>';
                        tableElements += '<td>' + value.Conversion + '</td>';
                        tableElements += '<td>' + value.ConversionRate + '%</td>';
                        tableElements += '</tr>';
                        fElmt += '<div class="block clearfix animate">';
                        fElmt += '<span><h3>' + value.PageName + '</h3></span>';
                        fElmt += '<span class="total">' + value.Visit + '</span>';
                        if (index == 0) {
                            fElmt += '<span class="sfPositiveDigit">' + value.Visit + '</span>';
                            funnelVisit = value.Visit;
                        }
                        if (index > 0) {
                            fElmt += '<span class="sfPositiveDigit">0</span>';
                        }
                        fElmt += '<span class="sfNegativeDigit">' + eval(parseInt(value.Visit) - parseInt(value.Conversion)) + '</span>';
                        fElmt += '<span style="width:' + value.ConversionRate + '%;" class="sfPositive ">&nbsp;</span>';
                        fElmt += '<span style="width:' + parseFloat(parseFloat(100.00).toFixed(2) - parseFloat(value.ConversionRate).toFixed(2)).toFixed(2) + '%;" class="sfNegative ">&nbsp;</span>';
                        fElmt += '</div>';
                        fElmt += '<div class="funnel animate">';
                        fElmt += '<span class="rate">';
                        fElmt += '<span class="conversionRate">' + value.Conversion + '</span>';                        
                        fElmt += '<span>(' + value.ConversionRate + '%)</span>';
                        fElmt += '<br />';
                        fElmt += '<span style="font-size:10px; font-weight:bold;"> Proceeded to &nbsp;';
                        fElmt += '<span>' + value.AlternatePageName + '</span>';
                        fElmt += '</span>';
                        fElmt += '</span>';
                        fElmt += '</span>';
                        fElmt += '</div>';
                        fElmt += ' <span class="arrows">&nbsp;</span>';
                    }
                    if (index == 5) {
                        funnelConversion = value.Visit;
                        tableElements += '<tr>';
                        tableElements += '<td>' + value.PageName + '</td>';
                        tableElements += '<td>' + value.Visit + '</td>';
                        tableElements += '<td>' + value.Visit + '</td>';
                        tableElements += '<td>100%</td>';
                        tableElements += '</tr>';
                        fElmt += '<div class="block clearfix animate">';
                        fElmt += '<span><h3>' + value.PageName + '</h3></span>';
                        fElmt += '<span class="total">' + value.Visit + '</span>';
                        fElmt += '<span class="smallText">' + value.ConversionRate + '%  '+getLocale(AspxKPILanguage,"Funnel Conversion Rate")+'</span>';
                        fElmt += '</div>';
                    }
                });
                tableElements += '<tr>';
                tableElements += '<td>'+getLocale(AspxKPILanguage,"Funnel Conversion Rate")+'</td>';
                tableElements += '<td>' + funnelVisit + '</td>';
                tableElements += '<td>' + funnelConversion + '</td>';
                var fConversionRate = 0;
                if (parseFloat(funnelVisit).toFixed(2) > 0) {
                    fConversionRate = parseFloat(parseFloat(funnelConversion).toFixed(2) * 100 / parseFloat(funnelVisit).toFixed(2)).toFixed(2);
                    tableElements += '<td>' + fConversionRate + '%</td>';
                }
                else {
                    tableElements += '<td>' + fConversionRate + '%</td>';
                    fElmt += '<div class="block clearfix animate">';
                    fElmt += '<span class="smallText">0%  '+getLocale(AspxKPILanguage,"Funnel Conversion Rate")+'</span>';
                    fElmt += '</div>';
                }
                tableElements += '</tr>';
            }
            else {
                tableElements += '<tr><td colspan="4">No Data</td></tr>';
                fElmt += '<div>No Data</div>';
            }
            $("#tblKPIDetails").find('tbody').html(tableElements);
            $("#divKPIFunnelVisualization").append(fElmt);
        },
        KPIFunnelCartGetAllError: function () {
            SageFrame.messaging.show(getLocale(AspxKPILanguage,"Failed to get funnel cart information!"), "error");
        },
        KPISettingsGetAll: function () {
            this.config.method = "KPISettingsGetAll";
            this.config.url = ModuleServicePath + this.config.method;
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = AspxKPI.KPISettingsGetAllSuccess;
            this.config.error = AspxKPI.KPISettingsGetAllError;
            this.ajaxCall(this.config);
        },
        KPISettingsGetAllSuccess: function (msg) {
            if (msg.d) {
                $('#ddlKPISettings').val('' + msg.d.IsActive + '');
                $('#ddlEmailNotification').val('' + msg.d.EmailNotification + '');
                $("#txtEndDate").val(msg.d.EndDate);
                $("#txtIPInfoDBAPIKey").val(msg.d.IPInfoDBAPIkey);
                SettingsInfo.KPISettingsID = msg.d.KPISettingsID;
            }
            else {
                $('#ddlKPISettings').val('' + false + '');
                $('#ddlEmailNotification').val('' + false + '');
                $("#txtEndDate").val('');
                 $("#txtIPInfoDBAPIKey").val('');
            }
        },
        KPISettingsGetAllError: function () {
            SageFrame.messaging.show(getLocale(AspxKPILanguage,"Failed to get settings information!"), "error");
        },
        KPISaveUpdateSettings: function () {
            SettingsInfo.IsActive = AspxKPI.Boolean($("#ddlKPISettings option:selected").val());
            SettingsInfo.EmailNotification = AspxKPI.Boolean($("#ddlEmailNotification option:selected").val());
            SettingsInfo.EndDate = $("#txtEndDate").val();
            SettingsInfo.IPInfoDBAPIkey = $("#txtIPInfoDBAPIKey").val();
            this.config.method = "KPISaveUpdateSettings";
            this.config.url = ModuleServicePath + this.config.method;
            this.config.data = JSON2.stringify({ settingsInfo: SettingsInfo, aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = AspxKPI.KPISaveUpdateSettingsSuccess;
            this.config.error = AspxKPI.KPISaveUpdateSettingsError;
            this.ajaxCall(this.config);
        },
        KPISaveUpdateSettingsSuccess: function (msg) {
            SageFrame.messaging.show(getLocale(AspxKPILanguage,"Settings saved successfully!"), "success");
        },
        KPISaveUpdateSettingsError: function () {
            SageFrame.messaging.show(getLocale(AspxKPILanguage,"Failed to save settings!"), "error");
        },
        InitializeTabs: function () {
            var $tabs = $('#containerKPI').tabs({ fx: [null, { height: 'show', opacity: 'show' }] });
            $tabs.tabs('option', 'active', 0);
        },
        InitializeTabs2: function () {
            var $tabs = $('#containerKPI2').tabs({ fx: [null, { height: 'show', opacity: 'show' }] });
            $tabs.tabs('option', 'active', 0);
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
        },
        ClearAll: function () {
            $("#txtEndDate").val('');
        }
    };
    AspxKPI.Init();
});