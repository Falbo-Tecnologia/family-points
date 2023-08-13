CREATE SCHEMA dbo;

SET search_path TO dbo;

CREATE TABLE usuario (
	id smallint NOT NULL GENERATED ALWAYS AS IDENTITY ,
	id_tipo_usuario smallint NOT NULL,
	nome varchar(64) NOT NULL,
	email varchar(128) NOT NULL,
	apelido varchar(16) NOT NULL,
	senha varchar(64) NOT NULL,
	data_cadastro timestamp NOT NULL,
	usuario_cadastro smallint NOT NULL,
	CONSTRAINT pk_usuario PRIMARY KEY (id),
	CONSTRAINT uq_usuario__email UNIQUE (email),
	CONSTRAINT uq_usuario__apelido UNIQUE (apelido)
);

CREATE TABLE tarefa (
	id smallint NOT NULL GENERATED ALWAYS AS IDENTITY ,
	id_usuario smallint NOT NULL,
	descricao varchar(128) NOT NULL,
	pontuacao smallint NOT NULL,
	data_cadastro timestamp NOT NULL,
	usuario_cadastro smallint NOT NULL,
	CONSTRAINT pk_tarefa PRIMARY KEY (id)
);

CREATE TABLE opcao_sistema (
	id smallint NOT NULL GENERATED ALWAYS AS IDENTITY ,
	id_opcao_mae smallint,
	descricao varchar(32) NOT NULL,
	CONSTRAINT pk_opcao_sistema PRIMARY KEY (id)
);

CREATE TABLE usuario_opcao (
	id_usuario smallint NOT NULL,
	id_opcao_sistema smallint NOT NULL,
	data_cadastro timestamp NOT NULL,
	usuario_cadastro smallint NOT NULL,
	CONSTRAINT pk_usuario_opcao PRIMARY KEY (id_usuario,id_opcao_sistema)
);

CREATE TABLE tipo_usuario (
	id smallint NOT NULL GENERATED ALWAYS AS IDENTITY ,
	tipo varchar(64) NOT NULL,
	CONSTRAINT "pk_ tipo_usuario" PRIMARY KEY (id)
);

ALTER TABLE usuario ADD CONSTRAINT fk_usuario__tipo_usuario FOREIGN KEY (id)
REFERENCES tipo_usuario (id) MATCH SIMPLE
ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE tarefa ADD CONSTRAINT fk_tarefa__usuario FOREIGN KEY (id_usuario)
REFERENCES usuario (id) MATCH SIMPLE
ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE opcao_sistema ADD CONSTRAINT fk_opcao_sistema__opcao_sistema FOREIGN KEY (id_opcao_mae)
REFERENCES opcao_sistema (id) MATCH SIMPLE
ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE usuario_opcao ADD CONSTRAINT fk_usuario_opcao__usuario FOREIGN KEY (id_usuario)
REFERENCES usuario (id) MATCH SIMPLE
ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE usuario_opcao ADD CONSTRAINT fk_usuario_opcao__opcao_sistema FOREIGN KEY (id_opcao_sistema)
REFERENCES opcao_sistema (id) MATCH SIMPLE
ON DELETE NO ACTION ON UPDATE NO ACTION;


