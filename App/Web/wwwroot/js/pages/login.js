var login = (function () {
    var configs = {
        urls: {
            autenticar: ''
        }
    };

    var init = function ($configs) {
        configs = $configs;
    };

    var mostraLoading = function () {
        document.getElementById('loadingOverlay').style.display = 'flex';
    };

    var escondeLoading = function (msg) {
        document.getElementById('loadingOverlay').style.display = 'none';
        site.toast.error(msg);
    };

    var redirectToAction = function (response) {
        escondeLoading();
        if (response.success)
            window.location.href = response.redirectUrl;
    };
    var autenticar = function (form, url) {
        mostraLoading();
        var model = $(`#${form}`).serializeObject();
        $.get(configs.urls[url], model)
            .done(redirectToAction)
            .fail(escondeLoading);
    };

    return {
        init: init,
        autenticar: autenticar,
    };
})();
