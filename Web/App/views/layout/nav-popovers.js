define([
    "ui/nav-popover",
    "sys/culture"
], function (Popover, culture) {
    "use strict";

    var
        langmsg = {
            en: "You current language is <b>" + _.culture.value + ".</b> Change it to: ",
            hr: "Vaš trenutni jezik je <b>" + _.culture.value + ".</b> Izaberite drugi: "
        },
        logmsg = {
            en: "Request log:<hr />",
            hr: "Povijest zahtjeva:<hr />"
        },
        optionsmsg = {
            en: "Click here for options...",
            hr: "Kliknite ovdje za opcije..."
        },
        langs = "",
        logs = logmsg[_.culture.name],
        cog;

    for (var i = 0; i < _.cultures.length; i++) {
        if (_.cultures[i].name !== _.culture.name) {
            if (langs) langs = langs + "<br />";
            langs = langs + "<a href='#' class='k-button' data-name='" + _.cultures[i].name + "'>" + _.cultures[i].value + "</a>";
        }
    };

    $(document).ajaxSuccess(function (event, xhr, settings) {
        logs = logs + settings.type + " <a href='" + settings.url + "' target='_blank'>" + settings.url + "</a><br />";
        cog.addClass("buzz-out");
        setTimeout(function () { cog.removeClass("buzz-out"); }, 500);
    });

    return function (header) {
        new Popover({
            element: $("#lang span", header),
            title: langmsg[_.culture.name],
            content: "<span id='languages'>" + langs + "</span>",
            shown: function() {
                $("a", $(this).next()).click(function() {
                    var c = $(this).data("name");
                    if (c !== _.culture) {
                        culture(c);
                        document.location.reload();
                    }
                    return false;
                });
            }
        });
        cog = $("#cog span", header);
        new Popover({
            element: cog,
            title: "<a href='#'>" + optionsmsg[_.culture.name] + "</a>",       
            content: function() {
                return "<span id='options'>" + logs + "</span>";
            }
        });
    };
});