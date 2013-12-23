

$(document).ready(function () {
    for (var handler in mvcsolution.documentReady) {
        mvcsolution.documentReady[handler]();
    }
});