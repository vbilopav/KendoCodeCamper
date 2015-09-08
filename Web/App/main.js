require.config({
    paths: {
        scripts: (_.debug ? ".." : "../..") + "/Scripts/libs",
        text: (_.debug ? ".." : "../..") + "/Scripts/libs/text",
        data: (_.debug ? ".." : "../..") + "/Scripts/data",              
        k: (_.debug ? ".." : "../..") + "/Scripts/kendo/" + _.kendover
    }
});
define([
    "scripts/debug",
    "scripts/pubsub",
    "k/kendo.router.min",
    "k/kendo.view.min",
    "k/kendo.fx.min",
    _.culture.name && _.culture.name !== "en-US" && _.culture.name !== "en" ? "k/cultures/kendo.culture." + _.culture.name + ".min" : ""
], function () {
    require(["sys/init"], function () {
        require(["app"]);
    });
});