var FlightManage = "";
var mappingID = '';
var ReservationID = '';
$.metadata.setType("attr", "validate");
$(function() {
    var storeId = AspxCommerce.utils.GetStoreID();
    var portalId = AspxCommerce.utils.GetPortalID();
    var userName = AspxCommerce.utils.GetUserName();
    var cultureName = AspxCommerce.utils.GetCultureName();
    var element = '';
    FlightManage = {
        config: {
            isPostBack: false,
            async: true,
            cache: true,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: 'json',
            baseURL: modulePath + "FlightWebServices/AspxFlightWebService.asmx/",
            method: "",
            url: "",
            ajaxCallMode: "",
            error: ""
        },
        ajaxCall: function(config) {
            $.ajax({
                type: FlightManage.config.type,
                contentType: FlightManage.config.contentType,
                cache: FlightManage.config.cache,
                async: FlightManage.config.async,
                url: FlightManage.config.url,
                data: FlightManage.config.data,
                dataType: FlightManage.config.dataType,
                success: FlightManage.config.ajaxCallMode,
                error: FlightManage.error
            });
        },

        BindMappingListInGrid: function(placeFrom, placeTo, storeId, portalId, culture) {
            this.config.method = "GetFlightMappingList";
            this.config.data = { PlaceFrom: placeFrom, PlaceTo: placeTo, storeID: storeId, portalID: portalId, CultureName: culture };
            var data = this.config.data;
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvFlightMapping_pagesize").length > 0) ? $("#gdvFlightMapping_pagesize :selected").text() : 10;
            $("#gdvFlightMapping").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: 'DomesticPlacesMapID', name: '_domesticPlacesMapID', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'flightChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox' },
                    { display: 'Place From', name: '_placeFrom', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Place To', name: '_placeTo', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Store ID', name: '_storeID', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Portal ID', name: '_portalID', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Is Active', name: '_isActive', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', hide: false, type: 'boolean', format: 'Yes/No' },
                    { display: 'Is Deleted', name: '_isDeleted', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', hide: true, type: 'boolean', format: 'Yes/No' },
                    { display: 'Is Modified', name: '_isModified', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', hide: true, type: 'boolean', format: 'Yes/No' },
                    { display: 'Added On', name: '_addedOn', cssclass: '', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd', hide: false },
                    { display: 'Updated On', name: '_updatedOn', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'center', type: 'date', format: 'yyyy/MM/dd', hide: true },
                    { display: 'Deleted On', name: '_deletedOn', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'center', type: 'date', format: 'yyyy/MM/dd', hide: true },
                    { display: 'Added By', name: '_addedBy', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Updated By', name: '_updatedBy', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Deleted By', name: '_deletedBy', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Actions', name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }

                ],
                buttons: [
                      { display: 'Delete', name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'FlightManage.DeleteFlightMapping', arguments: '1' }

                    ],
                rp: perpage,
                nomsg: "No Records Found!",
                param: data,
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 14: { sorter: false} }
            });
        },

        DeleteMultipleFlightMapping: function(ids, event) {
            FlightManage.DeleteFlightMappingByID(ids, event);
        },
        DeleteFlightMapping: function(tblID, argus) {
            switch (tblID) {
                case "gdvFlightMapping":
                    var properties = {
                        onComplete: function(e) {
                            FlightManage.DeleteFlightMappingByID(argus[0], e);
                        }
                    }
                    csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete this record?</p>", properties);
                    break;
                default:
                    break;
            }

        },
        DeleteFlightMappingByID: function(ids, event) {
            mappingID = ids;
            if (event) {
                var param = JSON2.stringify({ mappingID: mappingID, storeID: storeId, portalID: portalId, userName: userName, CultureName: cultureName });
                this.config.method = "DeleteFlightMapping";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = param;
                this.config.ajaxCallMode = FlightManage.DeleteFlightMappingSuccess;
                this.config.error = FlightManage.DeleteFlightMappingFailure;
                this.ajaxCall(this.config);

            }
        },

        DeleteFlightMappingSuccess: function() {
            FlightManage.BindMappingListInGrid(null, null, storeId, portalId, cultureName);
            csscody.info("<h2>Sucessful Message</h2><p>Record has been deleted successfully.</p>");
        },
        DeleteFlightMappingFailure: function() {
            csscody.error("<h2>Error Message</h2><p>Failed to delete record</p>");

        },
        GetFlightDomesticPlaces: function() {
            var param = JSON2.stringify({ storeID: storeId, portalID: portalId, CultureName: cultureName });
            this.config.method = "GetDomesticFlightLocation";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.async = false;
            this.config.data = param;
            this.config.ajaxCallMode = FlightManage.FlightDomesticPlacesSucess;
            this.ajaxCall(this.config);
        },
        FlightDomesticPlacesSucess: function(data) {
            if (data.d.length > 0) {
                $("#ddlFrom").html('');
                $("#ddlTo").html('');
                element = '';
                var ddlelement = "<option value='0'>Select Location</option>";
                $.each(data.d, function(index, value) {
                    element += "<option text='" + value.Name + "' value='" + value.DomesticPlacesID + "'>" + value.Name + "</option>";

                });
                ddlelement += element;
                $("#ddlFrom").append(ddlelement);
                $("#ddlTo").append(ddlelement);
            }
        },
        GetDomesticPlacesToIDbyFromIDForReservation: function(DomesticPlacesFromID) {
            var param = JSON2.stringify({ storeID: storeId, portalID: portalId, CultureName: cultureName, DomesticPlacesFromID: DomesticPlacesFromID });
            this.config.method = "GetDomesticPlacesToIDbyFromID";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.async = false;
            this.config.data = param;
            this.config.ajaxCallMode = FlightManage.GetDomesticPlacesToIDbyFromIDForReservationSucess;
            this.ajaxCall(this.config);
        },
        GetDomesticPlacesToIDbyFromIDForReservationSucess: function(data) {
            var element = ''
            element = "<option value=''>Please Select a city</option>";
            if (data.d.length > 0) {
                $.each(data.d, function(index, value) {
                    element += "<option value='" + value.DomesticPlacesID + "'>" + value.Name + "</option>";
                });
            }
            $('#ddlResPlaceTo')[0].options.length = 0;
            $('#ddlResPlaceTo').append(element);
        },
        SearchMapping: function() {
            var ddlFrom = '';
            var ddlTo = '';
            if ($("#ddlFrom").val() == '0') {
                ddlFrom = null;
            }
            if ($("#ddlTo").val() == '0') {
                ddlTo = null;
            }
            if ($("#ddlFrom").val() != '0') {
                ddlFrom = $("#ddlFrom").val();

            }
            if ($("#ddlTo").val() != '0') {
                ddlTo = $("#ddlTo").val();
            }
            FlightManage.BindMappingListInGrid(ddlFrom, ddlTo, storeId, portalId, cultureName);
        },

        InsertMapping: function(fromId, ToId) {
            var param = JSON2.stringify({ fromId: fromId, ToId: ToId, storeID: storeId, portalID: portalId, userName: userName, CultureName: cultureName });
            this.config.method = "InsertFlightMapping";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.ajaxCallMode = FlightManage.InsertMappingSuccess;
            this.ajaxCall(this.config);
        },

        InsertMappingSuccess: function(data) {
            if (data.d == true) {
                csscody.info("<h2>Sucessful Message</h2><p>Mapping has been Inserted successfully.</p>");
                $("#divAddMapping").hide();
                $("#divFlightManage").show();
                FlightManage.BindMappingListInGrid(null, null, storeId, portalId, cultureName);
                $("#ddlLocationFrom").val('0');
                $("#ddlLocationTo").val('0');
            }
            else {
                csscody.info("<h2>Mapping Exists</h2><p>Please select another mapping.</p>");
            }
        },

        BindFlightReservationListInGrid: function(from, to, fname, lname, storeId, portalId, culture) {
            this.config.method = "GetFlightReservationList";
            this.config.data = { From: from, To: to, FirstName: fname, LastName: lname, storeID: storeId, portalID: portalId, CultureName: culture };
            var data = this.config.data;
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#tblFlightReservation_pagesize").length > 0) ? $("#tblFlightReservation_pagesize :selected").text() : 10;
            $("#tblFlightReservation").sagegrid({
                url: this.config.baseURL,
                functionMethod: this.config.method,
                colModel: [
                    { display: 'ReservationID', name: 'ReservationID', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'ReservationChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox' },
                    { display: 'Name', name: 'Name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Flight Type', name: 'FlightTypeName', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Trip Type', name: '_tripName', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'From To', name: '_fromTo', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Phone', name: '_phone', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Mobile Number', name: '_mobileNumber', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Depart', name: '_depart', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Return', name: '_return', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Customers', name: '_customer', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Flight Class', name: '_flightClass', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Nationality', name: '_nationality', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Other Traveller', name: '_nameOfOtherTraveller', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: 'true' },
                    { display: 'Additional Info', name: '_additionalInfo', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: 'true' },
                    { display: 'Email', name: '_email', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: 'true' },
                    { display: 'Actions', name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                ],
                buttons: [
                 { display: 'Edit', name: 'edit', enable: true, _event: 'click', trigger: '3', callMethod: 'FlightManage.EditFlightReservation', arguments: '1' },
                 { display: 'Delete', name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'FlightManage.DeleteFlightReservation', arguments: '1' },
                  { display: 'Email', name: 'email', enable: true, _event: 'click', trigger: '1', callMethod: 'FlightManage.SendEmail', arguments: '1,2,3,4,5,6,7,8,9,10,11,12,13,14,15' }

                    ],
                rp: perpage,
                nomsg: "No Records Found!",
                param: data,
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 15: { sorter: false} }
            });
        },
        DeleteMultipleFlightReservation: function(ids, event) {
            FlightManage.DeleteFlightReservationByID(ids, event);
        },
        DeleteFlightReservation: function(tblID, argus) {
            switch (tblID) {
                case "tblFlightReservation":
                    var properties = {
                        onComplete: function(e) {
                            FlightManage.DeleteFlightReservationByID(argus[0], e);
                        }
                    }
                    csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete this record?</p>", properties);
                    break;
                default:
                    break;
            }
        },
        DeleteFlightReservationByID: function(ids, event) {
            ReservationID = ids;
            if (event) {
                var param = JSON2.stringify({ ReservationID: ReservationID, storeID: storeId, portalID: portalId, userName: userName, CultureName: cultureName });
                this.config.method = "DeleteFlightReservation";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = param;
                this.config.ajaxCallMode = FlightManage.DeleteFlightReservationSuccess;
                this.config.error = FlightManage.DeleteFlightReservationFailure;
                this.ajaxCall(this.config);
            }
        },

        DeleteFlightReservationSuccess: function() {
            FlightManage.BindFlightReservationListInGrid(null, null, null, null, storeId, portalId, cultureName);
            csscody.info("<h2>Sucessful Message</h2><p>Record has been deleted successfully.</p>");
        },
        DeleteFlightReservationFailure: function() {
            csscody.error("<h2>Error Message</h2><p>Failed to delete record</p>");
        },
        DeleteSingleFlightReservationByID: function(ReservationID) {
            var param = JSON2.stringify({ ReservationID: ReservationID, storeID: storeId, portalID: portalId, userName: userName, CultureName: cultureName });
            this.config.method = "DeleteFlightReservation";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.ajaxCallMode = FlightManage.DeleteSingleFlightReservationByIDSuccess;
            this.config.error = FlightManage.DeleteFlightReservationFailure;
            this.ajaxCall(this.config);
        },

        DeleteSingleFlightReservationByIDSuccess: function() {
            var properties = {
                onComplete: function(e) {
                    $("#divFlightEdit").hide();
                    $("#divFlightReservation").show();
                    FlightManage.BindFlightReservationListInGrid(null, null, null, null, storeId, portalId, cultureName);
                    csscody.info("<h2>Sucessful Message</h2><p>Record has been deleted successfully.</p>");
                }
            }
            csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete this record?</p>", properties);
        },

        SendEmail: function(tblID, argus) {
            var reservationId = argus[0];
            var name = argus[3];
            var fType = argus[4];
            var tType = argus[5];
            var fromTo = argus[6];
            var phone = argus[7];
            var mobNum = argus[8];
            var depart = argus[9];
            var retrn = argus[10];
            var custmoer = argus[11];
            var fClass = argus[12];
            var nation = argus[13];
            var otherTra = argus[14];
            var addInfo = argus[15];
            var email = argus[16];
            switch (tblID) {
                case "tblFlightReservation":
                    var properties = {
                        onComplete: function(e) {
                            FlightManage.SendEmailToUser(reservationId, name, fType, tType, fromTo, phone, mobNum, depart, retrn, custmoer, fClass, nation, otherTra, addInfo, email, e);
                        }
                    }
                    csscody.confirm("<h2>Email Confirmation</h2><p>Are you sure you want to send reservation mail?</p>", properties);
                    break;
                default:
                    break;
            }
        },

        SendEmailToUser: function(reservationId, name, fType, tType, fromTo, phone, mobNum, depart, retrn, custmoer, fClass, nation, otherTra, addInfo, email, event) {
            var subject = fType;
            var msgDetail = '';
            msgDetail += "<div class='msg'>";
            msgDetail += "<table width='100%' cellspacing='4' cellpadding='0' border='0' align='center'><tbody><tr><td bgcolor='#E8E6E2'> </td></tr>";
            msgDetail += " <tr> <td valign='top' align='left' style='border: 1px solid #488f47; padding: 2px'> <table width='100%' cellspacing='2' cellpadding='2'> <tbody> <tr> <td colspan='2'></td> </tr>";
            msgDetail += "<tr><td>Flight Route :</td><td>" + fromTo + "</td> </tr>";
            msgDetail += "<tr><td> Departure Date :</td><td>" + depart + "</td></tr>";
            msgDetail += "<tr><td> Return Date :</td><td>" + retrn + "</td> </tr>";
            msgDetail += "<tr><td> Number of travellers(Adult / Child / Infant) :</td><td>" + custmoer + "</td> </tr>";
            msgDetail += "<tr><td> Full Name :</td><td>" + name + "</td> </tr>";
            msgDetail += "<tr><td> Contact no.</td><td>" + phone + "</td> </tr>";
            msgDetail += "<tr><td> Mobile no.</td><td>" + mobNum + "</td> </tr>";
            msgDetail += "<tr><td> Email.</td><td><a  href='mailto:" + email + "'>" + email + "</a></td> </tr>";
            msgDetail += "<tr><td>  Additional Info:</td><td>" + addInfo + "</td> </tr>";
            msgDetail += "</tbody></table></div>";
            if (event) {
                var param = JSON2.stringify({ subject: subject, msgDetail: msgDetail, email: email, aspxCommonObj: aspxCommonObj, userName: name });
                this.config.method = "SendEmailForFlightReservation";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = param;
                this.config.ajaxCallMode = FlightManage.EmailSucess;
                this.config.error = FlightManage.EmailFailure;
                this.ajaxCall(this.config);
            }
        },
        EmailSucess: function() {
            csscody.info("<h2>Sucessful Message</h2><p>Email has been sent successfully.</p>");
        },
        EmailFailure: function() {
            csscody.error("<h2>Error</h2><p>Unable to sent mail.</p>");
        },
        EditFlightReservation: function(tblID, argus) {
            $("#divFlightReservation").hide();
            $("#divFlightEdit").show();
            FlightManage.GetReservationDetailByID(argus[0]);
        },
        ClearData: function() {
            $("#ddlResPlaceFrom").val('');
            $("#ddlResPlaceTo").val('');
            $("#txtResDepart").val('');
            $("#txtResReturn").val('');
            $("#ddlResAdult").val(1);
            $("#ddlResChild").val(0);
            $("#ddlResInfant").val(0);
            $("#txtFirstName").val('');
            $("#txtMiddleName").val('');
            $("#txtLastName").val('');
            $("#txtTraveller").val('');
            $("#txtPhone").val('');
            $("#txtMobile").val('');
            $("#txtEmail").val('');
            $("#txtAdditionalInfo").val('');
            $("#txtResReturn").removeAttr('disabled');
        },

        GetReservationDetailByID: function(reservationID) {
            var param = JSON2.stringify({ reservationID: reservationID, storeID: storeId, portalID: portalId, CultureName: cultureName });
            this.config.method = "GetFlightReservationDetailByID";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.ajaxCallMode = FlightManage.GetReservationDetailByIDSuccess;
            this.ajaxCall(this.config);
        },

        GetReservationDetailByIDSuccess: function(data) {
            $.each(data.d, function(index, value) {
                $("#txtReservationId").val(value.ReservationID);
                $("#flightType").find("input:radio[name=flightType][value='" + value.FlightTypeID + "']").attr('checked', true);
                $("#flightType").find("input:radio[name=flightType]").attr('disabled', true);
                $("#ResNationality").find("input:radio[name=nation][value='" + value.NationalityID + "']").attr('checked', true);
                $("#ResNationality").find("input:radio[name=nation]").attr('disabled', true);
                if (value.FlightTypeID == 1) {
                    $("#txtResFromLocation").hide();
                    $("#txtResToLocation").hide();
                    $("#ddlResPlaceFrom").show();
                    $("#ddlResPlaceTo").show();
                    $("#ddlResPlaceFrom option:contains(" + value.From + ")").attr('selected', 'selected');
                    FlightManage.GetDomesticPlacesToIDbyFromIDForReservation($("#ddlResPlaceFrom option:selected").val());
                    $("#ddlResPlaceTo option:contains(" + value.To + ")").attr('selected', 'selected');
                }
                else if (value.FlightTypeID == 2) {
                    $("#ddlResPlaceFrom").hide();
                    $("#ddlResPlaceTo").hide();
                    $("#txtResFromLocation").show();
                    $("#txtResToLocation").show();
                    $("#txtResFromLocation").val(value.From);
                    $("#txtResToLocation").val(value.To);
                }
                $("#tripType").find("input:radio[name=flightTripType][value='" + value.TripTypeID + "']").attr('checked', true);
                if (value.TripTypeID == 2) {
                    $("#txtResReturn").val('');
                    $("#txtResReturn").attr('disabled', true);
                    $("#txtResReturn").parents('td').prev('td').find('.cssClassRequired').hide();
                }
                else {
                    $("#txtResReturn").parents('td').prev('td').find('.cssClassRequired').show();
                }
                $("#txtResDepart").datepicker({
                    defaultDate: "+1w",
                    changeMonth: true,
                    numberOfMonths: 1,
                    onSelect: function(selectedDate) {
                        $("#txtReturnTime").datepicker("option", "minDate", selectedDate);
                    }
                });
                $("#txtResReturn").datepicker({
                    defaultDate: "+1w",
                    changeMonth: true,
                    numberOfMonths: 1,
                    onSelect: function(selectedDate) {
                        //$("#txtDepartTime").datepicker("option", "maxDate", selectedDate);
                    }
                });
                FlightManage.GetFlightClass(value.FlightTypeID);
                $("#txtResDepart").val(value.Depart);
                $("#txtResReturn").val(value.Return);
                $("#ddlResAdult").val(value.Adult);
                $("#ddlResChild").val(value.Child);
                $("#ddlResInfant").val(value.Infant);
                $("#txtFirstName").val(value.FirstName);
                $("#txtMiddleName").val(value.MiddleName);
                $("#txtLastName").val(value.LastName);
                $("#txtTraveller").val(value.NameOfOtherTraveller);
                $("#txtPhone").val(value.Phone);
                $("#txtMobile").val(value.MobileNumber);
                $("#txtEmail").val(value.Email);
                $("#txtAdditionalInfo").val(value.AdditionalInfo);
                $("#ddlFlightClass").val(value.ClassID);
                $("#txtResReturn").datepicker("option", "minDate", value.Depart);
            });
        },
        InsertDomesticLocation: function(location) {
            var param = JSON2.stringify({ Location: location, storeID: storeId, portalID: portalId, userName: userName, CultureName: cultureName });
            this.config.method = "InsertDomesticLocation";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.ajaxCallMode = FlightManage.InsertDomesticLocationSuccess;
            this.ajaxCall(this.config);

        },
        InsertDomesticLocationSuccess: function(data) {
            if (data.d == true) {
                csscody.info("<h2>Sucessful Message</h2><p>Location has been Inserted successfully.</p>");
                $("#divLocation").hide();
                $("#divFlightReservation").show();
                $("#txtLocation").val('');
                FlightManage.GetFlightDomesticPlaces();
                FlightManage.BindMappingListInGrid(null, null, storeId, portalId, cultureName);
            }
            else {
                csscody.info("<h2>Mapping Exists</h2><p>Please select another location.</p>");
            }
        },

        GetFlightType: function() {
            var param = JSON2.stringify({ storeID: storeId, portalID: portalId, CultureName: cultureName });
            this.config.method = "GetFlightType";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.ajaxCallMode = FlightManage.GetFlightTypeSuccess;
            this.ajaxCall(this.config);
        },

        GetFlightTypeSuccess: function(data) {
            $("#flightType").html('');
            if (data.d.length > 0) {
                var element = '';
                $.each(data.d, function(index, value) {
                    element += "<li><input type='radio' id='" + value.FlightTypeName + "' name='flightType' value='" + value.FlightTypeID + "'/>";
                    element += "<span>" + value.FlightTypeName + "</span></li>"
                });
                $("#flightType").append(element);
                $("#flightType").find("input:radio[name=flightType][disabled=false]:first").attr('checked', true);
            }

        },
        GetFlightTripType: function() {
            var param = JSON2.stringify({ storeID: storeId, portalID: portalId, CultureName: cultureName });
            this.config.method = "GetFlightTripType";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.ajaxCallMode = FlightManage.GetFlightTripTypeSuccess;
            this.ajaxCall(this.config);
        },
        GetFlightTripTypeSuccess: function(data) {
            $("#tripType").html('');
            if (data.d.length > 0) {
                var element = '';
                triptypeElement = '';
                $.each(data.d, function(index, value) {
                    element += "<li><input type='radio' id='" + value.TripName + "' name='flightTripType' value='" + value.FlightTripTypeID + "'/>";
                    element += "<span>" + value.TripName + "</span></li>"
                });
                triptypeElement = element;
                $("#tripType").append(element);
            }

        },
        GetFlightClass: function(FlightTypeID) {
            var param = JSON2.stringify({ storeID: storeId, portalID: portalId, CultureName: cultureName, FlightTypeID: FlightTypeID });
            this.config.method = "GetFlightClass";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.async = false;
            this.config.data = param;
            this.config.ajaxCallMode = FlightManage.GetFlightClassSucess;
            this.ajaxCall(this.config);
        },
        GetFlightClassSucess: function(data) {
            if (data.d.length > 0) {
                var element = ''
                $("#ddlFlightClass").html('');
                $.each(data.d, function(index, value) {
                    element += "<option value='" + value.FlightClassID + "'>" + value.FlightClass + "</option>";
                });
                $("#ddlFlightClass").append(element);
            }
        },
        GetNationality: function() {
            var param = JSON2.stringify({ storeID: storeId, portalID: portalId, CultureName: cultureName });
            this.config.method = "GetNationality";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.ajaxCallMode = FlightManage.GetNationalitySuccess;
            this.ajaxCall(this.config);
        },
        GetNationalitySuccess: function(data) {
            $("#ResNationality").html('');
            if (data.d.length > 0) {
                var element = '';
                $.each(data.d, function(index, value) {
                    element += "<li><input type='radio' id='" + value.Nationality + "' name='nation' value='" + value.NationalityID + "'/>";
                    element += "<span>" + value.Nationality + "</span></li>"
                });
                $("#ResNationality").append(element);
                $("#ResNationality").find("input:radio[name=nation][disabled=false]:first").attr('checked', true);

            }

        },
        UpdateFlightReservation: function(reservationID, flightTypId, tripType, from, to, nationalityId, flightClassId, departTime, returnTime, adult, child, infant, firstName, middleName, lastName, otherTravel, phone, mobileNum, email, addInfo, msgDetail, subject) {
            var param = JSON2.stringify({ reservationID: reservationID, flightTypId: flightTypId, tripType: tripType, from: from, to: to, departTime: departTime, returnTime: returnTime, adult: adult, child: child, infant: infant, flightClassId: flightClassId, nationalityId: nationalityId, fname: firstName, mname: middleName, lname: lastName, otherTravel: otherTravel, phone: phone, mobNum: mobileNum, email: email, addInfo: addInfo, storeID: storeId, portalID: portalId, CultureName: cultureName, userName: userName });
            this.config.method = "UpdateFlightReservation";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.ajaxCallMode = FlightManage.UpdateFlightReservationSuccess;
            this.ajaxCall(this.config);
        },
        UpdateFlightReservationSuccess: function() {
            csscody.info("<h2>Sucessful Message</h2><p>Flight has been updated successfully.</p>");
            $("#divFlightEdit").hide();
            $("#divFlightReservation").show();
            FlightManage.BindFlightReservationListInGrid(null, null, null, null, storeId, portalId, cultureName);
            FlightManage.ClearData();
        },

        Init: function() {
            FlightManage.BindMappingListInGrid(null, null, storeId, portalId, cultureName);
            FlightManage.GetFlightDomesticPlaces();
            FlightManage.BindFlightReservationListInGrid(null, null, null, null, storeId, portalId, cultureName);
            FlightManage.GetFlightType();
            FlightManage.GetFlightTripType();
            FlightManage.GetNationality();
            $("#ddlResPlaceFrom").append('<option value="">Please Select a city</option>');
            $("#ddlResPlaceFrom").append(element);
            $("#btnAddMap").bind('click', function() {
                $("#divFlightManage").hide();
                $("#divAddMapping").show();
                $("#ddlLocationFrom").append(element);
                $("#ddlLocationTo").append(element);
                element = '';
            });

            $("#btnCancel").bind('click', function() {
                $("#divAddMapping").hide();
                $("#divFlightManage").show();
                $("#ddlLocationFrom").val('0');
                $("#ddlLocationTo").val('0');
            });

            $("#btnShow").bind('click', function() {
                FlightManage.BindMappingListInGrid(null, null, storeId, portalId, cultureName);
            });

            $("#btnSearch").bind('click', function() {
                FlightManage.SearchMapping();

            });
            $("#btnReservationSearch").bind('click', function() {
                var from = $("#txtReservationFrom").val() == '' ? null : $("#txtReservationFrom").val();
                var to = $("#txtReservationTo").val() == '' ? null : $("#txtReservationTo").val();
                var fname = $("#txtFname").val() == '' ? null : $("#txtFname").val();
                var lname = $("#txtLname").val() == '' ? null : $("#txtLname").val();
                FlightManage.BindFlightReservationListInGrid(from, to, fname, lname, storeId, portalId, cultureName);

            });

            $("#btnDeleteSelected").click(function() {
                var mappingID = '';
                $(".flightChkbox").each(function(i) {
                    if ($(this).attr("checked")) {
                        mappingID += $(this).val() + ',';
                    }
                });
                if (mappingID == "") {
                    csscody.alert('<h2>Information Alert</h2><p>None of the data are selected</p>');
                    return false;
                }
                var properties = {
                    onComplete: function(e) {
                        FlightManage.DeleteMultipleFlightMapping(mappingID, e);
                    }
                };
                csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete records?</p>", properties);
            });

            $("#btnDeleteReservatin").click(function() {
                var reservationID = '';
                $(".ReservationChkbox").each(function(i) {
                    if ($(this).attr("checked")) {
                        reservationID += $(this).val() + ',';
                    }
                });
                if (reservationID == "") {
                    csscody.alert('<h2>Information Alert</h2><p>None of the data are selected</p>');
                    return false;
                }
                var properties = {
                    onComplete: function(e) {
                        FlightManage.DeleteMultipleFlightReservation(reservationID, e);
                    }
                };
                csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete records?</p>", properties);
            });
            $("#btnShowReservation").bind('click', function() {
                $("#txtReservationFrom").val('');
                $("#txtReservationTo").val('');
                FlightManage.BindFlightReservationListInGrid(null, null, null, null, storeId, portalId, cultureName);

            });
            $("#btnSave").bind('click', function() {
                var ddlFrom = $("#ddlLocationFrom").val();
                var ddlTo = $("#ddlLocationTo").val();
                if (ddlFrom == '0' || ddlTo == '0') {
                    csscody.info("<h2>Information Alert</h2><p>Please select available location.</p>");
                    return false;
                }
                if (ddlFrom == ddlTo) {
                    csscody.info("<h2>Information Alert</h2><p>Please select different location.</p>");
                    return false;
                }
                FlightManage.InsertMapping(ddlFrom, ddlTo);
            });
            $("#btnSingleDelete").bind('click', function() {
                var ReservationID = $("#txtReservationId").val();
                FlightManage.DeleteSingleFlightReservationByID(ReservationID);
            });
            $("#btnBack").bind('click', function() {
                $("#divFlightEdit").hide();
                $("#divFlightReservation").show();
                FlightManage.ClearData();
            });
            $("#btnShowMap").bind('click', function() {
                $("#divFlightReservation").hide();
                $("#divFlightManage").show();
            });
            $("#btnBackToFlight").bind('click', function() {
                $("#divFlightManage").hide();
                $("#divFlightReservation").show();
            });

            $("#tripType").find("input:radio[name=flightTripType]").live('change', function() {
                var val = $(this).val();
                if (val == "2") {
                    $("#txtResReturn").val('');
                    $("#txtResReturn").parents('td').prev('td').find('.cssClassRequired').hide();
                    $("#txtResReturn").attr('disabled', 'disabled');
                    $('#txtResReturn').next('label').hide();
                }
                else {
                    $("#txtResReturn").removeAttr('disabled');
                    $("#txtResReturn").parents('td').prev('td').find('.cssClassRequired').show();

                }

            });
            $("#ddlResPlaceFrom").live('change', function() {
                var ids = '';
                if ($(this).val() == "") {
                    ids = '0';
                }
                else {

                    ids = $(this).val();
                }
                FlightManage.GetDomesticPlacesToIDbyFromIDForReservation(ids);

            });
            $("#txtResDepart").datepicker({
                //defaultDate: "+1w",
                changeMonth: true,
                numberOfMonths: 1,
                onSelect: function(selectedDate) {
                    $("#txtResReturn").datepicker("option", "minDate", selectedDate);
                }
            });
            $("#txtResReturn").datepicker({
                //defaultDate: "+1w",
                changeMonth: true,
                numberOfMonths: 1,
                onSelect: function(selectedDate) {
                    //$("#txtResDepart").datepicker("option", "maxDate", selectedDate);
                }
            });
            var documentForm = document.forms[0].name;
            var v = $("#" + documentForm).validate({
                rules: {
                    fname: "required",
                    lname: "required",
                    email: {
                        required: true,
                        email: true
                    },
                    mobile: {
                        required: true,
                        number: true
                    },
                    from: "required",
                    to: "required",
                    ResdepartTime: "required",
                    ResReturnTime: "required",
                    resFrom: "required",
                    resTo: "required"

                },
                messages: {
                    fname: {
                        required: '*'
                    },
                    lname: {
                        required: '*'
                    },
                    email: {
                        required: '*',
                        email: '*'
                    },
                    mobile: {
                        required: '*',
                        number: '*'
                    },
                    from: "*",
                    to: "*",
                    ResdepartTime: "*",
                    ResReturnTime: "*",
                    resFrom: "*",
                    resTo: "*"
                },
                ignore: ':hidden'

            });
            $("#btnAddLocation").bind('click', function() {
                $("#divFlightReservation").hide();
                $("#divLocation").show();


            });
            $("#btnCancelLocation").bind('click', function() {

                $("#divLocation").hide();
                $("#divFlightReservation").show();

            });
            $("#btnSaveLocation").bind('click', function() {
                var location = $("#txtLocation").val();
                if (location == "") {
                    csscody.info("<h2>Information Alert</h2><p>Please enter location.</p>");
                    return false;
                }
                FlightManage.InsertDomesticLocation(location);
            });
            $('input.cssIntegerOnly').bind('keypress', function(e) {
                return (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) ? false : true;
            });
            $("#btnUpdate").live('click', function() {

                var flightTypId = $("#flightType").find("input:radio[name=flightType]:checked").val();
                var nationalityId = $("#ResNationality").find("input:radio[name=nation]:checked").val();
                var from = '';
                var to = '';
                if (flightTypId == '1') {

                    from = $("#ddlResPlaceFrom option:selected").text();
                    to = $("#ddlResPlaceTo option:selected").text();
                }
                else {

                    from = $("#txtResFromLocation").val();
                    to = $("#txtResToLocation").val();
                }
                var flightClassId = $("#ddlFlightClass").val();
                var tripType = $("#tripType").find("input:radio[name=flightTripType]:checked").val();
                if (tripType == '1') {
                    $("#txtResReturn").removeClass("required");

                }
                else {
                    $("#txtResReturn").addClass("required");

                }
                var departTime = $("#txtResDepart").val();
                var returnTime = $("#txtResReturn").val();
                var adult = $("#ddlResAdult").val();
                var child = $("#ddlResChild").val();
                var infant = $("#ddlResInfant").val();
                var firstName = $("#txtFirstName").val();
                var middleName = $("#txtMiddleName").val();
                var lastName = $("#txtLastName").val();
                var otherTravel = $("#txtTraveller").val();
                var phone = $("#txtPhone").val();
                var mobileNum = $("#txtMobile").val();
                var email = $("#txtEmail").val();
                var addInfo = $("#txtAdditionalInfo").val();
                var resId = $("#txtReservationId").val();
                if (v.form()) {
                    FlightManage.UpdateFlightReservation(resId, flightTypId, tripType, from, to, nationalityId, flightClassId, departTime, returnTime, adult, child, infant, firstName, middleName, lastName, otherTravel, phone, mobileNum, email, addInfo);
                    return false;
                }
                else {
                    return false;
                }

            });

        }
    };
    FlightManage.Init();
});

  