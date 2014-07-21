var displayName = [];
var rssLink = [];
var Mine = {
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
    ajaxSuccess: function(msg)
    {
        switch (Mine.config.ajaxCallMode) {
            case 1:
                $.each(msg.d, function(index, data)
                {
                    switch (data.Type.trim()) {
                        case 'RSS':
                            rssLink.push(data);
                            break;
                    }
                });
                break;
        }
    },
    ajaxFailure: function(data) {
     //alert('Not Ok!');
     },
    ajaxCall: function(config)
    {
        $.ajax({
            type: Mine.config.type,
            contentType: Mine.config.contentType,
            cache: Mine.config.cache,
            async: Mine.config.async,
            url: Mine.config.url,
            data: Mine.config.data,
            dataType: Mine.config.dataType,
            beforeSend: function()
            {
            },
            complete: function()
            {
            },
            success: function(msg)
            {
                Mine.ajaxSuccess(msg);
            },
            error: function(msg)
            {
                Mine.ajaxFailure(msg);
            }
        });
    }
}
function GetLinks(portalID, userModuleID, userName)
{
    Mine.config.ajaxCallMode = 1;
    Mine.config.method = "GetLinks";
    Mine.config.url = Mine.config.baseURL + Mine.config.method;
    Mine.config.data = JSON2.stringify({
        userName: userName,
        userModuleID: userModuleID,
        portalID: portalID
    });
    Mine.ajaxCall(Mine.config);
}
function SocialLinkView(portalID, userModuleID, userName)
{

    GetLinks(portalID, userModuleID, userName);
    Rss.GoogleRss();
}
//RssFeed
var Rss = {
    init: function()
    {

        $(rssLink).each(function(index, value)
        {
            Rss.initialize(value.Link,value.DisplayName);
        });
    },
    initialize: function(url,Title)
    {
        var noOfFeed = 3;
        var li = '';
        var divOne = '';
        var divTwo = '';
        var div = '';
        var feed = '';
        feed = new google.feeds.Feed(url);
        feed.setNumEntries(noOfFeed);
        feed.load(function(result)
        {
            if (!result.error) {
                var feeddivid = $('#feed');
                li += '<div class="cssSocialLinkTitle"><h2>' + (Title!=""?Title:result.feed.link) + '</h2></div>';
                for (var i = 0; i < result.feed.entries.length; i++) {
                    var entry = result.feed.entries[i];
                    divOne += '<a href="' + entry.link + '" target="_blank">' + entry.title + ' </a><br />';
                    divOne += '<div class ="ContentSnippet">' + entry.contentSnippet + '</div>';
                }
            }
            $('#vtab').append(li);
            div += '<div class="rssContents">' + divOne + '</div>';
            $('#vtab').append(div);
        });
        var $items = $('#vtab>ul>li');
        $items.click(function()
        {
            $items.removeClass('selected');
            $(this).addClass('selected');

            var index = $items.index($(this));
            $('#vtab>div').hide().eq(index).show();
        }).eq(1).click();
    },
    GoogleRss: function()
    {
        google.setOnLoadCallback(Rss.init);
    }
};