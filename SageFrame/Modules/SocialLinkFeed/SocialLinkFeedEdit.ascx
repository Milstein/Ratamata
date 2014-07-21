<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SocialLinkFeedEdit.ascx.cs" Inherits="Modules_SocialLinkFeed_SocialLinkFeedEdit" %>
<script type="text/javascript">
    $(function()
    {
        var portalID = '<%=PortalID%>';
        var userModuleID = '<%=UserModuleID%>';
        var userName = '<%=UserName%>';
        $(this).SocialLinks({ userName: userName, portalID: portalID, userModuleID: userModuleID });
    });    
</script>
<div class="sfFormwrapper">
<div id="SocialLinkCover">
    <div id="divRSS" class="SocialLink">
        <h3 class="name">
            RSS</h3>
        <div id="divRSSLinks">
        </div>
    </div>

</div>
<div id="popup_box">
<div class="sfPopupHeader"><label>Edit</label></div>
    <span style="display:none;"></span>    
    <label class="sfFormlabel"> Link: </label>&nbsp;&nbsp;
    <input type="text" id='txtLink' class="sfInputbox" style="width:225px"/><br /><br />
    <label class="sfFormlabel"> DisplayName: </label>&nbsp;&nbsp;
    <input type="text" id='txtDisplayName' class="sfInputbox" style="width:225px"/><br /><br />
    <input type="button" id='btnUpdate' value="Update" class="sfBtn" style="float:right"/>
    <input type="button" id='btnClose' value="Close" class="sfBtn" style="float:right" />
</div>
<div id="container">
</div>
</div>