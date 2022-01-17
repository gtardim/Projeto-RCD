
var inicializador = {
    init: function () {

        loadInicial();

    }
}

function loadInicial() {
    $.ajax({
        type: 'GET',
        url: '/LancarContasPagar/LoadInicial',
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
    $("#txtEditTipoDes").select2({
        data: data
    })
    $("#txtEstornarTipoDes").select2({
        data: data
    })

}

function obterAbertas() {

    var dataini = $("#txtPeridoIni").val();
    var datafim = $("#txtPeridoFim").val();

    if (datafim == "" && dataini != "" || dataini == "" && datafim == "" || datafim != "" && dataini != "") {
        $.ajax({
            type: 'GET',
            url: '/QuitarContasPagar/ObterAbertas',
            dataType: 'json',
            data: {
                dataini, datafim
            },
            success: function (res) {

                if (res.despesas != null) {
                    limparTabelaDespesa();
                    carregaTabelaPagar(res.despesas);
                }



            },
            error: function (XMLHttpRequest, txtStatus, errorThrown) {
                alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            }
        });

    }
    else {

        Swal.fire({
            icon:  'error',
            title: 'Quando informar data final informe  tambem data inicial!',
            showConfirmButton: false,
            timer: 5000
        });
    }



}

function obterPagas() {



    var dataini = $("#txtPeridoIni").val();
    var datafim = $("#txtPeridoFim").val();

    if (datafim == "" && dataini != "" || dataini == "" && datafim == "" || datafim != "" && dataini != "") {
        $.ajax({
            type: 'GET',
            url: '/QuitarContasPagar/ObterPagas',
            dataType: 'json',
            data: {
                dataini, datafim
            },
            success: function (res) {

                if (res.despesas != null) {
                    limparTabelaDespesa();
                    carregaTabelaEstornar(res.despesas);
                }



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


function carregaTabelaPagar(res) {
    var data = [];

    var action;
    var toggle;
    for (var i = 0; i < res.length; i++) {
        action = '<td> <button class="btn-outline-primary btn-rounded mb-4" onclick="showPagarModal(\'' + res[i].codigo + '\',\'' + res[i].datavenc + '\',\'' + res[i].descricao + '\',\'' + res[i].detalhes + '\',\'' + res[i].tipodespesa.cod_tipo + '\',\'' + res[i].valor + '\',\'' + res[i].valorpago + '\')">Pagar</button> </td> ';

        var dt = new Date(res[i].datavenc).toLocaleDateString();

        var valorFormatado = res[i].valor.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
        var valorFormatadoPago = res[i].valorpago.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });


        data.push([
            res[i].descricao,
            res[i].tipodespesa.descricao,
            dt,
            valorFormatado,
            valorFormatadoPago,
            action
        ]);
    }
    $('#tableDespesa').DataTable({
        data: data,
        "columnDefs": [
            { "visible": false, "targets": 4 }
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
        action = '<td> <button class="btn btn-danger mb-2" onclick="showEstornarModal(\'' + res[i].codigo + '\',\'' + res[i].datavenc + '\',\'' + res[i].descricao + '\',\'' + res[i].detalhes + '\',\'' + res[i].tipodespesa.cod_tipo + '\',\'' + res[i].valor + '\',\'' + res[i].valorpago + '\')">Estornar</button> </td> ';

        var dt = new Date(res[i].datavenc).toLocaleDateString();

        var valorFormatado = res[i].valor.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
        var valorFormatadoPago = res[i].valorpago.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });


        data.push([
            res[i].descricao,
            res[i].tipodespesa.descricao,
            dt,
            valorFormatado,
            valorFormatadoPago,
            action
        ]);
    }
    $('#tableDespesa').DataTable({
        data: data,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.21/i18n/Portuguese-Brasil.json"
        }


    });
}

function showPagarModal(codigo, datavenc, descricao, detalhes, cod_tipo, valor) {
    $("#txtEditCodigo").val(codigo);
    const date = new Date(datavenc).toLocaleDateString();
    $("#txtEditDataVenc").val(date);
    $("#txtEditDescricao").val(descricao);
    $("#txtEditDetalhe").val(detalhes);
    $("#txtEditTipoDes").val(cod_tipo).trigger("change.select2");
    $("#txtEditValor").val(valor);

    $('#pagarRowModal').modal('show');
}

function pagarTotal() {
    var codigo = $("#txtEditCodigo").val();
    var desc = $("#txtEditDescricao").val();
    var tipodes = $("#txtEditTipoDes").val();
    var valor = $("#txtEditValor").val();
    var detalhe = $("#txtEditDetalhe").val();
    var datavenc = $("#txtEditDataVenc").val();
    var valorpago = $("#txtValorPago").val();



    Swal.fire({
        title: 'Você tem certeza que deseja fazer pagamento Total',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sim, Pagamento Total!',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'POST',
                url: '/QuitarContasPagar/PagarTotal',
                data: {
                    codigo
                },

                success: function (res) {
                    if (res.ok == true) {
                        Swal.fire({
                            icon: 'success',
                            title: 'Conta Paga com Sucesso',
                            showConfirmButton: false,
                            timer: 1500
                        });
                        $('#pagarRowModal').modal('hide');

                        limparTabelaDespesa();
                        limpaCampos();
                        obterAbertas();

                    }
                },
                error: function (XMLHttpRequest, txtStatus, errorThrown) {
                    alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                }
            });

        }
    });

}

function pagarParcial() {
    var codigo = $("#txtEditCodigo").val();
    var datavenc = $("#txtEditDataVenc").val();
    var valorpago = $("#txtValorPago").val();
    var datavenc = $("#txtNovaDataVenc").val();


    Swal.fire({
        title: 'Você tem certeza que deseja fazer pagamento Parcial de:' + valorpago,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sim, Pagamento Parcial!',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'POST',
                url: '/QuitarContasPagar/PagarParcial',
                data: {
                    codigo, valorpago, datavenc
                },

                success: function (res) {
                    if (res.ok == true) {
                        Swal.fire({
                            icon: 'success',
                            title: 'Pagamento Parcial Efetuado com Sucesso',
                            showConfirmButton: false,
                            timer: 2500
                        });
                        $('#pagarRowModal').modal('hide');

                        limparTabelaDespesa();
                        limpaCampos();
                        obterAbertas();
                        

                    }
                },
                error: function (XMLHttpRequest, txtStatus, errorThrown) {
                    alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                }
            });

        }
    });

}

function showEstornarModal(codigo, datavenc, descricao, detalhes, cod_tipo, valor,valorpago) {
    $("#txtEstornarCodigo").val(codigo);
    const date = new Date(datavenc).toLocaleDateString();
    $("#txtEstornarDataVenc").val(date);
    $("#txtEstornarDescricao").val(descricao);
    $("#txtEstornarDetalhe").val(detalhes);
    $("#txtEstornarTipoDes").val(cod_tipo).trigger("change.select2");
    $("#txtEstornarValor").val(valor);
    $("#txtEstornarValorPago").val(valorpago);

    $('#estornarRowModal').modal('show');
}

function estornar() {
    var codigo = $("#txtEstornarCodigo").val();

    var para = 1;

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
                url: '/QuitarContasPagar/Estornar',
                data: {
                    codigo
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

                        limparTabelaDespesa();
                        obterPagas();
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

function limparTabelaDespesa() {
    $('#tableDespesa').DataTable().clear().destroy();
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
    var textarea = document.querySelectorAll('textarea');
    for (var i = 0; i < textarea.length; i++) {
        textarea[i].value = "";
    }

}



inicializador.init();