var login = (function () {
    var configs = {
        urls: {
            autenticar: ''
        }
    };

    var init = function ($configs) {
        configs = $configs;
    };

    var redirectToAction = function (response) {
        if (response.success)
            window.location.href = response.redirectUrl;
    };

    var autenticar = function (form, url) {
        var model = $(`#${form}`).serializeObject();
        $.get(configs.urls[url], model)
            .done(redirectToAction)
            .fail(site.toast.error);
    };

    return {
        init: init,
        autenticar: autenticar,
    };
})()
