<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResponsePG.aspx.cs" Inherits="ResponsePG" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Merchant ID<asp:TextBox ID="txtmerid" runat="server" 
            meta:resourcekey="txtmeridResource1"></asp:TextBox><br />
        Subscriber ID<asp:TextBox ID="txtsubscriberid" runat="server" 
            meta:resourcekey="txtsubscriberidResource1"></asp:TextBox><br />
        Trx Reference No.<asp:TextBox ID="txttxnrefno" runat="server" 
            meta:resourcekey="txttxnrefnoResource1"></asp:TextBox><br />
        Bank Reference No.<asp:TextBox ID="txtbankrefno" runat="server" 
            meta:resourcekey="txtbankrefnoResource1"></asp:TextBox><br />
        Txn Amount<asp:TextBox ID="txttxnamt" runat="server" 
            meta:resourcekey="txttxnamtResource1"></asp:TextBox><br />
        Bank ID<asp:TextBox ID="txtbankid" runat="server" 
            meta:resourcekey="txtbankidResource1"></asp:TextBox><br />
        Bank Merchant ID<asp:TextBox ID="txtbankmerid" runat="server" 
            meta:resourcekey="txtbankmeridResource1"></asp:TextBox><br />
        TCN Type<asp:TextBox ID="txttcntype" runat="server" 
            meta:resourcekey="txttcntypeResource1"></asp:TextBox><br />
        Currency Name<asp:TextBox ID="txtcurrencyname" runat="server" 
            meta:resourcekey="txtcurrencynameResource1"></asp:TextBox><br />
        Item Code<asp:TextBox ID="txttemcode" runat="server" 
            meta:resourcekey="txttemcodeResource1"></asp:TextBox><br />
        Security Type<asp:TextBox ID="txtsecuritytype" runat="server" 
            meta:resourcekey="txtsecuritytypeResource1"></asp:TextBox><br />
        Security ID<asp:TextBox ID="txtsecurityid" runat="server" 
            meta:resourcekey="txtsecurityidResource1"></asp:TextBox><br />
        Security Password<asp:TextBox ID="txtsecuritypass" runat="server" 
            meta:resourcekey="txtsecuritypassResource1"></asp:TextBox><br />
        <br />
        Txn Date<asp:TextBox ID="txttxndate" runat="server" 
            meta:resourcekey="txttxndateResource1"></asp:TextBox><br />
        Auth Status<asp:TextBox ID="txtauthstatus" runat="server" 
            meta:resourcekey="txtauthstatusResource1"></asp:TextBox><br />
        Settlement Type<asp:TextBox ID="txtsettlementtype" runat="server" 
            meta:resourcekey="txtsettlementtypeResource1"></asp:TextBox><br />
        Additional Info 1
        <asp:TextBox ID="txtaddtninfo1" runat="server" 
            meta:resourcekey="txtaddtninfo1Resource1"></asp:TextBox>
        <br />
        Additional Info 2
        <asp:TextBox ID="txtaddtninfo2" runat="server" 
            meta:resourcekey="txtaddtninfo2Resource1"></asp:TextBox>
        <br />
        Additional Info 3
        <asp:TextBox ID="txtaddtninfo3" runat="server" 
            meta:resourcekey="txtaddtninfo3Resource1"></asp:TextBox>
        <br />
        Additional Info 4
        <asp:TextBox ID="txtaddtninfo4" runat="server" 
            meta:resourcekey="txtaddtninfo4Resource1"></asp:TextBox>
        <br />
        Additional Info 5
        <asp:TextBox ID="txtaddtninfo5" runat="server" 
            meta:resourcekey="txtaddtninfo5Resource1"></asp:TextBox>
        <br />
        Additional Info 6
        <asp:TextBox ID="txtaddtninfo6" runat="server" 
            meta:resourcekey="txtaddtninfo6Resource1"></asp:TextBox>
        <br />
        Additional Info 7
        <asp:TextBox ID="txtaddtninfo7" runat="server" 
            meta:resourcekey="txtaddtninfo7Resource1"></asp:TextBox>
        <br />
        Error Status<asp:TextBox ID="txterrorstatus" runat="server" 
            meta:resourcekey="txterrorstatusResource1"></asp:TextBox><br />
        Error Description<asp:TextBox ID="txterrordesc" runat="server" 
            meta:resourcekey="txterrordescResource1"></asp:TextBox><br />
        Check Sum
        <asp:TextBox ID="txtchecksum" runat="server" 
            meta:resourcekey="txtchecksumResource1"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btnPayBill" runat="server" Text="Pay Bill" 
            meta:resourcekey="btnPayBillResource1"/>
    
    </div>
    </form>
</body>
</html>
