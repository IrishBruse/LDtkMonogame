window.$docsify = {
    alias: {},
    homepage:
        "https://raw.githubusercontent.com/IrishBruse/LDtkMonogame/main/README.md",
    copyCode: {
        buttonText: "&#xe14d;",
        errorText: "Error",
        successText: "Copied",
    },
    subMaxLevel: 2,
    routerMode: "history",
    topMargin: 70,
    loadSidebar: true,
    loadNavbar: true,
    notFoundPage: "_notfound.md",
    search: "auto",
    auto2top: true,
    relativePath: true,
    plugins: [
        function myPlugin1(hook, vm) {
            // Invoked on each page load after new HTML has been appended to the DOM
            hook.doneEach(function () {
                if (location.pathname == "/Documentation/") {
                    let test = document.querySelector(
                        ".sidebar-nav a[title=Documentation]"
                    );
                    setTimeout(() => {
                        test.click();
                    }, 0);
                }
            });
        },
    ],
};
