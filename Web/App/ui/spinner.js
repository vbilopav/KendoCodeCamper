define([    
], function () {
    "use strict";

    return function (p) {
        if (!p.parameters) p.parameters = {};
        if (!p.parameters.size) p.parameters.size = "28px";
        if (!p.parameters.timeout) p.parameters.timeout = 1000;
        var that = this;
        that.element = p.element;
        this.html =
            "<div style=\"font-size: " +
            p.parameters.size +
            "\" class=\"loading-spinner fade in\"><span class=\"fa fa-cog fa-spin\"" +
            (p.parameters.margins ? "style=\"margin: " + p.parameters.margins + "\"" : "") +
            "></span></div>";
        this.spin = function (e) {
            if (e) that.element = e;
            that.timeout = setTimeout(function () {
                _.animate(that._e = $(that.html).appendTo(_.$(that.element)));
            }, p.parameters.timeout);
        };
        this.remove = function () {
            if (that._e) {
                _.animate(that._e.remove());
                that._e = undefined;
            }
            if (that.timeout) clearTimeout(that.timeout);
        };
    };
});