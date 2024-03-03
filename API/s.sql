CREATE TABLE clientes (
    id serial NOT NULL,
    saldo integer NOT NULL,
    limite integer NOT NULL,
    CONSTRAINT pk_clientes PRIMARY KEY (id)
);


CREATE TABLE transacoes (
    id serial NOT NULL,
    cliente_id integer NOT NULL,
    valor integer NOT NULL,
    tipo CHAR(1) NOT NULL,
    descricao VARCHAR(10),
    realizada_em TIMESTAMP NOT NULL DEFAULT (NOW()),
    CONSTRAINT pk_transacoes PRIMARY KEY (id)
);


INSERT INTO clientes (id, limite, saldo)
VALUES (1, 100000, 0);
INSERT INTO clientes (id, limite, saldo)
VALUES (2, 80000, 0);
INSERT INTO clientes (id, limite, saldo)
VALUES (3, 1000000, 0);
INSERT INTO clientes (id, limite, saldo)
VALUES (4, 10000000, 0);
INSERT INTO clientes (id, limite, saldo)
VALUES (5, 500000, 0);


CREATE INDEX ix_transacoes_cliente_id ON transacoes (cliente_id);


SELECT setval(
    pg_get_serial_sequence('clientes', 'id'),
    GREATEST(
        (SELECT MAX(id) FROM clientes) + 1,
        nextval(pg_get_serial_sequence('clientes', 'id'))),
    false);


