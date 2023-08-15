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
        var model = $(`#${form}`).serializeObject();
        $.post(configs.urls[url], model).done().fail(site.toast.error);
    };

    return {
        init: init,
        cadastrar: cadastrar,
    };
})()
