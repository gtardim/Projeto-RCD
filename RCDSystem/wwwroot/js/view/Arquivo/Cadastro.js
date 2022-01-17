var inicializador = {
    init: function () {

        limparTabelaArquivo();
        LoadInicial();

    }
}

function LoadInicial() {

    $.ajax({
        type: 'GET',
        url: '/RegistrarArquivo/LoadInicial',
        dataType: 'json',
        success: function (res) {
            if (res.cliente != null)
                carregaCliente(res.cliente);

            if (res.tipoarq != null)
                carregaTipoArquivo(res.tipoarq);

            if (res.arquivos != null)
                carregaTabela(res.arquivos)

        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
        alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });
}



function showNovoModal() {
    $('#addRowModal').modal('show');
  
}

function gravar() {


    var fd = new FormData();
    var cliente = $("#txtCliente").val();
    var tipoarq = $("#txtTipoArq").val();
    var descricao = $("#txtDescricao").val();
    //var arquivo = new FileUploadWithPreview('mySecondImage');
    var arquivo = document.getElementById("arquivoGeral").files[0];
    fd.append("cliente", cliente);
    fd.append("tipoarq", tipoarq);
    fd.append("descricao", descricao);
    fd.append("arquivo", arquivo);

    var configFD = {
        method: "POST",
        headers: {
            "Accept": "application/json"
        },
        body: fd
    }
    fetch("/RegistrarArquivo/Gravar", configFD)
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
            $('#addRowModal').modal('hide');
            

            obterTabela();
            limpaCampos();
            
            
            


        })
        .catch(function () {

            document.getElementById("divMsg").innerHTML = "deu erro";
        })

        
        
}

function showEditModal(cod,nome,cli,descricao,tipoarq) {
    $("#txtEditCodigo").val(cod);
    $("#txtEditCliente").val(cli).trigger("change.select2");
    $("#txtEditDescricao").val(descricao);
    $("#txtEditTipoArq").val(tipoarq).trigger("change.select2");
    $("#txtEditNome").val(nome);

    $('#editRowModal').modal('show');
}

function alterar() {

    var fd = new FormData();
    var codigo = $("#txtEditCodigo").val();
    var cliente = $("#txtEditCliente").val();
    var tipoarq = $("#txtEditTipoArq").val();
    var descricao = $("#txtEditDescricao").val();
    var arquivo = document.getElementById("arquivoGeralEdit").files[0];
    fd.append("codigo", codigo);
    fd.append("cliente", cliente);
    fd.append("tipoarq", tipoarq);
    fd.append("descricao", descricao);
    fd.append("arquivo", arquivo);

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
                title: 'Gravado com sucesso!',
                showConfirmButton: false,
                timer: 1500
            });
            $('#editRowModal').modal('hide');


            obterTabela();
            limpaCampos();

        })
        .catch(function () {

            document.getElementById("divMsg").innerHTML = "deu erro";
        })

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

function carregaTipoArquivo(res) {
    
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



function carregaTabela(res) {
    var data = [];

    var action;
    var toggle;
    for (var i = 0; i < res.length; i++) {
        action = '<td> <button type="button" class="btn-outline-primary btn-rounded mb-1" onclick="showEditModal(\'' + res[i].cod + '\',\'' + res[i].nome + '\',\'' + res[i].cli.cod + '\',\'' + res[i].descricao + '\',\'' + res[i].tipoarq.cod_tipo + '\')">Editar</button> </td> <td> <button class="btn-danger mb-1" onclick ="excluir(' + res[i].cod + ')">Excluir </button></td><td> <button class="btn-outline-primary btn-rounded mb-1" onclick ="view(' + res[i].cod + ')">View </button></td><td>  <button class="btn-danger mb-1" onclick ="get(' + res[i].cod + ')"><svg xmlns="http://www.w3.org/2000/svg" width="13" height="13" viewBox="0 0 22 22" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-download"><path d="M21 15v4a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2v-4"></path><polyline points="7 10 12 15 17 10"></polyline><line x1="12" y1="15" x2="12" y2="3"></line></svg> </button></td>';

        data.push([
            res[i].cod,
            res[i].cli.nome,
            res[i].descricao,
            res[i].tipoarq.descricao,
            res[i].nome,
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
    window.open('/RegistrarArquivo/ViewArquivo?cod='+cod,'_blank');
}

function get(cod) {
    window.open('/RegistrarArquivo/GetArquivo?cod=' + cod, '_blank');
}

function obterTabela() {
    limparTabelaArquivo();

    $.ajax({
        type: 'GET',
        url: '/RegistrarArquivo/ObterTabela',
        dataType: 'json',
        success: function (res) {

            if (res != null)
                carregaTabela(res.arquivos);

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
                        $('#addRowModal').modal('hide');

                        
                        obterTabela();
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

function limparTabelaArquivo() {
    $('#tableArquivo').DataTable().clear().destroy();
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