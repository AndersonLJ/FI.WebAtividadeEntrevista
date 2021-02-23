$(document).ready(function () {
    $('#formBeneficiario').submit(function (e) {
        e.preventDefault();
        $.ajax({
            url: urlPostBeneficiario,
            method: "POST",
            data: {
                "Nome": $(this).find("#NomeBeneficiario").val(),
                "Cpf": $(this).find("#CpfBeneficiario").val(),
                "IdCliente": obj.id
            },
            error:
                function (r) {
                    if (r.status == 400)
                        alertDialog("danger", "Erro", r.responseJSON);
                    else if (r.status == 500)
                        alertDialog("danger", "Erro", "Ocorreu um erro interno no servidor.");
                },
            success:
                function (r) {
                    alertDialog("success", "Sucesso", r);
                    $("#formBeneficiario")[0].reset();
                    listBeneficiarios();
                }
        });
    });
});


function alertDialog(alert, titulo, texto) {
    var html =
        '<div class="alert alert-' + alert + ' alert-dismissible" role="alert">'+
        '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>'+
        '<strong>'+titulo+': </strong> ' +  texto +
        '</div>';

    document.getElementById("alertBeneficiario").innerHTML = html;
}


document.getElementById("cadBeneficiarios").onclick = function () {
    listBeneficiarios();
}

function listBeneficiarios() {
    $.post(urlRetornoBeneficiario,
        function (resultado) {
            var htmlList = "";
            for (var i = 0; i < resultado.Records.length; i++) {
                var html =
                    '<tr>' +
                        '<td>' + resultado.Records[i].Cpf + '</td>' +
                        '<td>' + resultado.Records[i].Nome + '</td>' +
                        '<td><button class="btn btn-success">Alterar</button></td>' +
                        '<td><button class="btn btn-danger">Excluir</button></td>' +
                        '</tr>';

                htmlList += html;
            }

            document.querySelector("#listBeneficiarios").innerHTML = htmlList;
        });
}