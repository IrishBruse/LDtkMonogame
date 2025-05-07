window.$docsify = {
    name: "",
    routerMode: "history",

    loadNavbar: true,
    loadSidebar: true,
    notFoundPage: "_notfound.md",

    copyCode: {
        buttonText: "&#xe14d;",
        errorText: "Error",
        successText: "Copied",
    },

    topMargin: 60,
    search: "auto",
    relativePath: true,
};

window.$docsify.search = {
    paths: ["/", "/Documentation/", "/Changelog.md", ...ApiSearchPaths],
};
