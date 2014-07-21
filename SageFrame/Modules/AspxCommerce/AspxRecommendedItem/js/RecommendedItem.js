var RecommendedItem = "";
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

    RecommendedItem = {
        config: {
            isPostBack: false,
            async: false,
            cache: false,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: "json",
            baseURL: modulePath + "RecommendedItemHandler.ashx/",
            url: "",
            method: "",
            ajaxCallMode: 0
        },
        ajaxCall: function (config) {
            $.ajax({
                type: RecommendedItem.config.type,
                contentType: RecommendedItem.config.contentType,
                cache: RecommendedItem.config.cache,
                async: RecommendedItem.config.async,
                data: RecommendedItem.config.data,
                dataType: RecommendedItem.config.dataType,
                url: RecommendedItem.config.url,
                success: RecommendedItem.config.ajaxSuccess,
                error: RecommendedItem.config.ajaxFailure
            });
        },
        GetRecommendedItem: function () {
            this.config.method = "GetRecommendedItemList";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({
                aspxCommonObj: AspxCommonObj(),
                count: 8
            });
            this.config.ajaxSuccess = RecommendedItem.BindRecommendedItem;
            this.config.ajaxFailure = RecommendedItem.RecommendedItemError;
            this.ajaxCall(this.config);
        },
        BindRecommendedItem: function (data) {
            $(".cssClassRecommendedItemUlList").html('');
            var recommendeItemList = '';
            if (data.d.length > 0) {
                $.each(data.d, function (index, item) {
                    recommendeItemList += "<li><a href=" + AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + item.SKU + pageExtension + ">" + item.ItemName + "</a></li>";
                });
                $(".cssClassRecommendedItemUlList").append(recommendeItemList);
            }
        },
        RecommendedItemError: function () {
            csscody.error('<h2>Error Message </h2><p>Sorry! Failed to load Recommended Item List!</p>');

        },
        Init: function () {
//            if (parseInt(AspxCommonObj().CustomerID) > 0) {
//                $('.cssClassRecommendedItemWrapper').show();
//            }           
        }
    };
    RecommendedItem.Init();
});