(function() {
    if (window.mvcsolution) {
        return;
    }
    window.mvcsolution = {
        ajax: {

        },
        notification: {

        },
        utils: {
            validator: {

            }
        },

        debug: true
    };

    window.page = {

    };

    $(document).ready(function() {
        if (window.page && page.onLoaded) {
            page.onLoaded();
        }
    });
})();