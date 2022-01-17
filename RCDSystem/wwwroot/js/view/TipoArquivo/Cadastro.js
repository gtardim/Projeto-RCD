var inicializador = {
    init: function () {
        obterTodos()

    }
}

function limparTabela() {
    $('#tableTipoArquivo').DataTable().clear().destroy();
}

function showEditModal(cod_tipo, descricao) {
    $("#txtEditCodigo").val(cod_tipo);
    $("#txtEditDescricao").val(descricao);
    $('#editRowModal').modal('show');
}



function showNovoModal() {
    $('#addRowModal').modal('show');
}

function gravar() {
    var cod_tipo = 0;
    var descricao = $('#txtNovoDescricao').val();

    var valida = validaDados();

    if (valida.msg == "") {

        $.ajax({

            type: 'POST',
            url: '/TipoArquivo/Gravar',
            dataType: 'json',
            data: {
                cod_tipo, descricao
            },
            success: function (res) {
                if (res.ok == true) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Gravado com sucesso!',
                        showConfirmButton: false,
                        timer: 1500
                    });
                    $('#txtNovoDescricao').val("");
                    $('#addRowModal').modal('hide');
                    limparTabela();
                    obterTodos();

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
            timer: 2200
        });
    }

}

function validaDados() {

    var desc = $("#txtNovoDescricao").val();



    var erro = "";

    if (desc.trim() == "") {

        erro += " Descrição nao Informada.<br>";
    }


    valida = {
        msg: erro,
        dados: {
            desc
        }
    }
    return valida;

}

function validaEditDados() {

    var desc = $("#txtEditDescricao").val();



    var erro = "";

    if (desc.trim() == "") {

        erro += " Nova Descrição nao Informada.<br>";
    }


    valida = {
        msg: erro,
        dados: {
            desc
        }
    }
    return valida;

}

function alterar() {
    var cod_tipo = $('#txtEditCodigo').val();
    var descricao = $("#txtEditDescricao").val();

    var valida = validaEditDados();

    if (valida.msg == "") {


        $.ajax({
            type: 'POST',
            url: '/TipoArquivo/Alterar',
            data: {
                cod_tipo, descricao
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
                    limparTabela();
                    obterTodos();
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
            timer: 2200
        });
    }
}
function obterTodos() {
    $.ajax({
        type: 'GET',
        url: '/TipoArquivo/ObterTodos',
        dataType: 'json',
        success: function (res) {
            if (res.dados != null)
                carregaTabela(res.dados);
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });
}

function validaExclusao(cod_tipo) {
            $.ajax({
                type: 'POST',
                url: '/TipoArquivo/ValidaExclusao',
                data: {
                    cod_tipo
                },
                success: function (res) {
                    if (res.tipoarq == null) {
                        excluir(cod_tipo);
                    }
                    else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Erro!' + "<br>" + "Tipo de Arquivo Possui Arquivo Cadastrado",
                            showConfirmButton: false,
                            timer: 2999
                        });
                    }
                },
                error: function (XMLHttpRequest, txtStatus, errorThrown) {
                    alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                }
            });
}


function excluir(cod_tipo) {
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
                url: '/TipoArquivo/Excluir',
                data: {
                    cod_tipo
                },
                success: function (res) {
                    if (res.ok == true) {
                        Swal.fire(
                            'Excluido!',
                            'O registro foi excluido.',
                            'success'
                        )
                        limparTabela();
                        obterTodos();
                    }
                },
                error: function (XMLHttpRequest, txtStatus, errorThrown) {
                    alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                }
            });
        }
    });
}

function carregaTabela(res) {
    var data = [];

    var action;
    var toggle;
    for (var i = 0; i < res.length; i++) {
        action = '<td> <button class="btn btn-outline-primary btn-rounded mb-2" onclick="showEditModal(\'' + res[i].cod_tipo + '\',\'' + res[i].descricao + '\')">Editar</button> </td> <td> <button class="btn btn-danger mb-2" onclick ="validaExclusao(' + res[i].cod_tipo + ')">Excluir </button> </td>';

        data.push([
            res[i].cod_tipo,
            res[i].descricao,
            action
        ]);
    }
    $('#tableTipoArquivo').DataTable({
        data: data,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.21/i18n/Portuguese-Brasil.json"
        }


    });
}



inicializador.init();