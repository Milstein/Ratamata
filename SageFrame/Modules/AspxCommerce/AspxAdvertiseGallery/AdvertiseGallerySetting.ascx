<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdvertiseGallerySetting.ascx.cs" Inherits="Modules_AspxCommerce_AspxAdvertiseGallery_AdvertiseGallerySetting" %>


<script type="text/javascript">
    $(function() {
        var modulePath = "<%=modulePath %>";
        var storeID = "<%=storeID %>";
        var portalID = "<%=portalID %>";
        var cultureName = "<%=cultureName %>";
        var count;
        var showUrl;
        var showDetails;
        var settingKeys;
        var settingValues;
       
        function SaveAdvertiseSetting() {
            if ($("#chkShowUrl").attr('checked') == true) {
                showUrl = "true";
            }
            else {
                showUrl = "false";
            }
            if ($("#chkShowDetails").attr('checked') == true) {
                showDetails = "true";
            }
            else {
                showDetails = "false";
            }
            count = $("#txtAdvertiseCount").val();
            settingKeys = "NoOfAdvertise*ShowUrl*ShowDetails";
            settingValues = count + "*" + showUrl + "*" + showDetails;
            $.ajax({
                type: "POST",
                url: modulePath + "AdvertiseWebService/AdvertiseWebService.asmx/SaveAdvertiseSetting",
                data: JSON2.stringify({ storeID: storeID, portalID: portalID, cultureName: cultureName, SettingKeys: settingKeys, SettingValues: settingValues }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(msg) {
                    csscody.info("<h2>Information Message</h2><p>Setting Updated Sucessfully</p>");
                },
                error: function() {
                    alert("error");

                }
            });
        }

        function GetAdvertiseSettingValue() {
            $.ajax({
                type: "POST",
                url: modulePath + "AdvertiseWebService/AdvertiseWebService.asmx/GetAdvertiseSetting",
                data: JSON2.stringify({ storeID: storeID, portalID: portalID, cultureName: cultureName }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(msg) {
                    BindSettingData(msg);
                },
                error: function() {
                    alert("error");

                }
            });
        }
        function BindSettingData(msg) {
            $.each(msg.d, function(index, item) {
                var advetiseCount = item.NoOfAdvertise;
                var showUrl = item.ShowUrl;
                var showDetail = item.ShowDetails;
                if (showUrl.toLowerCase() == "true") {
                    $("#chkShowUrl").attr('checked', 'checked');
                }
                if (showDetail.toLowerCase() == "true") {
                    $("#chkShowDetails").attr('checked', 'checked');
                }
                $("#txtAdvertiseCount").val(advetiseCount);

            });
        }

        $(document).ready(function() {
            GetAdvertiseSettingValue();
            $("#btnSaveSetting").bind("click", function() {               
                SaveAdvertiseSetting();
            });
        });
    });
        
    </script>
<div class="cssClassCommonBox Curve">
    <div class="cssClassHeader">
        <h2>
            <asp:Label ID="lblSeasonalSetting" Text="Seasonal Sale Setting" runat="server"></asp:Label>
        </h2>
    </div>
    <div class="cssClassFormWrapper">
        <div id="divAdvertise">
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblShowUrl" runat="server" Text="Show Advertise Url" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input id="chkShowUrl" type="checkbox" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblShowDetails" runat="server" Text="Show Advertise Details" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input id="chkShowDetails" type="checkbox" />
                    </td>
                </tr>
                <tr>
                <td>
                    <asp:Label ID="lblAdvertiseCount" runat="server" Text="Number Of Advertise Shown" CssClass="cssClassLabel"></asp:Label>
                </td>
                <td >
                    <input id="txtAdvertiseCount" type="text" />
                </td>
            </tr>
            </table>
        </div>       
    </div>
    <div class="cssClassButtonWrapper">
        
        <p>
            <button type="button" id="btnSaveSetting">
                <span><span>Save</span></span></button>
        </p>
    </div>
</div>
