
var inicializador = {
    init: function () {

        loadInicial();

    }
}

function loadInicial() {
    $.ajax({
        type: 'GET',
        url: '/Andamento/LoadInicial',
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
    $("#txtCliente2").select2({
        data: data
    })

}

function carregaStatus(res) {

    var data = [{
        id: 0, text: 'Selecione o Novo Status'
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
    $("#txtNovoStatus").select2({
        data: data
    })

    $("#txtStatus2").select2({
        data: data
    })
    $("#txtNovoStatus2").select2({
        data: data
    })
}

function ObterProcessos() {

    
        $.ajax({
            type: 'GET',
            url: '/Andamento/ObterProcessos',
            dataType: 'json',
            success: function (res) {

                if (res.processos != null) {
                    limparTabela();
                    carregaTabelaProcessos(res.processos);
                }
                else
                    limparTabela();



            },
            error: function (XMLHttpRequest, txtStatus, errorThrown) {
                alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            }
        });

}

function obterProtocolos() {

        $.ajax({
            type: 'GET',
            url: '/Andamento/ObterProtocolos',
            dataType: 'json',
            success: function (res) {

                if (res.protocolos != null) {
                    limparTabela();
                    carregaTabelaProtocolos(res.protocolos);
                }
                else
                    limparTabela();



            },
            error: function (XMLHttpRequest, txtStatus, errorThrown) {
                alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            }
        });

}


function carregaTabelaProcessos(res) {
    var data = [];

    var action;
    var toggle;
    for (var i = 0; i < res.length; i++) {
        action = '<td> <button type="button" class="btn btn-outline-primary btn-rounded mb-2" onclick="showAndamentoModal(\'' + res[i].cod + '\',\'' + res[i].descricao + '\',\'' + res[i].cliente.cod + '\',\'' + res[i].status.cod_tipo + '\')">Alterar</button> </td> ';

        data.push([
            res[i].descricao,
            res[i].cliente.nome,
            res[i].status.descricao,
            action
        ]);
    }
    $('#tablePP').DataTable({
        data: data,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.21/i18n/Portuguese-Brasil.json"
        }


    });
}

function carregaTabelaProtocolos(res) {
    var data = [];

    var action;
    var toggle;
    for (var i = 0; i < res.length; i++) {
        action = '<td> <button  type="button" class="btn btn-outline-primary btn-rounded mb-2" onclick="showAndamento2Modal(\'' + res[i].cod + '\',\'' + res[i].descricao + '\',\'' + res[i].cliente.cod + '\',\'' + res[i].status.cod_tipo + '\')">Alterar</button> </td> ';

        data.push([
            res[i].descricao,
            res[i].cliente.nome,
            res[i].status.descricao,
            action
        ]);
    }
    $('#tablePP').DataTable({
        data: data,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.21/i18n/Portuguese-Brasil.json"
        }


    });
}

function showAndamentoModal(cod,descricao,cliente,status) {

    $("#txtCodigo").val(cod);
    $("#txtDescricao").val(descricao);
    $("#txtCliente").val(cliente).trigger("change.select2");
    $("#txtStatus").val(status).trigger("change.select2");

    $('#AndamentoModal').modal('show');
}

function showAndamento2Modal(cod, descricao, cliente, status) {

    $("#txtCodigo2").val(cod);
    $("#txtDescricao2").val(descricao);
    $("#txtCliente2").val(cliente).trigger("change.select2");
    $("#txtStatus2").val(status).trigger("change.select2");

    $('#Andamento2Modal').modal('show');
}

function alterarProcesso() {

    var codigo = $("#txtCodigo").val();
    var novostatus = $('#txtNovoStatus').val()

    Swal.fire({
        title: 'Você tem certeza que deseja Alterar o Status ?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sim, Desejo Alterar!',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {

            $.ajax({
                type: 'POST',
                url: '/Andamento/AlterarProcesso',
                data: {
                    codigo,novostatus
                },
                success: function (res) {
                    if (res.ok == true) {
                        Swal.fire({
                            icon: 'success',
                            title: 'Status Alterado com sucesso!',
                            showConfirmButton: false,
                            timer: 1500
                        });
                        $('#AndamentoModal').modal('hide');

                        limparTabela();
                        ObterProcessos();
                        limpaCampos();

                    }
                    else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Erro ao Alterar Status!',
                            showConfirmButton: false,
                            timer: 5900
                        });

                    }
                },
                error: function (XMLHttpRequest, txtStatus, errorThrown) {
                    alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                }
            });

        }
    });

}

function alterarProtocolo() {

    var codigo = $("#txtCodigo2").val();
    var novostatus = $('#txtNovoStatus2').val()

    Swal.fire({
        title: 'Você tem certeza que deseja Alterar o Status ?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sim, Desejo Alterar!',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {

            $.ajax({
                type: 'POST',
                url: '/Andamento/AlterarProtocolo',
                data: {
                    codigo, novostatus
                },
                success: function (res) {
                    if (res.ok == true) {
                        Swal.fire({
                            icon: 'success',
                            title: 'Status Alterado com sucesso!',
                            showConfirmButton: false,
                            timer: 1500
                        });
                        $('#Andamento2Modal').modal('hide');

                        limparTabela();
                        obterProtocolos();
                        limpaCampos();

                    }
                    else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Erro ao Alterar Status!',
                            showConfirmButton: false,
                            timer: 5900
                        });

                    }
                },
                error: function (XMLHttpRequest, txtStatus, errorThrown) {
                    alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                }
            });

        }
    });

}



function limparTabela() {
    $('#tablePP').DataTable().clear().destroy();
}

function limpaCampos() {
    var input = document.querySelectorAll('input');
    for (var i = 0; i < input.length; i++) {
        input[i].value = "";
    }
    var select = document.querySelectorAll('select');
    for (var i = 0; i < select.length; i++) {
        $('#' + select[i].id).val('0').trigger("change.select2");
    }

}



inicializador.init();