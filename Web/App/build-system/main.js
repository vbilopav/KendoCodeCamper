/*
    >node Scripts/r.js -o App/config.js

    https://github.com/jrburke/r.js/blob/master/build/example.build.js

    http://requirejs.org/docs/api.html#config-shim:
    Do not mix CDN loading with shim config in a build. 
*/
({
    paths: {
        scripts: "../Scripts/libs/",
        text: "../Scripts/libs/text",
        data: "../Scripts/data",
        d: "../Scripts/data/",
        k: "../Scripts/kendo/2015.1.318"
    },
    include: [
        //  include additional data scripts into main script
        //"data/tags.1"  

        //include custom controls
        "ui/alert",
        "ui/back",
        "ui/data-filter",
        "ui/details",
        "ui/ratings"
    ],
    shim: {
        main: {
            deps: [
                "app",
                "sys/init",

                //
                //  full list of kendo culture modules (except en-US which is included in core module by default)
                //
                //  since these files are loaded dynamically, we should enlist them as shim deps
                //  and files from this list will be bundled into build/main.js
                //
                //  otherwise, they are loaded from scripts location
                //
                "k/cultures/kendo.culture.hr.min",

                //
                //  list dynamic require controls
                //    
                //"ui/details",
                //"ui/back",

                //
                //  list of other application modules loaded dynamically, which should be bundled into build/main.js
                //                
                "views/_home/_home",
                "views/notfound/notfound",
                "views/sessions/sessions",
                "views/speakers/speakers",
                "views/speakers/details/details"
            ]
        }
    },
    
    //optimize: "none",
    name: "main",
    preserveLicenseComments: false,
    baseUrl: "../",
    out: "../build/main.js"
})