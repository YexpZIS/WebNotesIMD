function load(url) {
    document.getElementById("page").src = "/Page/Get?page=" + url;
    //document.getElementById("page").contentWindow.location.reload();

    if (window.screen.width <= 480) {
        closeNav();
    }

    history.pushState({}, null, "/Page?page=" + url);
}

