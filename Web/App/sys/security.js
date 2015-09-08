define([    
], function () {

    var registerType = _.authorize.defaultRegisterType,
        verificationToken = {
            __RequestVerificationToken: _.user.crsf
        },
        initialized = false;

    function encodeHeaderData(value) {
        return value; // plain text
    }

    function buildHeaderData(data) {
        var d = "";
        $.each(data, function(i, v) {
            d = d + encodeHeaderData(v) + " ";
        });
        return d.substring(0, d.length - 1);
    }

    function getRequestHeaders(key, data) {
        var h = verificationToken;
        h[_.authorize.registerTypeKey] = registerType;
        h[key] = buildHeaderData(data);
        return h;
    }

    return function () {
        var that = this;
        that.postLogin = function(loginData, remember) {
            return $.ajax({
                url: remember == undefined ? _.authorize.login : _.authorize.login + "?rememberMe=" + remember,
                headers: getRequestHeaders(_.authorize.loginKey, loginData),
                type: "POST"
            });
        };
        that.setRegisterType = function(type) {
            registerType = type;
        };
        that.postRegister = function(registerData, isPersistent, model) {
            return $.ajax({
                url: isPersistent == undefined ? _.authorize.register : _.authorize.register + "?isPersistent=" + isPersistent,
                headers: getRequestHeaders(_.authorize.registerKey, registerData),
                data: model,
                type: "POST"
            });
        };
        that.postSignOff = function () {
            return $.ajax({
                url: _.authorize.logoff,
                headers: verificationToken,
                type: "POST"
            });
        };
        that.setAccessToken = function(token) {
            sessionStorage.setItem("accessToken", token);
        };
        that.getAccessToken = function() {
            return sessionStorage.getItem("accessToken");
        };

        that.removeAccessToken = function() {
            sessionStorage.removeItem("accessToken");
        };
        if (!initialized) {
            $.ajaxSetup({
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", _.authorize.authType + " " + that.getAccessToken());
                },
                error: function (xhr) {
                    if (xhr.status == 401) {
                        var req = new XMLHttpRequest();
                        req.open("GET", _.authorize.endpoint + "?client_id=" + _.authorize.publicid + "&response_type=token", false);
                        req.send();
                        that.setAccessToken(req.responseURL.substring(req.responseURL.indexOf("=") + 1, req.responseURL.indexOf("&")));
                        $.ajax(this).error(function (x) {
                            if (x.status == 401) {
                                that.removeAccessToken();
                                window.location.reload();
                            }
                        });
                    }
                }
            });
            initialized = true;
        }
    };
});