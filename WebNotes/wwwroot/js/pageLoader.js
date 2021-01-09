function load(url) {
    document.getElementById("page").src = "/Page/Get?page=" + url;
    //document.getElementById("page").contentWindow.location.reload();
}
