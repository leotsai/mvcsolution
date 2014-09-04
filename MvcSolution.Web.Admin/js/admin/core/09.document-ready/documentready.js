

$(document).ready(function () {
    for (var handler in admin.documentReady) {
        admin.documentReady[handler]();
    }
});