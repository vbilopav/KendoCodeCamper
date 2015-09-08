/*

*/
({
    paths: {
        text: "../Scripts/libs/text",
        data: "../Scripts/data"
    },

    baseUrl: "../",
    dir: "../build",

    modules: [
    ],

    //
    //  Exclude everything not in views or resources folders
    //

    fileExclusionRegExp: /build|layout|vendor|^setup.js$|^main.js$|^text.js$|^app.js$|^build.txt$/, 
    
    //optimize: "none",
    preserveLicenseComments: false
})