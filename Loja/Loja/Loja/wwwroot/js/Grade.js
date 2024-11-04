$(document).ready(function () {
    LimpaGrade(0);
    AtualizaGrade(0);

    $('#InserirGrade').click(function () {
        var formulario = $("#grade").serialize();
        var id = document.getElementById("Id").value;
        $.ajax({
            type: "POST",
            url: "/Grade/InsereGrade",
            data: formulario,
            success: function (result) {
                if (result.success) {
                    if (result.message != undefined) {
                        Mensagem(result.message, 'sucesso');
                        AtualizaGrade(id);
                        $("#Tamanho").focus();
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

    $('#AlterarGrade').click(function () {
        var formulario = $("#grade").serialize();
        var nidprod = document.getElementById("IdProd").value;
        $.ajax({
            type: "POST",
            url: "/Grade/AlterarGrade",
            data: formulario,
            success: function (result) {
                if (result.success) {
                    if (result.message != undefined) {
                        Mensagem(result.message, 'sucesso');
                        LimpaGrade(nidprod);
                        AtualizaGrade(nidprod);
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
        var nidprod = document.getElementById("IdProd").value;
        LimpaGrade(nidprod);
    });

});

function AtualizaGrade(id) {
    $.ajax({
        type: "GET",
        url: "/Grade/GridGrade",
        data: { Id: id },
        success: function (result) {
            $("#listagrade").html(result);
            $('#tablegrade').DataTable({
                language: { "url": "//cdn.datatables.net/plug-ins/1.10.21/i18n/Portuguese-Brasil.json" },
                responsive: true
            });
            $('[data-toggle="tooltip"]').tooltip();
        }
    });
}

function ExcluirGrade(id, idprod) {
    $.ajax({
        type: "POST",
        url: "/Grade/ExcluirGrade",
        data: { "id": id },
        success: function (result) {
            Mensagem(result.message, 'sucesso');
            AtualizaGrade(idprod);
        }
    });
}

function LimpaGrade(id) {
    $.ajax({
        type: "POST",
        url: "/Grade/PesquisaGradeId",
        data: { id: id, tipoRetorno: "View", acao: "Inserir" },
        success: function (result) {
            $('#vergrade').html(result);
            $("#InserirGrade").removeClass("hidden");
            $("#AlterarGrade").addClass("hidden");
            $("#Cancelar").addClass("hidden");
            //document.getElementById("Referencia").value = result.referencia;
            //  document.getElementById("Descricao").value = result.descricao;
           // $("#Referencia").focus();
        }
    });
}

function Manutencao(id, acao) {
    $.ajax({
        type: "POST",
        url: "/Grade/PesquisaGradeId",
        data: { id: id, tipoRetorno: "JSon", acao: "Alteracao"},
        success: function (result) {
            cDescrExcl = result.gradeDetalhe.tamanho + "-" + result.gradeDetalhe.padrao + "-" + result.gradeDetalhe.cor1;
            if (result.gradeDetalhe.cor2 != null) {
                cDescrExcl = cDescrExcl + "-" + result.gradeDetalhe.cor2;
            }
            if (result.gradeDetalhe.cor3 != null) {
                cDescrExcl = cDescrExcl + "-" + result.gradeDetalhe.cor3;
            }
            nidprod = result.idProd;
            // Editar
            if (acao == 1) {
                $("#InserirGrade").addClass("hidden");
                $("#AlterarGrade").removeClass("hidden");
                $("#Cancelar").removeClass("hidden");
                document.getElementById("Id").value = result.id;
                document.getElementById("IdProd").value = result.idProd;
                document.getElementById("Tamanho").value = result.tamanho;
                document.getElementById("Padrao").value = result.padrao;
                document.getElementById("Cor1").value = result.cor1;
                document.getElementById("Cor2").value = result.cor2;
                document.getElementById("Cor3").value = result.cor3;
            }
            // Excluir
            if (acao == 0) {
                AlertSW({
                    titulo: "Atenção !!!",
                    mensagem: "Confirma Exclusão da Grade " + cDescrExcl + " ?",
                    textconfirm: "Sim, Confirmo",
                    textcancel: "Não Quero Excluir",
                    funcaoconfirm: { funcao: "ExcluirGrade", parametro: id+","+nidprod }
                });
            }
        }
    });

};

function TrasProduto() {
    var referencia = document.getElementById("Referencia").value;
    $.ajax({
        type: "POST",
        url: "/Produto/PesquisaProdutoReferencia",
        data: { referencia: referencia },
        success: function (result) {
            $.ajax({
                type: "POST",
                url: "/Grade/PesquisaGradeId",
                data: { id: result.id, tipoRetorno: "View", acao: "Inserir" },
                success: function (data) {
                    $('#vergrade').html(data);
                    AtualizaGrade(result.id);
                }
            });
        }
    });

};