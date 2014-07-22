<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FlightManagement.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxFlight_FlightEdit" %>

<script type="text/javascript">
    //<![CDATA[
    //var StoreID = "<%=StoreID %>";
    //var PortalID = "<%=PortalID %>";
    //var CultureName = "<%=CultureName %>";
    //var UserName = '<%=UserName %>';
    //]]>
    var modulePath = '<%=modulePath %>';
    var DefaultPortalHomePage = '<%=DefaultPortalHomePage %>';
</script>

<div id="divFlightReservation">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="Label2" runat="server">Flight Reservation Management</asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper">
                    <p>
                        <button type="button" id="btnShowReservation">
                            <span><span>Show All</span></span></button>
                    </p>
                    <p>
                        <button type="button" id="btnDeleteReservatin">
                            <span><span>Delete All Selected</span></span></button>
                    </p>
                    <p>
                        <button type="button" id="btnAddLocation">
                            <span><span>Add Location</span></span></button>
                    </p>
                    <p>
                        <button type="button" id="btnShowMap">
                            <span><span>Add Mapping</span></span></button>
                    </p>
                </div>
            </div>
        </div>
        <div class="cssClassGridWrapper">
            <div class="cssClassGridWrapperContent">
                <div class="cssClassSearchPanel cssClassFormWrapper">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <label class="cssClassLabel">
                                    From:</label>
                                <input id="txtReservationFrom" type="text" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <label class="cssClassLabel">
                                    To:</label>
                                <input id="txtReservationTo" type="text" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <label class="cssClassLabel">
                                    First Name:</label>
                                <input id="txtFname" type="text" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <label class="cssClassLabel">
                                    Last Name:</label>
                                <input id="txtLname" type="text" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <div class="cssClassButtonWrapper cssClassPaddingNone">
                                    <p>
                                        <button type="button" id="btnReservationSearch">
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
                <table id="tblFlightReservation" cellspacing="0" cellpadding="0" width="100%">
                </table>
                <div class="cssClassClear">
                </div>
            </div>
        </div>
    </div>
</div>
<div id="divFlightEdit" style="display: none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <span id='resFlightType'>Editing Flight Reservation</span>
            </h2>
        </div>
        <div class="cssClassFormWrapper">
            <table id="tblReservation" border="0" class="cssClassPadding tdpadding" width="100%">
                <tbody>
                    <tr>
                        <td>
                            <asp:Label ID="lblFlightType" runat="server" Text="Flight Type:" CssClass="cssClassLabel"
                                meta:resourcekey="lblFlightTypeResource1"></asp:Label>
                        </td>
                        <td class="cssClassTableRightCol">
                            <ul id="flightType" style="display: inline">
                            </ul>
                            <input type="hidden" id='txtReservationId' />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblResNation" runat="server" Text="Nationality:" CssClass="cssClassLabel"
                                meta:resourcekey="lblResNationResource1"></asp:Label>
                        </td>
                        <td class="cssClassTableRightCol">
                            <ul id="ResNationality" style="display: inline">
                            </ul>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="cssClassLabel">Class:</span>
                        </td>
                        <td class="cssClassTableRightCol">
                            <select id="ddlFlightClass" name="flight_class" class="cssClassDropDown">
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblResCityFrom" runat="server" Text="From:" CssClass="cssClassLabel"
                                meta:resourcekey="lblResCityFromResource1"></asp:Label><span class="cssClassRequired">*</span>
                        </td>
                        <td class="cssClassTableRightCol">
                            <select id='ddlResPlaceFrom' class='cssClassDropDown' validate="required:true" name="from">
                            </select>
                            <input type='text' id='txtResFromLocation' name="resFrom" class="cssClassNormalTextBox required" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblResCityTo" Text="To:" runat="server" CssClass="cssClassLabel" meta:resourcekey="lblResCityToResource1"></asp:Label><span
                                class="cssClassRequired">*</span>
                        </td>
                        <td class="cssClassTableRightCol">
                            <select id='ddlResPlaceTo' class='cssClassDropDown' validate="required:true" name='to'>
                            </select>
                            <input type='text' id='txtResToLocation' name="resTo" class="cssClassNormalTextBox required" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblResTripType" runat="server" Text="Trip Type:" CssClass="cssClassLabel"
                                meta:resourcekey="lblResTripTypeResource1"></asp:Label>
                        </td>
                        <td class="cssClassTableRightCol">
                            <ul id="tripType" style="display: inline">
                            </ul>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblResDepart" Text="Depart Time:" runat="server" CssClass="cssClassLabel"
                                meta:resourcekey="lblResDepartResource1"></asp:Label>
                            <span class="cssClassRequired">*</span>
                        </td>
                        <td class="cssClassTableRightCol">
                            <input type="text" readonly="readonly" id="txtResDepart" name="ResdepartTime" class="datepicker cssClassNormalTextBox required" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblResReturn" Text="Return Time:" runat="server" CssClass="cssClassLabel"
                                meta:resourcekey="lblResReturnResource1"></asp:Label><span class="cssClassRequired">*</span>
                        </td>
                        <td class="cssClassTableRightCol">
                            <input type="text" readonly="readonly" id="txtResReturn" name="ResReturnTime" class="datepicker cssClassNormalTextBox required" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <table width="100%" cellspacing="1" cellpadding="0">
                                <tbody>
                                    <tr>
                                        <td>
                                            <span class="cssClassLabel">Adult (12+ yrs):</span>
                                            <select id="ddlResAdult" name="no_adult">
                                                <option value="1">1</option>
                                                <option value="2">2</option>
                                                <option value="3">3</option>
                                                <option value="4">4</option>
                                                <option value="5">5</option>
                                                <option value="6">6</option>
                                                <option value="7">7</option>
                                                <option value="8">8</option>
                                            </select>
                                            <span class="cssClassLabel">Child (2-12 yrs):</span>
                                            <select id="ddlResChild" name="no_child">
                                                <option value="0">0</option>
                                                <option value="1">1</option>
                                                <option value="2">2</option>
                                                <option value="3">3</option>
                                                <option value="4">4</option>
                                                <option value="5">5</option>
                                                <option value="6">6</option>
                                            </select>
                                            <span class="cssClassLabel">Infant (0- 2 yrs):</span>
                                            <select id="ddlResInfant" name="no_infant">
                                                <option value="0">0</option>
                                                <option value="1">1</option>
                                                <option value="2">2</option>
                                                <option value="3">3</option>
                                                <option value="4">4</option>
                                                <option value="5">5</option>
                                            </select>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblFirstName" runat="server" Text="First Name:" CssClass="cssClassLabel"
                                meta:resourcekey="lblFirstNameResource1"></asp:Label><span class="cssClassRequired">*</span>
                        </td>
                        <td class="cssClassTableRightCol">
                            <input type='text' id='txtFirstName' class="cssClassNormalTextBox required" name="fname" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblMiddleName" runat="server" Text="Middle Name:" CssClass="cssClassLabel"
                                meta:resourcekey="lblMiddleNameResource1"></asp:Label>
                        </td>
                        <td class="cssClassTableRightCol">
                            <input type='text' id='txtMiddleName' class="cssClassNormalTextBox" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblLastName" runat="server" Text="Last Name:" CssClass="cssClassLabel"
                                meta:resourcekey="lblLastNameResource1"></asp:Label><span class="cssClassRequired">*</span>
                        </td>
                        <td class="cssClassTableRightCol">
                            <input type='text' id='txtLastName' class="cssClassNormalTextBox required" name="lname" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblNameTravel" runat="server" Text="Name of Other Traveller:" CssClass="cssClassLabel"
                                meta:resourcekey="lblTravelResource1"></asp:Label>
                        </td>
                        <td class="cssClassTableRightCol">
                            <textarea id="txtTraveller" rows="5" cols="30"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblPhone" runat="server" Text="Phone:" CssClass="cssClassLabel" meta:resourcekey="lblPhoneResource1"></asp:Label>
                        </td>
                        <td class="cssClassTableRightCol">
                            <input type='text' id='txtPhone' class="cssClassNormalTextBox cssIntegerOnly" maxlength="10" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblMobile" runat="server" Text="Mobile Number:" CssClass="cssClassLabel"
                                meta:resourcekey="lblMobileResource1"></asp:Label><span class="cssClassRequired">*</span>
                        </td>
                        <td class="cssClassTableRightCol">
                            <input type='text' id='txtMobile' class="cssClassNormalTextBox required cssIntegerOnly"
                                name="mobile" maxlength="10" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblEmail" runat="server" Text="Email:" CssClass="cssClassLabel" meta:resourcekey="lblEmailResource1"></asp:Label><span
                                class="cssClassRequired">*</span>
                        </td>
                        <td class="cssClassTableRightCol">
                            <input type='text' id='txtEmail' class="cssClassNormalTextBox required email" name='email' />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblAdditinalInfo" runat="server" Text="Additional Info:" CssClass="cssClassLabel"
                                meta:resourcekey="lblAdditinalInfoResource1"></asp:Label>
                        </td>
                        <td class="cssClassTableRightCol">
                            <textarea id="txtAdditionalInfo" rows="5" cols="30"></textarea>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="cssClassButtonWrapper">
            <p>
                <button id="btnBack" type="button">
                    <span><span>Back</span></span></button></p>
            <p>
                <p>
                    <button id="btnSingleDelete" type="button">
                        <span><span>Delete</span></span></button></p>
                <p>
                    <button id="btnUpdate" type="button">
                        <span><span>Update</span></span></button></p>
        </div>
    </div>
</div>
<div id="divLocation" style="display: none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="Label3" runat="server">Add New Domestic Location</asp:Label>
            </h2>
        </div>
        <div class="cssClassFormWrapper">
            <table id="tblAddLocation" border="0" class="cssClassPadding tdpadding" width="100%">
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" CssClass="cssClassLabel" Text="Location:"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type='text' id='txtLocation' class="cssClassNormalTextBox" name="location" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="cssClassButtonWrapper">
            <p>
                <button type="button" id="btnSaveLocation">
                    <span><span>Add Location</span></span></button>
            </p>
            <p>
                <button type="button" id="btnCancelLocation">
                    <span><span>Cancel</span></span></button>
            </p>
        </div>
    </div>
</div>
<div id="divFlightManage" style="display: none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblFlightManage" runat="server">Flight Mapping Management</asp:Label>
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
                        <button type="button" id="btnAddMap">
                            <span><span>Add New Mapping</span></span></button>
                    </p>
                    <p>
                        <button type="button" id="btnBackToFlight">
                            <span><span>Back</span></span></button>
                    </p>
                </div>
            </div>
        </div>
        <div class="cssClassGridWrapper">
            <div class="cssClassGridWrapperContent">
                <div class="cssClassSearchPanel cssClassFormWrapper">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <label class="cssClassLabel">
                                    From:</label>
                                <select id="ddlFrom">
                                </select>
                            </td>
                            <td>
                                <label class="cssClassLabel">
                                    To:</label>
                                <select id="ddlTo">
                                </select>
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
                    <img id="Img1" />
                </div>
                <div class="log">
                </div>
                <table id="gdvFlightMapping" cellspacing="0" cellpadding="0" width="100%">
                </table>
                <div class="cssClassClear">
                </div>
            </div>
        </div>
    </div>
</div>
<br />
<div id="divAddMapping" style="display: none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblAddMapTitle" runat="server">Add New Mapping</asp:Label>
            </h2>
        </div>
        <div class="cssClassFormWrapper">
            <table id="tblEditNewsForm" border="0" class="cssClassPadding tdpadding" width="100%">
                <tr>
                    <td>
                        <asp:Label ID="lblLocationFrom" runat="server" CssClass="cssClassLabel" Text="Location From:"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <select id="ddlLocationFrom">
                            <option value="0">Select Location</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" CssClass="cssClassLabel" Text="Location To:"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <select id="ddlLocationTo">
                            <option value="0">Select Location</option>
                        </select>
                    </td>
                </tr>
            </table>
        </div>
        <div class="cssClassButtonWrapper">
            <p>
                <button type="button" id="btnSave">
                    <span><span>Add Mapping</span></span></button>
            </p>
            <p>
                <button type="button" id="btnCancel">
                    <span><span>Cancel</span></span></button>
            </p>
        </div>
    </div>
</div>
