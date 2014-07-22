var FlightBookingEntry = "";
$.metadata.setType("attr", "validate");
$(function() {
    var storeId = AspxCommerce.utils.GetStoreID();
    var portalId = AspxCommerce.utils.GetPortalID();
    var userName = AspxCommerce.utils.GetUserName();
    var cultureName = AspxCommerce.utils.GetCultureName();
    var domesticElement = '';
    var domesticMapElement = '';
    var flightTypId = '';
    var flightClassId = '';
    FlightBookingEntry = {
        config: {
            isPostBack: false,
            async: true,
            cache: false,
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
                type: FlightBookingEntry.config.type,
                contentType: FlightBookingEntry.config.contentType,
                cache: FlightBookingEntry.config.cache,
                async: FlightBookingEntry.config.async,
                url: FlightBookingEntry.config.url,
                data: FlightBookingEntry.config.data,
                dataType: FlightBookingEntry.config.dataType,
                success: FlightBookingEntry.config.ajaxCallMode,
                error: FlightBookingEntry.error
            });
        },
        GetFlightType: function() {
            var param = JSON2.stringify({ storeID: storeId, portalID: portalId, CultureName: cultureName });
            this.config.method = "GetFlightType";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.ajaxCallMode = FlightBookingEntry.GetFlightTypeSuccess;
            this.ajaxCall(this.config);
        },
        GetFlightTypeSuccess: function(data) {
            $("#flightType").html('');
            if (data.d.length > 0) {
                var element = '';
                $.each(data.d, function(index, value) {
                    element += "<li><input type='radio' id='" + value.FlightTypeName + "' name='flightType' value='" + value.FlightTypeID + "'/>";
                    element += "<span>" + value.FlightTypeName + "</span></li>";
                });
                $("#flightType").append(element);
                $("#flightType").find("input:radio[name=flightType][disabled=false]:first").attr('checked', true);
            }

        },
        GetFlightClass: function(FlightTypeID) {
            var param = JSON2.stringify({ storeID: storeId, portalID: portalId, CultureName: cultureName, FlightTypeID: FlightTypeID });
            this.config.method = "GetFlightClass";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.ajaxCallMode = FlightBookingEntry.GetFlightClassSucess;
            this.ajaxCall(this.config);
        },
        GetFlightClassSucess: function(data) {
            if (data.d.length > 0) {
                var element = '';
                $("#ddlFlightClass").html('');
                $.each(data.d, function(index, value) {

                    element += "<option value='" + value.FlightClassID + "'>" + value.FlightClass + "</option>";

                });
                $("#ddlFlightClass").append(element);
            }

        },
        GetFlightTripTypeForReservation: function() {
            var param = JSON2.stringify({ storeID: storeId, portalID: portalId, CultureName: cultureName });
            this.config.method = "GetFlightTripType";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.async = false;
            this.config.ajaxCallMode = FlightBookingEntry.GetFlightTripTypeForReservationSuccess;
            this.ajaxCall(this.config);
        },
        GetFlightTripTypeForReservationSuccess: function(data) {
            $("#ResTripType").html('');
            if (data.d.length > 0) {
                var element = '';
                $.each(data.d, function(index, value) {
                    element += "<li><input type='radio' id='" + value.TripName + "' name='flightResTripType' value='" + value.FlightTripTypeID + "'/>";
                    element += "<span>" + value.TripName + "</span></li>";
                });
                $("#ResTripType").append(element);
                $("#ResTripType").find("input:radio[name=flightResTripType][disabled=false]:first").attr('checked', true);
            }

        },
        GetFlightDomesticPlaces: function() {
            var param = JSON2.stringify({ storeID: storeId, portalID: portalId, CultureName: cultureName });
            this.config.method = "GetDomesticFlightLocation";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.async = false;
            this.config.data = param;
            this.config.ajaxCallMode = FlightBookingEntry.FlightDomesticPlacesSucess;
            this.ajaxCall(this.config);
        },
        FlightDomesticPlacesSucess: function(data) {
            $("#ddlResPlaceFrom").html('');
            var element = '';
            element = "<option value=''>Please Select a city</option>";
            if (data.d.length > 0) {
                $.each(data.d, function(index, value) {
                    element += "<option value='" + value.DomesticPlacesID + "'>" + value.Name + "</option>";
                });
            }
            domesticElement = element;
            $("#ddlResPlaceFrom").append(element);
        },
        GetDomesticPlacesToIDbyFromIDForReservation: function(DomesticPlacesFromID) {
            var param = JSON2.stringify({ storeID: storeId, portalID: portalId, CultureName: cultureName, DomesticPlacesFromID: DomesticPlacesFromID });
            this.config.method = "GetDomesticPlacesToIDbyFromID";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.ajaxCallMode = FlightBookingEntry.GetDomesticPlacesToIDbyFromIDForReservationSucess;
            this.ajaxCall(this.config);
        },
        GetDomesticPlacesToIDbyFromIDForReservationSucess: function(data) {
            domesticMapElement = '';
            var element = '';
            $("#ddlResPlaceTo").html('');
            element = "<option value=''>Please Select a city</option>";
            if (data.d.length > 0) {
                $.each(data.d, function(index, value) {
                    element += "<option value='" + value.DomesticPlacesID + "'>" + value.Name + "</option>";
                });
            }
            $("#ddlResPlaceTo").append(element);
        },
        GetNationality: function() {
            var param = JSON2.stringify({ storeID: storeId, portalID: portalId, CultureName: cultureName });
            this.config.method = "GetNationality";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.ajaxCallMode = FlightBookingEntry.GetNationalitySuccess;
            this.ajaxCall(this.config);
        },
        GetNationalitySuccess: function(data) {
            $("#ResNationality").html('');
            if (data.d.length > 0) {
                var element = '';
                $.each(data.d, function(index, value) {
                    element += "<li><input type='radio' id='" + value.Nationality + "' name='nation' value='" + value.NationalityID + "'/>";
                    element += "<span>" + value.Nationality + "</span></li>";
                });
                $("#ResNationality").append(element);
                $("#ResNationality").find("input:radio[name=nation][disabled=false]:first").attr('checked', true);
            }

        },
        InsertFlightReservation: function(flightTypId, tripType, from, to, nationalityId, flightClassId, departTime, returnTime, adult, child, infant, firstName, middleName, lastName, otherTravel, phone, mobileNum, email, addInfo, msgDetail, subject) {
            var res = '0';
            var param = JSON2.stringify({ reservationID: res, flightTypId: flightTypId, tripType: tripType, from: from, to: to, departTime: departTime, returnTime: returnTime, adult: adult, child: child, infant: infant, flightClassId: flightClassId, nationalityId: nationalityId, fname: firstName, mname: middleName, lname: lastName, otherTravel: otherTravel, phone: phone, mobNum: mobileNum, email: email, addInfo: addInfo, aspxCommonObj: aspxCommonObj, msgDetail: msgDetail, subject: subject });
            this.config.method = "InsertFlightReservation";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.ajaxCallMode = FlightBookingEntry.InsertFlightReservationSuccess;
            this.ajaxCall(this.config);
        },
        InsertFlightReservationSuccess: function() {
            csscody.info("<h2>Sucessful Message</h2><p>Flight has been Reserved successfully.</p>");
            FlightBookingEntry.ClearData();
            FlightBookingEntry.GetFlightClass(1);
            $("#ddlResPlaceFrom").html('');
            $("#ddlResPlaceTo").html('');
            $("#txtResFromLocation").hide();
            $("#txtResToLocation").hide();
            $("#ddlResPlaceFrom").show();
            $("#ddlResPlaceTo").show();
            FlightBookingEntry.GetFlightDomesticPlaces();
            FlightBookingEntry.GetDomesticPlacesToIDbyFromIDForReservation('0');
        },
        ClearData: function() {
            $("#ResNationality").find("input:radio[name=nation][disabled=false]:first").attr('checked', true);
            $("#ResTripType").find("input:radio[name=flightResTripType]:first").attr('checked', true);
            $("#flightType").find("input:radio[name=flightType][disabled=false]:first").attr('checked', true);
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
            $("#txtResFromLocation").val('');
            $("#txtResToLocation").val('');
        },
        Init: function() {
            $("#txtResFromLocation").hide();
            $("#txtResToLocation").hide();
            $("#ddlResPlaceFrom").show();
            $("#ddlResPlaceTo").show();
            FlightBookingEntry.GetFlightType();
            FlightBookingEntry.GetFlightClass(1);
            FlightBookingEntry.GetNationality();
            FlightBookingEntry.GetFlightTripTypeForReservation();
            FlightBookingEntry.GetFlightDomesticPlaces();
            FlightBookingEntry.GetDomesticPlacesToIDbyFromIDForReservation('0');
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
            $("#flightType").find("input:radio[name=flightType]").live('change', function() {
                $("label[class=''error'']").hide();
                var val = $(this).val();
                if (val == "2") {
                    $("#txtResFromLocation").val('');
                    $("#txtResToLocation").val('');
                    $("#ddlResPlaceFrom").hide();
                    $("#ddlResPlaceTo").hide();
                    $("#txtResFromLocation").show();
                    $("#txtResToLocation").show();
                    FlightBookingEntry.GetFlightClass($(this).val());

                }
                else {
                    $("#ddlResPlaceFrom").html('');
                    $("#ddlResPlaceTo").html('');
                    $("#txtResFromLocation").hide();
                    $("#txtResToLocation").hide();
                    $("#ddlResPlaceFrom").show();
                    $("#ddlResPlaceTo").show();
                    FlightBookingEntry.GetFlightClass($(this).val());
                    FlightBookingEntry.GetFlightDomesticPlaces();
                    FlightBookingEntry.GetDomesticPlacesToIDbyFromIDForReservation('0');
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
                FlightBookingEntry.GetDomesticPlacesToIDbyFromIDForReservation(ids);

            });
            $("#ResTripType").find("input:radio[name=flightResTripType]").live('change', function() {
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
            flightTypId = $.cookies.get("flightTypId");
            var tripType = $.cookies.get("tripType");
            var from = $.cookies.get("from");
            var to = $.cookies.get("to");
            var departTime = $.cookies.get("departTime");
            var returnTime = $.cookies.get("returnTime");
            var adult = $.cookies.get("adult");
            var child = $.cookies.get("child");
            var infant = $.cookies.get("infant");
            flightClassId = $.cookies.get("flightClassId");
            if (flightTypId != null || tripType != null || from != null || to != null || departTime != null || returnTime != null || adult != null || child != null || infant != null || flightClassId != null) {
                $("#divFlightReservation").show();
                $("#flightType").find("input:radio[name=flightType][value='" + flightTypId + "']").attr('checked', true);
                if (flightTypId == "1") {
                    FlightBookingEntry.GetFlightClass(flightTypId);
                    if (from == "") {
                        from = '0';
                    }
                    FlightBookingEntry.GetDomesticPlacesToIDbyFromIDForReservation(from);
                    $("#txtResFromLocation").hide();
                    $("#txtResToLocation").hide();
                    $("#ddlResPlaceFrom").show();
                    $("#ddlResPlaceTo").show();
                    $("#ddlResPlaceFrom").val(from);
                    $("#ddlResPlaceTo").val(to);
                } else if (flightTypId = "2") {
                    FlightBookingEntry.GetFlightClass(flightTypId);
                    $("#ddlResPlaceFrom").hide();
                    $("#ddlResPlaceTo").hide();
                    $("#txtResFromLocation").show();
                    $("#txtResToLocation").show();
                    $("#txtResFromLocation").val(from);
                    $("#txtResToLocation").val(to);
                }
                $("#ResTripType").find("input:radio[name=flightResTripType][value='" + tripType + "']").attr('checked', true);
                $("#txtResDepart").val(departTime);
                $("#txtResReturn").val(returnTime);
                $("#ddlResAdult").val(adult);
                $("#ddlResChild").val(child);
                $("#ddlResInfant").val(infant);


                $("#txtResReturn").datepicker("option", "minDate", departTime);
                if ($("#ResTripType").find("input:radio[name=flightResTripType]:checked").val() == "2") {
                    $("#txtResReturn").attr('disabled', 'disabled');
                    $("#txtResReturn").parents('td').prev('td').find('.cssClassRequired').hide();
                } else {
                    $("#txtResReturn").parents('td').prev('td').find('.cssClassRequired').show();
                }
            }
            $('input.cssIntegerOnly').bind('keypress', function(e) {
                return (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) ? false : true;
            });
            $("#btnBack").live('click', function() {
                window.location.href = homePageUrl;

            });
            $("#btnSave").live('click', function() {
                $("label[class=''error'']").show();
                flightTypId = $("#flightType").find("input:radio[name=flightType]:checked").val();
                var nationalityId = $("#ResNationality").find("input:radio[name=nation]:checked").val();
                flightClassId = $("#ddlFlightClass").val();
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
                var tripType = $("#ResTripType").find("input:radio[name=flightResTripType]:checked").val();
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
                var subject = $("#flightType").find("input:radio[name=flightType]:checked").attr('id');
                var name = '';
                name += firstName + ' ';
                if (middleName != "") {
                    name += middleName + ' ';
                }
                name += lastName;
                var msgDetail = '';
                msgDetail += "<div class='msg'>";
                msgDetail += "<table width='100%' cellspacing='4' cellpadding='0' border='0' align='center'><tbody><tr><td bgcolor='#E8E6E2'></td></tr>";
                msgDetail += " <tr> <td valign='top' align='left' style='border: 1px solid #488f47; padding: 2px'> <table width='100%' cellspacing='2' cellpadding='2'> <tbody> <tr> <td colspan='2'></td> </tr>";
                msgDetail += "<tr><td>Flight Route :</td><td>" + from + " - " + to + "</td> </tr>";
                msgDetail += "<tr><td> Departure Date :</td><td>" + departTime + "</td></tr>";
                msgDetail += "<tr><td> Return Date :</td><td>" + returnTime + "</td> </tr>";
                msgDetail += "<tr><td> Number of travellers :</td><td>" + adult + " " + " Adults&nbsp;," + child + "&nbsp;Child&nbsp;," + infant + "&nbsp;Infant </td> </tr>";
                msgDetail += "<tr><td> Full Name :</td><td>" + name + "</td> </tr>";
                msgDetail += "<tr><td> Contact no.</td><td>" + phone + "</td> </tr>";
                msgDetail += "<tr><td> Mobile no.</td><td>" + mobileNum + "</td> </tr>";
                msgDetail += "<tr><td> Email.</td><td><a  href='mailto:" + email + "'>" + email + "</a></td> </tr>";
                msgDetail += "<tr><td>  Additional Info:</td><td>" + addInfo + "</td> </tr>";
                msgDetail += "</tbody></table></div>";
                if (v.form()) {
                    FlightBookingEntry.InsertFlightReservation(flightTypId, tripType, from, to, nationalityId, flightClassId, departTime, returnTime, adult, child, infant, firstName, middleName, lastName, otherTravel, phone, mobileNum, email, addInfo, msgDetail, subject);
                    return false;
                }
                else {
                    return false;
                }
            });
        }
    };
    FlightBookingEntry.Init();
});                         
                          