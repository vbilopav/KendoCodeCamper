define([
    "text!ui/data-filter.html",
    "ui/draggable",
    "ui/input-defaults"
], function (template, draggable, defaults) {
    "use strict";

    var t = _.compile(template);

    return {
        html: function (e) {
            return _.template(t, {
                inner: e.element.html(),
                placeholder: e.parameters.placeholder ? e.parameters.placeholder : "",
                attributes: e.attributes.id ? "id=\"" + e.attributes.id + "\"" : ""
            });
        },

        shown: function (e) {
            defaults(e);
            if (!e.parameters.left) e.parameters.left = 0;
            draggable(e);
        }
    };
});