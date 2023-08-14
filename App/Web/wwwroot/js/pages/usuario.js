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
        console.log(model);
        $.post(configs.urls[url], model).done(() => {
            console.log('cadastrado com sucesso');
        });
    };

    return {
        init: init,
        cadastrar: cadastrar,
    };
})()
