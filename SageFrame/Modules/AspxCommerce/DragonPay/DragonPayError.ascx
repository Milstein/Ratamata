<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DragonPayError.ascx.cs"
    Inherits="Modules_AspxCommerce_DragonPay_DragonPayError" %>


<script type="text/javascript">
    var isAppointment = "<%=IsAppointment %>";
    $(document).ready(function() {
        $('#btnPrint').click(function() {
            printPage();
        });
        if (isAppointment.toLowerCase() == "true") {
            $('.cssScheduleSuccess').show();
            $('.cssNormalSuccess').hide();
        } else {
            $('.cssScheduleSuccess').hide();
            $('.cssNormalSuccess').show();
        }
    });
    $(window).load(function() {
        $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
    });
    function printPage() {
        var content = $('#<%=divPage.ClientID%>').html();
        var pwin = window.open('', 'print_content', 'width=100,height=100');
        pwin.document.open();
        pwin.document.write('<html><body onload="window.print()">' + content + '</body></html>');
        pwin.document.close();
        setTimeout(function() { pwin.close(); }, 5000);
    }
</script>

<div>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="cssClassLoadingBG">
                &nbsp;</div>
            <div class="cssClassloadingDiv">
                <asp:Image ID="imgPrgress" runat="server" AlternateText="Loading..." ToolTip="Loading..."
                    meta:resourcekey="imgPrgressResource1" />
                <br />
                <asp:Label ID="lblPrgress" runat="server" Text="Please wait..." meta:resourcekey="lblPrgressResource1"></asp:Label>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div id="divPageOuter" class="PageOuter">
        <div id="error" runat="server">
            <asp:Label ID="lblerror" runat="server" meta:resourcekey="lblerrorResource1"></asp:Label>
        </div>
        <div id="divClickAway">
            <div class="sfButtonwrapper">
                <asp:HyperLink ID="hlnkHomePage" runat="server" meta:resourcekey="hlnkHomePageResource1">Back to Home page</asp:HyperLink>
                <button id="btnPrint" type="button">
                    <span><span class="sfLocale">Print</span></span></button>
            </div>
        </div>
        <div id="divPage" class="Page" runat="server">
            <div id="divThankYou" class="sfLocale">
                <span style="font-weight: bold; color: Red;" class="sfLocale">Transaction is <asp:Label runat="server" ID="lblStatus"></asp:Label></span>
            </div>
            <hr class="HrTop" />
            <div id="divReceiptMsg" class="sfLocale">
                You may print this receipt page for your records.
            </div>
            <div class="SectionBar sfLocale">
                Order Information</div>
            <table id="tablePaymentDetails2Rcpt" cellspacing="0" cellpadding="0" width="100%">
                <tr>
                    <td class="LabelColInfo1R sfLocale">
                        Date/Time:
                    </td>
                    <td class="DataColInfo1R">
                        <asp:Label ID="lblDateTime" runat="server" meta:resourcekey="lblDateTimeResource1"></asp:Label>
                    </td>
                    <td class="LabelColInfo1R sfLocale">
                    Invoice Number:                                    
                    </td>
                    <td class="DataColInfo1R">
                        <asp:Label ID="lblInvoice" runat="server" meta:resourcekey="lblInvoiceResource1"></asp:Label>
                    </td>
                </tr>
            </table>
            <hr id="hrBillingShippingBefore" />  
            <div id="divOrderDetailsBottomSpacerR">
            </div>   
            <div class="SectionBar">
            </div>         
        <table class="PaymentSectionTable cssNormalSuccess" cellspacing="0" cellpadding="0"
                width="50%" style="display: none">
                <tr>
                    <td class="LabelColInfo2R sfLocale">
                        Payment Status:
                    </td>
                    <td class="DataColInfo2R">
                        <asp:Label ID="lblPaymentStatus" runat="server" meta:resourcekey="lblTransactionResource1"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="LabelColInfo2R sfLocale">
                        Transaction ID:
                    </td>
                    <td class="DataColInfo2R">
                        <asp:Label ID="lblTransaction" runat="server" meta:resourcekey="lblTransactionResource1"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="LabelColInfo2R sfLocale">
                        Payment Method:
                    </td>
                    <td class="DataColInfo2R">
                        <asp:Label ID="lblPaymentMethod" runat="server" meta:resourcekey="lblPaymentMethodResource1"></asp:Label>
                    </td>
                </tr>
            </table>
            <table class="PaymentSectionTable cssScheduleSuccess" cellspacing="0" cellpadding="0"
                width="100%" style="display: none">
                <tr>
                    <td class="sfLocale">
                        Seminar Schedule:
                    </td>
                    <td colspan="3">
                        <asp:Label ID="lblSeminarSchedule" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="sfLocale">
                        Name:
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblUserName"></asp:Label>
                    </td>
                    <td class="sfLocale">
                        Contact:
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblContact"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="sfLocale">
                        Email:
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblEmail"></asp:Label>
                    </td>
                    <td class="sfLocale">
                        Transaction ID:
                    </td>
                    <td>
                        <asp:Label ID="lblScheduleTransID" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="sfLocale">
                        Quantity:
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblQuantity"></asp:Label>
                    </td>
                    <td class="sfLocale">
                        Amount:
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblAmount" CssClass="cssClassFormatCurrency"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="sfLocale">
                        Payment Method:
                    </td>
                    <td>
                        <asp:Label ID="lblSchedulePaymentMethod" runat="server"></asp:Label>
                    </td>
                    <td class="sfLocale">
                        Payment Status:
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblSchedulePaymentStatus"></asp:Label>
                    </td>
                </tr>
            </table>
            <div class="PaymentSectionSpacer">
            </div>
        </div>
    </div>
</div>
