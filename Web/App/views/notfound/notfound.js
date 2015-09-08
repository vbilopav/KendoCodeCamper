define([
    "text!views/notfound/notfound."+ _.culture.name + ".html"
], function (template) {
    
    return {
        view: new _.View(template, {
            id: "notfound",
            data: {},
            show: function () {
                var a = location.hash.split("/");
                a.shift();
                if (a[0].indexOf("?aspxerrorpath=") !== -1) a.shift();                    
                $("input#goog-wm-qt").val(a.join(" ")).keydown(function(e) {
                    if (e.which == 13) {
                        $("input#goog-wm-sb").click();
                    }
                }).focus(function() { this.select(); }).focus();
                _.animate(this.element);
                window.document.title = _._resx.core.notFound;
            }
        })
    };
});
