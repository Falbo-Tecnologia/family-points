var tarefa = (function () {
    var configs = {
        urls: {
            cadastrar: '',
            listaTarefas: ''
        }
    };

    var init = function ($configs) {
        configs = $configs;
    };

    const criarBotao = (nomeIconeUikit, funcao) => $('<span>', {
        html: `<i uk-icon="icon: ${nomeIconeUikit}"></i>`,
        class: nomeIconeUikit == 'file-edit' ? 'botao-editar' : 'botao-excluir',
        on: { click: funcao }
    });

    var excluirTarefa = function (event) {
        $(event.target).closest('tr').remove();
    };

    var editarTarefa = function (event) {
        var tarefa = criarItem($(event.target).closest('tr'));

        $("#cadastro-tarefa-descricao").val(tarefa.descricao);
        $("#cadastro-tarefa-pontuacao").val(tarefa.pontuacao);

        excluirTarefa(event);
    };

    function criarItem(row) {
        var item = {
            descricao: row.find('[data-descricao]').data('descricao'),
            pontuacao: row.find('[data-pontuacao]').data('pontuacao')
        };
        return item;
    }

    var inserirTarefa = function () {
        const cabecalho = $('#tabela-tarefas-adicionadas th');
        const tarefa = $('#form-cadastro-tarefa').serializeObject();
        const linha = $('<tr>');

        for (var propriedade in tarefa) {
            if (tarefa.hasOwnProperty(propriedade) && cabecalho.filter(`[data-property="${propriedade}"]`).length > 0) {
                var texto = tarefa[propriedade];
                const td = $(`<td data-${propriedade}="${texto}">${texto}</td>`);
                linha.append(td);
            }
        }

        linha.append($('<td>').append(criarBotao('file-edit', editarTarefa)));
        linha.append($('<td>').append(criarBotao('close', excluirTarefa)));

        $('#corpo-tabela-tarefas-adicionadas').append(linha);
    };

    var finalizar = function (url) {
        var model = {};
        model.tarefas = percorrerTabela();
        if (model == null) {
            site.toast.error("Nenhum tarefa foi registrada.")
            console.log(configs.urls[url], 'model nulo');
        }
        else {
            $.post(configs.urls[url], model).done(function () {
                site.toast.success("Tarefa(s) registrada(s) com sucesso.");
                $('#corpo-tabela-tarefas-adicionadas').empty();
            }).fail(site.toast.error)
        }
    };

    function percorrerTabela() {
        var corpoTabela = $("#tabela-tarefas-adicionadas").find("tbody");
        var tarefas = [];

        corpoTabela.find("tr").each(function () {
            var row = $(this);
            var item = criarItem(row);

            tarefas.push(item);
        });

        return tarefas;
    }

    var mostrarViewListaTarefas = function () {
        var tarefas = $('#mostrar-tarefas');
        $.get(configs.urls.listaTarefas).done(function (html) {
            tarefas.html(html);
        }).fail(site.toast.error);
    };

    return {
        init: init,
        inserirTarefa: inserirTarefa,
        finalizar: finalizar,
        mostrarViewListaTarefas: mostrarViewListaTarefas
    };
})();
