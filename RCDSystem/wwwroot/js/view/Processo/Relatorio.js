var inicializador = {
    init: function () {
        loadInicial()
    }
}

function loadInicial() {
    $.ajax({
        type: 'GET',
        url: '/Processo/LoadRelatorio',
        dataType: 'json',
        success: function (res) {
            if (res.cliente != null)
                carregaCliente(res.cliente);
            if (res.status != null)
                carregaStatus(res.status);

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

function carregaStatus(res) {

    var data = [{
        id: 0, text: 'Selecione o Status'
    }];
    for (var i = 0; i < res.length; i++) {
        data.push({
            id: res[i].cod_tipo,
            text: res[i].descricao
        });
    }
    $("#txtStatus").select2({
        data: data
    })
}

function preview() {
    var cliente = $("#txtCliente").val();
    var status = $("#txtStatus").val();
    var data_i = $("#txtDataInicial").val();
    var data_f = $("#txtDataFinal").val();

    if (data_i == "" && data_f != "") {
        Swal.fire({
            icon: 'warning',
            title: 'Data inicial não pode ser nula e a Final Preenchida!'
        });
    } else {

        if (moment(data_i).isAfter(data_f)) {
            Swal.fire({
                icon: 'warning',
                title: 'Data inicial não pode ser maior que data termino!'
            });
        }
        else {


            window.open('/Processo/Preview/?cliente=' + cliente + "&status=" + status + "&data_i=" + data_i + "&data_f=" + data_f, '_blank');
        }
    }

   
}

inicializador.init();