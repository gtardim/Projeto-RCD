var inicializador = {
    init: function () {
        loadInicial()
    }
}

function loadInicial() {
    $.ajax({
        type: 'GET',
        url: '/RegistrarArquivo/LoadRelatorio',
        dataType: 'json',
        success: function (res) {
            if (res.cliente != null)
                carregaCliente(res.cliente);
            if (res.tipoarq != null)
                carregaTipoArquivo(res.tipoarq);
            if (res.processo != null)
                carregaProcesso(res.processo);

        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });
}

function carregaCliente(res) {

    var data = [{
        id: 0, text: 'Selecione o Cliente'
    }];
    for (var i = 0; i < res.length; i++) {
        data.push({
            id: res[i].cod,
            text: res[i].nome
        });
    }
    $("#txtCliente").select2({
        data: data
    })
}

function carregaProcesso(res) {

    var data = [{
        id: 0, text: 'Selecione o Processo'
    }];
    for (var i = 0; i < res.length; i++) {
        data.push({
            id: res[i].cod,
            text: res[i].descricao
        });
    }
    $("#txtProcesso").select2({
        data: data
    })
}

function carregaTipoArquivo(res) {

    var data = [{
        id: 0, text: 'Selecione o Tipo de Arquivo'
    }];
    for (var i = 0; i < res.length; i++) {
        data.push({
            id: res[i].cod_tipo,
            text: res[i].descricao
        });
    }
    $("#txtTipoArq").select2({
        data: data
    })
}

function preview() {
    var cliente = $("#txtCliente").val();
    var tipoarq = $("#txtTipoArq").val();
    var processo = $("#txtProcesso").val();

    window.open('/RegistrarArquivo/Preview/?cliente=' + cliente + "&tipoarq=" + tipoarq + "&processo=" + processo, '_blank');

}

inicializador.init();