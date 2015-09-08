define([
    "text!views/speakers/speakers.html",
    "sys/ajax",
    "ui/alert"
], function (template, Read, alert) {
   
    var
        t, $e, $f, $i;

    var
        afterChangeElapsed = function (e) {
            if (!e.items.length) {
                _.animate($e.html(alert.custom("comment-o", _._resx.default.nothing2See, "transparent")));
            } else {
                var val = $f.val();
                if (val) {
                    val = val.toUpperCase();
                    $("div", $e).each(function (i, v) {
                        var s = $(v).html();
                        s = s.replace(val, "<span class=\"selected-text\">" + val + "</span>");
                        $(v).html(s);
                    });
                }
            }
        };

    var
        dataSource = new kendo.data.DataSource({
            transport: {
                read: new Read("/Speakers", { element: function () { return $e; }, spinnerMargins: "25px" })
            },
            change: function (e) {
                $e.html("");
                if (e.items.length) {
                    $i.html(kendo.format(_._resx.default.speakersResultMsg, e.items.length));
                } else {
                    $i.html(_._resx.default.noSpeakersMsg);
                }
                $i.css("visibility", "visible");
                if (t) clearTimeout(t);
                t = setTimeout(function () {
                    afterChangeElapsed(e);
                }, 1000);
            },
            requestStart: function () {
                $e.html("");
                $i.html("loading...");
            },
            requestEnd: function () {
                $i.css("visibility", "hidden");
            }
        });

    var
        oninput = function (e) {
            dataSource.filter({
                'logic': "or",
                'filters': [
                    { field: "firstName", operator: "contains", value: e.target.value },
                    { field: "lastName", operator: "contains", value: e.target.value }
                ]
            });
        };

    return {
        view: new _.View(template, {
            id: "speakers",
            data: {},
            model: { speakers: dataSource },

            show: function () {
                _.animate(this.element);
                if ($f) $f.focus();
            },

            parse: function(e) {
                if (e.id === "speakers-filter") {
                    var $s = e.element;
                    $f = $("input[autofocus]", $s).on('input', oninput);
                    $i = $("div#info", $s);
                }
                $f.focus();
                dataSource.read();
            },

            init: function () {
                $e = $("#speakers-list", this.element)
                    .on("DOMSubtreeModified", function () {
                        $.publish("/view/height/changed");
                    });                                
            }
        })
    };
});
