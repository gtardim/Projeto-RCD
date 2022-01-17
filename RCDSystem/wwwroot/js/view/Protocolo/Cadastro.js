var inicializador = {
    init: function () {

        loadInicial();

    }
}

function loadInicial() {
    $.ajax({
        type: 'GET',
        url: '/Protocolo/LoadInicial',
        dataType: 'json',
        success: function (res) {
            if (res.cliente != null)
                carregaCliente(res.cliente);
            if (res.categoria != null)
                carregaCategoria(res.categoria);


        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });
}

function loadBusca() {
    $.ajax({
        type: 'GET',
        url: '/Protocolo/LoadBusca',
        dataType: 'json',
        success: function (res) {
       
            if (res.protocolo != null)
                carregaTabelaBuscaProtocolo(res.protocolo);

        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });
}


function carregaTabelaBuscaProtocolo(res) {
    var data = [];

    var action;
    var toggle;
    for (var i = 0; i < res.length; i++) {
        action = '<td> <button class="btn btn-outline-primary btn-rounded mb-2" type="button" onclick="loadValorRecebido(\'' + res[i].cod + '\',\'' + res[i].descricao + '\',\'' + res[i].cliente.cod + '\',\'' + res[i].categoria.cod_tipo + '\',\''  + res[i].valortotal + '\',\'' + res[i].observacoes + '\')"> Editar </button> </td>';

        data.push([
            res[i].cod,
            res[i].descricao,
            res[i].cliente.nome,
            res[i].categoria.descricao,
            action
        ]);
    }
    $('#tableBuscaProtocolo').DataTable({
        data: data,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.21/i18n/Portuguese-Brasil.json"
        }

    });
}

function editar(cod,descricao,cliente,categoria,valortotal,observacoes,valorrecebido) {

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

    limparTabelaAtendimento();
    loadAtendimento(cod);
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

    limparTabelaAtendimento();
    loadAtendimento(cod);
    $('#AtendimentoModal').modal('hide');

    Swal.fire({
        icon: 'success',
        title: 'Atendimento Gravado com sucesso!',
        showConfirmButton: false,
        timer: 2500
    });



}

function loadAtendimento(cod) {
    $.ajax({
        type: 'GET',
        url: '/Protocolo/LoadAtendimento',
        dataType: 'json',
        data: {
            cod
        },
        success: function (res) {


            if (res.atendimento != null) {

                limparTabelaAtendimento();
                carregaTabelaAtendimento(res.atendimento);
            }
                
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });
}

function loadValorRecebido(cod, descricao, cliente, categoria, valortotal, observacoes) {

    var tipo = 1;

    $.ajax({
        type: 'GET',
        url: '/ContasReceber/ValorRecebido',
        dataType: 'json',
        data: {
            cod,tipo
        },
        success: function (res) {

            if (res.valorrecebido != null)
                editar(cod, descricao, cliente, categoria, valortotal,observacoes,res.valorrecebido);

        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });
}

function loadValorRecebidoA(cod, descricao, cliente, categoria, valortotal, observacoes) {

    $.ajax({
        type: 'GET',
        url: '/ContasReceber/ValorRecebido',
        dataType: 'json',
        data: {
            cod
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

function carregaTabelaAtendimento(res) {
    var data = [];

    var action;
    var toggle;
    for (var i = 0; i < res.length; i++) {
        action = '<td> <button class="btn btn-outline-primary btn-rounded mb-2" type="button" onclick="AtendimentoEditModal(\'' + res[i].codigo + '\',\'' + res[i].titulo + '\',\'' + res[i].data + '\',\'' + res[i].horainicial + '\',\'' + res[i].horafinal + '\',\'' + res[i].detalhamento + '\')"> Editar </button> <button type="button" class="btn btn-danger mb-2" onclick ="excluirAtendimento(\'' + res[i].codigo + '\',\'' + res[i].protocolo.cod + '\')">Excluir </button> </td>';

        var dt = new Date(res[i].data).toLocaleDateString();

        data.push([
            res[i].titulo,
            dt,
            action
        ]);
    }
    $('#tableAtendimento').DataTable({
        data: data,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.21/i18n/Portuguese-Brasil.json"
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

function modalRecebimentoParcial() {
    var valortotal = $("#txtValorTotal").val();
    var valorrecebido = $("#txtValorRecebido").val();

    var codprotocolo = $("#txtCodigo").val();
    if (codprotocolo != "") {


        if (valortotal > valorrecebido) {
            $("#txtValorTotalP").val($("#txtValorTotal").val());
            $("#txtValorRecebidoP").val($("#txtValorRecebido").val());

            $('#RecebimentoParcialModal').modal('show');

        }
        else {

            Swal.fire({
                icon: 'error',
                title: 'Erro!' + "<br>" + "Valor Total ja Recebido",
                showConfirmButton: false,
                timer: 6500
            });

        }
    }
    else {
        Swal.fire({
            icon: 'error',
            title: 'Erro!' + "<br>" + "Favor Selecionar um Protocolo !!",
            showConfirmButton: false,
            timer: 6500
        });

    }


}

function AtendimentoModal() {

    var codprotocolo = $("#txtCodigo").val();
    if (codprotocolo != "") {

        $('#AtendimentoModal').modal('show');
        $("#txtHoraInicio").flatpickr(
            {
                enableTime: true,
                noCalendar: true,
                dateFormat: "H:i",
                defaultDate: "13:45",
                time_24hr: true,
                static: true
            });

        $("#txtHoraFim").flatpickr(
            {
                enableTime: true,
                noCalendar: true,
                dateFormat: "H:i",
                defaultDate: "14:00",
                time_24hr: true,
                static: true
            });

    }
    else {
        Swal.fire({
            icon: 'error',
            title: 'Erro!' + "<br>" + "Favor Selecionar um Protocolo !!",
            showConfirmButton: false,
            timer: 6500
        });

    }
    

}

function AtendimentoEditModal(codigo,titulo,data,horainicial,horafinal,detalhamento ) {

    var dataini = new Date(horainicial);
    var horaini = dataini.getHours(); 
    var minini = dataini.getMinutes();

    var datafim = new Date(horafinal);
    var horafim = datafim.getHours();
    var minfim = datafim.getMinutes();

        
    $("#txtEditHoraInicio").flatpickr(
            {
                enableTime: true,
                noCalendar: true,
                dateFormat: "H:i",
                time_24hr: true,
                defaultDate: horaini + ":" + minini,
                static: true
            });

    $("#txtEditHoraFim").flatpickr(
            {
                enableTime: true,
                noCalendar: true,
                dateFormat: "H:i",
                time_24hr: true,
                defaultDate: horafim + ":" + minfim,
                static: true
            });



    $("#txtCodigoEditAtend").val(codigo);
    $("#txtEditTitulo").val(titulo);
    $("#txtEditAtendimento").val(detalhamento);
    const dt = new Date(data).toLocaleDateString();
    $("#txtEditDiaAtend").flatpickr(
        {
            dateFormat: "d/m/Y",
            static: true
        }).setDate(dt);

    $('#EditAtendimentoModal').modal('show');

}

function validaAtendimento() {

    var codprotocolo = $("#txtCodigo").val();
    var cod = $("#txtCodigoAtend").val();
    var diaatend = $("#txtDiaAtend").val();
    var horainicio = $("#txtHoraInicio").val() + ':00';
    var horafim = $("#txtHoraFim").val()+':00';

    var inicio = new Date('1970-01-01T' + horainicio);
    var fim = new Date('1970-01-01T' + horafim);
    
    if (inicio.getHours() <= fim.getHours()) {

        if (inicio.getHours() == fim.getHours()) {

            if (inicio.getMinutes() < fim.getMinutes()) {

                $.ajax({
                    type: 'GET',
                    url: '/Protocolo/ValidaAtendimento',
                    dataType: 'json',
                    data: {
                        codprotocolo, diaatend, horainicio, horafim
                    },
                    success: function (res) {
                        if (res.ok != null) {

                            Swal.fire({
                                icon: 'error',
                                title: 'Erro!' + "<br>" + "Ja Existe um atendimento nesta data e hora",
                                showConfirmButton: false,
                                timer: 6500
                            });
                        }
                        else {

                            gravarAtendimento();
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
                    title: 'Erro!' + "<br>" + "Horario Inicial nao pode ser menor que o final",
                    showConfirmButton: false,
                    timer: 6500
                });

            }
        }
        else {

            $.ajax({
                type: 'GET',
                url: '/Protocolo/ValidaAtendimento',
                dataType: 'json',
                data: {
                    codprotocolo, diaatend, horainicio, horafim
                },
                success: function (res) {
                    if (res.ok != null) {

                        Swal.fire({
                            icon: 'error',
                            title: 'Erro!' + "<br>" + "Ja Existe um atendimento nesta data e hora",
                            showConfirmButton: false,
                            timer: 6500
                        });
                    }
                    else {

                        gravarAtendimento();
                    }
                },
                error: function (XMLHttpRequest, txtStatus, errorThrown) {
                    alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                }
            });

        }

    }
    else {

        Swal.fire({
            icon: 'error',
            title: 'Erro!' + "<br>" + "Horario Inicial nao pode ser menor que o final",
            showConfirmButton: false,
            timer: 6500
        });


    }
    
}

function validaDescricaoProtocolo() {

    var descricao = $("#txtDescricao").val();
    var cod = $("#txtCodigo").val();

    $.ajax({
        type: 'GET',
        url: '/Protocolo/ValidaDescricaoProtocolo',
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
                    title: 'Erro!' + "<br>" + "Descriçao de protocolo ja existente",
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
    var categoria = $("#txtCategoria").val();
    var valortotal = $("#txtValorTotal").val();
    var observacoes = $("#txtObservacoes").val();
    var valorrecebido = $("#txtValorRecebido").val();
    var tipo = 1;

    var valida = validaGravacao();

    if (valida.msg == "") {

        if (cod == "") {

            Swal.fire({
                title: 'Deseja Gravar este NOVO Protocolo  ?',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Sim, eu quero Gravar!',
                cancelButtonText: 'Cancelar'
            }).then((result) => {
                if (result.isConfirmed) {

                    $.ajax({

                        type: 'POST',
                        url: '/Protocolo/Gravar',
                        dataType: 'json',
                        data: {
                            cod, descricao, cliente, categoria, valortotal, observacoes
                        },
                        success: function (res) {
                            if (res.ok == true) {

                                if (cod == "") {
                                    gerarRecebimentoTotal(res.codigo, tipo);
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
            });


        }
        else {

            Swal.fire({
                title: 'Deseja realmente atualizar o Protocolo:' + cod,
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Sim, eu quero Atualizar!',
                cancelButtonText: 'Cancelar'
            }).then((result) => {
                if (result.isConfirmed) {

                    $.ajax({

                        type: 'POST',
                        url: '/Protocolo/Gravar',
                        dataType: 'json',
                        data: {
                            cod, descricao, cliente, categoria, valortotal, observacoes
                        },
                        success: function (res) {
                            if (res.ok == true) {

                                if (cod == "") {
                                    gerarRecebimentoTotal(res.codigo, tipo);
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
            });


        }

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

function GerarRecebimentoParcial() {

    var cod = $("#txtCodigo").val();
    var descricao = $("#txtDescricao").val();
    var categoria = $("#txtCategoria").val();
    var observacoes = $("#txtObservacoes").val();
    var cliente = $("#txtCliente").val();
    var valortotal = $("#txtValorTotal").val();
    var valorrecebido = $("#txtValorRecebido").val();
    var valorareceber = $("#txtNovoRecebimento").val();

    valortotal = valortotal.replace(".", ",");
    valorareceber = valorareceber.replace(".", ",");
    valorrecebido = valorrecebido.replace(".", ",");
    var tipo = 1;

    if (cod != "") {

        if (valorrecebido <valortotal) {


            Swal.fire({
                title: 'Você tem certeza que deseja gerar o recebimento recebimento parcial de:'+valorareceber,
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Sim, eu quero Gerar!',
                cancelButtonText: 'Cancelar'
            }).then((result) => {
                if (result.isConfirmed) {

                    $.ajax({

                        type: 'POST',
                        url: '/ContasReceber/GerarRecebimentoParcial',
                        dataType: 'json',
                        data: {
                            cod, cliente, valortotal, tipo, valorareceber, valorrecebido
                        },
                        success: function (res) {
                            if (res.ok == true) {
                                Swal.fire({
                                    icon: 'success',
                                    title: 'Pagamento Total Gerado com sucesso!',
                                    showConfirmButton: false,
                                    timer: 3500
                                });
                                loadValorRecebido(cod, descricao, cliente, categoria, valortotal, observacoes);
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
                title: 'Erro!' + "<br>" + "Protocolo ja possui recebimento criado",
                showConfirmButton: false,
                timer: 6500
            });
        }
    }
    else {

        Swal.fire({
            icon: 'error',
            title: 'Erro!' + "<br>" + "Favor Selecionar um Protocolo !!",
            showConfirmButton: false,
            timer: 6500
        });
    }




}

function gravarAtendimento() {



        var descricao = $("#txtDescricao").val();
        var cliente = $("#txtCliente").val();
        var categoria = $("#txtCategoria").val();
        var valortotal = $("#txtValorTotal").val();
        var observacoes = $("#txtObservacoes").val();

        var codprotocolo = $("#txtCodigo").val();
        var cod = $("#txtCodigoAtend").val();
        var titulo = $("#txtTitulo").val();
        var atendimento = $("#txtAtendimento").val();
        var diaatend = $("#txtDiaAtend").val();
        var horainicio = $("#txtHoraInicio").val();
        var horafim = $("#txtHoraFim").val();

    var valida = validaGravarAtendimento();

    if (valida.msg == "") {

        $.ajax({

            type: 'POST',
            url: '/Atendimento/GravarAtendimento',
            dataType: 'json',
            data: {
                cod, titulo, atendimento, diaatend, horainicio, horafim, codprotocolo
            },
            success: function (res) {
                if (res.ok == true) {

                    limpaCamposA();
                    loadValorRecebidoA(codprotocolo, descricao, cliente, categoria, valortotal, observacoes);

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


function AlterarAtendimento() {

    var codprotocolo = $("#txtCodigo").val();
    var cod = $("#txtCodigoEditAtend").val();
    var titulo = $("#txtEditTitulo").val();
    var atendimento = $("#txtEditAtendimento").val();
    var diaatend = $("#txtEditDiaAtend").val();
    var horainicio = $("#txtEditHoraInicio").val();
    var horafim = $("#txtEditHoraFim").val();

    var valida = validaGravarAtendimento();

    if (valida.msg == "") {

        $.ajax({

            type: 'POST',
            url: '/Atendimento/AlterarAtendimento',
            dataType: 'json',
            data: {
                cod, titulo, atendimento, diaatend, horainicio, horafim, codprotocolo
            },
            success: function (res) {
                if (res.ok == true) {

                    Swal.fire({
                        icon: 'success',
                        title: 'Alterado com sucesso!',
                        showConfirmButton: false,
                        timer: 2500
                    });

                    loadAtendimento(codprotocolo);

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

function excluir() {

    var cod = $("#txtCodigo").val();
    var recebido = $("#txtValorRecebido").val();

    if (cod != ""){

        if (recebido == 0 || recebido == '0,00' ) {

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
                        url: '/Protocolo/Excluir',
                        data: {
                            cod
                        },
                        success: function (res) {
                            if (res.ok == true && res.atendimento == true) {

                                Swal.fire({
                                    icon: 'success',
                                    title: "Excluido com sucesso!!",
                                    showConfirmButton: false,
                                    timer: 6500
                                });

                                setTimeout(() => { location.reload(); }, 2000);

                            }
                            else {
                                Swal.fire({
                                    icon: 'error',
                                    title: 'Erro!' + "<br>" + "Protocolo possui atendimentos Realizados!!",
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
            });
        }
        else {
            Swal.fire({
                icon: 'error',
                title: 'Erro!' + "<br>" + "Erro Protocolo ja possui valor pago!!",
                showConfirmButton: false,
                timer: 6500
            });
        }

    }
    else {
        Swal.fire({
            icon: 'error',
            title: 'Erro!' + "<br>" + "Protocolo não Selecionado",
            showConfirmButton: false,
            timer: 15000
        });
    }
   
}

function excluirAtendimento(cod,codprotocolo) {



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
                url: '/Atendimento/Excluir',
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
                        loadAtendimento(codprotocolo);
                    }
                },
                error: function (XMLHttpRequest, txtStatus, errorThrown) {
                    alert("Status: " + txtStatus); alert("Error: " + errorThrown);
                }
            });
        }
    });

}

function atualizaRecebimento(valortotal,cod) {
    var tipo = 1;
    $.ajax({

        type: 'POST',
        url: '/ContasReceber/AtualizaRecebimento',
        dataType: 'json',
        data: {
            valortotal,cod,tipo
        },
        success: function (res) {
            if (res.ok == true) {
                Swal.fire({
                    icon: 'success',
                    title: 'Protocolo Atualizado Com Sucesso!',
                    showConfirmButton: false,
                    timer: 3500
                });
                loadValorRecebido(cod, descricao, cliente, categoria, valortotal, observacoes);
            }
            else {
                Swal.fire({
                    icon: 'error',
                    title: 'Erro!' + "<br>" + "Erro Protocolo ja possui valor pago!!",
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

function gerarRecebimentoTotal(cod)
{

    var descricao = $("#txtDescricao").val();
    var categoria = $("#txtCategoria").val();
    var observacoes = $("#txtObservacoes").val();
    var cliente = $("#txtCliente").val();
    var valortotal = $("#txtValorTotal").val();
    var valorrecebido = $("#txtValorRecebido").val();
    valortotal = valortotal.replace(".", ",");


    var tipo = 1;

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
                                    title: 'Protocolo Gerado Com Sucesso!',
                                    showConfirmButton: false,
                                    timer: 3500
                                });
                                loadValorRecebido(cod, descricao, cliente, categoria, valortotal, observacoes);
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
                                    title: 'Protocolo Gerado Com Sucesso!',
                                    showConfirmButton: false,
                                    timer: 3500
                                });
                                loadValorRecebido(cod, descricao, cliente, categoria, valortotal, observacoes);
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
            title: 'Erro!' + "<br>" + "Erro ao Gerar Protocolo !!",
            showConfirmButton: false,
            timer: 6500
        });
    }



}

function validaDescricaoProcesso() {

    var descricao = $("#txtDescricao").val();
    var cod = $("#txtCodigo").val();

    Swal.fire({
        html: '<label> Descrição do Processo </label>' +
            '<input data-toggle="datepicker" id="swal-input1" class="swal2-input">' +
            '<label>Valor do Processo </label>' +
            '<input data-toggle="datepicker" id="swal-input2" class="swal2-input">',
        confirmButtonText: 'Confirmar',
        showCancelButton: true,
        cancelButtonText: 'Cancelar',
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        preConfirm: () => {
            var desc = document.getElementById('swal-input1').value;
            var valorprocesso = document.getElementById('swal-input2').value;
            valorprocesso = valorprocesso.replace(".", ",");

            if (desc != "") {

                if (valorprocesso != "") {

                    $.ajax({
                        type: 'GET',
                        url: '/Processo/ValidaDescricaoProcesso',
                        dataType: 'json',
                        data: {
                            desc
                        },
                        success: function (res) {
                            if (res.ok == true || cod != "")
                                gerarProcesso(desc,valorprocesso);
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
                else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Erro!' + "<br>" + "Valor nao Preenchido !!",
                        showConfirmButton: false,
                        timer: 6500
                    });

                }
              

            }
            else {
                Swal.fire({
                    icon: 'error',
                    title: 'Erro!' + "<br>" + "Descrição nao Preenchida !!",
                    showConfirmButton: false,
                    timer: 6500
                });

            }
           

        }
    })

}



function gerarProcesso(desc, valorprocesso) {

    var cod = $("#txtCodigo").val();
    var descricao = $("#txtDescricao").val();
    var cliente = $("#txtCliente").val();
    var categoria = $("#txtCategoria").val();
    var valortotal = $("#txtValorTotal").val();
    var observacoes = $("#txtObservacoes").val();



    if (cod != "") {


                $.ajax({

                    type: 'POST',
                    url: '/Processo/GeraProcesso',
                    dataType: 'json',
                    data: {
                        cod, desc, cliente, categoria, valorprocesso,observacoes
                    },
                    success: function (res) {
                        if (res.ok == true) {

                            gerarRecebimentoTotalProcesso(res.codigo,valorprocesso);

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
            title: 'Erro!' + "<br>" + "Por Favor Selecione um Protocolo !!",
            showConfirmButton: false,
            timer: 6500
        });
    }



}

function gerarRecebimentoTotalProcesso(cod,valortotal) {

    var cliente = $("#txtCliente").val();
    valortotal = valortotal.replace(".", ",");

    var tipo = 2;

    if (cod != "") {

            $.ajax({

                type: 'POST',
                url: '/ContasReceber/GerarRecebimentoTotal',
                dataType: 'json',
                data: {
                    cod,cliente,valortotal, tipo
                },
                success: function (res) {
                    if (res.ok == true) {
                        Swal.fire({
                            icon: 'success',
                            title: 'Processo Gerado Com Sucesso!',
                            showConfirmButton: false,
                            timer: 3500
                        });
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
            title: 'Erro!' + "<br>" + "Erro ao Gerar Processo !!",
            showConfirmButton: false,
            timer: 6500
        });
    }

}

function validaGravacao() {

    var descricao = $("#txtDescricao").val();
    var cliente = $("#txtCliente").val();
    var categoria = $("#txtCategoria").val();
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

function validaGravarAtendimento() {


    var titulo = $("#txtTitulo").val();
    var atendimento = $("#txtAtendimento").val();
    var diaatend = $("#txtDiaAtend").val();
    var horainicio = $("#txtHoraInicio").val();
    var horafim = $("#txtHoraFim").val();

    var erro = "";

    if (titulo.trim() == "") {

        erro += " Titulo nao Informado.<br>";
    }
    if (atendimento.trim() == "") {

        erro += "Detalhe do Atendimento Não informado.<br>";
    }

    if (diaatend == "") {

        erro += "Dia Não informado.<br>";
    }

    if (horainicio == "") {

        erro += " Hora Inicio nao Informada.<br>";
    }
    if (horafim == "") {

        erro += " Hora Fim nao Informada.<br>";
    }

    valida = {
        msg: erro,
        dados: {
            titulo,
            atendimento,
            diaatend,
            horainicio,
            horafim,
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
            limparTabelaAtendimento();

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
    $('#tableBuscaProtocolo').DataTable().clear().destroy();
}

function limparTabelaAtendimento() {
    $('#tableAtendimento').DataTable().clear().destroy();
}
function fecharModalAtendimento() {

    $('#AtendimentoModal').modal('hide');
}

function fecharEditModalAtendimento() {

    $('#EditAtendimentoModal').modal('hide');
}

function fecharModalRecebimentoParcial() {

    $('#RecebimentoParcialModal').modal('hide');
}

inicializador.init();

$(document).ready(function () {
    $('#txtValorTotal').mask('000.000.000.000.000,00', { reverse: true });
    
});