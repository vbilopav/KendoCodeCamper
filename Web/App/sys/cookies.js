define(function () {

    var pub = {
        encodeCookie: function (value) {
            return value; // plain text
        },

        decodeCookie: function (value) {
            return value; // plain text
        },
 
        getCookieValue: function (key) {
            var neq = _.cookiemodel.name + "=";
            var ca = document.cookie.split(";");
            for (var i = 0; i < ca.length; i++) {
                var c = ca[i];
                while (c.charAt(0) == " ") c = c.substring(1, c.length);
                if (c.indexOf(neq) == 0) {
                    var val = JSON.parse(pub.decodeCookie(c.substring(neq.length, c.length)));
                    if (!key) {
                        return val;
                    } else {
                        return val[key];
                    }
                }
            }
            return undefined;
        },

        setCookieValue: function (key, value) {
            var c = pub.getCookieValue();
            if (!c) c = _.cookiemodel.default;
            c[key] = value;
            var expires = "";
            if (_.cookiemodel.expireDays) {
                var date = new Date();
                date.setTime(date.getTime() + (_.cookiemodel.expireDays * 24 * 60 * 60 * 1000));
                expires = "; expires=" + date.toGMTString();
            };
            document.cookie = _.cookiemodel.name +
                "=" + 
                pub.encodeCookie(kendo.stringify(c)) +
                expires + 
                "; path=" +
                _.cookiemodel.path;
        }
    };
    return pub;
});