var usuario = (function () {
    var configs = {
        urls: {
            cadastrar: ''
        }
    };

    var init = function ($configs) {
        configs = $configs;
    };

    var cadastrar = function (form, url) {
        site.mostraLoading();
        var model = $(`#${form}`).serializeObject();
        $.post(configs.urls[url], model)
            .done(site.redirectToAction)
            .fail(site.escondeLoading);
    };

    return {
        init: init,
        cadastrar: cadastrar,
    };
})()
