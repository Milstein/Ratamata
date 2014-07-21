<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdvertiseGalleryManagement.ascx.cs" Inherits="Modules_AspxCommerce_AspxAdvertiseGallery_AdvertiseGalleryManagement" %>


<script type="text/javascript">
    var AdvertiseManage = '';
    $(function() {
        var maxFileSize = '<%=maxFileSize %>';
        var storeID = '<%=storeID %>';
        var portalID = '<%=portalID %>';
        var cultureName = '<%=cultureName %>';
        var modulePath = '<%=modulePath %>';
        var advertiseName = '';
        var imageID = 0;
        AdvertiseManage = {
            BindAdvertiseGallery: function(searchImageType) {
                var offset_ = 1;
                var current_ = 1;
                var perpage = ($("#advertiseGallery_pagesize").length > 0) ? $("#advertiseGallery_pagesize :selected").text() : 10;

                $("#advertiseGallery").sagegrid({
                    url: modulePath + "AdvertiseWebService/AdvertiseWebService.asmx/",
                    functionMethod: 'GetAdvertiseGalleyList',
                    colModel: [
                    { display: 'ImageID', name: '_imageID', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'ImageChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox' },
                    { display: 'Advertise Name', name: '_advertiseName', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Advertise Url', name: '_advertiseUrl', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Advertise Content', name: '_advertiseDescription', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Image', name: '_imagePath', cssclass: '', controlclass: 'cssImage', coltype: 'image', align: 'left' },
                    { display: 'Is Active', name: 'status', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Actions', name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }

                	],
                    buttons: [
                    { display: 'Edit', name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'AdvertiseManage.EditImage', arguments: '0,1,2,3,4,5,6' },
			        { display: 'Delete', name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'AdvertiseManage.DeleteImage', arguments: '' },
			        { display: 'Activate', name: 'active', enable: true, _event: 'click', trigger: '3', callMethod: 'AdvertiseManage.ActiveAdvertise', arguments: '' },
                { display: 'Deactivate', name: 'deactive', enable: true, _event: 'click', trigger: '4', callMethod: 'AdvertiseManage.DeactiveAdvertise', arguments: '' }
			    ],
                    rp: perpage,
                    nomsg: "No Records Found!",
                    param: { advertiseName: searchImageType, storeID: storeID, portalID: portalID, cultureName: cultureName },
                    current: current_,
                    pnew: offset_,
                    sortcol: { 0: { sorter: false }, 6: { sorter: false} }
                });
            },


            LoadAdvertiseStaticImage: function() {
                $('#ajaxImageLoad').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
            },

            HideAllAdvertiseDivs: function() {
                $("#divShowAdvertise").hide();
                $("#divAdvertiseProviderForm").hide();
            },

            EditImage: function(tblID, argus) {
                switch (tblID) {
                    case "advertiseGallery":
                        $("#<%=_lblImageFormTitle.ClientID %>").html("Edit Advertise Gallery: '" + argus[0] + "'");
                        AdvertiseManage.HideAllAdvertiseDivs();
                        $("#divAdvertiseProviderForm").show();
                        $("#txtAdvertiseName").val(argus[4]);
                        $("#txtUrl").val(argus[5]);
                        $("#txtAdvertiseContain").val(argus[6]);
                        var img = argus[7];
                        imageID = argus[0];
                        $("#ProductImage").html('<img src="' + aspxRootPath + img + '" height="90px" width="100px"/>');
                        break;
                    default:
                        break;
                }
            },

            DeleteImage: function(tblID, argus) {
                switch (tblID) {
                    case "advertiseGallery":
                        var properties = { onComplete: function(e) {
                            AdvertiseManage.DeleteImageByID(argus[0], e);
                        }
                        }
                        csscody.confirm("<h1>Delete Confirmation</h1><p>Do you want to delete?</p>", properties);
                        break;
                    default:
                        break;
                }
            },

            DeleteImageByID: function(ids, event) {
                imageID = ids;
                if (event) {
                    $.ajax({
                        type: "POST",
                        url: modulePath + "AdvertiseWebService/AdvertiseWebService.asmx/DeleteAdvertise",
                        data: JSON2.stringify({ imageID: imageID, storeID: storeID, portalID: portalID, cultureName: cultureName }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function(msg) {
                            AdvertiseManage.BindAdvertiseGallery(null);
                        },
                        error: function() {
                            alert("error");

                        }
                    });
                }
            },

            DeleteMultipleImage: function(ids, event) {
                AdvertiseManage.DeleteImageByID(ids, event);

            },

            ActiveAdvertise: function(tblID, argus) {
                switch (tblID) {
                    case "advertiseGallery":
                        AdvertiseManage.ActivateImageID(argus[0]);
                        break;
                    default:
                        break;
                }
            },
            ActivateImageID: function(imageID) {
                $.ajax({
                    type: "POST",
                    url: modulePath + "AdvertiseWebService/AdvertiseWebService.asmx/ActivateAdvertise",
                    data: JSON2.stringify({ imageID: imageID, storeID: storeID, portalID: portalID, cultureName: cultureName }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(msg) {
                        csscody.info("<h2>Information Message</h2><p>Advertise Activated Sucessfully</p>");
                        AdvertiseManage.BindAdvertiseGallery(null);
                    },
                    error: function() {
                        alert("error");

                    }
                });
            },
            DeactiveAdvertise: function(tblID, argus) {
                switch (tblID) {
                    case "advertiseGallery":
                        AdvertiseManage.DeActivateImageID(argus[0]);
                        break;
                    default:
                        break;
                }
            },

            DeActivateImageID: function(imageID) {
                $.ajax({
                    type: "POST",
                    url: modulePath + "AdvertiseWebService/AdvertiseWebService.asmx/DeActivateAdvertise",
                    data: JSON2.stringify({ imageID: imageID, storeID: storeID, portalID: portalID, cultureName: cultureName }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(msg) {
                        csscody.info("<h2>Information Message</h2><p>Advertise DeActivated Sucessfully</p>");
                        AdvertiseManage.BindAdvertiseGallery(null);
                    },
                    error: function() {
                        alert("error");

                    }
                });
            },
            ImageUploader: function() {
                var upload = new AjaxUpload($('#txtImagePath'), {
                    action: modulePath + "fileuploadhandler.aspx",
                    name: 'myfile[]',
                    multiple: false,
                    data: {},
                    autoSubmit: true,
                    responseType: 'json',
                    onChange: function(file, ext) {
                    },
                    onSubmit: function(file, ext) {
                        if (ext != "exe") {
                            if (ext && /^(jpg|jpeg|jpe|gif|bmp|png|ico)$/i.test(ext)) {
                                this.setData({
                                    'MaxFileSize': maxFileSize
                                });
                            } else {
                                csscody.alert('<h1>Alert Message</h1><p>Not a valid image!</p>');
                                return false;
                            }
                        }
                        else {
                            csscody.alert('<h1>Alert Message</h1><p>Not a valid image!</p>');
                            return false;
                        }
                    },
                    onComplete: function(file, response) {
                        var res = eval(response);
                        if (res.Message != null && res.Status > 0) {
                            advertiseName = res.Message.split('/')[2];
                            AdvertiseManage.AddNewImages(res);
                            return false;
                        }
                        else {
                            csscody.error('<h1>Error Message</h1><p>Can\'t upload the image!</p>');
                            return false;
                        }
                    }
                });
            },

            AddNewImages: function(response) {
                $("#ProductImage").html('<img src="' + aspxRootPath + response.Message + '" class="uploadImage" height="90px" width="100px"/>');
            },


            AddUpdateAdvertise: function() {
                var defaultImageProductURL = $("#ProductImage>img").attr("src").replace(aspxRootPath, "");
                var prevFilePath = $("#hdnPrevFilePath").val();
                var advertiseName = $("#txtAdvertiseName").val();
                var advertiseDetails = $("#txtAdvertiseContain").val();
                var advertiseUrl = $("#txtUrl").val();
                $.ajax({
                    type: "POST",
                    url: modulePath + "AdvertiseWebService/AdvertiseWebService.asmx/AddUpdateAdvertise",
                    data: JSON2.stringify({ newFilePath: defaultImageProductURL, prevFilePath: prevFilePath, storeID: storeID, portalID: portalID, cultureName: cultureName, imageID: imageID, advertiseName: advertiseName, advertiseUrl: advertiseUrl, advertiseDetails: advertiseDetails, aspxCommonObj: aspxCommonObj }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(msg) {
                        AdvertiseManage.BindAdvertiseGallery(null);
                        AdvertiseManage.HideAllAdvertiseDivs();
                        $("#divShowAdvertise").show();
                    },
                    error: function(msg) {
                        alert('Error!');
                    }
                });

            },

            ClearData: function() {
                $("#txtAdvertiseName").val('');
                $("#txtAdvertiseContain").val('');
                $("#txtUrl").val('');
                $("#txtImagePath").val('');
                $("#ProductImage").html('');

            },

            SearchAdvertiseImage: function() {
                var searchImageType = $.trim($("#AdvertiseName").val());
                if (searchImageType.length < 1) {
                    searchImageType = null;
                }
                AdvertiseManage.BindAdvertiseGallery(searchImageType);

            },
            Init: function() {
                AdvertiseManage.LoadAdvertiseStaticImage();
                AdvertiseManage.BindAdvertiseGallery(null);
                AdvertiseManage.HideAllAdvertiseDivs();
                $("#divShowAdvertise").show();
                $("#btnAddNewAdvertise").click(function() {
                    AdvertiseManage.ClearData();
                    imageID = 0;
                    $("#<%=_lblImageFormTitle.ClientID %>").html("Add New Advertisement");
                    AdvertiseManage.HideAllAdvertiseDivs();
                    $("#divAdvertiseProviderForm").show();
                    $("#txtNewCouponType").val('');

                });
                $("#btnSearch").click(function() {
                    AdvertiseManage.SearchAdvertiseImage();
                });
                $("#btnDeleteSelected").click(function() {
                    var image_ids = '';
                    $(".ImageChkbox").each(function(i) {
                        if ($(this).attr("checked")) {
                            image_ids += $(this).val() + ',';
                        }
                    });
                    if (image_ids == "") {
                        csscody.alert('<h2>Information Alert</h2><p>None of the data are selected</p>');
                        return false;
                    }
                    var properties = { onComplete: function(e) {
                        AdvertiseManage.DeleteMultipleImage(image_ids, e);
                    }
                    }
                    csscody.confirm("<h1>Delete Confirmation</h1><p>Do you want to delete?</p>", properties);
                });

                $("#btnShow").click(function() {
                    AdvertiseManage.BindAdvertiseGallery(null);
                });

                $("#btnSave").click(function() {
                    AdvertiseManage.AddUpdateAdvertise();
                    AdvertiseManage.ClearData();
                });
                $("#btnCancel").click(function() {
                    AdvertiseManage.HideAllAdvertiseDivs();
                    $("#divShowAdvertise").show();
                });
                AdvertiseManage.ImageUploader();
            }
        }
        AdvertiseManage.Init();
    });
    
</script>

<div id="divShowAdvertise">
    <div class="cssImageCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="_lblAdvertiseGridTitle" runat="server" Text="Manage Advertise Gallery"></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper">
                     <p>
                        <button type="button" id="btnShow">
                            <span><span>Show All</span></span></button>
                    </p>
                    <p>
                        <button type="button" id="btnDeleteSelected">
                            <span><span>Delete All Selected</span></span></button>
                    </p>
                    <p>
                        <button type="button" id="btnAddNewAdvertise">
                            <span>Add New Advertisement</span></button>
                    </p>
                    <div class="cssClassClear">
                    </div>
                </div>
            </div>
            <div class="cssClassClear">
            </div>
        </div>
        <div class="cssClassGridWrapper">
            <div class="cssClassGridWrapperContent">
                <div class="cssClassSearchPanel cssClassFormWrapper">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>                           
                            <td>
                            <label class="cssClassLabel">
                                   Advertise Name:</label>
                                <input type="text" id="AdvertiseName" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <div class="cssClassButtonWrapper cssClassPaddingNone">
                                    <p>
                                        <button type="button" id="btnSearch">
                                            <span><span>Search</span></span></button>
                                    </p>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxImageLoad" />
                </div>
                <div class="log">
                </div>
                <table id="advertiseGallery" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
            </div>
        </div>
    </div>
</div>
<div id="divAdvertiseProviderForm">
    <div class="cssClassImageBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="_lblImageFormTitle" runat="server"></asp:Label>
            </h2>
        </div>
        <div class="cssClassFormWrapper">
            <table border="0" width="100%" id="tblEditAdvertiseGallery" class="cssClassPadding tdpadding">                
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="_lblImageName" runat="server" Text="Advertise Name:" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="text" id="txtAdvertiseName" name="AdvertiseName" class="cssClassNormalTextBox required" />
                    </td>
                </tr>
                 <tr>
                    <td>
                        <asp:Label ID="_lblAdvertiseContain" runat="server" Text="Advertise Content:" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="text" id="txtAdvertiseContain" name="AdvertiseContent" class="cssClassNormalTextBox " />
                    </td>
                </tr>
                 <tr>
                    <td>
                        <asp:Label ID="lblWebUrl" runat="server" Text="Url (without http:\\):" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="text" id="txtUrl" name="Url" class="cssClassNormalTextBox " />
                    </td>
                </tr>
                 <tr>
                    <td>
                        <asp:Label ID="_lblImagePath" runat="server" Text="Advertise Image:" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                    <input id="txtImagePath" type="file" class="cssClassBrowse" />                        
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="ProductImage">
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div class="cssClassButtonWrapper">
            <p>
                <button type="button" id="btnCancel">
                    <span><span>Cancel</span></span></button>
            </p>
            <p>
                <button type="button" id="btnSave">
                    <span><span>Save</span></span></button>
            </p>
        </div>
    </div>
</div>

<input type="hidden" id="hdnPrevFilePath" />