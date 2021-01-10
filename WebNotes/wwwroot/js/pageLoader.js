function load(book, page) {
    document.getElementById("page").src = "/Page/Get?book=" + book + "&page=" + page;
    //document.getElementById("page").contentWindow.location.reload();

    if (window.screen.width <= 480) {
        closeNav();
    }

    history.pushState({}, null, "/Page?book=" + book + "&page=" + page);
}

