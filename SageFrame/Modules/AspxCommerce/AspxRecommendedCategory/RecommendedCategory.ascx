<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RecommendedCategory.ascx.cs" Inherits="Modules_AspxCommerce_AspxRecommendedCategory_RecommendedCategory" %>
<script type="text/javascript">
 //<![CDATA[
    var modulePath = "<%=ModulePath %>";

       //]]>
</script>


<%--<div class="cssClassLeftSideBox cssClassRecommendedItemWrapper">
    <div id="divRecommendedCategory">
        <h2 class="cssClassLeftHeader">
            Recommended Category</h2>
        <div class="cssClassCategoryList cssClassProductLists">
            <ul class="cssClassRecommendedCategoryUlList">
            </ul>
        </div>
        
    </div>
</div>
--%>
 <asp:Literal id="litRecommendedCategoryList" runat="server" EnableViewState="false"></asp:Literal>