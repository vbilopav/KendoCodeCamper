define([
    "ui/spinner",
    "ui/alert"
], function (Spinner, alert) {
    "use strict";

    var ajaxFunc = function (url, ajax) {
        if (!(this instanceof ajaxFunc)) {
            throw new TypeError();
        }
        if (!ajax.type) ajax.type = "GET";
        return function () {
            var that = { running: true };
            var options = undefined, u = url;                
            if (u[u.length - 1] === "/") u = u.substring(0, u.length - 1);                
            for (var i = 0; i < arguments.length; i++) {
                if (typeof arguments[i] === "string") {
                    u = u + "/" + arguments[i];
                } else if (typeof arguments[i] === "object") {
                    if (arguments[i].success || arguments[i].error || arguments[i].complete) {
                        options = arguments[i];
                    } else {
                        if (u.indexOf("?") === -1) {
                            u = u + "?";
                        } else {
                            u = u + "&";
                        }
                        u = u + $.param(arguments[i]);
                    }                   
                }
            }
            if (!options) {
                options = { success: function() {}, error: function() {}, complete: function() {} };
            } else {
                if (!options.success) options.success = function () { };
                if (!options.error) options.error = function () { };
                if (!options.complete) options.complete = function () { };
            }
            var spinner = new Spinner({
                parameters: { size: ajax.spinnerSize, margins: ajax.spinnerMargins },
                element: ajax.element
            });
            $.ajax({
                url: u,
                type: ajax.type,
                beforeSend: function () {
                    that.running = true;
                    if (ajax.element) {
                        spinner.spin();
                    }
                },
                success: function (r) {
                    if (r.success) {
                        options.success(r.model);
                    } else {
                        options.error(r);                        
                        if (ajax.element) {
                            _.$(ajax.element).append(alert.error(r.errors[0].errorMessage));
                        }
                    }                    
                },
                error: function (r) {        
                    options.error(r);                                    
                    if (ajax.element) {
                        _.$(ajax.element).append(alert.error());
                    }
                },
                complete: function () {
                    if (ajax.element) {
                        spinner.remove();
                    }
                    that.running = false;
                    options.complete();
                }
            });
            return that;
        };
    };
    return ajaxFunc;
});