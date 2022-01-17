var inicializador = {
    init: function () {

        loadInicial();

    }
}

function loadInicial() {
    $.ajax({
        type: 'GET',
        url: '/Processo/LoadInicial',
        dataType: 'json',
        success: function (res) {
            if (res.cliente != null)
                carregaCliente(res.cliente);
            if (res.categoria != null)
                carregaCategoria(res.categoria);
            if (res.tipoarq != null)
                carregaTipoArq(res.tipoarq);


        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });
}

function loadBusca() {
    $.ajax({
        type: 'GET',
        url: '/Processo/LoadBusca',
        dataType: 'json',
        success: function (res) {

            if (res.processo != null)
                carregaTabelaBuscaProcesso(res.processo);



        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });
}


function carregaTabelaBuscaProcesso(res) {
    var data = [];

    var action;
    var toggle;
    for (var i = 0; i < res.length; i++) {
        action = '<td> <button class="btn btn-outline-primary btn-rounded mb-2" type="button" onclick="loadValorRecebido(\'' + res[i].cod + '\',\'' + res[i].descricao + '\',\'' + res[i].cliente.cod + '\',\'' + res[i].categoria.cod_tipo + '\',\'' + res[i].valortotal + '\',\'' + res[i].observacoes + '\',\'' + res[i].numregistro + '\')"> Editar </button> </td>';

        data.push([
            res[i].cod,
            res[i].descricao,
            res[i].cliente.nome,
            res[i].categoria.descricao,
            action
        ]);
    }
    $('#tableBuscaProcesso').DataTable({
        data: data,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.21/i18n/Portuguese-Brasil.json"
        }


    });
}

function editar(cod, descricao, cliente, categoria, valortotal, observacoes, valorrecebido,numregistro) {



    $("#txtCodigo").val(cod);
    $("#txtDescricao").val(descricao);
    $("#txtCliente").val(cliente).trigger("change.select2");
    $("#txtCategoria").val(categoria).trigger("change.select2");

    var pointNum = parseFloat(valortotal);
    var valorFormatado = pointNum.toLocaleString('pt-BR', { minimumFractionDigits: 2 });
    $("#txtValorTotal").val(valorFormatado);

    $("#txtObservacoes").val(observacoes);

    var pointNum2 = parseFloat(valorrecebido);
    var valorFormatado2 = pointNum2.toLocaleString('pt-BR', { minimumFractionDigits: 2 });
    $("#txtValorRecebido").val(valorFormatado2);

    $("#txtNumRegistro").val(numregistro);


    loadArquivo(cod);
    $('#buscaModal').modal('hide');



}

function editarA(cod, descricao, cliente, categoria, valortotal, observacoes, valorrecebido) {



    $("#txtCodigo").val(cod);
    $("#txtDescricao").val(descricao);
    $("#txtCliente").val(cliente).trigger("change.select2");
    $("#txtCategoria").val(categoria).trigger("change.select2");

    var pointNum = parseFloat(valortotal);
    var valorFormatado = pointNum.toLocaleString('pt-BR', { minimumFractionDigits: 2 });
    $("#txtValorTotal").val(valorFormatado);

    $("#txtObservacoes").val(observacoes);

    var pointNum2 = parseFloat(valorrecebido);
    var valorFormatado2 = pointNum2.toLocaleString('pt-BR', { minimumFractionDigits: 2 });
    $("#txtValorRecebido").val(valorFormatado2);


    limparTabelaArquivo()
    loadArquivo(cod);
    $('#NovoArquivoModal').modal('hide');

    Swal.fire({
        icon: 'success',
        title: 'Arquivo Gravado com sucesso!',
        showConfirmButton: false,
        timer: 2500
    });



}

function loadArquivo(cod) {
    $.ajax({
        type: 'GET',
        url: '/Processo/LoadArquivo',
        dataType: 'json',
        data: {
            cod
        },
        success: function (res) {


            if (res.arquivo != null) {

                limparTabelaArquivo();
                carregaTabelaArquivo(res.arquivo);
            }
            




        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });
}

function validaDescricaoArquivo() {

    var descricao = $("#txtTitulo").val();
    var processocod = $("#txtCodigo").val();

    

    $.ajax({
        type: 'GET',
        url: '/Processo/ValidaDescricaoArquivo',
        dataType: 'json',
        data: {
            descricao, processocod
        },
        success: function (res) {
            if (res.ok == false) {
                Swal.fire({
                    icon: 'error',
                    title: 'Erro!' + "<br>" + "Descriçao do arquivo ja existente neste processo",
                    showConfirmButton: false,
                    timer: 6500
                });
            }
            else {
                gravarArquivo();

            }



        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });
}

function loadValorRecebido(cod, descricao, cliente, categoria, valortotal, observacoes,numregistro) {

    var tipo = 2; 

    $.ajax({
        type: 'GET',
        url: '/ContasReceber/ValorRecebido',
        dataType: 'json',
        data: {
            cod,tipo
        },
        success: function (res) {

            if (res.valorrecebido != null)
                editar(cod, descricao, cliente, categoria, valortotal, observacoes, res.valorrecebido, numregistro);



        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });
}

function loadValorRecebidoA(cod, descricao, cliente, categoria, valortotal, observacoes) {

    var tipo = 2; 

    $.ajax({
        type: 'GET',
        url: '/ContasReceber/ValorRecebido',
        dataType: 'json',
        data: {
            cod,tipo
        },
        success: function (res) {

            if (res.valorrecebido != null)
                editarA(cod, descricao, cliente, categoria, valortotal, observacoes, res.valorrecebido);



        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });
}

function carregaTabelaArquivo(res) {
    var data = [];

    var action;
    var toggle;
    for (var i = 0; i < res.length; i++) {
        action = '<td> <button class="btn-outline-primary btn-rounded mb-1" type="button" onclick="EditArquivoModal(\'' + res[i].cod + '\',\'' + res[i].descricao + '\',\'' + res[i].nome + '\',\'' + res[i].tipoarq.cod_tipo + '\')">Editar</button> </td> <td> <button type="button" class="btn-danger mb-1" onclick ="excluirArquivo(' + res[i].cod + ')">Excluir </button></td><td> <button  type="button" class="btn-outline-primary btn-rounded mb-1" onclick ="view(' + res[i].cod + ')">View </button></td><td>  <button  type="button" class="btn-danger mb-1" onclick ="get(' + res[i].cod + ')"><svg xmlns="http://www.w3.org/2000/svg" width="13" height="13" viewBox="0 0 22 22" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-download"><path d="M21 15v4a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2v-4"></path><polyline points="7 10 12 15 17 10"></polyline><line x1="12" y1="15" x2="12" y2="3"></line></svg> </button></td>';

        data.push([
            res[i].descricao,
            res[i].tipoarq.descricao,
            action
        ]);
    }
    $('#tableArquivo').DataTable({
        data: data,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.21/i18n/Portuguese-Brasil.json"
        }


    });
}

function view(cod) {
    window.open('/RegistrarArquivo/ViewArquivo?cod=' + cod, '_blank');
}

function get(cod) {
    window.open('/RegistrarArquivo/GetArquivo?cod=' + cod, '_blank');
}

function obterTabelaArquivo() {
    limparTabelaArquivo();
    var cod = $("#txtCodigo").val();

    $.ajax({
        type: 'GET',
        url: '/RegistrarArquivo/ObterTabelaProcesso',
        dataType: 'json',
        data: {
            cod
        },
        success: function (res) {

            if (res != null)
                carregaTabelaArquivo(res.arquivos);

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

function carregaTipoArq(res) {

    var data = [{
        id: 0, text: 'Selecione o Tipo de Arquivo'
    }];
    for (var i = 0; i < res.length; i++) {
        data.push({
            id: res[i].cod_tipo,
            text: res[i].descricao
        });
    }
    $("#txtTipoArq").select2({
        data: data
    })
    $("#txtEditTipoArq").select2({
        data: data
    })

    
}

function carregaCategoria(res) {

    var data = [{
        id: 0, text: 'Selecione a Categoria do Potocolo'
    }];
    for (var i = 0; i < res.length; i++) {
        data.push({
            id: res[i].cod_tipo,
            text: res[i].descricao
        });
    }
    $("#txtCategoria").select2({
        data: data
    })

}

function modalBuscar() {
    limparTabelaBusca();
    loadBusca();
    $('#buscaModal').modal('show');

}



function NovoArquivoModal() {

    var codprocesso = $("#txtCodigo").val();
    if (codprocesso != "") {

        $('#NovoArquivoModal').modal('show');

    }
    else {
        Swal.fire({
            icon: 'error',
            title: 'Erro!' + "<br>" + "Favor Selecionar um Processo !!",
            showConfirmButton: false,
            timer: 6500
        });

    }


}

function EditArquivoModal(cod,descricao,nome,tipoarq ) {

    $("#txtCodigoEditArq").val(cod);
    $("#txtEditTitulo").val(descricao);
    $("#txtEditTipoArq").val(tipoarq).trigger("change.select2");;
    $("#txtArquivoCarregado").val(nome);

    $('#EditArquivoModal').modal('show');

}

function validaDescricaoProcesso() {

    var descricao = $("#txtDescricao").val();
    var cod = $("#txtCodigo").val();


        $.ajax({
            type: 'GET',
            url: '/Processo/ValidaDescricaoProcesso',
            dataType: 'json',
            data: {
                descricao
            },
            success: function (res) {
                if (res.ok == true || cod != "")
                    gravar();
                else {

                    Swal.fire({
                        icon: 'error',
                        title: 'Erro!' + "<br>" + "Descriçao de processo ja existente",
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

function gravar() {

    var cod = $("#txtCodigo").val();
    var descricao = $("#txtDescricao").val();
    var cliente = $("#txtCliente").val();
    var numregistro = $("#txtNumRegistro").val();
    var categoria = $("#txtCategoria").val();
    var valortotal = $("#txtValorTotal").val();
    var observacoes = $("#txtObservacoes").val();
    var valorrecebido = $("#txtValorRecebido").val();

    var valida = validaGravacao();

    if (valida.msg == "") {

        $.ajax({

            type: 'POST',
            url: '/Processo/Gravar',
            dataType: 'json',
            data: {
                cod, descricao, cliente, categoria, valortotal, observacoes, numregistro
            },
            success: function (res) {
                if (res.ok == true) {

                    if (cod == "") {
                        gerarRecebimentoTotal(res.codigo);
                    }
                    else {
                        atualizaRecebimento(valortotal, cod);
                        Swal.fire({
                            icon: 'success',
                            title: 'Atualização realizada com sucesso !',
                            showConfirmButton: false,
                            timer: 3500
                        });
                    }
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


function gravarArquivo() {

    var descricao2 = $("#txtDescricao").val();
    var categoria2 = $("#txtCategoria").val();
    var observacoes2 = $("#txtObservacoes").val();
    var cliente2 = $("#txtCliente").val();
    var valortotal2 = $("#txtValorTotal").val();
    var valorrecebido2 = $("#txtValorRecebido").val();
    var numregistro2 = $("#txtNumRegistro").val();

    var fd = new FormData();
    var descricao = $("#txtTitulo").val();
    var tipoarq = $("#txtTipoArq").val();
    var cliente = $("#txtCliente").val();
    var processo = $("#txtCodigo").val();

    var cod = processo;
    //var arquivo = new FileUploadWithPreview('mySecondImage');
    var arquivo = document.getElementById("arquivoGeral").files[0];
    fd.append("cliente", cliente);
    fd.append("tipoarq", tipoarq);
    fd.append("descricao", descricao);
    fd.append("arquivo", arquivo);
    fd.append("processo", processo);


    var valida = validaGravacaoArquivo();

    if (valida.msg == "") {

        var configFD = {
            method: "POST",
            headers: {
                "Accept": "application/json"
            },
            body: fd
        }
        fetch("/RegistrarArquivo/GravarComProcesso", configFD)
            .then(function (res) {
                var obj = res.json(); //deserializando
                return obj;
            })
            .then(function (res) {
                Swal.fire({
                    icon: 'success',
                    title: 'Gravado com sucesso!',
                    showConfirmButton: false,
                    timer: 1500
                });
                $('#NovoArquivoModal').modal('hide');

                loadValorRecebido(cod, descricao2, cliente2, categoria2, valortotal2, observacoes2, numregistro2);

            })
            .catch(function () {

                document.getElementById("divMsg").innerHTML = "deu erro";
            })

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


function alterarArquivo() {
    var descricao2 = $("#txtDescricao").val();
    var categoria2 = $("#txtCategoria").val();
    var observacoes2 = $("#txtObservacoes").val();
    var cliente2 = $("#txtCliente").val();
    var valortotal2 = $("#txtValorTotal").val();
    var valorrecebido2 = $("#txtValorRecebido").val();
    var numregistro2 = $("#txtNumRegistro").val();


    var fd = new FormData();
    var codigo = $("#txtCodigoEditArq").val();
    var descricao = $("#txtEditTitulo").val();
    var tipoarq = $("#txtEditTipoArq").val();
    var cliente = cliente2;
    var processo = $("#txtCodigo").val();

    var cod = processo;
    //var arquivo = new FileUploadWithPreview('mySecondImage');
    var arquivo = document.getElementById("EditarquivoGeral").files[0];
    fd.append("codigo", codigo);
    fd.append("cliente", cliente);
    fd.append("tipoarq", tipoarq);
    fd.append("descricao", descricao);
    fd.append("arquivo", arquivo);
    fd.append("processo", processo);


    var configFD = {
        method: "POST",
        headers: {
            "Accept": "application/json"
        },
        body: fd
    }
    fetch("/RegistrarArquivo/Alterar", configFD)
        .then(function (res) {
            var obj = res.json(); //deserializando
            return obj;
        })
        .then(function (res) {
            Swal.fire({
                icon: 'success',
                title: 'Alterado com sucesso!',
                showConfirmButton: false,
                timer: 1500
            });
            $('#editRowModal').modal('hide');



            loadValorRecebido(cod, descricao2, cliente2, categoria2, valortotal2, observacoes2, numregistro2);


        })
        .catch(function () {

            document.getElementById("divMsg").innerHTML = "deu erro";
        })

}

function excluir() {

    var cod = $("#txtCodigo").val();
    var recebido = $("#txtValorRecebido").val();

    if (recebido == 0 || recebido == '0,00') {
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
                    url: '/Processo/Excluir',
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
                            setTimeout(() => { location.reload(); }, 2000); 
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
            title: 'Erro!' + "<br>" + "Erro Processo ja possui valor pago!!",
            showConfirmButton: false,
            timer: 6500
        });
    }


}

function excluirArquivo(cod) {

    var cod2 = $("#txtCodigo").val();
    var descricao2 = $("#txtDescricao").val();
    var categoria2 = $("#txtCategoria").val();
    var observacoes2 = $("#txtObservacoes").val();
    var cliente2 = $("#txtCliente").val();
    var valortotal2 = $("#txtValorTotal").val();
    var numregistro2 = $("#txtNumRegistro").val();


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
                url: '/RegistrarArquivo/Excluir',
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

                        loadValorRecebido(cod2, descricao2, cliente2, categoria2, valortotal2, observacoes2, numregistro2);
                    }
                },
                error: function (XMLHttpRequest, txtStatus, errorThrown) {
                    alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                }
            });
        }
    });

}

function atualizaRecebimento(valortotal, cod) {

    var tipo = 2;
    $.ajax({

        type: 'POST',
        url: '/ContasReceber/AtualizaRecebimento',
        dataType: 'json',
        data: {
            valortotal, cod,tipo
        },
        success: function (res) {
            if (res.ok == true) {
                Swal.fire({
                    icon: 'success',
                    title: 'Processo Atualizado Com Sucesso!',
                    showConfirmButton: false,
                    timer: 3500
                });
                loadValorRecebido(cod, descricao, cliente, categoria, valortotal, observacoes, numregistro);
            }
            else {
                Swal.fire({
                    icon: 'error',
                    title: 'Erro!' + "<br>" + "Erro Processo la possui valor pago!!",
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

function gerarRecebimentoTotal(cod) {

    var descricao = $("#txtDescricao").val();
    var categoria = $("#txtCategoria").val();
    var observacoes = $("#txtObservacoes").val();
    var cliente = $("#txtCliente").val();
    var valortotal = $("#txtValorTotal").val();
    var valorrecebido = $("#txtValorRecebido").val();
    var numregistro = $("#txtNumRegistro").val();
    valortotal = valortotal.replace(".", ",");


    var tipo = 2;

    if (cod != "") {

        if (valorrecebido == 0) {

            Swal.fire({
                title: 'Deseja gerar com valor baixado?',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Sim,gerar com valor ja recebido!',
                cancelButtonText: 'Nao, Gerar em aberto !'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({

                        type: 'POST',
                        url: '/ContasReceber/GerarRecebimentoTotalRecebido',
                        dataType: 'json',
                        data: {
                            cod, cliente, valortotal, tipo, descricao, observacoes, categoria
                        },
                        success: function (res) {
                            if (res.ok == true) {
                                Swal.fire({
                                    icon: 'success',
                                    title: 'Processo Gerado Com Sucesso!',
                                    showConfirmButton: false,
                                    timer: 3500
                                });
                                loadValorRecebido(cod, descricao, cliente, categoria, valortotal, observacoes, numregistro);
                            }
                        },
                        error: function (XMLHttpRequest, txtStatus, errorThrown) {
                            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                        }

                    });
                }
                else {
                    $.ajax({

                        type: 'POST',
                        url: '/ContasReceber/GerarRecebimentoTotal',
                        dataType: 'json',
                        data: {
                            cod, cliente, valortotal, tipo, descricao, observacoes, categoria
                        },
                        success: function (res) {
                            if (res.ok == true) {
                                Swal.fire({
                                    icon: 'success',
                                    title: 'Processo Gerado Com Sucesso!',
                                    showConfirmButton: false,
                                    timer: 3500
                                });
                                loadValorRecebido(cod, descricao, cliente, categoria, valortotal, observacoes, numregistro);
                            }
                        },
                        error: function (XMLHttpRequest, txtStatus, errorThrown) {
                            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                        }

                    });
                }


            }); 

        }
    }
    else {

        Swal.fire({
            icon: 'error',
            title: 'Erro!' + "<br>" + "Erro ao Gerar Processo !!",
            showConfirmButton: false,
            timer: 6500
        });
    }

}

function validaGravacao() {

    var descricao = $("#txtDescricao").val();
    var categoria = $("#txtCategoria").val();
    var cliente = $("#txtCliente").val();
    var valortotal = $("#txtValorTotal").val();



    var erro = "";

    if (descricao.trim() == "") {

        erro += " Descrição nao Informada.<br>";
    }
    if (categoria == "0") {

        erro += "Categoria Não informada.<br>";
    }

    if (cliente == "0") {

        erro += "Cliente Não informado.<br>";
    }

    if (valortotal.trim() == "") {

        erro += " Valor Total nao Informado.<br>";
    }

    valida = {
        msg: erro,
        dados: {
            descricao,
            categoria,
            cliente,
            valortotal,
        }
    }
    return valida;

}

function validaGravacaoArquivo() {

    var descricao = $("#txtTitulo").val();
    var tipoarq = $("#txtTipoArq").val();
    var cliente = $("#txtCliente").val();
    var arquivo = document.getElementById("arquivoGeral").files[0];



    var erro = "";

    if (descricao.trim() == "") {

        erro += " Descrição nao Informada.<br>";
    }
    if (tipoarq == "0") {

        erro += "Categoria Não informada.<br>";
    }

    if (cliente == "0") {

        erro += "Cliente Não informado.<br>";
    }

    if (arquivo == undefined ) {

        erro += " Arquivo nao Informado.<br>";
    }

    valida = {
        msg: erro,
        dados: {
            descricao,
            tipoarq,
            cliente,
            arquivo,
        }
    }
    return valida;

}


function limpaCampos() {

    Swal.fire({
        title: 'Você tem certeza que deseja criar um novo?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sim, eu quero Criar!',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {

            location.reload();

        }
    });

}

function limpaCampos2() {


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

function limpaCamposA() {


    var input = document.querySelectorAll('input');
    for (var i = 0; i < input.length; i++) {
        input[i].value = "";
    }
    var textarea = document.querySelectorAll('textarea');
    for (var i = 0; i < textarea.length; i++) {
        textarea[i].value = "";
    }

}

function limparTabelaBusca() {
    $('#tableBuscaProcesso').DataTable().clear().destroy();
}

function limparTabelaArquivo() {
    $('#tableArquivo').DataTable().clear().destroy();
}
function fecharModalArquivo() {

    $('#NovoArquivoModal').modal('hide');
}

function fecharEditModalEditArquivo() {

    $('#EditArquivoModal').modal('hide');
}

inicializador.init();

$(document).ready(function () {

    $('#txtValorTotal').mask('000.000.000.000.000,00', { reverse: true });

});