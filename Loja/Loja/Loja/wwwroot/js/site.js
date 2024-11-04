// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function Notification(message, tipo, titulo) {
    titulo = titulo == null ? 'Alerta' : titulo;
    Swal.fire(titulo, message, tipo);

    //if (tipo == 'erro') {
    //    $.confirm({
    //        icon: 'warning',
    //        title: 'Atenção !!!',
    //        closeIcon: true,
    //        closeIconClass: 'fa fa-close',
    //        content: message,
    //        type: 'red',
    //        typeAnimated: true,
    //        autoClose: 'fechar|2000',
    //        buttons: {
    //            fechar: {
    //                text: 'Fechar',
    //                btnClass: 'btn-red',
    //                action: function () {
    //                }
    //            }
    //        }
    //    });
    //}
    //if (tipo == 'sucesso') {
    //    $.confirm({
    //        icon: 'success',
    //        title: 'Informação !!!',
    //        closeIcon: true,
    //        closeIconClass: 'fa fa-close',
    //        content: message,
    //        type: 'blue',
    //        typeAnimated: true,
    //        autoClose: 'fechar|2000',
    //        buttons: {
    //            fechar: {
    //                text: 'Fechar',
    //                btnClass: 'btn-blue',
    //                action: function () {
    //                }
    //            }
    //        }
    //    });
    //}

};


function Mensagem(message, tipo) {
    const Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 2000,
        timerProgressBar: true,

    });

    if (tipo == 'erro') {
        Toast.fire({
            icon: 'error',
            title: message
        });
    }
    if (tipo == 'sucesso') {
        Toast.fire({
            icon: 'success',
            title: message
        });
    }
};


function AlertSW(jSonAtrib) {
    Swal.fire({
        title: (jSonAtrib.titulo != null ? jSonAtrib.titulo : "Atenção !!!"),
        text: (jSonAtrib.mensagem != null ? jSonAtrib.mensagem : "<Não Ha Mensagem>"),
        icon: (jSonAtrib.icone != null ? jSonAtrib.icone : 'warning'),
        showDenyButton: true,
        confirmButtonColor: '#dc3545',
        denyButtonColor: '#6c757d',
        confirmButtonText: (jSonAtrib.textconfirm != null ? jSonAtrib.textconfirm : "Confirma"),
        denyButtonText: (jSonAtrib.textcancel != null ? jSonAtrib.textcancel : "Cancelar")
    }).then((result) => {
        if (result.isConfirmed) {
            if (jSonAtrib.funcaoconfirm != null) {
                if (jSonAtrib.funcaoconfirm.parametro == null) {
                    console.log("nada");
                    eval(jSonAtrib.funcaoconfirm.funcao + "()");
                } else {

                    if ($.isNumeric(jSonAtrib.funcaoconfirm.parametro)) {
                        console.log("numero");
                        eval(jSonAtrib.funcaoconfirm.funcao + "(" + jSonAtrib.funcaoconfirm.parametro + ")");
                    } else {
                        console.log("character");
                        eval(jSonAtrib.funcaoconfirm.funcao + "(" + jSonAtrib.funcaoconfirm.parametro + ")");
                    }
                }
            }
            if (jSonAtrib.messageconfirm != null) {
                Swal.fire(
                    jSonAtrib.messageconfirm.titulo,
                    jSonAtrib.messageconfirm.mensagem,
                    jSonAtrib.messageconfirm.icone
                );
            }
        }
        else if (result.isDenied) {
            if (jSonAtrib.funcaocancel != null) {
                if (jSonAtrib.funcaocancel.parametro == null) {
                    eval(jSonAtrib.funcaocancel.funcao + "()");
                } else {

                    if ($.isNumeric(jSonAtrib.funcaocancel.parametro)) {
                        eval(jSonAtrib.funcaocancel.funcao + "(" + jSonAtrib.funcaocancel.parametro + ")");
                    } else {
                        eval(jSonAtrib.funcaocancel.funcao + "(" + jSonAtrib.funcaocancel.parametro + ")");
                    }
                }
            }
            if (jSonAtrib.messagecancel != null) {
                Swal.fire(
                    jSonAtrib.messagecancel.titulo,
                    jSonAtrib.messagecancel.mensagem,
                    jSonAtrib.messagecancel.icone
                );
            }
        }
        return result.value;
    });
};