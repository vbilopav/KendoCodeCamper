define([
    "sys/security",
    "sys/log"
], function (security, log) {
    "use strict";

    if (_.culture.name && _.culture.name !== "en-US" && _.culture.name !== "en") { kendo.culture(_.culture); };

    navigator.saywho = (function () {
        var who = [];
        for (var k in kendo.support.browser) {
            if (kendo.support.browser.hasOwnProperty(k)) {
                if (typeof kendo.support.browser[k] === "boolean") {
                    navigator[k] = kendo.support.browser[k];
                    who.push(k);
                } else {
                    who.push(kendo.support.browser[k]);
                }
            }
        }
        return who.join(" ");
    })();

    if (_.logerrors) {
        window.onerror = function (message, url, lineNumber) {
            log.error(message, url, lineNumber);
            if (navigator.chrome) {
                throw url + " " + message;   
            } else {
                return false;
            }
        };
    };

    if (_.authorize.endpoint) { security(); };

    _.source = function(err) {
        if (err && err.stack) {
            if (navigator.chrome) {
                return err.stack.split("\n")[1].trim();
            } else {
                return err.stack.split("\n")[0].trim();
            }
        }
    };
   
    _.url = function(url) {
        if (url === "") return _.hashbang ? "#!" : "#";
        if (url === "#" && _.hashbang) return "#!";
        if (url === "#!" && !_.hashbang) return "#";
        if (url.indexOf("#") == 0) return url;
        return (_.hashbang ? "#!" : "#") + (!url ? "" : url);
    };

    _.$ = function(e) {
        if (typeof e === "object") return e;
        if (typeof e === "string") return $(e);
        if (typeof e === "function") return e();
        return e;
    };

    _.animate = function (e) {
        kendo.fx(e).fade("in").duration(1000).play();
    };


    var _t = {};

    _.compile = function(html, id) {
        return id ? _t[id] = kendo.template(html) : kendo.template(html);
    };

    _.template = function (o, data, id) {
        if (typeof o === "function") {
            return o(data, { useWithBlock: false });
        } else {
            if (!id) {
                return kendo.template(o)(data, { useWithBlock: false });
            } else {
                var t = _t[id];
                if (!t) {
                    _t[id] = t = kendo.template(o);
                }
                return t(data, { useWithBlock: false });
            }
        }
    };

    _.parse = function($e, callback) {
        var req = $e.length === 1 ? $("require:not(require require)", $e) : $e;
        if (req.length) {
            req.each(function() {
                var $t = $(this), m = $t.attr("src");
                require([m], function (r) {
                    if (!r) return;
                    var $s, o, attr = {}, p = eval("({" + $t.data("params") + "})");
                    for (var a, i = 0, atts = $t[0].attributes, n = atts.length; i < n; i++) {
                        a = atts[i];
                        if (a.nodeName !== "data-params") attr[a.nodeName] = a.nodeValue;
                    }
                    var e = { parameters: p, attributes: attr, element: $t };
                    if (typeof r === "string") {
                        $s = $(r);
                    } else {
                        if (typeof r === "object") {
                            if (typeof r.html !== "function") {
                                throw new Error("Module " + m + " missing html function!");
                            } else {
                                o = r;
                                $s = $(o.html(e));
                            }
                        }
                        if (typeof r === "function") {
                            o = new r(e);
                            if (typeof o.html !== "string") {
                                throw new Error("Module " + m + " class is missing html string object!");
                            } else
                                $s = $(o.html);
                        }
                    }
                    if ($s) {
                        e = { element: $s, id: $t.attr("id"), parameters: p, attributes: attr, module: o ? o : r };
                        _.animate($t.after($s).remove());
                        if (o != undefined && o.show) o.show(e);
                        if (callback) callback(e);                            
                        if (o != undefined && o.shown) o.shown(e);
                        _.parse($("require:not(require require)", $e), callback);
                    }
                });
            });
        }
    };

    var initView = function(element, options) {
        var init = options.init, show = options.show, parse = options.parse;
        options.init = function () {
            var that = this;
            if (!options.skipControls) {
                _.parse(this.element, function (e) { if (parse) parse.call(that, e); });
            }
            if (init) init.call(that);
        };
        options.show = function () {
            _.animate(this.element);
            if (show) show.call(this);
        };
        if (options.data) {
            return _.template(element, options.data, options.id ? "_" + options.id : undefined);
        }
        return element;
    };

    _.Layout = kendo.Layout.extend({
        init: function (element, options) {
            kendo.Layout.fn.init.call(this, initView(element, options), options);
        }
    });

    _.View = kendo.View.extend({
        init: function (element, options) {
            kendo.View.fn.init.call(this, initView(element, options), options);
        }
    });
  
    log.info("App initialized!", _.source(new Error));
});