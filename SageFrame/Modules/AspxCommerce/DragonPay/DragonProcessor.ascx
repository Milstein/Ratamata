<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DragonProcessor.ascx.cs" Inherits="Modules_AspxCommerce_DragonPay_DragonProcessor" %>


<script type="text/javascript" src="http://code.jquery.com/jquery-latest.pack.js"></script>
    <script type="text/javascript">
        $(function() {
            $(this).bind("contextmenu", function(e) {
                e.preventDefault();
            });
            $("#clickhere").click(function() {
                document.DragonPayForm.submit();

            });
        });

    </script>

    <style type="text/css">
        body
        {
            font-family: verdana;
            font-size: 15px;
        }
    </style>
    
    <div>
    <p>
        Connecting to Paypal...</p>
    <p>
       
        <asp:Label ID="lblnotity" runat="server" Text="if you are not redirecting to DragonPay click"></asp:Label>
        <asp:LinkButton ID="clickhere" runat="server" OnClick="clickhere_Click">here..</asp:LinkButton></p>
</div>