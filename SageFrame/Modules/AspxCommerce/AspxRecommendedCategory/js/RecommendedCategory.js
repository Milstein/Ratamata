var RecommendedCategory = "";
$(function () {
    var AspxCommonObj = function () {
        var aspxCommonObj = {
            StoreID: AspxCommerce.utils.GetStoreID(),
            PortalID: AspxCommerce.utils.GetPortalID(),
            CultureName: AspxCommerce.utils.GetCultureName(),
            UserName: AspxCommerce.utils.GetUserName(),
            SessionCode: AspxCommerce.utils.GetSessionCode(),
            CustomerID: AspxCommerce.utils.GetCustomerID()
        };
        return aspxCommonObj;
    };

    RecommendedCategory = {
        config: {
            isPostBack: false,
            async: false,
            cache: false,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: "json",
            baseURL: modulePath + "RecommendedCategoryHandler.ashx/",
            url: "",
            method: "",
            ajaxCallMode: 0
        },
        ajaxCall: function (config) {
            $.ajax({
                type: RecommendedCategory.config.type,
                contentType: RecommendedCategory.config.contentType,
                cache: RecommendedCategory.config.cache,
                async: RecommendedCategory.config.async,
                data: RecommendedCategory.config.data,
                dataType: RecommendedCategory.config.dataType,
                url: RecommendedCategory.config.url,
                success: RecommendedCategory.config.ajaxSuccess,
                error: RecommendedCategory.config.ajaxFailure
            });
        },
        GetRecommendedCategory: function () {
            this.config.method = "GetRecommendedCategoryList";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({
                asxpCommonObj: AspxCommonObj(),
                count: 8
            });
            this.config.ajaxSuccess = RecommendedCategory.BindRecommendedCategory;
            this.config.ajaxFailure = RecommendedCategory.RecommendedCategoryError;
            this.ajaxCall(this.config);
        },
        BindRecommendedCategory: function (data) {
            $(".cssClassRecommendedCategoryUlList").html('');
            var recommendeCategoryList = '';
            if (data.d.length > 0) {
                var path = decodeURIComponent(window.location.href);
                var applicationPath = path.substring(path.indexOf(aspxRedirectPath) + 1, path.length);
                hrefValue = applicationPath.split('/');
                var catName = hrefValue[2].split('.');
                catName = catName[0];
                catName = decodeURIComponent(catName.replace('ampersand', '&').replace(new RegExp("-", "g"), ' ').replace('_', '-'));
                $.each(data.d, function (index, item) {
                    var cat = item.CategoryName;
                    var CategoryName = decodeURIComponent(cat.replace('ampersand', '&').replace(new RegExp("-", "g"), ' ').replace('_', '-'));
                    if (item.CategoryName != catName) {

                        recommendeCategoryList += "<li><a href=" + AspxCommerce.utils.GetAspxRedirectPath() + 'category/' + CategoryName + pageExtension + ">" + item.CategoryName + "</a><span class=\"cssCount\"> (" + item.ItemCount + ")</span></li>";
                    }
                });
                $(".cssClassRecommendedCategoryUlList").append(recommendeCategoryList);
            }
        },
        RecommendedCategoryError: function () {
            csscody.error('<h2>Error Message </h2><p>Sorry! Failed to load Recommended Category!</p>');

        },
        Init: function () {          
            // RecommendedCategory.GetRecommendedCategory();
        }
    };
    RecommendedCategory.Init();
});