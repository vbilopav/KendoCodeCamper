define([    
], function () {

    function encodeLogData(value) {
        return value; // plain text
    }

    function serverLog(method, message, url, lineNumber) {
        if (_.logendpoint) {
            $.ajax({
                url: _.logendpoint + method,
                cache: false,
                data: encodeLogData(kendo.stringify({
                    Browser: window.navigator.saywho,
                    Message: message, Url: url,
                    LineNumber: lineNumber
                })),
                type: "POST",
                async: true
            });
        }
    }

    return {
        error: function (message, url, lineNumber) {
            serverLog("/Error", message, url, lineNumber);            
        },
        warning: function (message, url, lineNumber) {
            serverLog("/Warning", message, url, lineNumber);
        },
        info: function (message, url, lineNumber) {
            serverLog("/Info", message, url, lineNumber);
        }
    };
});