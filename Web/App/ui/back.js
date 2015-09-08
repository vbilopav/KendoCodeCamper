define([], function () {
    "use strict";
        
    return {
        html: function() {
            return  "<a class=\"back k-button\" onclick=\"javascript: window.history.back(); return false;\" href=\"" + _._resx.core.back + "\">" +
                    "    <span class=\"fa fa-arrow-circle-left fa-2x\"></span>&nbsp;" +
                    "    <span style=\"position: relative; top: -4px;\">" + _._resx.core.back + "</span>" +
                    "</a>";
        }
    };
});