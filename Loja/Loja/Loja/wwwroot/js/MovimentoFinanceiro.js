$(document).ready(function () {
    $("[data-tt=tooltip]").tooltip();

    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl)
    })

    $('#btnCancelar').click(function () {
        //alert("Cancelar");
    });
    $('#btnSalvar').click(function () {
        //alert("Salvar");
    });
});

function AlteraAno(MaisMenos) {
    if (MaisMenos > 0) {
        document.getElementById("AnoAtivo").value = String(parseInt(document.getElementById("AnoAtivo").value) + 1);
    }
    if (MaisMenos < 0) {
        document.getElementById("AnoAtivo").value = String(parseInt(document.getElementById("AnoAtivo").value) - 1);
    }
};

function CarregaMes(lermes) {
    var MostraMes = lermes + "/" + $("#AnoAtivo").val(); //document.getElementById("AnoAtivo").value;
    $("#mesAtual").val(MostraMes);
};

function ConsultaFinanceiro(id) {
    $.ajax({
        type: "POST",
        url: "/Financeiro/Consulta_Financeiro",
        data: { "id": id },
        success: function (result) {
            $('#bodymodal').html(result);
            $('#myModal').show();
        }
    });
};

