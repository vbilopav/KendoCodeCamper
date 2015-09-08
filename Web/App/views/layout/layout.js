define([
    "text!views/layout/layout.html",
    "views/views",
    "views/layout/nav-popovers"
], function (template, views, popovers) {
    "use strict";

    var
        stickyFotter = true,
        fixedHeader = false,
        fluidHeader = false,
        links, brand, toggle, footer, win, body, nav;

    var
        navclick = function (e) {
            if (toggle.is(":visible") && links.is(":visible")) {
                if (links.is(":visible")) toggle.trigger("click");
            }
        };

    var
        fixHeader = function (t) {
            if (t) {
                body.css("padding-top", "60px");
                nav.addClass("navbar-fixed-top");
            } else {
                nav.removeClass("navbar-fixed-top");
                body.css("padding-top", "");
            }
        };

    var
        navFloat = function () {
            body.css("padding-top", "60px");
            nav.addClass("navbar-fixed-top");
        };

    var
        resize = function () {
            if (stickyFotter) {
                if (win.height() - body.height() < footer.height() + 10) {
                    footer.removeClass("bottom-fixed");
                } else {
                    footer.addClass("bottom-fixed");
                }
            }
        };

    var
        data = {
            fluid: fluidHeader,
            lang: _.culture.name,
            links: []
        };

    $.each(views, function (i, v) {
        if (v.nav && v.title) {
            var link = {
                id: v.url.replace(new RegExp("/", "g"), ""),
                url: _.url(v.url),
                title: v.title,
                nav: v.nav
            };
            if (v.url === "/") {
                data.home = link;
            } else {
                data.links.push(link);
            }
        }
    });

    $.subscribe("/lang/change", function () { kendo.bind($("nav"), data); });
    $.subscribe("/view/height/changed", function () { win.resize(); });
   
    return {
        viewChanged: function () {
            win.resize();
        },
        urlChanged: function (e) {
            if (e.url === "/" || e.url === "") {
                links.find("li").removeClass("active");
                brand.addClass("active");
            } else {
                var url = e.url.split("/")[1];
                var i = url.indexOf("?");
                if (i && i > -1) url = url.substring(0, i - 1);
                var active = $("li#" + url, links);
                $("li", links).removeClass("active");
                if (active.length > 0) {
                    active.addClass("active");
                }
                brand.removeClass("active");
            }
            win.resize();
        },

        layout: new _.Layout(template, {
            data: data,
            init: function () {
                nav = $("nav.navbar");
                var h = $("#navbar-header", nav);
                links = this.element.find("#nav-links-main", h);
                links.parent().on('hidden.bs.collapse shown.bs.collapse', function () {
                    win.resize();
                });
                brand = this.element.find(".navbar-brand", h);
                toggle = $("button.navbar-toggle", h);
                $("a", h).click(navclick);
                footer = $("#footer");
                win = $(window);
                body = $("body");
                win.resize(resize);
                win.resize();
                if (!stickyFotter) {
                    $("html").css("background-color", footer.css("background-color"));
                }
                fixHeader(fixedHeader);
                popovers(h);
            }
        })
    };
});