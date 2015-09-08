define([
    "text!views/sessions/sessions.html",
    "data!tags",
    "data!tracks",
    "data!rooms",
    "k/kendo.multiselect.min"
], function (template, tagsData, tracksData, roomsData) {

    var tags, tracks, rooms, $i;

    return {
        change: function (e) {
            //win.close();
        },
        view: new _.View(template, {           
            show: function () {
                _.animate(this.element);
            },
            parse: function (e) {
  
                tags = $("#tags", e.element).kendoMultiSelect({
                    dataSource: {
                        data: tagsData                        
                    },
                    dataTextField: "tag",
                    dataValueField: "tag"
                }).data("kendoMultiSelect");

                tracks = $("#tracks", e.element).kendoMultiSelect({
                    dataSource: {
                        data: tracksData
                    },
                    dataTextField: "name",
                    dataValueField: "id"
                }).data("kendoMultiSelect");

                rooms = $("#rooms", e.element).kendoMultiSelect({
                    dataSource: {
                        data: roomsData
                    },
                    dataTextField: "name",
                    dataValueField: "id"
                }).data("kendoMultiSelect");

                $i = $("input[autofocus]", e.element);
            },

            init: function () {               
            }
        })
    };
});
