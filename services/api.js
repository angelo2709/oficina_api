const BASE_URL = process.env.API_URL;

async function tratarResposta(response) {

    if (response.ok) {

        if (response.status === 204) {
            return null;
        }

        return await response.json();
    }

    let mensagem = `Erro ${response.status}`;

    try {

        const erro = await response.json();

        if (erro.title) {
            mensagem = erro.title;
        }

        if (erro.errors) {

            const detalhes = [];

            Object.keys(erro.errors).forEach(campo => {
                detalhes.push(
                    `${campo}: ${erro.errors[campo].join(', ')}`
                );
            });

            mensagem += ' - ' + detalhes.join(' | ');
        }

    } catch {
        try {
            mensagem = await response.text();
        } catch {
        }
    }

    throw new Error(mensagem);
}

async function get(endpoint) {

    const response =
        await fetch(`${BASE_URL}${endpoint}`);

    return tratarResposta(response);
}

async function post(endpoint, data) {

    const response =
        await fetch(`${BASE_URL}${endpoint}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        });

    return tratarResposta(response);
}

async function put(endpoint, data) {

    const response =
        await fetch(`${BASE_URL}${endpoint}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        });

    return tratarResposta(response);
}

async function del(endpoint) {

    const response =
        await fetch(`${BASE_URL}${endpoint}`, {
            method: 'DELETE'
        });

    return tratarResposta(response);
}

module.exports = {
    get,
    post,
    put,
    del
};