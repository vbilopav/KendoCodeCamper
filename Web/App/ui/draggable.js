define([    
], function () {
    "use strict";

    return function(e) {
        var $e = e.element,
            p = e.parameters ? e.parameters : {},
            $f = $("input:focus");        
        $("input", $e).focus(function() {
            $f = $(this);
        });
        $("span.fa-thumb-tack", $e).click(function () {
            var i = $("input:focus", $e);
            var $t = $(this);
            $t.toggleClass("fa-rotate-90");
            $e.toggleClass("fixed").toggleClass("sticky");
            if ($e.hasClass("sticky")) {
                var pos = $e.position();
                $e.css("top", $e.data("restoreY") ? $e.data("restoreY") : p.top != undefined ? p.top : pos.top)
                    .css("left", $e.data("restoreX") ? $e.data("restoreX") : p.left != undefined ? p.left : pos.left);
                $t.attr("title", _._resx.core.pin);
            } else {
                $e.data("restoreY", $e.css("top")).data("restoreX", $e.css("left")).css("top", "auto").css("left", "auto");
                $t.attr("title", _._resx.core.unpin);
            }
            $f.focus();
        });
        var move = function(ev) {
            if ($e.css("position") !== "fixed") return;
            if ($e.data("mouseMove")) {
                $e.css("left", parseInt($e.css("left")) + ev.clientX - $e.data("mouseX"))
                    .css("top", parseInt($e.css("top")) + ev.clientY - $e.data("mouseY"))
                    .data("mouseX", ev.clientX).data("mouseY", ev.clientY);
            }
            $f.focus();
        };
        $e.mousedown(function(ev) {
            $e.data("mouseMove", true).data("mouseX", ev.clientX).data("mouseY", ev.clientY);
        }).parents(":last").mouseup(function() {
            $e.data("mouseMove", false);
        }).mouseout(move).mousemove(move);
    };
});