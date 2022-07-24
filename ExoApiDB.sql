CREATE DATABASE ExoApiDB
GO

USE ExoApiDB
GO

CREATE TABLE Projetos (
	Id INT PRIMARY KEY IDENTITY,
	Titulo VARCHAR(255) UNIQUE NOT NULL,
	Situacao VARCHAR(255) NOT NULL,
	DataInicio VARCHAR(50) NOT NULL,
	Descricao VARCHAR(255) NOT NULL
)
GO

INSERT INTO Projetos (Titulo, Situacao, DataInicio, Descricao) 
VALUES ('Aplicativo ComaBem', 'Finalizado', '10/03/2022', 'Aplicativo para cadastrar e listar informações de produtos')
GO

INSERT INTO Projetos (Titulo, Situacao, DataInicio, Descricao)
VALUES ('Jogo RPG Online', 'Finalizado', '20/04/2022', 'Jogo de RPG Online, cada usuário possuirá um personagem exclusivo que possuirá uma classe e poderá possuir diversas habilidades')
GO

INSERT INTO Projetos (Titulo, Situacao, DataInicio, Descricao)
VALUES ('Aplicativo Balti', 'Em desenvolvimento', '15/01/2022', 'Aplicativo para redes de clínicas veterinárias, para monitoramento de ferramentas, equipamentos e medicamentos')
GO

INSERT INTO Projetos (Titulo, Situacao, DataInicio, Descricao)
VALUES ('Uber Clone', 'Não iniciado', '20/09/2022', 'Projeto de um clone do aplicativo uber, com fins educacionais')
GO

SELECT Titulo AS 'Titulo', Situacao AS 'Status', DataInicio AS 'Data de Início', Descricao AS 'Descricao' FROM Projetos
GO


CREATE TABLE Usuarios (
	Id INT PRIMARY KEY IDENTITY,
    Email VARCHAR(255) UNIQUE NOT NULL,
    Senha VARCHAR(120) NOT NULL,
	Tipo CHAR(1) NOT NULL
)

INSERT INTO Usuarios VALUES ('usuario1@email.com', 'senha', 2)
GO

INSERT INTO Usuarios VALUES ('administrador1@email.com', 'senha', 1)
GO

UPDATE Usuarios SET Senha = '12345' WHERE Id = 2

SELECT * FROM Usuarios
