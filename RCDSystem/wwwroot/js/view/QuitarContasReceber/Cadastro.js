
var inicializador = {
    init: function () {

        loadInicial();

    }
}

function loadInicial() {
    $.ajax({
        type: 'GET',
        url: '/QuitarContasReceber/LoadInicial',
        dataType: 'json',
        success: function (res) {
            if (res.cliente != null)
                carregaCliente(res.cliente);

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
    $("#txtEditCliente").select2({
        data: data
    })

    

}

function obterAbertas() {

    var dataini = $("#txtPeridoIni").val();
    var datafim = $("#txtPeridoFim").val();

    if (datafim == "" && dataini != "" || dataini == "" && datafim == "" || datafim != "" && dataini != "") {
        $.ajax({
            type: 'GET',
            url: '/QuitarContasReceber/ObterAbertas',
            dataType: 'json',
            data: {
                dataini, datafim
            },
            success: function (res) {

                if (res.recebimentos != null) {
                    limparTabelaRecebimentos();
                    carregaTabelaBaixar(res.recebimentos);
                }
                else
                    limparTabelaRecebimentos();



            },
            error: function (XMLHttpRequest, txtStatus, errorThrown) {
                alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            }
        });

    }
    else {

        Swal.fire({
            icon: 'error',
            title: 'Quando informar data final informe  tambem data inicial!',
            showConfirmButton: false,
            timer: 5000
        });
    }



}

function obterBaixadas() {



    var dataini = $("#txtPeridoIni").val();
    var datafim = $("#txtPeridoFim").val();

    if (datafim == "" && dataini != "" || dataini == "" && datafim == "" || datafim != "" && dataini != "") {
        $.ajax({
            type: 'GET',
            url: '/QuitarContasReceber/ObterBaixadas',
            dataType: 'json',
            data: {
                dataini, datafim
            },
            success: function (res) {

                if (res.recebimentos != null) {
                    limparTabelaRecebimentos();
                    carregaTabelaEstornar(res.recebimentos);
                }
                else
                    limparTabelaRecebimentos();



            },
            error: function (XMLHttpRequest, txtStatus, errorThrown) {
                alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            }
        });

    }
    else {

        Swal.fire({
            icon: 'error',
            title: 'Quando informar data final informe  tambem data inicial!',
            showConfirmButton: false,
            timer: 5000
        });
    }




}

function excluir(codigo) {
    Swal.fire({
        title: 'Você tem certeza que deseja excluir essa parcela ?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sim, eu quero deletar!',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'POST',
                url: '/QuitarContasPagar/ExcluirParcela',
                data: {
                    codigo
                },
                success: function (res) {
                    if (res.ok == true) {
                        Swal.fire(
                            'Excluido!',
                            'O registro foi excluido.',
                            'success'
                        )
                        limparTabelaDespesa();
                        obterAbertas;
                        limpaCampos();
                    }
                },
                error: function (XMLHttpRequest, txtStatus, errorThrown) {
                    alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                }
            });
        }
    });
}


function carregaTabelaBaixar(res) {
    var data = [];

    var action;
    var toggle;
    for (var i = 0; i < res.length; i++) {
        action = '<td> <button type="button" class="btn-outline-primary btn-rounded mb-4" onclick="showPagarModal(\'' + res[i].codigo + '\',\'' + res[i].descricao + '\',\'' + res[i].cliente.cod + '\',\'' + res[i].valor + '\',\'' + res[i].datageracao + '\',\'' + res[i].tipo + '\',\'' + res[i].pago + '\')">Receber</button> </td> ';

        var dt = new Date(res[i].datageracao).toLocaleDateString();
        var dt2 = new Date(res[i].pago).toLocaleDateString();

        var dt = new Date(res[i].datavenc).toLocaleDateString();

        var valorFormatado = res[i].valor.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
        var valorFormatadoPago = res[i].valorpago.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });

        data.push([
            res[i].descricao,
            res[i].cliente.nome,
            dt,
            valorFormatado,
            valorFormatadoPago,
            dt2,
            action
        ]);
    }
    $('#tableRecebimento').DataTable({
        data: data,
        "columnDefs": [
            { "visible": false, "targets": 4 },
            { "visible": false, "targets": 5 }
        ],

        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.21/i18n/Portuguese-Brasil.json"
        }


    });
}

function carregaTabelaEstornar(res) {
    var data = [];

    var action;
    var toggle;
    for (var i = 0; i < res.length; i++) {
        action = '<td> <button  type="button" class=" btn-danger mb-2" onclick="showEstornarModal(\'' + res[i].codigo + '\',\'' + res[i].descricao + '\',\'' + res[i].cliente.cod + '\',\'' + res[i].valorpago + '\',\'' + res[i].datageracao + '\',\'' + res[i].tipo + '\',\'' + res[i].pago + '\')">Estornar</button> <button  type="button" class="btn-primary mb-2" onclick="emitirRecibo(\'' + res[i].codigo +'\')">Recibo</button> </td> ';

        var dt = new Date(res[i].datageracao).toLocaleDateString();
        var dt2 = new Date(res[i].pago).toLocaleDateString();

        var valorFormatado = res[i].valor.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
        var valorFormatadoPago = res[i].valorpago.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });

        data.push([
            res[i].descricao,
            res[i].cliente.nome,
            dt,
            valorFormatado,
            valorFormatadoPago,
            dt2,
            action
        ]);
    }
    $('#tableRecebimento').DataTable({
        data: data,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.21/i18n/Portuguese-Brasil.json"
        }


    });
}

function showPagarModal(codigo,descricao,cliente,valor,datageracao,tipo) {
    $("#txtEditCodigo").val(codigo);
    $("#txtEditTipo").val(tipo);
    const dt = new Date(datageracao).toLocaleDateString();
    $("#txtEditDataGeracao").flatpickr(
        {
            dateFormat: "d/m/Y",
            static: true
        }).setDate(dt);

    $("#txtEditDescricao").val(descricao);
    $("#txtCliente").val(cliente).trigger("change.select2");
    $("#txtEditValor").val(valor);

    $('#pagarRowModal').modal('show');
}

function receberTotal() {
    var codigo = $("#txtEditCodigo").val();
    var tipo = $("#txtEditTipo").val();

    Swal.fire({
        title: 'Você tem certeza que deseja fazer Recebimento Total',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sim, Recebimento Total!',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'POST',
                url: '/QuitarContasReceber/ReceberTotal',
                data: {
                    codigo,tipo
                },

                success: function (res) {
                    if (res.ok == true) {
                        Swal.fire({
                            icon: 'success',
                            title: 'Conta Recebida com Sucesso',
                            showConfirmButton: false,
                            timer: 1500
                        });
                        $('#pagarRowModal').modal('hide');

                        limparTabelaRecebimentos();
                        obterAbertas();
                        limpaCampos();


                    }
                },
                error: function (XMLHttpRequest, txtStatus, errorThrown) {
                    alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                }
            });

        }
    });

}

function receberParcial() {
    var codigo = $("#txtEditCodigo").val();
    var datareceb = $("#txtNovaDataRecebimento").val();
    var valorpago = $("#txtValorAReceber").val();
    var tipo = $("#txtEditTipo").val();


    Swal.fire({
        title: 'Você tem certeza que deseja fazer o recebimento Parcial de:' + valorpago,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sim, Recebimento Parcial!',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'POST',
                url: '/QuitarContasReceber/RecebimentoParcial',
                data: {
                    codigo, valorpago, datareceb,tipo
                },

                success: function (res) {
                    if (res.ok == true) {
                        Swal.fire({
                            icon: 'success',
                            title: 'Recebimento Parcial Efetuado com Sucesso',
                            showConfirmButton: false,
                            timer: 2500
                        });
                        $('#pagarRowModal').modal('hide');

                        limparTabelaRecebimentos();
                        obterAbertas();
                        limpaCampos();


                    }
                },
                error: function (XMLHttpRequest, txtStatus, errorThrown) {
                    alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                }
            });

        }
    });

}

function showEstornarModal(codigo, descricao, cliente, valorpago, datageracao,tipo) {

    $("#txtCodigo").val(codigo);
    $("#txtEstTipo").val(tipo);
    const dt = new Date(datageracao).toLocaleDateString();
    $("#txtDataGeracao").flatpickr(
        {
            dateFormat: "d/m/Y",
            static: true
        }).setDate(dt);

    $("#txtDescricao").val(descricao);
    $("#txtEditCliente").val(cliente).trigger("change.select2");
    $("#txtValor").val(valorpago);

    $('#estornarRowModal').modal('show');
}

function emitirRecibo(codigo) {

    if (codigo != "")
    {
        window.open('/QuitarContasReceber/Recibo/?codigo=' + codigo, '_blank');
    }

}



function estornar() {
    var codigo = $("#txtCodigo").val();

    var tipo = $("#txtEstTipo").val();

    Swal.fire({
        title: 'Você tem certeza que deseja ESTORNAR ?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sim, Desejo Estornar!',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {

            $.ajax({
                type: 'POST',
                url: '/QuitarContasReceber/Estornar',
                data: {
                    codigo,tipo
                },
                success: function (res) {
                    if (res.ok == true) {
                        Swal.fire({
                            icon: 'success',
                            title: 'Estornado com sucesso!',
                            showConfirmButton: false,
                            timer: 1500
                        });
                        $('#estornarRowModal').modal('hide');

                        limparTabelaRecebimentos();
                        obterBaixadas();
                        limpaCampos();

                    }
                    else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Recebimento Possui Parcela Posterior Baixada , Favor Cancelar a baixa da mesma!',
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

function limparTabelaRecebimentos() {
    $('#tableRecebimento').DataTable().clear().destroy();
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