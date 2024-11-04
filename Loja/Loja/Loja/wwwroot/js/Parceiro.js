$(document).ready(function () {
    LimpaParceiros();
    AtualizaParceiros();

    $('#InserirParceiro').click(function () {
        var formulario = $("#Parceiro").serialize();
        $.ajax({
            type: "POST",
            url: "/Parceiro/InsereParceiro",
            data: formulario,
            success: function (result) {
                if (result.success) {
                    if (result.message != undefined) {
                        Mensagem(result.message, 'sucesso');
                        LimpaParceiros();
                        AtualizaParceiros();
                    }
                }
                else {
                    if (result.message != undefined) {
                        Mensagem(result.message, 'erro');
                    }
                }
            },
            error: function (result) {
                //alert(result.message);
            }

        });
    });

    $('#AlterarParceiro').click(function () {
        var formulario = $("#Parceiro").serialize();
        console.log(formulario);
        $.ajax({
            type: "POST",
            url: "/Parceiro/AlterarParceiro",
            data: formulario,
            success: function (result) {
                if (result.success) {
                    if (result.message != undefined) {
                        Mensagem(result.message, 'sucesso');
                        LimpaParceiros();
                        AtualizaParceiros();
                    }
                }
                else {
                    if (result.message != undefined) {
                        Mensagem(result.message, 'erro');
                    }
                }
            },
            error: function (result) {
                //alert(result.message);
            }

        });
    });

    $('#Cancelar').click(function () {
        LimpaParceiros();
    });

});

function AtualizaParceiros() {
    $.ajax({
        type: "GET",
        url: "/Parceiro/GridParceiro",
        success: function (result) {
            $("#listaParceiro").html(result);
            $('#tableParceiro').DataTable({
                language: { "url": "//cdn.datatables.net/plug-ins/1.10.21/i18n/Portuguese-Brasil.json" },
                responsive: true
            });
            $('[data-toggle="tooltip"]').tooltip();
        }
    });
}

function ExcluirParceiros(id) {
    $.ajax({
        type: "POST",
        url: "/Parceiro/ExcluirParceiro",
        data: { "id": id },
        success: function (result) {
            Mensagem(result.message, 'sucesso');
            LimpaParceiros();
            AtualizaParceiros();
        }
    });
}

function LimpaParceiros() {
    $.ajax({
        type: "POST",
        url: "/Parceiro/PesquisaParceiroId",
        data: { "id": 0, "tipoRetorno": "View" },
        success: function (result) {
            $('#verParceiro').html(result);
            $("#InserirParceiro").removeClass("hidden");
            $("#AlterarParceiro").addClass("hidden");
            $("#Cancelar").addClass("hidden");
        }
    });
}

function Manutencao(id, acao) {
    var cNome = " ";
    $.ajax({
        type: "POST",
        url: "/Parceiro/PesquisaParceiroId",
        data: { "id": id, "tipoRetorno": "View" },
        success: function (result) {
            document.getElementById("Id").value = result.id;
            $('#verParceiro').html(result);

            // Atualiza as Mascaras na View
            var tamanho = $("#Cnpj_cpf").val().replace(/\D/g, '').length;
            if (tamanho == 11) {
                $("#Cnpj_cpf").mask("999.999.999-99");
            } else if (tamanho == 14) {
                $("#Cnpj_cpf").mask("99.999.999/9999-99");
            }
            $("#Cep").mask("00.000-000");
            $("#Celular").mask("(00) 00000-0000");

            cNome = document.getElementById("Nome").value;
            // Editar
            if (acao == 1) {
                $("#InserirParceiro").addClass("hidden");
                $("#AlterarParceiro").removeClass("hidden");
                $("#Cancelar").removeClass("hidden");
            }
            // Excluir
            if (acao == 0) {
                AlertSW({
                    titulo: "Atenção !!!",
                    mensagem: "Confirma Exclusão do Parceiro " + cNome + " ?",
                    textconfirm: "Sim, Confirmo",
                    textcancel: "Não Quero Excluir",
                    funcaoconfirm: { funcao: "ExcluirParceiros", parametro: id }
                });
            }
        }
    });

};


