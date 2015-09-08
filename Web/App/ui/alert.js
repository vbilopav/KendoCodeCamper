define([
], function () {
    "use strict";

    var content =
        "<div class=\"model1-alert alert transparent #= alert #\" role=\"alert\">" +
        "    <div style=\"width: 442px; font-weight: bold; display: inline\">" +
        "        <i class=\"fa #= fa # fa-2x\" style=\"position: relative; top: 5px;\"></i>" +
        "        &nbsp;&nbsp;&nbsp;#= msg #" +
        "    </div>" +
        "</div>";

    var tmpl = _.compile(content),
        fa = function(type) {
            switch (type) {
                case alert.types.warning: return "fa-exclamation-triangle warning-color";
                case alert.types.danger: return "fa-bug exclamation-icon-color";
                case alert.types.success: return "fa-check";
                case alert.types.info: return "fa-exclamation";
            }
            return "fa-" + type;
        },
        alert = {
            types: {
                warning: "alert-warning",
                danger: "alert-danger",
                success: "alert-success",
                info: "alert-info"
            }
        },
        html = function(type, msg) { return _.template(tmpl, { alert: type, fa: fa(type), msg: !msg ? "" : msg }); };

    alert.custom = function (icon, msg, classes) {
        return _.template(tmpl, {
            alert: classes ? "alert-info " + classes : "alert-info",
            fa: fa(icon),
            msg: !msg ? "" : msg
        });
    };
    alert.warning = function (msg) {
        return html(alert.types.warning, (msg ? "&nbsp;&nbsp;&nbsp;" + msg : ""));
    };
    alert.error = function (msg) {
        return html(alert.types.danger, _._resx.core.genericError + (msg ? "&nbsp;&nbsp;&nbsp;" + msg : ""));
    };
    alert.success = function (msg) {
        return html(alert.types.success, _._resx.core.success + (msg ? "&nbsp;&nbsp;&nbsp;" + msg : ""));
    };
    alert.info = function (msg) {
        return html(alert.types.info, msg);
    };
    alert.html = function (e) {
        return html(e.parameters.type ? alert.types[e.parameters.type] : alert.types.info, e.element.html());
    };
    return alert;
});