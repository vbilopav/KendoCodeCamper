/*
data requirejs plugin
 */
define(['module'], function (module) {
    'use strict';
    return {
        version: '1.0.0 ',
        load: function (name, req, onload, config) {
            if (config.isBuild) { return onload(); };
            for (var i = 0; i < _.data.length; i++) {
                var v = _.data[i];
                if (v.key === name) {
                    req(["/Scripts/data/" + v.module + ".js"], function(d) {
                         onload(d.data);
                    });
                    return;
                }
            }
            req(["/Scripts/data/" + name + ".js"], function(d) {
                 onload(d.data);
            });
        }
    };
});