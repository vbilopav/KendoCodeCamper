define([], function () {
    "use strict";

    var t = _.compile(
        "<span>" +
        "   <div class=\"star-rating\" style=\"font-size: #: size #\" data-rating=\"#: rating #\">" +
        "       <span class=\"fa #= rating >= 1 ? 'fa-star' : 'fa-star-o' #\" data-rating=\"1\"></span>" +
        "       <span class=\"fa #= rating >= 2 ? 'fa-star' : 'fa-star-o' #\" data-rating=\"2\"></span>" +
        "       <span class=\"fa #= rating >= 3 ? 'fa-star' : 'fa-star-o' #\" data-rating=\"3\"></span>" +
        "       <span class=\"fa #= rating >= 4 ? 'fa-star' : 'fa-star-o' #\" data-rating=\"4\"></span>" +
        "       <span class=\"fa #= rating >= 5 ? 'fa-star' : 'fa-star-o' #\" data-rating=\"5\"></span>" +        
        "   </div>" +
        "   <div class=\"star-rating-msg text-info small\"></div>" +
        "</span>");

    return function (e) {
        var that = this;
        this.html = _.template(t, { rating: e.parameters.rating ? e.parameters.rating : 0, size: e.parameters.size ? e.parameters.size : "22px" });
        var click = function() {
            var r = $(this).data("rating");
            var ev = { rating: that.element.data("rating"), newRating: r, cancel: false };
            if (that.onchange)
                that.onchange(ev);
            if (!ev.cancel)
                that.change(r);
        };
        this.show = function(e) {
            that.element = e.element;
            $("span", e.element).click(click);
        };
        this.change = function(r) {
            that.element.data("rating", r);
            $("span", that.element).each(function() {
                var t = $(this);
                if (t.data("rating") <= r) {
                    _.animate(t.addClass("fa-star").removeClass("fa-star-o"));
                } else {
                    _.animate(t.removeClass("fa-star").addClass("fa-star-o"));
                }
            });
        };
        this.disable = function() {
            $("span", that.element).off("click");
            $("div", that.element).css("cursor", "default");
        };
        this.enable = function() {
            $("span", that.element).click(click);
            $("div", that.element).css("cursor", "pointer");
        };
    }
});