$(document).ready(function () {
    LimpaProdutos();
    AtualizaProdutos();

    $('#InserirProduto').click(function () {
        var formulario = $("#produto").serialize();
        $.ajax({
            type: "POST",
            url: "/Produto/InsereProduto",
            data: formulario,
            success: function (result) {
                if (result.success) {
                    if (result.message != undefined) {
                        Mensagem(result.message, 'sucesso');
                        LimpaProdutos();
                        AtualizaProdutos();
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

    $('#AlterarProduto').click(function () {
        var formulario = $("#produto").serialize();
        $.ajax({
            type: "POST",
            url: "/Produto/AlterarProduto",
            data: formulario,
            success: function (result) {
                if (result.success) {
                    if (result.message != undefined) {
                        Mensagem(result.message, 'sucesso');
                        LimpaProdutos();
                        AtualizaProdutos();
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
        LimpaProdutos();
    });
});

function AtualizaProdutos() {
    $.ajax({
        type: "GET",
        url: "/Produto/GridProduto",
        success: function (result) {
            $("#listaproduto").html(result);
            $('#tableproduto').DataTable({
                language: { "url": "//cdn.datatables.net/plug-ins/1.10.21/i18n/Portuguese-Brasil.json" },
                responsive: true
            });
            $('[data-toggle="tooltip"]').tooltip();
        }
    });
}

function ExcluirProdutos(id) {
    $.ajax({
        type: "POST",
        url: "/Produto/ExcluirProduto",
        data: { "id": id },
        success: function (result) {
            Mensagem(result.message, 'sucesso');
            LimpaProdutos();
            AtualizaProdutos();
        }
    });
}

function LimpaProdutos() {
    $.ajax({
        type: "POST",
        url: "/Produto/PesquisaProdutoId",
        data: { "id": 0, "tipoRetorno": "View" },
        success: function (result) {
            $('#verproduto').html(result);
            $("#InserirProduto").removeClass("hidden");
            $("#AlterarProduto").addClass("hidden");
            $("#Cancelar").addClass("hidden");
            document.getElementById("Referencia").value = "";
            document.getElementById("Descricao").value = "";
            $("#Referencia").focus();
        }
    });
}

function Manutencao(id, acao) {
    var cReferencia = " ";
    var cDescricao = " ";
    $.ajax({
        type: "POST",
        url: "/Produto/PesquisaProdutoId",
        data: { "id": id, "tipoRetorno": "JSon" },
        success: function (result) {
            document.getElementById("Id").value = result.id;
            document.getElementById("Referencia").value = result.referencia;
            document.getElementById("Descricao").value = result.descricao;
            cReferencia = result.referencia;
            cDescricao = result.descricao;
            // Editar
            if (acao == 1) {
                $("#InserirProduto").addClass("hidden");
                $("#AlterarProduto").removeClass("hidden");
                $("#Cancelar").removeClass("hidden");
            }
            // Excluir
            if (acao == 0) {
                AlertSW({
                    titulo: "Atenção !!!",
                    mensagem: "Confirma Exclusão do Produto " + cReferencia + "-" + cDescricao + " ?",
                    textconfirm: "Sim, Confirmo",
                    textcancel: "Não Quero Excluir",
                    funcaoconfirm: { funcao: "ExcluirProdutos", parametro: id }
                });
            }
        }
    });

};


