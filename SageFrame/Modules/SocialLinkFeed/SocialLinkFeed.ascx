<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SocialLinkFeed.ascx.cs" Inherits="Modules_SocialLinkFeed_SocialLinkFeed" %>
<script type="text/javascript" src="https://www.google.com/jsapi"></script>
<script type="text/javascript">
    google.load("feeds", "1");
</script>
<script type="text/javascript">
    $(function()
    {
        var portalID = '<%=PortalID%>';
        var userModuleID = '<%=UserModuleID%>'
        var userName = '<%=UserName%>';
        SocialLinkView(portalID, userModuleID, userName);
    });
</script>
<div id="jstweets">
</div>

<div id="feed">
</div>
<div id="vtab">
    <ul>
    </ul>
</div>

