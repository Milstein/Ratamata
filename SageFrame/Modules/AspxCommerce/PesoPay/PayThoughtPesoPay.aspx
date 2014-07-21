<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayThoughtPesoPay.aspx.cs" Inherits="Modules_AspxCommerce_PesoPay_PayThoughtPesoPay" %>

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
            document.PaypalForm.submit();

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
<body>
    <form id="form1" runat="server">
   <div>
<p>Connecting to AsiaPay...</p>
<p><asp:Label ID="lblnotity" runat="server">if you are not redirecting to AsiaPay click </asp:Label>  <asp:LinkButton ID="clickhere" runat="server" onclick="clickhere_Click"  >here..</asp:LinkButton></p>
       
</div>
    </form>
</body>
</html>
