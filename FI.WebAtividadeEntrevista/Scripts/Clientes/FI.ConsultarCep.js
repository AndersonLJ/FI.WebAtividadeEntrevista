function limpa_form_cep() {
    document.getElementById('Logradouro').value = ("");
    document.getElementById('Cidade').value = ("");
    document.getElementById('Estado').value = ("");
}

function meu_callback(conteudo) {
    if (!("erro" in conteudo)) {
        document.getElementById('Logradouro').value = (conteudo.logradouro) + ", " + (conteudo.bairro);
        document.getElementById('Cidade').value = (conteudo.localidade);
        document.getElementById('Estado').value = (conteudo.uf);
    }
    else {
        limpa_form_cep();
        alert("CEP não encontrado.");
    }
}

function pesquisa_cep(valor) {
    var cep = valor.replace(/\D/g, '');

    if (cep != "") {
        var validacep = /^[0-9]{8}$/;

        if (validacep.test(cep)) {
            document.getElementById('Logradouro').value = "...";
            document.getElementById('Cidade').value = "...";
            document.getElementById('Estado').value = "...";

            var script = document.createElement('script');
            script.src = 'https://viacep.com.br/ws/' + cep + '/json/?callback=meu_callback';

            document.body.appendChild(script);

        } 
        else {
            limpa_form_cep();
            alert("Formato de CEP inválido.");
        }
    } 
    else {
        limpa_form_cep();
    }
};