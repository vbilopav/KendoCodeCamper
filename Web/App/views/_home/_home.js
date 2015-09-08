define([
    "text!views/_home/home.html",
], function (homeTemplate) {

    var viewModel = {
        title: "Home"
    };

    return {
        view: new kendo.View(homeTemplate, {
            model: viewModel,
            show: function() {
                //var x = c.getCookieValue("anonymId");
                //console.log(x);
                _.animate(this.element);
            }
        })
    };
});
