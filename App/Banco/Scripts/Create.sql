-- tabela usuario
CREATE TABLE usuario (
	id int generated always as identity,
	nome varchar(64) NOT NULL,
	email varchar(128) NOT NULL,
	login varchar(128) NOT NULL,
	senha varchar(64) NOT NULL,
	data_cadastro timestamp NOT NULL,
    usuario_cadastro int not null,
	CONSTRAINT pk_usuario PRIMARY KEY (id),
	CONSTRAINT uq_usuario_email UNIQUE (email),
	CONSTRAINT uq_usuario_login UNIQUE (login)
);

-- Inserts for usuario table
INSERT INTO usuario (nome, email, login, senha, data_cadastro) 
VALUES
('Thomaz Falbo', 'thomazfalbo@falbotec.com', 'thofalbo', '123', now());

-- tabela tarefa
CREATE TABLE tarefa (
    id int generated always as identity,
    id_usuario int NOT NULL,
    descricao varchar(128) NOT NULL,
    pontuacao int NOT NULL,
    data_cadastro timestamp NOT NULL,
    usuario_cadastro int not null,
    CONSTRAINT pk_tarefa PRIMARY KEY (id),
    CONSTRAINT fk_tarefa_usuario FOREIGN KEY (id_usuario) REFERENCES usuario (id)
);

-- Inserts for tarefa table
INSERT INTO tarefa (id_usuario, descricao, pontuacao, data_cadastro, usuario_cadastro)
VALUES
(1, 'Dormir no horário', 1, now(), 1);