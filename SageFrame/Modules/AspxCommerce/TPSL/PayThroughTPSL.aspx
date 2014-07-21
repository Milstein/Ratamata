<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayThroughTPSL.aspx.cs"
    Inherits="Modules_AspxCommerce_TPSLGateWay_PayThroughTPSL" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Processing your Order....</title>

   <script type="text/javascript" src="http://code.jquery.com/jquery-latest.pack.js"></script>
    
<script type="text/javascript">
    $(function() {
        $(this).bind("contextmenu", function(e) {
            e.preventDefault();
        });
        $("#clickhere").click(function() {
            document.TPSLForm.submit();

        });
    });

</script>

<style type="text/css">

body {
font-family:verdana;
font-size:15px;
}
</style>

</head>
<body >
<form id="Form1" runat="server">
<div>
 <asp:Label ID="lblnotity" runat="server" 
        Text="Something goes wrong, hit refresh or go back to checkout" Visible="False" 
        meta:resourcekey="lblnotityResource1"></asp:Label> 
    <asp:LinkButton ID="clickhere" runat="server" onclick="clickhere_Click" 
        meta:resourcekey="clickhereResource1" Text="here.."  ></asp:LinkButton>
  
</div>


    </form>
</body>
</html>
