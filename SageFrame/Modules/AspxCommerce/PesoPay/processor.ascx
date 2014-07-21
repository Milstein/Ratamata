<%@ Control Language="C#" AutoEventWireup="true" CodeFile="processor.ascx.cs" Inherits="Modules_AspxCommerce_PesoPay_processor" %>
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

 <div>
<p>Connecting to AsiaPay...</p>
<p><asp:Label ID="lblnotity" runat="server">if you are not redirecting to AsiaPay click </asp:Label>  <asp:LinkButton ID="clickhere" runat="server" onclick="clickhere_Click"  >here..</asp:LinkButton></p>
       
</div>