var inicializador = {
    init: function () {
        obterTodos();
        $('#tableUsuario').on('change', 'input[type="checkbox"]', function () {
            if ($(this).prop("checked") == false)//Inativa Linha 
                desativaUsuario($(this).val());
            else if ($(this).prop("checked") == true) //Ativa Linha
                ativaUsuario($(this).val());
        });

    }
}

function ativaUsuario(codigo) {

    $.ajax({

        type: 'POST',
        url: '/Login/AtivaUsuario',
        dataType: 'json',
        data: {
            codigo
        },
        success: function (res) {
            if (res.ok == true) {
                Swal.fire({
                    icon: 'success',
                    title: 'Usuario Ativado!',
                    showConfirmButton: false,
                    timer: 1500
                });
            }
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });
}

function desativaUsuario(codigo) {

    $.ajax({

        type: 'POST',
        url: '/Login/DesativaUsuario',
        dataType: 'json',
        data: {
            codigo
        },
        success: function (res) {
            if (res.ok == true) {
                Swal.fire({
                    icon: 'success',
                    title: 'Usuario Desativado!',
                    showConfirmButton: false,
                    timer: 1500
                });
            }
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });
}

function limparTabela() {
    $('#tableUsuario').DataTable().clear().destroy();
}

function showEditModal(codigo,usuario) {
    $("#txtEditCodigo").val(codigo);
    $("#txtEditUsuario").val(usuario);
    $('#editRowModal').modal('show');
}



function showNovoModal() {
    $('#addRowModal').modal('show');
}

function validaUsuario() {

    var usuario = $('#txtUsuario').val()

    $.ajax({
        type: 'GET',
        url: '/Login/ValidaUsuario',
        dataType: 'json',
        data: {
            usuario
        },
        success: function (res) {
            if (res.dados == null)
                gravar();
            else {
                Swal.fire({
                    icon: 'error',
                    title: 'Erro!' + "<br>" + "Usuario Ja Existente !!",
                    showConfirmButton: false,
                    timer: 2200
                });

            }
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });
}

function validaUsuarioAlteração() {

    var usuario = $('#txtEditUsuario').val()

    $.ajax({
        type: 'GET',
        url: '/Login/ValidaUsuario',
        dataType: 'json',
        data: {
            usuario
        },
        success: function (res) {
            if (res.dados == null)
                alterar();
            else {
                Swal.fire({
                    icon: 'error',
                    title: 'Erro!' + "<br>" + "Usuario Ja Existente !!",
                    showConfirmButton: false,
                    timer: 2200
                });

            }
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });
}

function gravar() {

    var codigo = 0;
    var usuario = $('#txtUsuario').val()
    var senha = $('#txtSenha').val()
    var senha2 = $('#txtSenha2').val()

    var valida = validaDados(senha,senha2);

    if (valida.msg == "") {

        if (senha.length >= 6) {

            if (senha == senha2) {

                $.ajax({

                    type: 'POST',
                    url: '/Login/Gravar',
                    dataType: 'json',
                    data: {
                        codigo, usuario, senha
                    },
                    success: function (res) {
                        if (res.ok == true) {
                            Swal.fire({
                                icon: 'success',
                                title: 'Gravado com sucesso!',
                                showConfirmButton: false,
                                timer: 1500
                            });
                            $('#txtUsuario').val("");
                            $('#txtSenha').val("");
                            $('#txtSenha2').val("");
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
                    title: 'Erro!' + "<br>" + "As Senhas nao Conferem",
                    showConfirmButton: false,
                    timer: 2200
                });
            }

        }
        else
        {
            Swal.fire({
                icon: 'error',
                title: 'Erro!' + "<br>" + "A Senha de Conter mais de 5 Caracteres",
                showConfirmButton: false,
                timer: 2200
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

function validaDados(senha,senha2) {

    var usuario = $("#txtUsuario").val();



    var erro = "";

    if (usuario.trim() == "") {

        erro += " Usuario nao Informado.<br>";
    }
    if (senha.trim() == "") {

        erro += " Senha nao Informada.<br>";
    }
    if (senha2.trim() == "") {

        erro += " Confirmação da Senha nao Informada.<br>";
    }


    valida = {
        msg: erro,
        dados: {
            usuario,
            senha,
            senha2
        }
    }
    return valida;

}

function validaEditDados(senha,senha2) {

    var usuario = $("#txtEditUsuario").val();


    var erro = "";

    if (usuario.trim() == "") {

        erro += " Usuario nao Informado.<br>";
    }
    if (senha.trim() == "") {

        erro += " Senha nao Informada.<br>";
    }
    if (senha2.trim() == "") {

        erro += " Confirmação da Senha nao Informada.<br>";
    }


    valida = {
        msg: erro,
        dados: {
            usuario,
            senha,
            senha2
        }
    }
    return valida;

}

function alterar() {


    var codigo = $('#txtEditCodigo').val()
    var usuario = $('#txtEditUsuario').val()
    var senha = $('#txtEditSenha').val()
    var senha2 = $('#txtEditSenha2').val()

    var valida = validaEditDados(senha,senha2);

    if (valida.msg == "") {

        if (codigo != 1) {

            $.ajax({
                type: 'POST',
                url: '/Login/Alterar',
                data: {
                    codigo, usuario, senha, senha2
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
            if (usuario != "admin") {
                Swal.fire({
                    icon: 'error',
                    title: 'Erro!' + "<br>" + "Usuario admin so pode ser alterada a senha",
                    showConfirmButton: false,
                    timer: 2200
                });

            }
            else {
                $.ajax({
                    type: 'POST',
                    url: '/Login/Alterar',
                    data: {
                        codigo, usuario, senha, senha2
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
        url: '/Login/ObterTodos',
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




function excluir(codigo) {

    if (codigo != 1) {

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
                    url: '/Login/Excluir',
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
            title: 'Erro!' + "<br>" + "Usuario Admin nao pode ser excluido",
            showConfirmButton: false,
            timer: 2200
        });

    }
    
}

function carregaTabela(res) {
    var data = [];

    var action;
    var situacao;
    var toggle;
    for (var i = 0; i < res.length; i++) {
        action = '<td> <button class="btn btn-outline-primary btn-rounded mb-2" onclick="showEditModal(\'' + res[i].codigo + '\',\'' + res[i].usuario + '\',\'' + res[i].senha + '\',\'' + res[i].ativo + '\')">Editar</button> </td> <td> <button class="btn btn-danger mb-2" onclick ="excluir(' + res[i].codigo + ')">Excluir </button> </td>';

   
        situacao = '<input type="checkbox" checked>'

        if (res[i].ativo = true)
            situacao = '<label class="switch s-icons s-outline  s-outline-primary  mb-4 mr-2"><input type="checkbox" value="'+res[i].codigo+'" checked><span class="slider round"></span></label>';
        else
            situacao = '<label class="switch s-icons s-outline  s-outline-primary  mb-4 mr-2"><input type="checkbox" value="' + res[i].codigo +'"><span class="slider round"></span> </label>';

        data.push([
            res[i].codigo,
            res[i].usuario,
            situacao,
            action
        ]);
    }
    $('#tableUsuario').DataTable({
        data: data,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.21/i18n/Portuguese-Brasil.json"
        },
        "initComplete": function (settings, json) {
            $('.slider round').bootstrapSwitch();
        }

    });
}



inicializador.init();