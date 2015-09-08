define([
    "views/layout/layout",
    "views/views",
    "views/_home/_home"
], function (layout, views, current) {
    "use strict";

    views._home.instance = current;

    var
        $content,
        $host = $("#applicationHost");

    var
        requireRoute = function(viewid, args, model) {
            var v = views[viewid];
            require([v.module ? v.module : "views/" + viewid + "/" + viewid], function(module) {
                var obj;
                if (typeof module === "function") {
                    obj = new module(args, model);
                } else {
                    obj = module;
                }
                if (obj.parameters && args) {
                    obj.parameters(args);
                }
                if (obj.model && v.model) {
                    obj.model(v.model);
                }
                if (v.wintitle) document.title = v.wintitle;
                layout.layout.showIn($content, obj.view);
                layout.viewChanged(v);
                $.publish("/view/changed", [v]);
                v.instance = current = obj;
            });
        };

    var
        router = new kendo.Router({
            init: function () {
                $host.empty();
                layout.layout.render($host);
                $content = $("#content", $host);
            },
            routeMissing: function (e) {
                debug.error("No Route Found", e.url);
                requireRoute("notfound");
            },
            change: function (e) {
                if (current && current.change) {
                    current.change(e);
                }
                if (!e.isDefaultPrevented()) {
                    layout.urlChanged(e);
                }
            },
            hashBang: _.hashbang,
            ignoreCase: true
        });

    $.each(views, function (i, v) {
        if (v.url) {
            router.route(v.url, function () {
                requireRoute(i, arguments);
            });
        }
    });

    $.subscribe("view/model", function (viewid, model) {
        views[viewid].model = model;
    });

    $.subscribe("view/navigate", function (viewid, url, args, model) {
        requireRoute(viewid, args, model);
        if (url) {
            url = url.replace(_.hashbang ? "/#!" : "/#", "");
        } else {
            url = views[viewid].url;
        }
        router.navigate(url ? url : views[viewid].url, true);
    });

    $.subscribe("url/navigate", function (url) {
        router.navigate(url, true);
    });

    router.start();
});