define([], function () {
    "use strict";
    var t = _.compile('<require src="ui/back"/><div id="details" class="k-textbox k-widget details-group">#= e #</div>');
    return {
        html: function (e) {
            return _.template(t, { e: e && e.element ? e.element.html() : "" });
        }
    };
});