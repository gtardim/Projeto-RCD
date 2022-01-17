
var inicializador = {
    init: function () {

        loadInicial();

    }
}

function showNovoModal() {
    $('#addRowModal').modal('show');

}

function loadInicial() {
    $.ajax({
        type: 'GET',
        url: '/LancarContasPagar/LoadInicial',
        dataType: 'json',
        success: function (res) {
            if (res.tipodespesa != null)
                carregaDespesa(res.tipodespesa);
            if (res.despesas != null)
                carregaTabela(res.despesas);


        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });
}

function validaPagamento(codigo) {
    $.ajax({
        type: 'GET',
        url: '/LancarContasPagar/ValidaPagamento',
        dataType: 'json',
        data: {
            codigo
        },
        success: function (res) {

            excluir(codigo, res.msg);
           
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });
}





function gravar() {

    var desc = $("#txtDescricao").val();
    var tipodes = $("#txtTipoDes").val();
    var valor = $("#txtValor").val();
    var detalhe = $("#txtDetalhe").val();
    var datavenc = $("#txtDataVenc").val();
    

    var valida = validaDados();

    if (valida.msg == "") {

        $.ajax({

            type: 'POST',
            url: '/LancarContasPagar/Gravar',
            dataType: 'json',
            data: {
                desc, tipodes, valor, datavenc, detalhe
            },
            success: function (res) {

                if (res.ok == true) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Gravado com sucesso!',
                        showConfirmButton: false,
                        timer: 1500
                    });
                    $('#addRowModal').modal('hide');

                    limparTabelaDespesa();
                    loadInicial();
                    limpaCampos();

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
            title: 'Erro!' + "<br>" + valida.msg,
            showConfirmButton: false,
            timer: 15000
        });


    }




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
    $("#txtTipoDes").select2({
        data: data
    })
    $("#txtEditTipoDes").select2({
        data: data
    })

}

function obterTodos(){

    $.ajax({
        type: 'GET',
        url: '/LancarContasPagar/ObterTodos',
        dataType: 'json',
        success: function (res) {
        
            if (res.despesas != null)
                carregaTabela(res.despesas);


        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });

}


function carregaTabela(res) {
    var data = [];

    var action;
    var toggle;
    for (var i = 0; i < res.length; i++) {
        action = '<td> <button class="btn btn-outline-primary btn-rounded mb-2" onclick="showEditModal(\'' + res[i].codigo + '\',\'' + res[i].datavenc + '\',\'' + res[i].descricao + '\',\'' + res[i].detalhes + '\',\'' + res[i].tipodespesa.cod_tipo + '\',\'' + res[i].valor + '\')">Editar</button> </td> <td> <button class="btn btn-danger mb-2" onclick ="validaPagamento(' + res[i].codigo + ')">Excluir </button> </td>';

        var valorFormatado = res[i].valor.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });


        data.push([
            res[i].codigo,        
            res[i].descricao,
            valorFormatado,
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

function showEditModal(codigo, datavenc, descricao, detalhes, cod_tipo, valor) {

    $("#txtEditCodigo").val(codigo);
  
    $("#txtEditDescricao").val(descricao);
    $("#txtEditDetalhe").val(detalhes);
    $("#txtEditTipoDes").val(cod_tipo).trigger("change.select2");
    $("#txtEditValor").val(valor);
    

    
    //$("#txtEditDataVenc").val(date);

    $('#editRowModal').modal('show');

    const data = new Date(datavenc).toLocaleDateString();
    $("#txtEditDataVenc").flatpickr(
        {
            dateFormat: "d/m/Y",
            static: true
        }).setDate(data);

}

function alterar() {
    var codigo = $("#txtEditCodigo").val();
    var desc = $("#txtEditDescricao").val();
    var tipodes = $("#txtEditTipoDes").val();
    var valor = $("#txtEditValor").val();
    var detalhe = $("#txtEditDetalhe").val();
    var datavenc = $("#txtEditDataVenc").val();



    var valida = validaEditDados();

    if (valida.msg == "") {

        $.ajax({
            type: 'POST',
            url: '/LancarContasPagar/Alterar',
            data: {
                codigo, desc, tipodes, valor, detalhe, datavenc
            },

            success: function (res) {
                if (res.ok == true) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Alterado com sucesso!',
                        showConfirmButton: false,
                        timer: 1500
                    });
                    $('#editRowModal').modal('hide');

                    limparTabelaDespesa();
                    loadInicial();
                    limpaCampos();

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
            title: 'Erro!' + "<br>" + valida.msg,
            showConfirmButton: false,
            timer: 15000
        });


    }

   


         
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

function excluir(cod,msg) {


    if (msg == "")
    {

        Swal.fire({
            title: 'Você tem certeza?',
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
                    url: '/LancarContasPagar/Excluir',
                    data: {
                        cod
                    },
                    success: function (res) {
                        if (res.ok == true) {
                            Swal.fire(
                                'Excluido!',
                                'O registro foi excluido.',
                                'success'
                            )
                            limparTabelaDespesa();
                            loadInicial();
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
    else
    {
        Swal.fire({
            icon: 'error',
            title: msg,
            showConfirmButton: false,
            timer: 5000
        });
    }

}

function validaDados() {

    var desc = $("#txtDescricao").val();
    var tipodes = $("#txtTipoDes").val();
    var valor = $("#txtValor").val();
    var detalhe = $("#txtDetalhe").val();
    var datavenc = $("#txtDataVenc").val();


    var erro = "";

    if (desc.trim() == "") {

        erro += " Descrição nao Informada.<br>";
    }
    if (tipodes.trim() == 0) {

        erro += "Tipo Despesa Não informada.<br>";
    }
    if (valor.trim() == "") {

        erro += "Valor Não informado.<br>";
    }
    if (datavenc.trim() == "") {

        erro += "Data de Vencimento Não informada.<br>";
    }

    if (detalhe.trim() == "") {

        erro += "Detalhe nao Informado .<br>";
    }


    valida = {
        msg: erro,
        dados: {
            desc,
            tipodes,
            valor, 
            detalhe ,
            datavenc
        }
    }
    return valida;

}

function validaEditDados() {

    var desc = $("#txtEditDescricao").val();
    var tipodes = $("#txtEditTipoDes").val();
    var valor = $("#txtEditValor").val();
    var detalhe = $("#txtEditDetalhe").val();
    var datavenc = $("#txtEditDataVenc").val();


    var erro = "";

    if (desc.trim() == "") {

        erro += " Descrição nao Informada.<br>";
    }
    if (tipodes.trim() == 0) {

        erro += "Tipo Despesa Não informada.<br>";
    }
    if (valor.trim() == "") {

        erro += "Valor Não informado.<br>";
    }
    if (datavenc.trim() == "") {

        erro += "Data de Vencimento Não informada.<br>";
    }

    if (detalhe.trim() == "") {

        erro += "Detalhe nao Informado .<br>";
    }


    valida = {
        msg: erro,
        dados: {
            desc,
            tipodes,
            valor,
            detalhe,
            datavenc
        }
    }
    return valida;

}



inicializador.init();

$(document).ready(function () {

    $('#txtValor').mask('000.000.000.000.000,00', { reverse: true });
    $('#txtEditValor').mask('000.000.000.000.000,00', { reverse: true });

});
