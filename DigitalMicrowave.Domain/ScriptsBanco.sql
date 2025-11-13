-- 1. Criar a tabelas e fazer a carga inicial
IF NOT EXISTS (SELECT 1 FROM sysobjects WHERE name = 'HeatingPrograms' AND xtype = 'U')
BEGIN
    CREATE TABLE HeatingPrograms (
        Id INT NOT NULL PRIMARY KEY,
        ProgramName VARCHAR(250) NOT NULL,
        Food VARCHAR(150) NOT NULL,
        Power INT NOT NULL,
        HeatingCharacteristic VARCHAR(150) NOT NULL,
        Instructions VARCHAR(250) NULL,
        Time INT NOT NULL, -- armazenado em segundos
        ProgramDefault BIT NULL
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM HeatingPrograms WHERE Id = 1)
BEGIN
    INSERT INTO HeatingPrograms 
    (Id, ProgramName, Food, Time, Power, Instructions, HeatingCharacteristic, ProgramDefault)
    VALUES
    (1, 'Pipoca', 'Pipoca (de micro-ondas)', 180, 7,
     'Observar o barulho de estouros do milho, caso houver um intervalo de mais de 10 segundos entre um estouro e outro, interrompa o aquecimento.',
     'ppc', 1);
END

IF NOT EXISTS (SELECT 1 FROM HeatingPrograms WHERE Id = 2)
BEGIN
    INSERT INTO HeatingPrograms 
    (Id, ProgramName, Food, Time, Power, Instructions, HeatingCharacteristic, ProgramDefault)
    VALUES
    (2, 'Leite', 'Leite', 300, 5,
     'Cuidado com aquecimento de líquidos, o choque térmico aliado ao movimento do recipiente pode causar fervura imediata causando risco de queimaduras.',
     'lt', 1);
END

IF NOT EXISTS (SELECT 1 FROM HeatingPrograms WHERE Id = 3)
BEGIN
    INSERT INTO HeatingPrograms 
    (Id, ProgramName, Food, Time, Power, Instructions, HeatingCharacteristic, ProgramDefault)
    VALUES
    (3, 'Carnes de boi', 'Carne em pedaço ou fatias', 840, 4,
     'Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o descongelamento uniforme.',
     'cnb', 1);
END

IF NOT EXISTS (SELECT 1 FROM HeatingPrograms WHERE Id = 4)
BEGIN
    INSERT INTO HeatingPrograms 
    (Id, ProgramName, Food, Time, Power, Instructions, HeatingCharacteristic, ProgramDefault)
    VALUES
    (4, 'Frango', 'Frango (qualquer corte)', 480, 7,
     'Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o descongelamento uniforme.',
     'cnfg', 1);
END

IF NOT EXISTS (SELECT 1 FROM HeatingPrograms WHERE Id = 5)
BEGIN
    INSERT INTO HeatingPrograms 
    (Id, ProgramName, Food, Time, Power, Instructions, HeatingCharacteristic, ProgramDefault)
    VALUES
    (5, 'Feijão', 'Feijão congelado', 480, 9,
     'Deixe o recipiente destampado e em casos de plástico, cuidado ao retirar o recipiente pois o mesmo pode perder resistência em altas temperaturas.',
     'fj', 1);
END
GO

IF NOT EXISTS (SELECT 1 FROM sysobjects WHERE name = 'Users' AND xtype = 'U')
BEGIN
    CREATE TABLE [dbo].[Users](
	[Username] [varchar](100) NOT NULL,
	[PasswordHash] [varchar](250) NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT 1 FROM Users WHERE Id = 1)
BEGIN
    INSERT INTO Users (Username, PasswordHash)
VALUES ('admin', '7c4a8d09ca3762af61e59520943dc26494f8941b');
END
