<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AspxKPI.ascx.cs" Inherits="Modules_AspxCommerce_AspxKeyPerformanceIndicator_AspxKPI" %>

<script type="text/javascript">
    //<![CDATA[   
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxKPILanguage
        });
    });
    var modulePath = '<%=AspxKPIModulePath %>';
    //]]>
</script>

<div id="divAsxpKPI">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <label id="lblHeading" class="sfLocale">Key Performance Indicator (KPI)</label>
            </h1>
        </div>

        <div class="cssClassTabPanelTable">
            <div id="containerKPI">
                <div id="KPIfragment-1">
                   
                        <div class="cssClassTabPanelTable">
                            <div id="containerKPI2">
                                <ul>
                                    <li><a href="#KPIfragment2-1" onclick="return false;">
                                        <label id="lblTabTitle2-1" class="sfLocale">Cart</label>
                                    </a></li>
                                    <li><a href="#KPIfragment2-2" onclick="return false;">
                                        <label id="lblTabTitle2-2" class="sfLocale">Sales</label>
                                    </a></li>
                                    <li><a href="#KPIfragment2-3" onclick="return false;">
                                        <label id="lblTabTitle2-3" class="sfLocale">Visit</label>
                                    </a></li>
                                    <li><a href="#KPIfragment-2" onclick="return false;">
                                        <label id="lblTabTitle2" class="sfLocale">Settings</label>
                                    </a></li>

                                </ul>
                                <div id="KPIfragment2-1">
                                    <div class="sfFormwrapper">
                                        <div id="divKPIShort" class="clearfix">

                                            <div class="sfTabInterface">
                                                <a href="#" onclick="return false;" id="ADay" class="sfLocale">Day</a>
                                                        <a href="#" onclick="return false;" id="AWeek" class="sfLocale">Week</a>
                                                        <a href="#" onclick="return false;" id="AMonth" class="sfLocale">Month</a>
                                                        <a href="#" onclick="return false;" id="AYear" class="sfLocale">Year</a>
                                                        <a href="#" onclick="return false;" id="AAll" class="sfLocale">All</a>
                                            </div>

                                        </div>
                                        <div class="cssClassTabPanelTable">
                                            <table id="tblCart" width="100%">
                                                <tr>
                                                    <td width="50%" style="vertical-align: top">
                                                        <div id="divKPIFunnelVisualization" class="sfHtmlview">
                                                        </div>
                                                    </td>
                                                    <td style="vertical-align: top">
                                                        <div id="divKPIGrid" class="sfHtmlview">
                                                            <h1 class ="sfLocale">Conversion Details:</h1>
                                                            <div class="cssClassCommonBox Curve" id="divKPIDetailForm">
                                                                <div class="cssClassCommonBox Curve">
                                                                    <div class="sfGridwrapper">
                                                                        <div class="sfGridWrapperContent">
                                                                            <table id="tblKPIDetails" cellspacing="0" cellpadding="0" border="0" width="100%">
                                                                                <thead>
                                                                                    <tr class="cssClassHeading">
                                                                                        <td class="sfLocale cssClassTDHeading">Page Name</td>
                                                                                        <td class="sfLocale cssClassTDHeading">Visit</td>
                                                                                        <td class="sfLocale cssClassTDHeading">Conversion</td>
                                                                                        <td class="sfLocale cssClassTDHeading">Conversion Rate</td>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                </tbody>
                                                                            </table>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <div id="KPIfragment2-2">
                                    <div class="sfFormwrapper">
                                        <div id="divSalesConversionShort" class="clearfix">
                                            <div class="sfTabInterface">
                                                <a href="#" onclick="return false;" id="ADayOS" class="sfLocale">Day</a>
                                                        <a href="#" onclick="return false;" id="AWeekOS" class="sfLocale">Week</a>
                                                        <a href="#" onclick="return false;" id="AMonthOS" class="sfLocale">Month</a>
                                                        <a href="#" onclick="return false;" id="AYearOS" class="sfLocale">Year</a>
                                                        <a href="#" onclick="return false;" id="AAllOS" class="sfLocale">All</a>
                                            </div>

                                        </div>
                                        <div class="cssClassTabPanelTable">
                                            <table id="tblSalesConversion" width="100%">
                                                <tr>
                                                    <td colspan="3">
                                                        <table class="sfTable">
                                                            <tr>
                                                                <td>
                                                                    <div class="cssClassCommonBox Curve" id="divOrdersByAccountsDetails">
                                                                        <div class="cssClassCommonBox Curve">
                                                                            <div class="sfGridwrapper">
                                                                                <div class="sfGridWrapperContent">
                                                                                    <table id="tblOrdersByAccountsDetails" style="border-width: 0px; width: 100%">
                                                                                        <tr class="cssClassHeading">
                                                                                            <td class="sfLocale cssClassTDHeading">Orders Breakdown by Accounts</td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <table id="tblOrdersByAccounts" style="border-width: 0px;">
                                                                                                    <tr>
                                                                                                        <td width="65%">
                                                                                                            <div id="divOrdersChart">
                                                                                                            </div>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <table>
                                                                                                                <tr>
                                                                                                                    <td class="sfLocale" style="color: #008080;">New Accounts</td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td class="sfLocale"><span style="color: #008080;" id="spanOrdersByNewAccount" class="sfLargeNumber"></span></td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td class="sfLocale" style="color: #084B8A;">Existing Accounts</td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td><span style="color: #084B8A;" id="spanOrdersByExistingAccount" class="sfLargeNumber"></span></td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td class="sfLocale" style="color: #C35817;">Guests</td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td><span style="color: #C35817;" id="spanOrdersByGuestAccount" class="sfLargeNumber"></span></td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td class="sfLocale" style="color: #070719;">Total</td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td><span style="color: #070719; font-weight: bold;" id="spanTotalOrders2" class="sfLargeNumber"></span></td>
                                                                                                                </tr>
                                                                                                            </table>

                                                                                                        </td>
                                                                                                    </tr>

                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div class="cssClassCommonBox Curve" id="divSalesByAccountsDetails">
                                                                        <div class="cssClassCommonBox Curve">
                                                                            <div class="sfGridwrapper">
                                                                                <div class="sfGridWrapperContent">
                                                                                    <table id="tblSalesByAccountsDetails">
                                                                                        <tr class="cssClassHeading">
                                                                                            <td class="sfLocale cssClassTDHeading">Sales Breakdown by Accounts</td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <table id="tblSalesByAccounts">
                                                                                                    <tr>
                                                                                                        <td width="65%">
                                                                                                            <div id="divSalesChart">
                                                                                                            </div>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <table>
                                                                                                                <tr>
                                                                                                                    <td class="sfLocale" style="color: #008080;">New Accounts</td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td><span style="color: #008080;" class="sfLargeNumber cssClassFormatCurrency" id="spanSalesByNewAccount" class="sfLargeNumber"></span><span id="spanSalesByNewAccountUnit" style="color: #008080;" class="sfCurrencyNumber"></span></td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td class="sfLocale" style="color: #084B8A;">Existing Accounts</td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td><span style="color: #084B8A;" class="sfLargeNumber cssClassFormatCurrency" id="spanSalesByExistingAccount" class="sfLargeNumber"></span><span id="spanSalesByExistingAccountUnit" style="color: #084B8A;" class="sfCurrencyNumber"></span></td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td class="sfLocale" style="color: #C35817;">Guests</td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td><span style="color: #C35817;" class="sfLargeNumber cssClassFormatCurrency" id="spanSalesByGuestAccount" class="sfLargeNumber"></span><span id="spanSalesByGuestAccountUnit" style="color: #C35817;" class="sfCurrencyNumber"></span></td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td class="sfLocale" style="color: #070719;">Total</td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td><span style="color: #070719;" class="sfLargeNumber cssClassFormatCurrency" id="spanTotalSales2"></span><span id="spanTotalSales2Unit" style="color: #070719;" class="sfCurrencyNumber"></span></td>
                                                                                                                </tr>
                                                                                                            </table>

                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20%;">
                                                        <div class="cssClassCommonBox Curve" id="divOrderSalesDetails">
                                                            <div class="cssClassCommonBox Curve">
                                                                <div class="sfGridwrapper">
                                                                    <div class="sfGridWrapperContent">
                                                                        <table id="tblOrderSalesDetails">
                                                                            <tr class="cssClassHeading">
                                                                                <td class="cssClassTDHeading sfLocale">Sales/Orders:</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <table id="tblSalesOrders" style="border-width: 0px;">
                                                                                        <tr>
                                                                                            <td class="sfLocale">Total Orders</td>
                                                                                            <td class="sfLocale">Total Sales</td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td><span id="spanTotalOrders"></span></td>
                                                                                            <td><span class="cssClassFormatCurrency" id="spanTotalSales"></span><span id="spanTotalSalesUnit"></span></td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="cssClassCommonBox Curve" id="divAverageOrderValueDetails">
                                                            <div class="cssClassCommonBox Curve">
                                                                <div class="sfGridwrapper">
                                                                    <div class="sfGridWrapperContent">
                                                                        <table id="tblAverageOrderValueDetails" style="border-width: 0px; width: auto">
                                                                            <tr class="cssClassHeading">
                                                                                <td class="sfLocale cssClassTDHeading">Average Order Value</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <table id="tblAverageOrderValue" style="border-width: 0px;">
                                                                                        <tr>
                                                                                            <td class="sfLocale">Total</td>
                                                                                            <td class="sfLocale">New Accounts</td>
                                                                                            <td class="sfLocale">Existing Accounts</td>
                                                                                            <td class="sfLocale">Guests</td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td><span class="cssClassFormatCurrency" id="spanTotalAverageOrderValue"></span><span id="spanTotalAverageOrderValueUnit"></span></td>
                                                                                            <td><span class="cssClassFormatCurrency" id="spanAverageOrderValueByNewAccount"></span><span id="spanAverageOrderValueByNewAccountUnit"></span></td>
                                                                                            <td><span class="cssClassFormatCurrency" id="spanAverageOrderValueByExistingAccount"></span><span id="spanAverageOrderValueByExistingAccountUnit"></span></td>
                                                                                            <td><span class="cssClassFormatCurrency" id="spanAverageOrderValueByGuest"></span><span id="spanAverageOrderValueByGuestUnit"></span></td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="cssClassCommonBox Curve" id="divItemsDetails">
                                                            <div class="cssClassCommonBox Curve">
                                                                <div class="sfGridwrapper">
                                                                    <div class="sfGridWrapperContent">
                                                                        <table id="tblItemsDetails" style="border-width: 0px; width: auto">
                                                                            <tr class="cssClassHeading">
                                                                                <td class="sfLocale cssClassTDHeading">Items/SKU Sold</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <table id="tblItems" style="border-width: 0px;">
                                                                                        <tr>
                                                                                            <td class="sfLocale">Total Items</td>
                                                                                            <td class="sfLocale">Items/Order</td>
                                                                                            <td class="sfLocale">Total SKU</td>

                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td><span id="spanTotlaItemsSold"></span></td>
                                                                                            <td><span id="spanItemsSoldPerOrder"></span></td>
                                                                                            <td><span id="spanTotalSKUSold"></span></td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 33%;">
                                                        <div class="cssClassCommonBox Curve" id="divDiscountsDetails">
                                                            <div class="cssClassCommonBox Curve">
                                                                <div class="sfGridwrapper">
                                                                    <div class="sfGridWrapperContent">
                                                                        <table id="tblDiscountsDetails" style="border-width: 0px; width: auto">
                                                                            <tr class="cssClassHeading">
                                                                                <td class="sfLocale cssClassTDHeading">Discounts</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <table id="Discounts" style="border-width: 0px;">
                                                                                        <tr>
                                                                                            <td class="sfLocale">Total</td>
                                                                                            <td class="sfLocale">Average</td>
                                                                                            <td class="sfLocale">Average %</td>
                                                                                            <td class="sfLocale">Orders %</td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td><span id="spanTotalDiscount" class="cssClassFormatCurrency"></span><span id="spanTotalDiscountUnit"></span></td>
                                                                                            <td><span id="spanAverageDiscount" class="cssClassFormatCurrency"></span><span id="spanAverageDiscountUnit"></span></td>
                                                                                            <td><span id="spanAverageDiscountPer"></span></td>
                                                                                            <td><span id="spanOrderDiscountedPer"></span></td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="cssClassCommonBox Curve" id="divShippingDetails">
                                                            <div class="cssClassCommonBox Curve">
                                                                <div class="sfGridwrapper">
                                                                    <div class="sfGridWrapperContent">
                                                                        <table id="tblShippingDetails" style="border-width: 0px; width: auto">
                                                                            <tr class="cssClassHeading">
                                                                                <td class="sfLocale cssClassTDHeading">Shipping</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <table id="tblShipping" style="border-width: 0px;">
                                                                                        <tr>
                                                                                            <td class="sfLocale">Total</td>
                                                                                            <td class="sfLocale">Average</td>
                                                                                            <td class="sfLocale">Orders</td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td><span id="spanTotalShippingCost" class="cssClassFormatCurrency"></span><span id="spanTotalShippingCostUnit"></span></td>
                                                                                            <td><span id="spanAverageShipping" class="cssClassFormatCurrency"></span><span id="spanAverageShippingUnit"></span></td>
                                                                                            <td><span id="spanShippingPer"></span></td>

                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="cssClassCommonBox Curve" id="divTaxesDetails">
                                                            <div class="cssClassCommonBox Curve">
                                                                <div class="sfGridwrapper">
                                                                    <div class="sfGridWrapperContent">
                                                                        <table id="tblTaxesDetails" style="border-width: 0px; width: auto">
                                                                            <tr class="cssClassHeading">
                                                                                <td class="sfLocale cssClassTDHeading">Taxes</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <table id="tblTaxes" style="border-width: 0px;">
                                                                                        <tr>
                                                                                            <td class="sfLocale">Total</td>
                                                                                            <td class="sfLocale">Orders</td>
                                                                                            <td class="sfLocale">Average</td>
                                                                                            <td class="sfLocale">Average %</td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td><span id="spanTotalTax" class="cssClassFormatCurrency"></span><span id="spanTotalTaxUnit"></span></td>
                                                                                            <td><span id="spanTaxedOrderPer"></span></td>
                                                                                            <td><span id="spanAverageTax" class="cssClassFormatCurrency"></span><span id="spanAverageTaxUnit"></span></td>
                                                                                            <td><span id="spanAverageTaxPer"></span></td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <div id="KPIfragment2-3">
                                    <div class="sfFormwrapper">
                                        <div id="divVisitShort" class="clearfix">
                                            <div class="sfTabInterface">
                                                <a href="#" onclick="return false;" id="AVisitDay" class="sfLocale">Day</a>
                                                            <a href="#" onclick="return false;" id="AVisitWeek" class="sfLocale">Week</a>
                                                            <a href="#" onclick="return false;" id="AVisitMonth" class="sfLocale">Month</a>
                                                            <a href="#" onclick="return false;" id="AVisitYear" class="sfLocale">Year</a>
                                                            <a href="#" onclick="return false;" id="AVisitAll" class="sfLocale">All</a>
                                            </div>
                                        </div>
                                        <div class="cssClassTabPanelTable">
                                            <table border="0" id="tblVisitDetails">
                                                <tr>
                                                    <td style="width: 50%;">
                                                        <div class="cssClassCommonBox Curve" id="divVisitors">
                                                            <div class="cssClassCommonBox Curve">
                                                                <div class="sfGridwrapper">
                                                                    <div class="sfGridWrapperContent">
                                                                        <table id="tblVisitorsDetails">
                                                                            <tr class="cssClassHeading">
                                                                                <td class="cssClassTDHeading"><span class="sfLocale">Visitors :</span>&nbsp;&nbsp;<span id="spanTotalVisitor"></span>

                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <span class="cssClassspanVisitMetric"></span>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <table id="tblVisitors" cellspacing="0" cellpadding="0" border="0" width="100%">
                                                                                        <tr>
                                                                                            <td width="65%">
                                                                                                <div id="divVisitorsChart">
                                                                                                </div>
                                                                                            </td>
                                                                                            <td>
                                                                                                <table>
                                                                                                    <tr>
                                                                                                        <td class="sfLocale" style="color: #008080;">New Visitors</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="sfLocale"><span class="sfLargeNumber" style="color: #008080;" id="spanNewVisitors"></span></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="sfLocale" style="color: #084B8A;">Returning Visitors</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td><span class="sfLargeNumber" style="color: #084B8A; font-weight: bold;" id="spanReturningVisitors"></span></td>
                                                                                                    </tr>

                                                                                                </table>

                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div id="divViews" class="sfHtmlview">
                                                            <h1><span class="sfLocale">Total Visits:</span>&nbsp;&nbsp;<span id="spanTotalVisit"></span>
                                                            </h1>
                                                            <span class="cssClassspanVisitMetric"></span>
                                                            <div class="cssClassCommonBox Curve" id="divVisits">
                                                                <div class="cssClassCommonBox Curve">
                                                                    <div class="sfGridwrapper">
                                                                        <div class="sfGridWrapperContent">
                                                                            <table id="tblViews" cellspacing="0" cellpadding="0" border="0" width="100%">
                                                                                <thead>
                                                                                    <tr class="cssClassHeading">
                                                                                        <td class="sfLocale cssClassTDHeading">Page name</td>
                                                                                        <td class="sfLocale cssClassTDHeading">Visits</td>
                                                                                        <td class="sfLocale cssClassTDHeading">Visits %</td>
                                                                                        <td class="sfLocale cssClassTDHeading"></td>
                                                                                        <td class="sfLocale cssClassTDHeading">Avg. Duration</td>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                </tbody>
                                                                            </table>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <div id="divLocationShort" class="sfHtmlview">
                                                            <h1><span class="sfLocale">Locations:</span><div class="sfTabInterface" style="position:relative; top:30px;">
                                                                    <a href="#" onclick="return false;" id="AVisitCountry" class="sfLocale">Country</a>
                                                                    <a href="#" onclick="return false;" id="AVisitCity" class="sfLocale">City</a>
                                                                </div></h1>
                                                            
                                                            <div id="divLocationwiseShort" class="clearfix">

                                                                

                                                            </div>
                                                            <span class="cssClassspanLocationMetric"></span>

                                                            <div class="cssClassCommonBox Curve" id="divLocationsDetails">
                                                                <div class="cssClassCommonBox Curve">
                                                                    <div class="sfGridwrapper">
                                                                        <div class="sfGridWrapperContent">
                                                                            <table id="tblLocationsDetails" cellspacing="0" cellpadding="0" border="0" width="100%">
                                                                                <thead>
                                                                                    <tr class="cssClassHeading">
                                                                                        <td class="cssClassTDHeading" width="40%">
                                                                                            <label id="lblMetrics" class="sfLocale">Country</label>
                                                                                        </td>
                                                                                        <td class="sfLocale cssClassTDHeading">Visits</td>
                                                                                        <td class="sfLocale cssClassTDHeading"  width="200px">Visits %</td>
                                                                                        <td class="sfLocale cssClassTDHeading"  width="100px"></td>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                </tbody>
                                                                            </table>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>

                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                   
                </div>
                <div id="KPIfragment-2">
                    <div class="sfFormwrapper">
                        <table id="tblGeneralSettingForm" style="width: auto;">
                            <tr>
                                <td>
                                    <label id="lblKPISettings" class="sfFormlabel sfLocale">KPI :</label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <select id="ddlKPISettings" name="KPISettings" class="sfListmenu">
                                        <option value="true" class="sfLocale">ON</option>
                                        <option value="false" class="sfLocale">OFF</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label id="lblEmailNotification" class="sfFormlabel sfLocale">Email Notification :</label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <select id="ddlEmailNotification" name="EmailNotification" class="sfListmenu">
                                        <option value="true" class="sfLocale">ON</option>
                                        <option value="false" class="sfLocale">OFF</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label id="lblEndDate" class="sfFormlabel sfLocale">End Date :</label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtEndDate" name="EndDate" class="sfInputbox required" />
                                </td>
                            </tr>
                              <tr>
                                <td>
                                    <label id="Label1" class="sfFormlabel sfLocale">IPInfoDB API key :</label>
                                </td>
                                <td class="cssClassGridRightCol">
                                    <input type="text" id="txtIPInfoDBAPIKey" name="IPInfoDBAPIkey" class="sfInputbox required" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="sfButtonwrapper sftype1">
                        <label id="btnSaveKPISettings" class="sfBtn sfLocale icon-save">Save</label>
                        <label id="btnRefrershKPISettings" class="sfBtn sfLocale icon-refresh">Refresh</label>
                    </div>

                </div>
            </div>
        </div>

    </div>
</div>

<script type="text/javascript">
    var x = $('.sfPositiveDigit').width();
    $(".sfPositiveDigit").css({ left: (-x) + (-40) });
    var y = $('.sfNegativeDigit').width();
    $(".sfNegativeDigit").css({ right: (-y) + (-40) });
    $("#AAll").addClass("active");
    $('.sfTabInterface a').click(function () {
        $(this).siblings().removeClass("active");
        $(this).addClass("active");
    });
</script>
