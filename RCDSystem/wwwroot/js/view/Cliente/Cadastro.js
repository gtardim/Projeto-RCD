var inicializador = {
    init: function () {
        limparTabelaCliente();
        

        obterTodos();
        
    }
}

function carregaEstado() {
    $.ajax({
        type: 'GET',
        url: '/Estado/ObterTodos',
        success: function (res) {
            var data = [{
                id: 0, text: 'Selecione um Estado'
            }];
            for (var i = 0; i < res.dados.length; i++) {
                data.push({
                    id: res.dados[i].cod_tipo,
                    text: res.dados[i].descricao
                });
            }
            $("#txtEstado").select2({
                data: data
            })
            $("#txtEditEstado").select2({
                data: data
            })
        }
    });

   $('#txtEstado').on("change", function (e) {
        var val = $(this).val();
        carregaCidade(val);
    });
    $('#txtEditEstado').on("change", function (e) {
        var val = $(this).val();
        carregaEditCidade(val); 
    }); 
}

function carregaCidade(estado) {
    $('#txtCidade').select2({ allowClear: true });
    $('#txtCidade').empty().trigger('change');

    var id;
    if (estado == "")
        id = document.getElementById("txtEstado").value;
    else
        id = estado;
    $.ajax({
        type: 'GET',
        url: '/Cidade/ObterCidades', data: {
            id
        },
        success: function (res) {
            var data = [{
                id: 0, text: 'Selecione uma Cidade'
            }];
            for (var i = 0; i < res.length; i++) {
                data.push({
                    id: res[i].cod,
                    text: res[i].descricao
                });
            }
            $("#txtCidade").select2({
                data: data
            })
            $("#txtEditCidade").select2({
                data: data
            })
        }
    });
}


function carregaEditCidade(estado,cidade) {
    $('#txtEditCidade').select2({ allowClear: true });
    $('#txtEditCidade').empty().trigger('change');

    var id;
    if (estado == "")
        id = document.getElementById("txtEditEstado").value;
    else
        id = estado;
    $.ajax({
        type: 'GET',
        url: '/Cidade/ObterCidades', data: {
            id
        },
        success: function (res) {
            var data = [{
                id: 0, text: 'Selecione uma Cidade'
            }];
            for (var i = 0; i < res.length; i++) {
                data.push({
                    id: res[i].cod,
                    text: res[i].descricao
                });
            }
            $("#txtEditCidade").select2({
                data: data
            })
            $("#txtEditCidade").val(cidade).trigger("change.select2");
        }
    });

}

function limparTabelaCliente() {
    $('#tableCliente').DataTable().clear().destroy();
}

function showEditModal(cod, nome, nacionalidade, estadocivil, RG, CPF, CNPJ, rua, bairro, numero, estado, cidade, CEP, email, contato, representante, tipocliente) 
{
    $("#txtEditCodigo").val(cod);
    $("#txtEditNomeCliente").val(nome);
    $("#txtEditNacionalidade").val(nacionalidade);
    $("#txtEditEstadoCivil").val(estadocivil);
    $("#txtEditRG").val(RG);
    $("#txtEditRua").val(rua);
    $("#txtEditBairro").val(bairro);
    $("#txtEditNumero").val(numero);
    $("#txtEditEstado").val(estado).trigger("change.select2");
    carregaEditCidade(estado,cidade);
    $("#txtEditCEP").val(CEP);
    $("#txtEditEmail").val(email);
    $("#txtEditContato").val(contato);
    //$("#txtFoto").val(foto);
    $("#txtEditRepresentante").val(representante);

    if (tipocliente == 1) {
        EditchkCPF.checked = true;
        EditchkCNPJ.checked = false;
        $("#txtEditCPFCNPJ").val(CPF);
    }

    else
    {
        EditchkCPF.checked = false;
        EditchkCNPJ.checked = true;
        $("#txtEditCPFCNPJ").val(CNPJ);
    }
        
   
    $('#editRowModal').modal('show');
}

function enviaDocumento (id) {

    var fd = new FormData();
    var foto = document.getElementById("txtFoto").files[0];
    var arquivo;

    if (foto != null) {
        arquivo = true;
    }

    if (arquivo) {
        fd.append("Foto", foto);


        var configFD = {
            method: "POST",
            headers: {
                "Accept": "application/json"
            },
            body: fd
        }

        fetch("/Cadastro/GravarFoto", configFD)
            .then(function (res) {
                var obj = res.json(); //deserializando
                return obj;
            })
            .then(function (res) {
                if (res.ok) {
                    window.location.href = '/GerenciarEmpresa/EditarFuncionario?empresa=' + getParametro("empresa") + '&id=' + id + '&novo=true';

                } else {
                    erroBanco('Erro ao cadastrar funcionário!', res.msg);
                }
                ocultaLoading();
            })
            .catch(function () {

            })
    } else {
        abreModal("success", "Cadastrado com sucesso!");
        ocultaLoading();
    }
}


function showNovoModal() {
        $('#addRowModal').modal('show');
        
}

function gravar() {
    var nome = $("#txtNomeCliente").val();
    var nacionalidade = $("#txtNacionalidade").val();
    var estadocivil = $("#txtEstadoCivil").val();
    var RG = $("#txtRG").val();
    var CPFCNPJ = $("#txtCPFCNPJ").val();
    var rua = $("#txtRua").val();
    var bairro = $("#txtBairro").val();
    var numero = $("#txtNumero").val();
    var cidade = $("#txtCidade").val();
    var CEP = $("#txtCEP").val();
    var email = $("#txtEmail").val();
    var contato = $("#txtContato").val();
    var representante = $("#txtRepresentante").val();
    var chekCPF = document.getElementById("chkCPF").checked;

    var valida = validaDados();

    if (valida.msg == "") {

        if (chekCPF.checked) 
            chekCPF = true;

        $.ajax({

            type: 'POST',
            url: '/Cliente/Gravar',
            dataType: 'json',
            data: {
                nome, nacionalidade, estadocivil, RG, CPFCNPJ, rua, bairro, numero, cidade, CEP, email, contato, representante, chekCPF
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

                    limparTabelaCliente();
                    obterTodos();
                    limpaCampos();

                }
            },
            error: function (XMLHttpRequest, txtStatus, errorThrown) {
                alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            }

        });

    }
    else
    {

        Swal.fire({
            icon: 'error',
            title: 'Erro!'+"<br>" + valida.msg,
            showConfirmButton: false,
            timer: 15000
        });
        
       
    }

}

function validaDados() {

    var nome = $("#txtNomeCliente").val();
    var nacionalidade = $("#txtNacionalidade").val();
    var estadocivil = $("#txtEstadoCivil").val();
    var RG = $("#txtRG").val();
    var CPFCNPJ = $("#txtCPFCNPJ").val();
    var rua = $("#txtRua").val();
    var bairro = $("#txtBairro").val();
    var numero = $("#txtNumero").val();
    var cidade = $("#txtCidade").val();
    var CEP = $("#txtCEP").val();
    var email = $("#txtEmail").val();
    var contato = $("#txtContato").val();
    var representante = $("#txtRepresentante").val();
    var chekCPF = document.getElementById("chkCPF").checked;

  
    var erro = "";

    if (nome.trim() == "") {

        erro += " Nome nao Informado.<br>";
    }
    if (nacionalidade.trim() == "") {

        erro += "Nacionalidade Não informada.<br>";
    }
    if (estadocivil.trim() == "") {

        erro += "Estado Civil Não informado.<br>";
    }
    if (RG.trim() == "") {

        erro += "RG Não informado.<br>";
    }
    if (CPFCNPJ.trim() == "") {

        erro += "CPF ou CNPJ Não informado.<br>";
    }

    if (rua.trim() == "") {

        erro += "Rua Não informada.<br>";
    }
    if (bairro.trim() == "") {

        erro += "Bairro Não informado.<br>";
    }
    if (numero.trim() == "") {

        erro += "Numero Não informado.<br>";
    }
    if (cidade == null) {

        erro += "Cidade Não informada.<br>";
    }
    if (CEP.trim() == "") {

        erro += "CEP Não informado.<br>";
    }
    if (email.trim() == "") {

        erro += "E-mail Não informado.<br>";
    }
    if (contato.trim() == "") {

        erro += "Contato Não informado.<br>";
    }


   
 
    valida = {
        msg: erro,
        dados: {
             nome,
            nacionalidade,
            estadocivil,
            RG,
            CPFCNPJ,
            rua,
            bairro,
            numero,
            cidade,
            CEP,
            email ,
            contato,
        }
    }
    return valida;

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

function alterar() {
    var cod = $("#txtEditCodigo").val();
    var nome = $("#txtEditNomeCliente").val();
    var nacionalidade = $("#txtEditNacionalidade").val();
    var estadocivil = $("#txtEditEstadoCivil").val();
    var RG = $("#txtEditRG").val();
    var CPFCNPJ = $("#txtEditCPFCNPJ").val();
    var rua = $("#txtEditRua").val();
    var bairro = $("#txtEditBairro").val();
    var numero = $("#txtEditNumero").val();
    var cidade = $("#txtEditCidade").val();
    var CEP = $("#txtEditCEP").val();
    var email = $("#txtEditEmail").val();
    var contato = $("#txtEditContato").val();
    var representante = 0;
    var editchekCPF = document.getElementById("EditchkCPF").checked; 

    var valida = validaEditDados();

    if (valida.msg == "") {


        $.ajax({
            type: 'POST',
            url: '/Cliente/Alterar',
            data: {
                cod, nome, nacionalidade, estadocivil, RG, CPFCNPJ, rua, bairro, numero, cidade, CEP, email, contato, representante, editchekCPF
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

                    limparTabelaCliente();
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
            timer: 15000
        });

    }
}


function validaEditDados() {

    var nome = $("#txtEditNomeCliente").val();
    var nacionalidade = $("#txtEditNacionalidade").val();
    var estadocivil = $("#txtEditEstadoCivil").val();
    var RG = $("#txtEditRG").val();
    var CPFCNPJ = $("#txtEditCPFCNPJ").val();
    var rua = $("#txtEditRua").val();
    var bairro = $("#txtEditBairro").val();
    var numero = $("#txtEditNumero").val();
    var cidade = $("#txtEditCidade").val();
    var CEP = $("#txtEditCEP").val();
    var email = $("#txtEditEmail").val();
    var contato = $("#txtEditContato").val();
    var representante = $("#txtEditRepresentante").val();
    var chekCPF = document.getElementById("EditchkCPF").checked;


    var erro = "";

    if (nome.trim() == "") {

        erro += " Nome nao Informado.<br>";
    }
    if (nacionalidade.trim() == "") {

        erro += "Nacionalidade Não informada.<br>";
    }
    if (estadocivil.trim() == "") {

        erro += "Estado Civil Não informado.<br>";
    }
    if (RG.trim() == "") {

        erro += "RG Não informado.<br>";
    }
    if (CPFCNPJ.trim() == "") {

        erro += "CPF ou CNPJ Não informado.<br>";
    }

    if (rua.trim() == "") {

        erro += "Rua Não informada.<br>";
    }
    if (bairro.trim() == "") {

        erro += "Bairro Não informado.<br>";
    }
    if (numero.trim() == "") {

        erro += "Numero Não informado.<br>";
    }
    if (cidade == null) {

        erro += "Cidade Não informada.<br>";
    }
    if (CEP.trim() == "") {

        erro += "CEP Não informado.<br>";
    }
    if (email.trim() == "") {

        erro += "E-mail Não informado.<br>";
    }
    if (contato.trim() == "") {

        erro += "Contato Não informado.<br>";
    }


    valida = {
        msg: erro,
        dados: {
            nome,
            nacionalidade,
            estadocivil,
            RG,
            CPFCNPJ,
            rua,
            bairro,
            numero,
            cidade,
            CEP,
            email,
            contato,
        }
    }
    return valida;

}

function obterTodos() {
    $.ajax({
        type: 'GET',
        url: '/Cliente/ObterTodos',
        dataType: 'json',
        success: function (res) {
            if (res.dados != null)
                carregaTabela(res.dados);
            carregaEstado();
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });
}

function verificaCPFexistente(cpf) {

    var chekCPF = document.getElementById("chkCPF").checked;
    var CPFCNPJ = $("#txtCPFCNPJ").val();

    if (chekCPF == true) {
        $.ajax({
            type: 'POST',
            url: '/Cliente/VerificaCPFexistente',
            data: {
                CPFCNPJ
            },
            success: function (res) {
                if (res.cpf != null) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Erro!' + "<br>" + "CPF Ja Existente no Cadastro",
                        showConfirmButton: false,
                        timer: 15000
                    });

                }
                else
                    gravar();

            },
            error: function (XMLHttpRequest, txtStatus, errorThrown) {
                alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            }
        });
    }
    else {
        $.ajax({
            type: 'POST',
            url: '/Cliente/VerificaCNPJexistente',
            data: {
                CPFCNPJ
            },
            success: function (res) {
                if (res.cnpj != null) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Erro!' + "<br>" + "CNPJ Ja Existente no Cadastro",
                        showConfirmButton: false,
                        timer: 15000
                    });

                }
                else
                    gravar();

            },
            error: function (XMLHttpRequest, txtStatus, errorThrown) {
                alert("Status: " + txtStatus); alert("Error: " + errorThrown);
            }
        });
    } 
}

function verificaExclusao(cod) {

    var msg = "";
            $.ajax({
                type: 'POST',
                url: '/Cliente/VerificaExclusao',
                data: {
                    cod
                },
                success: function (res) {
                    if (res.arq == null && res.processo == null && res.protocolo == null) {
                        excluir(cod);
                    }
                    else {
                        if (res.arq != null)
                            msg = msg + "Existem arquivos cadastrados nesse cliente.<br>";
                        if (res.processo != null)
                            msg = msg + "Existem processos cadastrados para esse cliente.<br>";
                        if (res.protocolo != null)
                            msg = msg + "Existem protocolos cadastrados para esse cliente.<br>";

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


function excluir(cod) {
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
                url: '/Cliente/Excluir',
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
                        limparTabelaCliente();
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
        action = '<td> <button class="btn btn-outline-primary btn-rounded mb-2" onclick="showEditModal(\'' + res[i].cod + '\',\'' + res[i].nome + '\',\'' + res[i].nacionalidade + '\',\'' + res[i].estadocivil + '\',\'' + res[i].rg + '\',\'' + res[i].cpf + '\',\'' + res[i].cnpj + '\',\'' + res[i].rua + '\',\'' + res[i].bairro + '\',\'' + res[i].numero + '\',\'' + res[i].cidade.estado + '\',\'' + res[i].cidade.cod + '\',\'' + res[i].cep + '\',\'' + res[i].email + '\',\'' + res[i].contato + '\',\'' + res[i].representante + '\',\'' + res[i].tipocliente + '\')">Editar</button> </td> <td> <button class="btn btn-danger mb-2" onclick ="verificaExclusao(' + res[i].cod + ')">Excluir </button> </td>';

        data.push([
            res[i].cod,
            res[i].nome,
            action
        ]);
    }
    $('#tableCliente').DataTable({
        data: data,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.21/i18n/Portuguese-Brasil.json"
        }


    });
}

function mascaraCPF(chkCPF){

    
    if (chkCPF == 'on') {
        document.getElementById("txtCPFCNPJ").value = "";
        $("#txtCPFCNPJ").mask('###.###.###-##', { reverse: true });
    }
    
}

function mascaraEditCPF(EditchkCPF) {


    if (EditchkCPF == 'on') {
        document.getElementById("txtEditCPFCNPJ").value = "";
        $("#txtEditCPFCNPJ").mask('###.###.###-##', { reverse: true });
    }

}

function mascaraCNPJ(chkCNPJ) {

   

    if (chkCNPJ == 'on') {
        document.getElementById("txtCPFCNPJ").value = "";
        $("#txtCPFCNPJ").mask('##.###.###/####-##', { reverse: true });
    }

}
function mascaraEditCNPJ(EditchkCNPJ) {



    if (EditchkCNPJ == 'on') {
        document.getElementById("txtEditCPFCNPJ").value = "";
        $("#txtEditCPFCNPJ").mask('##.###.###/####-##', { reverse: true });
    }

}

function validarCPF(cpf) {
    cpf = cpf.replace(/[^\d]+/g, '');
    if (cpf == '') return false;
    // Elimina CPFs invalidos conhecidos	
    if (cpf.length != 11 ||
        cpf == "00000000000" ||
        cpf == "11111111111" ||
        cpf == "22222222222" ||
        cpf == "33333333333" ||
        cpf == "44444444444" ||
        cpf == "55555555555" ||
        cpf == "66666666666" ||
        cpf == "77777777777" ||
        cpf == "88888888888" ||
        cpf == "99999999999")
        return false;
    // Valida 1o digito	
    add = 0;
    for (i = 0; i < 9; i++)
        add += parseInt(cpf.charAt(i)) * (10 - i);
    rev = 11 - (add % 11);
    if (rev == 10 || rev == 11)
        rev = 0;
    if (rev != parseInt(cpf.charAt(9)))
        return false;
    // Valida 2o digito	
    add = 0;
    for (i = 0; i < 10; i++)
        add += parseInt(cpf.charAt(i)) * (11 - i);
    rev = 11 - (add % 11);
    if (rev == 10 || rev == 11)
        rev = 0;
    if (rev != parseInt(cpf.charAt(10)))
        return false;
    return true;
}



inicializador.init();

$(document).ready(function () {
    //$("#quantParcelas").mask("99");
    //$("#novadata").mask("99/99/9999");
    //$("#dtpag").mask("99/99/9999");
    $("#txtRG").mask('##.###.###-#', { reverse: true });
    $("#txtCPFCNPJ").mask('##.###.###/####-##', { reverse: true });
    $("#txtEditCPFCNPJ").mask('##.###.###/####-##', { reverse: true });
    $("#txtCEP").mask('#####-###', { reverse: true });
    $("#txtContato").mask('(##)# ####-####', {});

    $("#txtEditRG").mask('##.###.###-#', { reverse: true });
    $("#txtEditCEP").mask('#####-###', { reverse: true });
    $("#txtEditContato").mask('(##)# ####-####', {});

    
});