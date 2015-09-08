(function() {
    if (typeof (Storage) !== "undefined") {
        if (_.authorize.endpoint) {
            if (!sessionStorage.getItem("accessToken")) {
                var token = window.location.hash.substring(window.location.hash.indexOf('=') + 1, window.location.hash.indexOf('&'));
                if (token) {
                    var stateIndex = window.location.hash.indexOf('state=');
                    if (stateIndex !== -1) {
                        window.location.hash = decodeURIComponent(window.location.hash.substring(stateIndex + 6));
                    } else window.location.hash = '';
                    sessionStorage.setItem("accessToken", token);
                } else {
                    window.location = _.authorize.endpoint +
                        '?client_id=' + _.authorize.publicid +
                        '&response_type=token' +
                        '&state=' + encodeURIComponent(window.location.hash);
                }
            }
        }
    } else {
        window.location = _.errorpage + '?id=na&l=' + _.culture;
    }
})();
