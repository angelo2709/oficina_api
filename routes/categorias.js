const express = require('express');
const router = express.Router();

const api = require('../services/api');

router.get('/', async (req, res, next) => {
    try {

        const categorias = await api.get('/Categorias');

        res.render('categorias/index', {
            categorias
        });

    } catch (err) {
        next(err);
    }
});

router.get('/novo', (req, res) => {

    res.render('categorias/form', {
        categoria: {},
        titulo: 'Nova Categoria'
    });
});

router.post('/', async (req, res, next) => {

    try {

        const categoria = {
            categoriaNome: req.body.categoriaNome,
            categoriaDescricao:
                req.body.categoriaDescricao || null
        };

        await api.post('/Categorias', categoria);

        res.redirect('/categorias');

    } catch (err) {
        next(err);
    }
});

router.get('/:id/editar', async (req, res, next) => {

    try {

        const categoria =
            await api.get(`/Categorias/${req.params.id}`);

        res.render('categorias/form', {
            categoria,
            titulo: 'Editar Categoria'
        });

    } catch (err) {
        next(err);
    }
});

router.put('/:id', async (req, res, next) => {

    try {

        const categoria = {
            categoriaId: Number(req.params.id),
            categoriaNome: req.body.categoriaNome,
            categoriaDescricao:
                req.body.categoriaDescricao || null
        };

        await api.put(
            `/Categorias/${req.params.id}`,
            categoria
        );

        res.redirect('/categorias');

    } catch (err) {
        next(err);
    }
});

router.delete('/:id', async (req, res, next) => {

    try {

        await api.del(`/Categorias/${req.params.id}`);

        res.redirect('/categorias');

    } catch (err) {
        next(err);
    }
});

module.exports = router;