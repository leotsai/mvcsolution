(function() {
    
    MvcSolution.documentReady.setCkey = function() {
        
    };


})();

$(document).ready(function () {
    for (var handler in MvcSolution.documentReady) {
        MvcSolution.documentReady[handler]();
    }
});