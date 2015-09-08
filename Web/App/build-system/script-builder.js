/*
%1 = $(ConfigurationName) 
%2 = $(ProjectDir) 
%3 = $(SolutionDir)
*/
({
    baseFolder: "%2", //default is project folder
    binFolder: "../bin", 
    formatting: "None", //Indented

    resources: {
        folder: "../Scripts/resources",
        cultures: "en,hr",
        wrap: {
            start: "_._resx = ",
            end: ";"
        },
        list: [{
            key: "core",
            type: "Infrastructure._resx.Core, Infrastructure"
        }, {
            key: "web",
            type: "Infrastructure.Web._resx.Web, Infrastructure.Web"
        }, {
            key: "default",
            type: "Resources.Default, Resources"
        }]
    },

    types: [
        {
            script: "../Scripts/data/tracks.1.js",
            type: "Service.TracksService, Service",
            method: "BuildDataScript",
            format: "raw" 
        },
        {
            script: "../Scripts/data/rooms.1.js",
            type: "Service.RoomsService, Service",
            method: "BuildDataScript",
            format: "raw"
        },
        {
            script: "../Scripts/data/tags.1.js",
            type: "Service.TagsService, Service",
            method: "BuildDataScript",
            format: "raw"
        }
    ]
})