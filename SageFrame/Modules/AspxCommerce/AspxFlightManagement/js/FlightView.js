var FlightBooking = "";
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
    FlightBooking = {
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
                type: FlightBooking.config.type,
                contentType: FlightBooking.config.contentType,
                cache: FlightBooking.config.cache,
                async: FlightBooking.config.async,
                url: FlightBooking.config.url,
                data: FlightBooking.config.data,
                dataType: FlightBooking.config.dataType,
                success: FlightBooking.config.ajaxCallMode,
                error: FlightBooking.error
            });
        },
        GetFlightType: function() {
            var param = JSON2.stringify({ storeID: storeId, portalID: portalId, CultureName: cultureName });
            this.config.method = "GetFlightType";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.ajaxCallMode = FlightBooking.GetFlightTypeSuccess;
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
        GetFlightTripType: function() {
            var param = JSON2.stringify({ storeID: storeId, portalID: portalId, CultureName: cultureName });
            this.config.method = "GetFlightTripType";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.ajaxCallMode = FlightBooking.GetFlightTripTypeSuccess;
            this.ajaxCall(this.config);
        },
        GetFlightTripTypeSuccess: function(data) {
            $("#tripType").html('');
            if (data.d.length > 0) {
                var element = '';
                $.each(data.d, function(index, value) {
                    element += "<li><input type='radio' id='t_" + value.TripName + "' name='flightTripType' value='" + value.FlightTripTypeID + "'/>";
                    element += "<span>" + value.TripName + "</span></li>";
                });
                $("#tripType").append(element);
                $("#tripType").find("input:radio[name=flightTripType][disabled=false]:first").attr('checked', true);
            }
        },
        SetCookies: function(flightTypId, tripType, from, to, departTime, returnTime, adult, child, infant, flightClassId) {
            var date = new Date();
            date.setTime(date.getTime() + (60 * 1000));
            $.cookies.set("flightTypId", flightTypId, { expiresAt: date });
            $.cookies.set("tripType", tripType, { expiresAt: date });
            $.cookies.set("from", from, { expiresAt: date });
            $.cookies.set("to", to, { expiresAt: date });
            $.cookies.set("departTime", departTime, { expiresAt: date });
            $.cookies.set("returnTime", returnTime, { expiresAt: date });
            $.cookies.set("adult", adult, { expiresAt: date });
            $.cookies.set("child", child, { expiresAt: date });
            $.cookies.set("infant", infant, { expiresAt: date });
            $.cookies.set("flightClassId", flightClassId, { expiresAt: date });
        },
        GetFlightDomesticPlaces: function() {
            var param = JSON2.stringify({ storeID: storeId, portalID: portalId, CultureName: cultureName });
            this.config.method = "GetDomesticFlightLocation";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.async = false;
            this.config.data = param;
            this.config.ajaxCallMode = FlightBooking.FlightDomesticPlacesSucess;
            this.ajaxCall(this.config);
        },
        FlightDomesticPlacesSucess: function(data) {
            $("#divFromLocation").html('');
            var element = '';
            var ddl = "<select id='ddlPlaceFrom' class='cssClassDropDown'><option value=''>Please Select a city</option></select>";
            if (data.d.length > 0) {
                $.each(data.d, function(index, value) {
                    element += "<option value='" + value.DomesticPlacesID + "'>" + value.Name + "</option>";
                });
            }
            domesticElement = element;
            $("#divFromLocation").append(ddl);
            $("#ddlPlaceFrom").append(element);
        },
        GetDomesticPlacesToIDbyFromID: function(DomesticPlacesFromID) {
            var param = JSON2.stringify({ storeID: storeId, portalID: portalId, CultureName: cultureName, DomesticPlacesFromID: DomesticPlacesFromID });
            this.config.method = "GetDomesticPlacesToIDbyFromID";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.ajaxCallMode = FlightBooking.GetDomesticPlacesToIDbyFromIDSucess;
            this.ajaxCall(this.config);
        },
        GetDomesticPlacesToIDbyFromIDSucess: function(data) {
            domesticMapElement = '';
            var element = '';
            element = "<option value=''>Please Select a city</option>";
            if (data.d.length > 0) {
                $.each(data.d, function(index, value) {
                    element += "<option value='" + value.DomesticPlacesID + "'>" + value.Name + "</option>";
                });
            }
            domesticMapElement = element;
            $('#ddlPlaceTo')[0].options.length = 0;
            $('#ddlPlaceTo').append(element);
        },
        GetFlightClass: function(FlightTypeID) {
            var param = JSON2.stringify({ storeID: storeId, portalID: portalId, CultureName: cultureName, FlightTypeID: FlightTypeID });
            this.config.method = "GetFlightClass";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = param;
            this.config.ajaxCallMode = FlightBooking.GetFlightClassSucess;
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
        Init: function() {
            FlightBooking.GetFlightType();
            FlightBooking.GetFlightTripType();
            FlightBooking.GetFlightDomesticPlaces();
            $("#divToLocation").html('');
            $("#divToLocation").append("<select id='ddlPlaceTo' class='cssClassDropDown'></select>");
            FlightBooking.GetDomesticPlacesToIDbyFromID('0');
            FlightBooking.GetFlightClass(1);
            $("#flightType").find("input:radio[name=flightType]").live('change', function() {
                var val = $(this).val();
                if (val == "2") {
                    $("#divFromLocation").html('');
                    $("#divToLocation").html('');
                    $("#divFromLocation").append("<input type='text' id='txtFromLocation'/>");
                    $("#divToLocation").append("<input type='text' id='txtToLocation'/>");
                    FlightBooking.GetFlightClass($(this).val());

                }
                else {
                    $("#divFromLocation").html('');
                    $("#divToLocation").html('');
                    FlightBooking.GetFlightDomesticPlaces();
                    $("#divToLocation").append("<select id='ddlPlaceTo' class='cssClassDropDown' validate='required:true'></select>");
                    FlightBooking.GetDomesticPlacesToIDbyFromID('0');
                    FlightBooking.GetFlightClass($(this).val());
                }

            });
            $("#divFromLocation").find("#ddlPlaceFrom").live('change', function() {
                FlightBooking.GetDomesticPlacesToIDbyFromID($(this).val());
            });


            $("#txtDepartTime").datepicker({
                // defaultDate: "+1w",
                changeMonth: true,
                numberOfMonths: 1,
                onSelect: function(selectedDate) {
                    $("#txtReturnTime").datepicker("option", "minDate", selectedDate);
                }
            });
            $("#txtReturnTime").datepicker({
                //defaultDate: "+1w",
                changeMonth: true,
                numberOfMonths: 1,
                onSelect: function(selectedDate) {
                    //$("#txtDepartTime").datepicker("option", "maxDate", selectedDate);
                }
            });
            $("#tripType").find("input:radio[name=flightTripType]").live('change', function() {
                var val = $(this).val();
                if (val == "2") {
                    $("#txtReturnTime").val('');
                    $("#txtReturnTime").attr('disabled', 'disabled');

                }
                else {
                    $("#txtReturnTime").removeAttr('disabled');
                }

            });
            $("#btnSearch").live('click', function() {
                var flightType = $("#flightType").find("input:radio[name=flightType]:checked").val();
                flightTypId = flightType;
                var tripType = $("#tripType").find("input:radio[name=flightTripType]:checked").val();
                var from = '';
                var to = '';
                if (flightType == "1") {
                    from = $("#ddlPlaceFrom").val();
                    if (from == '0') {
                        from = "";
                    }
                    to = $("#ddlPlaceTo").val();
                }
                else if (flightType == "2") {
                    from = $("#txtFromLocation").val();
                    to = $("#txtToLocation").val();
                }
                var departTime = $("#txtDepartTime").val();
                var returnTime = $("#txtReturnTime").val();
                var adult = $("#ddlAdult").val();
                var child = $("#ddlChild").val();
                var infant = $("#ddlInfant").val();
                var flightClass = $("#ddlFlightClass").val();
                flightClassId = flightClass;
                FlightBooking.SetCookies(flightTypId, tripType, from, to, departTime, returnTime, adult, child, infant, flightClassId);
                window.location.href = AspxCommerce.utils.GetAspxRedirectPath() + 'Flight-Booking' + '.aspx';
            });

        }
    };
    FlightBooking.Init();
});




           
                          
               
                 
                        
                                 
                  
             
          
                               
                                    
                                   
                               
                          
                                   






        
            
                
                   
                       
               
          
           
               
                   
                      
                           
                               
                                  
                              
                           
                          
                                
                                  
                               
                                
                                   
                                
                           
                           
                            
                           
                           
                          
                        
                          
                            
                           
                             
                                   
                               
                            
                      
           


  