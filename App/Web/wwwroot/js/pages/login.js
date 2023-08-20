var login = (function () {
    var configs = {
        urls: {
            autenticar: ''
        }
    };

    var init = function ($configs) {
        configs = $configs;
    };

    var autenticar = function (form, url) {
        site.mostraLoading();
        var model = $(`#${form}`).serializeObject();
        $.get(configs.urls[url], model)
            .done(site.redirectToAction)
            .fail(site.escondeLoading);
    };

    return {
        init: init,
        autenticar: autenticar
    };
})();
