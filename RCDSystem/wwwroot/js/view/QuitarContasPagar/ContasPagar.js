var inicializador = {
    init: function () {
        loadInicial()
    }
}

function loadInicial() {
    $.ajax({
        type: 'GET',
        url: '/LancarContasPagar/LoadReport',
        dataType: 'json',
        success: function (res) {
            if (res.tipodespesa != null)
                carregaDespesa(res.tipodespesa);

        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });
}

function carregaDespesa(res) {

    var data = [{
        id: 0, text: 'Selecione o Tipo de Despesa'
    }];
    for (var i = 0; i < res.length; i++) {
        data.push({
            id: res[i].cod_tipo,
            text: res[i].descricao
        });
    }
    $("#txtTipoConta").select2({
        data: data
    })
}

function preview() {
    var tipodes = $("#txtTipoConta").val();
    var data_i = $("#txtDataInicial").val();
    var data_f = $("#txtDataFinal").val();
    var chkPagas = document.getElementById("chkPagas").checked;

    if (chkPagas) {
        chkPagas = true;
    }
    else {
        chkPagas = false;
    }

    if (moment(data_i).isAfter(data_f)) {
        Swal.fire({
            icon: 'warning',
            title: 'Data inicial não pode ser maior que data termino!'
        });
    }
    else {


        window.open('/QuitarContasPagar/Preview/?tipodes=' + tipodes + "&data_i=" + data_i + "&data_f=" + data_f + "&chkPagas=" + chkPagas, '_blank');
    }
}
function previewVencidas() {

        window.open('/QuitarContasPagar/PreviewVencidas/', '_blank');
}


inicializador.init();