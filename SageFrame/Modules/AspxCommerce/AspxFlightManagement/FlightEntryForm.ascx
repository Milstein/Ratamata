<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FlightEntryForm.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxFlightManagement_FlightEntryForm" %>

<script type="text/javascript">
    //<![CDATA[
    var StoreID = "<%=StoreID %>";
    var PortalID = "<%=PortalID %>";
    var CultureName = "<%=CultureName %>";
    var UserName = '<%=UserName %>';
    var modulePath = '<%=modulePath %>';
    var homePageUrl = '<%=homePageUrl %>';
    //]]>
</script>

<div id="divFlightReservation">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassFormWrapper">
            <div class="divInformation">
                <span>Fields marked with <span style="color: Red">*</span> are compulsory.</span></div>
            <table id="tblReservation" width="100%" border="0" cellpadding="0" cellspacing="0">
                <tbody>
                    <tr>
                        <td>
                            <asp:Label ID="lblFlightType" runat="server" Text="Flight Type:" CssClass="cssClassLabel"
                                meta:resourcekey="lblFlightTripTypeResource1"></asp:Label>
                        </td>
                        <td class="cssClassTableRightCol">
                            <ul id="flightType" style="display: inline">
                            </ul>
                        </td>
                    </tr>
                    <tr>
                          <td>
                            <asp:Label ID="lblFlightClass" runat="server" Text="Flight Class:" CssClass="cssClassLabel"
                                meta:resourcekey="lblFlightClassResource1"></asp:Label>
                        </td>
                        <td>
                            <select id="ddlFlightClass" name="flight_class" class="cssClassDropDown">
                            </select>
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
                            <ul id="ResTripType" style="display: inline">
                            </ul>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblResDepart" Text="Depart Time:" runat="server" CssClass="cssClassLabel"
                                meta:resourcekey="lblResDepartResource1"></asp:Label>
                            <span class="cssClassRequired">*</span>
                        </td>
                        <td>
                            <input type="text" readonly="readonly" id="txtResDepart" name="ResdepartTime" class="datepicker cssClassNormalTextBox required" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblResReturn" Text="Return Time:" runat="server" CssClass="cssClassLabel"
                                meta:resourcekey="lblResReturnResource1"></asp:Label><span class="cssClassRequired">*</span>
                        </td>
                        <td>
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
                                            <select id="ddlResAdult" name="no_adult" class="cssClassDropDown">
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
                                            <select id="ddlResChild" name="no_child" class="cssClassDropDown">
                                                <option value="0">0</option>
                                                <option value="1">1</option>
                                                <option value="2">2</option>
                                                <option value="3">3</option>
                                                <option value="4">4</option>
                                                <option value="5">5</option>
                                                <option value="6">6</option>
                                            </select>
                                            <span class="cssClassLabel">Infant(0-2 yrs):</span>
                                            <select id="ddlResInfant" name="no_infant" class="cssClassDropDown">
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
                        <tr>
                            <td>
                                <asp:Label ID="lblLastName" runat="server" Text="Last Name:" CssClass="cssClassLabel"
                                    meta:resourcekey="lblLastNameResource1"></asp:Label><span class="cssClassRequired">*</span>
                            </td>
                            <td class="cssClassTableRightCol">
                                <input type='text' id='txtLastName' lass="cssClassNormalTextBox required" name="lname" />
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
            <div class="cssClassButton">
                <input id="btnBack" value="Back To Home" type="button" class="cssClassSubmitBtn sfLocale">
                <input id="btnSave" value="Get Quote" type="button" class="cssClassSubmitBtn sfLocale">
            </div>
        </div>
    </div>
</div>
