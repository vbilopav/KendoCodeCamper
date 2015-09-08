define([
    "sys/cookies"
], function (cookies) {
    "use strict";

    function resxPath(culture) {
        var l = culture;
        $.each(_.cultures, function (i, v) {
            if (l === v.name) {
                return "Scripts/resources/" + l + ".js" + _.resxVersion;
            } else {
                if (v.name.length === 2 && l.substring(0, 2) === v.name) {
                    l = v.name;
                }
            }
        });
        return "Scripts/resources/" + l + ".js" + _.resxVersion;
    };

    function loadCulture(callback) {
        if (_.culture.name !== "en-US" && _.culture.name !== "en") {
            require(["k/cultures/kendo.culture." + _.culture.name + ".min"], function () {
                kendo.culture(_.culture.name);
                if (callback !== undefined) callback();
            });
        } else {
            if (callback !== undefined) callback();
        }
    };

    return function (culture, options) {
        if (!options) options = { reload: true};
        if (culture !== _.culture.name) {
            if (options.reload) {
                cookies.setCookieValue("culture", culture);
                document.location.reload();
            } else {
                $.ajax({
                    url: resxPath(culture),
                    cache: true,
                    type: "GET",
                    async: true
                }).done(function(resx) {
                    eval(resx);
                    _.culture.name = _._resx.culture.name;
                    _.culture.value = _._resx.culture.value;
                    if (!_.user.authenticated) {
                        _.user.name = _._resx.core.anonymUser;
                    };
                    cookies.setCookieValue("culture", _.culture.name);
                    loadCulture(function() { $.publish("/language/change", [culture]); });
                    if (options.success) options.success();
                });
            }
        }
    };
});