define([
], function () {
    "use strict";

    var interval = 1000;

    return function (e) {
        var that = this, timeout = null;
        this.clear = function () {
            if (!that.element.next().length) return;
            if (that.element.next().is(":hover") || that.element.is(":hover")) {
                clearTimeout(timeout);
                that._timeout = setTimeout(that.clear, interval);
            } else {
                if (timeout) {
                    that.element.popover("hide");
                    clearTimeout(timeout);
                    timeout = null;
                }
            }
        };
        this.show = function () {
            if (!timeout || !that.element.next().length) {
                timeout = setTimeout(that.clear, interval);
                that.element.popover("show");
            }
        };
        this.element = e.element
            .popover({
                trigger: "manual",
                placement: "bottom",
                html: true,
                title: e.title,
                content: e.content
            })
            .click(function () { return false; })
            .hover(that.show)
            .on('hide.bs.popover', function () {
                clearTimeout(timeout);
                timeout = null;
            })
            .on('shown.bs.popover', function () {
                $("a", $(this).parent().parent()).not(this).popover("hide");
                if (e.shown) {
                    e.shown.call(this);
                }
            });
        $(window).on("click resize", that.clear);
    }
});