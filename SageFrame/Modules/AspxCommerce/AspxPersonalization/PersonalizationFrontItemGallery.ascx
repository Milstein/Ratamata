<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PersonalizationFrontItemGallery.ascx.cs" Inherits="Modules_AspxCommerce_AspxPersonalization_PersonalizationFrontItemGallery" %>

<script type="text/javascript">
    //<![CDATA[
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxFrontItemGallery
        });
        if (ModuleCollapsible.toLowerCase() == 'true') {
            $("#divFrontGalleryHeader").addClass("sfCollapsible");
            $("#divFrontGalleryHeader").on('click', function () {
                $(".cssFeatureFrontGallery").slideToggle('fast');
            });
        }
    });
    var noImageFeaturedItemPathSetting = '<%=NoImageFeaturedItemPath %>';
    var featureItemRss = '<%=FeatureItemRss %>';
    var specialItemRss = '<%=SpecialItemRss %>';
    var rssFeedUrl = '<%=RssFeedUrl %>';
    var ModuleCollapsible = "<%=ModuleCollapsible %>";
    var GalleryDisplayAs = '<%=GalleryDisplayAs %>';
    var ItemCount = '<%=Count %>';
    var rowTotal = '<%=RowTotal %>';
    //]]>
</script>

<div class="cssClassLeftSideBox cssClassFeatureWrapper">
    <div id="divFrontGalleryHeader">
        <h2 class="cssClassLeftHeader">
            <span class="cssClassFeatured"></span><a href="#" class="cssRssImage" style="display: none">
                <img id="featureItemRssImage" alt="" src="" title="" />
            </a>
        </h2>
    </div>
    <div class="cssClassCommonSideBoxTable cssFeatureFrontGallery">
        <asp:Literal runat="server" ID="ltGallery"></asp:Literal>
    </div>
</div>
