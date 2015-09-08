define([    
], function () {
    "use strict";

    return function (e) {
        $("input[type=text]", e.element)
            .on('focus', function () { $(this).addClass("k-state-focused"); })
            .on('blur', function () { $(this).removeClass("k-state-focused"); }); //bootstrap glitch
        var input = $("input:visible", e.element)
             .attr("autocomplete", "off")
             .each(function (i, el) {
                 $(el).data("i", i);
             })
             .focus(function () {
                 $(this).select();
             })
             .on("keydown", function (e) {
                 if (e.which === 13) {
                     var i = $(this).data("i");
                     if (input[i + 1])
                         input[i + 1].focus();
                     else
                         input[0].focus();
                 }
             });
        input.first().focus();
    };
});