var FrontAdvertiseGallery ={};
$(function() {
    var storeId = AspxCommerce.utils.GetStoreID();
    var portalId = AspxCommerce.utils.GetPortalID();
    var userName = AspxCommerce.utils.GetUserName();
    var cultureName = AspxCommerce.utils.GetCultureName();
    var count;
    var showUrl;
    var showDetails;
    var FrontAdvertiseGallery = {
        config: {
            isPostBack: false,
            async: true,
            cache: false,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: 'json',
            baseURL: ModulePath + "AdvertiseWebService/AdvertiseWebService.asmx/",
            method: "",
            url: "",
            ajaxCallMode: "",
            error: 0
        },

        ajaxCall: function(config) {
            $.ajax({
                type: FrontAdvertiseGallery.config.type,
                contentType: FrontAdvertiseGallery.config.contentType,
                cache: FrontAdvertiseGallery.config.cache,
                async: FrontAdvertiseGallery.config.async,
                url: FrontAdvertiseGallery.config.url,
                data: FrontAdvertiseGallery.config.data,
                dataType: FrontAdvertiseGallery.config.dataType,
                success: FrontAdvertiseGallery.config.ajaxCallMode,
                error: FrontAdvertiseGallery.ajaxFailure
            });
        },
        GetAdvertiseSetting: function() {
            var params = { storeID: storeId, portalID: portalId, cultureName: cultureName };
            var mydata = JSON2.stringify(params);
            this.config.method = "GetAdvertiseSetting";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = mydata;
            this.config.ajaxCallMode = FrontAdvertiseGallery.BindSettingData;
            this.ajaxCall(this.config);
        },

        BindSettingData: function(msg) {
            if (msg.d.length > 0) {
                $.each(msg.d, function(index, item) {
                    count = item.NoOfAdvertise;
                    showUrl = item.ShowUrl;
                    showDetails = item.ShowDetails;
                });
                FrontAdvertiseGallery.BindAdvertiseGallery(count);
            }

        },
        BindAdvertiseGallery: function(count) {
            var params = { storeID: storeId, portalID: portalId, cultureName: cultureName, count: count };
            var mydata = JSON2.stringify(params);
            this.config.method = "AdvertiseFrontImageList";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = mydata;
            this.config.ajaxCallMode = FrontAdvertiseGallery.BindAdvertiseList;
            this.ajaxCall(this.config);

        },

        BindAdvertiseList: function(msg) {
            var advertiseGalleryContents = '';
            var advertiseCaption = '';
            var advertiseGallery = '';
            $("#htmlcaption").html('');
            if (msg.d.length > 0) {
                $.each(msg.d, function(index, item) {
                    if (item.ImagePath == null) {
                        item.ImagePath = aspxRootPath + noImageFeaturedItemPathSetting;
                    }
                    var medpath = item.ImagePath;
                    var url = item.AdvertiseUrl;
                    medpath = medpath.replace('uploads', 'uploads/Medium');                    
                    if (showUrl.toLowerCase() == 'false' || url == "") {
                        advertiseGalleryContents += '<a href="#">';
                    }
                    else {
                        advertiseGalleryContents += '<a href="http://' + url + '" target="_blank">';
                    }
                    advertiseGalleryContents += '<img alt="' + item.AdvertiseName + '" src="' + medpath + '" class=\"cssClassItemImage\" title="#Caption-' + item.ImageID + '" /></a>';
                    if (showDetails.toLowerCase() == 'true') {
                        advertiseCaption += FrontAdvertiseGallery.BindAdvertiseCaption(item.ImageID, item.AdvertiseName, Encoder.htmlDecode(item.AdvertiseDescription), url);
                    }
                });
                advertiseGallery += '<div id="advertiseSlider" class="nivoSlider">' + advertiseGalleryContents + '</div>' + advertiseCaption;
                $("#advertiseSlider-wrapper").html(advertiseGallery);
                $('#advertiseSlider').nivoSlider();
            }
            else {
                advertiseGallery += "<div class=\"nivoSlider\"><div class=\"cssClassNotFound\">This store has no advertise!</div></div>";
                $("#advertiseSlider-wrapper").html(advertiseGallery);
            }
        },
        BindAdvertiseCaption: function(imageId, advertiseName, advertiseShortDesc, url) {
            var advertiseCap = '';
            advertiseCap = '<div id="Caption-' + imageId + '" class="nivo-html-caption">';           
            if (showUrl.toLowerCase() == 'false' || url == "") {               
                advertiseCap += '<a href="#">';
            }
            else {
                advertiseCap += '<a href="http://' + url + '" target="_blank">';
            }
            advertiseCap += '' + advertiseName + '</a><span>' + advertiseShortDesc + '</div>';
            return advertiseCap;
        },



        Init: function() {
            FrontAdvertiseGallery.GetAdvertiseSetting();
            //FrontAdvertiseGallery.BindAdvertiseGallery();
        }

    }
    FrontAdvertiseGallery.Init();
});