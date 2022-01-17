var inicializador = {
    init: function () {
        obterTodos()

    }
}

function limparTabela() {
    $('#tableStatusPP').DataTable().clear().destroy();
}

function showEditModal(cod_tipo, descricao) {

    if (cod_tipo != 1) {

        if (cod_tipo != 2) {

            $("#txtEditCodigo").val(cod_tipo);
            $("#txtEditDescricao").val(descricao);
            $('#editRowModal').modal('show');

        }
        else {
            Swal.fire({
                icon: 'error',
                title: 'Erro!' + "<br>" + 'Nao é possivel Alterar o Item Finalizado',
                showConfirmButton: false,
                timer: 5200
            });

        }


    }
    else {

        Swal.fire({
            icon: 'error',
            title: 'Erro!' + "<br>" + 'Nao é possivel Alterar o Item Criado',
            showConfirmButton: false,
            timer: 5200
        });

    }


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
                    url: '/StatusPP/Gravar',
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


        if (cod_tipo != 1 ) {

            if (cod_tipo != 2) {

                $.ajax({
                    type: 'POST',
                    url: '/StatusPP/Alterar',
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
                    title: 'Erro!' + "<br>" + 'Nao é possivel Alterar o Item Finalizado',
                    showConfirmButton: false,
                    timer: 5200
                });

            }


        }
        else {

            Swal.fire({
                icon: 'error',
                title: 'Erro!' + "<br>" + 'Nao é possivel Alterar o Item Criado',
                showConfirmButton: false,
                timer: 5200
            });

        }
    
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
        url: '/StatusPP/ObterTodos',
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

function verificaExclusao(cod) {

    var msg = "";
    $.ajax({
        type: 'POST',
        url: '/StatusPP/VerificaExclusao',
        data: {
            cod
        },
        success: function (res) {
            if (res.processo == null && res.protocolo == null) {
                excluir(cod);
            }
            else {
                if (res.processo != null)
                    msg = msg + "Existem processos cadastrados com esse status.<br>";
                if (res.protocolo != null)
                    msg = msg + "Existem protocolos cadastrados com esse status.<br>";

                if (msg != "")
                    Swal.fire({
                        icon: 'error',
                        title: msg,
                        showConfirmButton: false,
                        timer: 6500
                    });
            }
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });
}

function excluir(cod_tipo) {


    if (cod_tipo != 1) {

        if (cod_tipo != 2) {


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
                        url: '/StatusPP/Excluir',
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
        else {
            Swal.fire({
                icon: 'error',
                title: 'Erro!' + "<br>" + 'Nao é possivel Excluir o Item Finalizado',
                showConfirmButton: false,
                timer: 5200
            });

        }


    }
    else {

        Swal.fire({
            icon: 'error',
            title: 'Erro!' + "<br>" + 'Nao é possivel Excluir o Item Criado',
            showConfirmButton: false,
            timer: 5200
        });

    }
}

function carregaTabela(res) {
    var data = [];

    var action;
    var toggle;
    for (var i = 0; i < res.length; i++) {
        action = '<td> <button class="btn btn-outline-primary btn-rounded mb-2" onclick="showEditModal(\'' + res[i].cod_tipo + '\',\'' + res[i].descricao + '\')">Editar</button> </td> <td> <button class="btn btn-danger mb-2" onclick ="verificaExclusao(' + res[i].cod_tipo + ')">Excluir </button> </td>';

        data.push([
            res[i].cod_tipo,
            res[i].descricao,
            action
        ]);
    }
    $('#tableStatusPP').DataTable({
        data: data,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.21/i18n/Portuguese-Brasil.json"
        }


    });
}



inicializador.init();