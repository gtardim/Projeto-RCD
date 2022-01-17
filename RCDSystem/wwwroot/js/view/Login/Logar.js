
function verificaLogin() {

    var usuario = $('#username').val()
    var senha = $('#password').val()


    $.ajax({

        type: 'POST',
        url: '/Login/ValidaUsuario',
        dataType: 'json',
        data: {
            usuario,senha
        },
        success: function (res) {
            if (res.u == true && res.s == true && res.a == true) {
                LogarAsync(usuario);
            }
            else {

                if (res.u == true && res.s == false && res.a == true) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Senha incorreta!!',
                        showConfirmButton: false,
                        timer: 1500
                    });

                }
                else {

                    if (res.u == true && res.s == true && res.a == false) {

                        Swal.fire({
                            icon: 'error',
                            title: 'Usuario Inativo !!',
                            showConfirmButton: false,
                            timer: 1500
                        });
                    }
                    else {

                        if (res.u == false) {

                            Swal.fire({
                                icon: 'error',
                                title: 'Usuario nao existe !!',
                                showConfirmButton: false,
                                timer: 1500
                            });
                        }
                    }  
                }
            }
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });
}

function LogarAsync(usuario) {

    $.ajax({

        type: 'POST',
        url: '/Login/Logar',
        dataType: 'json',
        data: {
            usuario
        },
        success: function (res) {
            window.location.href = "/Home/Index";
        },
        error: function (XMLHttpRequest, txtStatus, errorThrown) {
            alert("Status: " + txtStatus); alert("Error: " + errorThrown);
        }
    });
}

function logar() {

    var usuario = $('#username').val()
    var senha = $('#password').val()
    

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