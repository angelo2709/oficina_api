require('dotenv').config();

const express = require('express');
const methodOverride = require('method-override');
const path = require('path');

const categoriasRouter = require('./routes/categorias');
const fabricantesRouter = require('./routes/fabricantes');
const fornecedoresRouter = require('./routes/fornecedores');
const pecasRouter = require('./routes/pecas');

const app = express();

app.set('view engine', 'ejs');
app.set('views', path.join(__dirname, 'views'));

app.use(express.urlencoded({ extended: true }));
app.use(express.json());

app.use(methodOverride('_method'));

app.use(express.static(path.join(__dirname, 'public')));

app.get('/', (req, res) => {
    res.redirect('/categorias');
});

app.use('/categorias', categoriasRouter);
app.use('/fabricantes', fabricantesRouter);
app.use('/fornecedores', fornecedoresRouter);
app.use('/pecas', pecasRouter);

app.use((err, req, res, next) => {
    console.error(err);

    res.status(500).render('erro', {
        mensagem: err.message || 'Erro interno do servidor'
    });
});

const PORT = process.env.PORT || 3000;

app.listen(PORT, () => {
    console.log(`Frontend executando em http://localhost:${PORT}`);
});