﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PesoPaySuccess.ascx.cs"
    Inherits="Modules_AspxCommerce_PesoPay_PesoPaySuccess" %>

<script type="text/javascript">
    $(document).ready(function() {
        $('#btnPrint').click(function() {
            printPage();
        });
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
                <asp:Image ID="imgPrgress" runat="server" AlternateText="Loading..." ToolTip="Loading..." />
                <br />
                <asp:Label ID="lblPrgress" runat="server" Text="Please wait..."></asp:Label>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div id="divPageOuter" class="PageOuter">
        <div id="error" runat="server">
            <asp:Label ID="lblerror" runat="server" Text=""></asp:Label>
        </div>
        <div id="divClickAway">
            <div class="cssClassButtonWrapper">
                <asp:HyperLink ID="hlnkHomePage" runat="server">Back to Home page</asp:HyperLink>
                <button id="btnPrint" type="button">
                    <span><span>Print</span></span></button>
            </div>
        </div>
        <div id="divPage" class="Page" runat="server">
            <div>
            </div>
           
            <div id="divThankYou">
                Thank you for your order!</div>
            <hr class="HrTop" />
            <div id="divReceiptMsg">
                You may print this receipt page for your records.
            </div>
            <div class="SectionBar">
                Order Information</div>
            <table id="tablePaymentDetails2Rcpt" cellspacing="0" cellpadding="0">
                <tr>
                    <td class="LabelColInfo1R">
                        Date/Time:
                    </td>
                    <td class="DataColInfo1R">
                        <asp:Label ID="lblDateTime" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="LabelColInfo1R">
                        Invoice Number:
                    </td>
                    <td class="DataColInfo1R">
                        <asp:Label ID="lblInvoice" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
            <hr id="hrBillingShippingBefore">
            <div id="divOrderDetailsBottomR">
                    <asp:Literal runat="server" ID="ltOrderItems"></asp:Literal>
                <div class="SectionBar"> </div>
                <table id="tableOrderDetailsBottom" cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td class="LabelColTotal">
                        </td>
                        <td class="DescrColTotal" >
                            <asp:Label ID="Label3" runat="server" Text="Sub Total:"></asp:Label>
                        </td>
                        <td class="DataColTotal">
                            <asp:Label ID="lblSubTotal" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="LabelColTotal">
                        </td>
                        <td class="DescrColTotal" >
                            <asp:Label ID="Label2" runat="server" Text="Shipping Cost:"></asp:Label>
                        </td>
                        <td class="DataColTotal">
                            <asp:Label ID="lblShippingCost" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="LabelColTotal" >
                        </td>
                        <td class="DescrColTotal" >
                            <asp:Label ID="Label1" runat="server" Text="Tax Total:"></asp:Label>
                        </td>
                        <td class="DataColTotal">
                            <asp:Label ID="lblTaxTotal" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="LabelColTotal">
                        </td>
                        <td class="DescrColTotal" >
                            <asp:Label ID="Label4" runat="server" Text="Total Discount:"></asp:Label>
                        </td>
                        <td class="DataColTotal">
                            <asp:Label ID="lblTotalDiscount" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="LabelColTotal">
                        </td>
                        <td class="DescrColTotal" >
                            <asp:Label ID="lblTotal" runat="server" Text="Grand Total:"></asp:Label>
                        </td>
                        <td class="DataColTotal">
                            <asp:Label ID="lblGrandTotal" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <!-- tableOrderDetailsBottom -->
            </div>
            <div id="divOrderDetailsBottomSpacerR">
            </div>
            <div class="SectionBar">
            </div>
            <table class="PaymentSectionTable" cellspacing="0" cellpadding="0">
                <tr>
                    <td class="LabelColInfo2R">
                        Transaction ID:
                    </td>
                    <td class="DataColInfo2R">
                        <asp:Label ID="lblTransaction" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="DataColInfo2R">
                        <asp:Label ID="lblAuthorizationCode" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="LabelColInfo2R">
                        Payment Method:
                    </td>
                    <td class="DataColInfo2R">
                        <asp:Label ID="lblPaymentMethod" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
            <div class="PaymentSectionSpacer">
            </div>
        </div>
        <!-- entire BODY -->
    </div>
</div>
