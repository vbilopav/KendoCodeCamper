define({

    _home: {
        url: "/",
        title: _._resx.default.homeTitle,
        wintitle: _._resx.default.homeTitle,
        nav: "<span class=\"fa fa-home\"></span>"
    },

    sessions: {
        url: "/sessions",
        title: _._resx.default.sessionsTitle,
        wintitle: _._resx.default.sessionsTitle,
        nav: "<span class=\"fa fa-microphone\"></span> " + _._resx.default.sessionsTitle
    },

    speakers: {
        url: "/speakers",
        title: _._resx.default.speakersTitle,
        wintitle: _._resx.default.speakersTitle,
        nav: "<span class=\"fa fa-male\"></span> " + _._resx.default.speakersTitle
    },

    speakerdetails: {
        wintitle: _._resx.default.speakerDetailsTitle,
        url: "/speakers/:id",
        module: "views/speakers/details/details"
    },

    notfound: {
        module: "views/notfound/notfound"
    }

});
