<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FlightView.ascx.cs" Inherits="Modules_AspxCommerce_AspxFlight_FlightView" %>

<script type="text/javascript">
    //<![CDATA[
    var StoreID = "<%=StoreID %>";
    var PortalID = "<%=PortalID %>";
    var CultureName = "<%=CultureName %>";
    var UserName = '<%=UserName %>';
    var modulePath = '<%=modulePath %>';
    //]]>
</script>

<div id="divFlightBooking">
    <div class="cssClassCommonSideBox">
        <h2>
            <asp:Label ID="lblAddressTitle" runat="server" Text="Book Your Flight" meta:resourcekey="lblAddressTitleResource1"></asp:Label>
        </h2>
        <div class="cssClassCommonSideBoxTable cssClassFormWrapper">
            <table id="tblFlightBooking" border="0" class="cssClassPadding tdpadding" width="100%">
                <tbody>
                    <tr>
                        <td class="cssClassTableRightCol">
                            <ul id="flightType" style="display: inline">
                            </ul>
                        </td>
                        <td class="cssClassHeading1">
                            <asp:Label ID="lblFlightTripType" runat="server" Text="Trip Type:" CssClass="cssClassLabel"
                                meta:resourcekey="lblFlightTripTypeResource1"></asp:Label>
                        </td>
                        <td class="cssClassTableRightCol">
                            <ul id="tripType" style="display: inline">
                            </ul>
                        </td>
                    </tr>
                    <tr>
                        <td class="Cssclassdestination">
                            <asp:Label ID="lblFrom" runat="server" Text="From:" CssClass="cssClassLabel" meta:resourcekey="lblEmailResource1"></asp:Label>
                            <div id='divFromLocation'>
                            </div>
                        </td>
                        <td class="Cssclassdestination">
                            <asp:Label ID="lblTo" Text="To:" runat="server" CssClass="cssClassLabel" meta:resourcekey="lblToResource1"></asp:Label>
                            <div id='divToLocation'>
                            </div>
                        </td>
                        <td class="Cssclassdestination">
                            <asp:Label ID="lblDepartTime" Text="Depart Time:" runat="server" CssClass="cssClassLabel"
                                meta:resourcekey="lblDepartTimeResource1"></asp:Label>
                            <input type="text" readonly="readonly" id="txtDepartTime" name="departTime" class="datepicker" />
                        </td>
                        <td class="Cssclassdestination">
                            <asp:Label ID="lblReturnTime" Text="Return Time:" runat="server" CssClass="cssClassLabel"
                                meta:resourcekey="lblReturnTimeResource1"></asp:Label>
                            <input type="text" readonly="readonly" id="txtReturnTime" name="departTime" class="datepicker" />
                        </td>
                    </tr>
                    <tr>
                        <td class="cssClassagegroup">
                            <span class="cssClassLabel">Adult:</span>
                            <select id="ddlAdult" name="no_adult" class="cssClassDropDown">
                                <option value="1">1</option>
                                <option value="2">2</option>
                                <option value="3">3</option>
                                <option value="4">4</option>
                                <option value="5">5</option>
                                <option value="6">6</option>
                                <option value="7">7</option>
                                <option value="8">8</option>
                            </select>
                        </td>
                        <td class="cssClassagegroup">
                            <span class="cssClassLabel">Child:</span>
                            <select id="ddlChild" name="no_child" class="cssClassDropDown">
                                <option value="0">0</option>
                                <option value="1">1</option>
                                <option value="2">2</option>
                                <option value="3">3</option>
                                <option value="4">4</option>
                                <option value="5">5</option>
                                <option value="6">6</option>
                            </select>
                        </td>
                        <td class="cssClassagegroup">
                            <span class="cssClassLabel">Infant:</span>
                            <select id="ddlInfant" name="no_infant" class="cssClassDropDown">
                                <option value="0">0</option>
                                <option value="1">1</option>
                                <option value="2">2</option>
                                <option value="3">3</option>
                                <option value="4">4</option>
                                <option value="5">5</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="cssClassLabel">Class:</span>
                            <select id="ddlFlightClass" name="flight_class" class="cssClassDropDown">
                            </select>
                        </td>
                    </tr>
                </tbody>
            </table>
            <div class="cssClassButton">
                <input id="btnSearch" value="Proceed" type="button" rel="popuprel6" class="cssClassSubmitBtn">
            </div>
        </div>
    </div>
</div>