(function ($) {
    $.SocialLink = function (Settings) {
        var Option = $.extend({ userName: 'BJ', portalID: 0, userModuleID: 0 }, Settings);
        Mine = {
            config: {
                isPostBack: false,
                async: false,
                cache: false,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: 'json',
                baseURL: WorkLogPath + "Services/SocialLinkFeed.asmx/",
                method: "",
                url: "",
                ajaxCallMode: 0
            },
            ajaxSuccess: function (msg) {
                switch (Mine.config.ajaxCallMode) {
                    case 1:
                        var rss = '';
                        var linkedIn = '';
                        var twitter = '';
                        $.each(msg.d, function (index, data) {
                            var li = '<li><span style="visibility:hidden">' + data.LinkID + '</span><label class="sfFormlabel">' + data.Link + '</label>&nbsp;&nbsp;';
                            li += '<label class="sfFormlabel">' + data.DisplayName + '</label>';
                            li += '<img class="LinkModify edit" id="imgEdit" src=' + SageFrame.utils.GetAdminImage("imgEdit.png") + '>&nbsp;&nbsp;<img class="LinkModify delete" id="imgDelete" src=' + SageFrame.utils.GetAdminImage("imgdelete.png") + '>';
                            switch (data.Type) {
                                case 'RSS':
                                    rss += li;
                                    break;

                            }
                        });
                        $('#divRSSLinks').html(rss);
                        break;
                    case 2:
                        Mine.GetLinks();
                        break;
                    case 3:
                        Mine.GetLinks();
                        break;
                }
            },
            ajaxFailure: function (data) {
                //alert('Not Ok!');
            },
            ajaxCall: function (config) {
                $.ajax({
                    type: Mine.config.type,
                    contentType: Mine.config.contentType,
                    cache: Mine.config.cache,
                    async: Mine.config.async,
                    url: Mine.config.url,
                    data: Mine.config.data,
                    dataType: Mine.config.dataType,
                    beforeSend: function () {
                    },
                    complete: function () {
                    },
                    success: function (msg) {
                        Mine.ajaxSuccess(msg);
                    },
                    error: function (msg) {
                        Mine.ajaxFailure(msg);
                    }
                });
            },
            GetLinks: function () {
                Mine.config.ajaxCallMode = 1;
                Mine.config.method = "GetLinks";
                Mine.config.url = Mine.config.baseURL + Mine.config.method;
                Mine.config.data = JSON2.stringify({
                    userName: Option.userName,
                    userModuleID: Option.userModuleID,
                    portalID: Option.portalID
                });
                Mine.ajaxCall(Mine.config);
                $('.LinkModify').unbind().bind('click', function () {
                    var parent = $(this).parent().parent().parent();
                    var name = parent.find('.name').text().trim();
                    var id = $(this).parent().find('span').text().trim();
                    var Link = $(this).parent().find('label:first').text().trim();
                    var displayName = $(this).parent().find('label:last').text().trim();

                    if ($(this).hasClass('edit')) {
                        Mine.loadPopupBox(id, Link, displayName);
                        $('#btnUpdate').unbind().bind('click', function () {
                            var popUp = $('#popup_box');
                            link = popUp.find('input[type="text"]:first').val();
                            var displayName = popUp.find('input[type="text"]:last').val();
                            Mine.config.ajaxCallMode = 2;
                            Mine.config.method = "ModifyLink";
                            Mine.config.url = Mine.config.baseURL + Mine.config.method;
                            Mine.config.data = JSON2.stringify({
                                linkID: parseInt(id),
                                link: link,
                                type: name,
                                userName: Option.userName,
                                userModuleID: Option.userModuleID,
                                portalID: Option.portalID,
                                displayName: displayName
                            });
                            Mine.ajaxCall(Mine.config);
                            Mine.unloadPopupBox();
                        });
                        $('#btnClose').click(function () {
                            Mine.unloadPopupBox();
                        });

                    }
                    else {
                        //var isTrue = confirm('Do you want to delete the link  "' + Link + '" ?');

                        jConfirm('Are you sure you want to delete this link ?', 'Confirmation Dialog', function (r) {
                            if (r) {
                                Mine.config.ajaxCallMode = 3;
                                Mine.config.method = "DeleteLink";
                                Mine.config.url = Mine.config.baseURL + Mine.config.method;
                                Mine.config.data = JSON2.stringify({
                                    linkID: id
                                });
                                Mine.ajaxCall(Mine.config);
                                if ($('#btnAdd-' + name).length == 1) {
                                    $('#btnAdd-' + name).show();
                                }
                                else {
                                }
                                Mine.ChangeButtonValue($('#div' + name + 'Links li').length, parent.find('.' + name).length, name);

                            }
                        });

                    }
                });
            },
            Social: function () {
                Mine.GetLinks();
                $('.SocialLink').each(function (index, value) {
                    var $this = $(this);
                    var name = $this.find('.name').text().trim();
                    var LinkLength = $('#div' + name + 'Links li').length;
                    var Buttons = '<div id="div' + name + 'button"><input type="button" class="btnAdd sfBtn " id="btnAdd-' + name + '" value ="Add ' + name + '" ></div>';
                    $this.append(Buttons);
                    if (LinkLength < 5) {
                        if (LinkLength == 0) {
                            $('#btnAdd-' + name).attr('value', 'Start adding ' + name + ' link');
                        }
                        else {
                            $('#btnAdd-' + name).attr('value', 'Add ' + name + ' link');
                        }
                    }
                    else {
                        $('#btnAdd-' + name).hide();
                    }
                });
                $('.btnAdd').click(function () {
                    var me = $(this);
                    var parent = $(this).parent().parent();
                    var name = parent.find('.name').text().trim();
                    var txtBox = parent.find('.' + name).length;
                    var LinkLength = $('#div' + name + 'Links li').length;
                    var count = 0;
                    if (txtBox > 0) {
                        var textnull = 0;
                        parent.find('.' + name).find('input[type="text"]:first').each(function () {
                            if ($(this).val().trim() == '') {
                                textnull = 1;
                            }
                        });
                        if (textnull == 0) {
                            var $this = parent.find('.' + name + ':last');
                            var lastid = $this.find('input').attr('id');
                            count = lastid.split('-');
                            count = parseInt(count[1]);
                            var inputLink = Mine.MakeLinks((count + 1), name);
                            $('#div' + name + 'button').before(inputLink);
                            if ((count + 1) == 5) {
                                me.hide();
                            }
                        }
                    }
                    else {
                        var inputLink = Mine.MakeLinks((LinkLength + 1), name);
                        $('#div' + name + 'button').before(inputLink);
                        if ($('#btnSave-' + name).length == 0) {
                            var button = '<input class="Save sfBtn" type="button" id="btnSave-' + name + '" value="Save" ><input class="Cancel sfBtn" type="button" id="btnCancel-' + name + '" value="cancel" >';
                        }
                        if ($('#btnCancel-' + name).length != 0) {
                            $('#btnCancel-' + name).show();
                        }
                        if ($('#btnSave-' + name).length == 1) {
                            $('#btnSave-' + name).show();
                        }
                        $('#div' + name + 'button').append(button);
                        if ((LinkLength + 1) == 5) {
                            me.hide();
                        }
                    }
                    Mine.ChangeButtonValue($('#div' + name + 'Links li').length, parent.find('.' + name).length, name);
                    $('.Cancel').unbind().bind('click', function () {
                        var me = $(this);
                        var parent = $(this).parent().parent();
                        var name = parent.find('.name').text().trim();
                        var length = $('.' + name).length;
                        if (length == 1) {
                            $('.' + name + ':last').remove();
                            me.hide();
                            $('#btnSave-' + name).hide();
                        }
                        else {
                            $('.' + name + ':last').remove();
                        }
                        if ($('#div' + name + 'Links li').length < 5) {
                            $('#btnAdd-' + name).show();
                        }
                        Mine.ChangeButtonValue($('#div' + name + 'Links li').length, parent.find('.' + name).length, name);
                    });
                    $('.Save').unbind().bind('click', function () {
                        var me = $(this);
                        var parent = $(this).parent().parent();
                        var name = parent.find('.name').text().trim();
                        var value = [];
                        var dispalyName = [];
                        $('.' + name).each(function () {
                            value.push($(this).find('input[type="text"]:first').val().trim());
                            dispalyName.push($(this).find('input[type="text"]:last').val().trim());
                        });
                        value = value.join(',');
                        dispalyName = dispalyName.join(',');
                        Mine.config.ajaxCallMode = 2;
                        Mine.config.method = "ModifyLink";
                        Mine.config.url = Mine.config.baseURL + Mine.config.method;
                        Mine.config.data = JSON2.stringify({
                            linkID: 0,
                            link: value,
                            type: name,
                            userName: Option.userName,
                            userModuleID: Option.userModuleID,
                            portalID: Option.portalID,
                            displayName: dispalyName
                        });
                        Mine.ajaxCall(Mine.config);
                        $('.' + name).remove();
                        me.hide();
                        $('#btnCancel-' + name).hide();
                        if ($('#div' + name + 'Links li').length < 5) {
                            $('#btnAdd-' + name).show();
                        }
                        Mine.ChangeButtonValue($('#div' + name + 'Links li').length, parent.find('.' + name).length, name);
                    });
                });
            },
            ChangeButtonValue: function (LinkLength, inputLength, name) {
                if (LinkLength == 0 && inputLength == 0) {
                    $('#btnAdd-' + name).attr('value', 'Start adding ' + name + 'link');
                }
                else {
                    $('#btnAdd-' + name).attr('value', 'Add' + name + 'link');
                }
            },
            MakeLinks: function (count, name) {
                if (count < 6) {
                    var inputLink = '<div class="' + name + '"><label class="sfFormlabel">Links:</label><input id="' + name + '-' + count + '" type="text" class="' + name + 'Link sfInputbox"><label class="sfFormlabel">Display Name:</label><input id="' + name + '-' + count + '_dispname" type="text" class="sfInputbox"><br/></div>';
                    return inputLink;
                }
                else {
                    return false;
                }
            },
            unloadPopupBox: function () { // TO Unload the Popupbox
                var popUp = $('#popup_box');
                popUp.fadeOut('fast');
                $("#container").fadeOut("fast");
            },

            loadPopupBox: function (linkID, Link, DisplayName) { // To Load the Popupbox
                var popUp = $('#popup_box');
                popUp.find('span').text(linkID);
                popUp.find('input[type="text"]:first').val(Link);
                popUp.find('input[type="text"]:last').val(DisplayName);
                $('#popup_box').fadeIn("slow");
                $("#container").css({ // this is just for style
                    "opacity": "0.3",
                    'display': 'inline'
                });
            },
            init: function () {
                Mine.Social();
            }
        };
        Mine.init();
    };
    $.fn.SocialLinks = function (settings) {
        $.SocialLink(settings);
    };
} (jQuery));