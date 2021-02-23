$(document).ready(function () {
    $('#formBeneficiario').submit(function (e) {
        e.preventDefault();
        var idBeneficiario = $(this).find("#IdBeneficiario").val();
        var urlPost = urlAlterarBeneficiario;
        var dados = {
            "Id": idBeneficiario,
            "Nome": $(this).find("#NomeBeneficiario").val(),
            "Cpf": $(this).find("#CpfBeneficiario").val(),
            "IdCliente": obj.id
        }

        if (idBeneficiario === "") {
            urlPost = urlIncluirBeneficiario;
            dados = {
                "Nome": $(this).find("#NomeBeneficiario").val(),
                "Cpf": $(this).find("#CpfBeneficiario").val(),
                "IdCliente": obj.id
            }
        }

        $.ajax({
            url: urlPost,
            method: "POST",
            data: dados,
            error:
                function (r) {
                    if (r.status == 400)
                        alertDialog("danger", "Erro", r.responseJSON);
                    else if (r.status == 500)
                        alertDialog("danger", "Erro", "Ocorreu um erro interno no servidor.");

                    $("#formBeneficiario")[0].reset();
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
        '<div class="alert alert-' + alert + ' alert-dismissible" role="alert">' +
        '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
        '<strong>' + titulo + ': </strong> ' + texto +
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
                var beneficiario = resultado.Records[i];
                var html =
                    '<tr id="beneficiario' + beneficiario.Id + '">' +
                    '<td>' + beneficiario.Cpf + '</td>' +
                    '<td>' + beneficiario.Nome + '</td>' +
                    '<td>' +
                    '<button class="btn btn-success" onclick="alterarBeneficiario(this)" data-id="' + beneficiario.Id + '" data-cpf="' + beneficiario.Cpf + '" data-nome="' + beneficiario.Nome + '">Alterar</button>' +
                    '</td>' +
                    '<td>' +
                    '<button class="btn btn-danger" onclick="excluirBeneficiario(this)"  data-id="' + beneficiario.Id + '">Excluir</button>' +
                    '</td>' +
                    '</tr>';

                htmlList += html;
            }

            document.querySelector("#listBeneficiarios").innerHTML = htmlList;
        });
}

function alterarBeneficiario(element) {
    $("#IdBeneficiario").val($(element).data("id"));
    $("#CpfBeneficiario").val($(element).data("cpf"));
    $("#NomeBeneficiario").val($(element).data("nome"));
}

function excluirBeneficiario(element) {
    $.ajax({
        url: urlRemoverBeneficiario,
        method: "POST",
        data: {
            "id": $(element).data("id")
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
}