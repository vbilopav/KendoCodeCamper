define([
    "ui/details",
    "text!views/speakers/details/details.html",
    "sys/ajax"
], function(details, template, Ajax) {

    var id,
        read = new Ajax("/Speakers/Details", { element: function () { return $d; }, spinnerMargins: "25px" }),
        rate = new Ajax("/Speakers/Rate", { element: function () { return $r; }, spinnerSize: "18px" }),
        $d, $r, t = _.compile(template),
        ratingMsg = {
            en: "Thank you for rating.",
            hr: "Hvala na ocijeni."
        }
    
    var initRatings = function(e) {
        var ret = {};
        $r = $(".star-rating-msg", e.element);
        e.module.onchange = function (r) {
            if (ret.running || r.rating === r.newRating) r.cancel = true;
            else {
                e.module.disable();
                $r.html("");
                ret = rate(
                    {
                        complete: function () {
                            $r.html(ratingMsg[_.culture.name]);
                            e.module.enable();
                        }
                    },
                    { id: id, rating: r.newRating }
                );
            }
        };
    };

    return {
        parameters: function (params) {
            id = params[0];
        },
        view: new _.View(details.html(), {
            show: function () {
                $d.html("");
                read({
                    success: function (data) {                        
                        _.parse($d.html(_.template(t, data)), function (e) {
                            if (e.id === "ratings") {
                                initRatings(e);
                            };                            
                        });                        
                        $.publish("/view/height/changed");
                    }
                }, { Id: id });                
            },
            init: function () {
                $d = $("#details", this.element);
            }
        })
    };
});
