CREATE TABLE [Locations] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(450) NULL,
    [Address] nvarchar(450) NULL,
    [PostCode] nvarchar(max) NULL,
    CONSTRAINT [PK_Locations] PRIMARY KEY ([Id])
);

GO

CREATE UNIQUE INDEX [IX_Locations_Address] ON [Locations] ([Address]) WHERE [Address] IS NOT NULL;

GO

CREATE UNIQUE INDEX [IX_Locations_Name] ON [Locations] ([Name]) WHERE [Name] IS NOT NULL;

GO

CREATE TABLE [Logs] (
    [Id] int NOT NULL IDENTITY,
    [LocationId] int NOT NULL,
    [Date] datetime2 NOT NULL,
    [Action] int NOT NULL,
    CONSTRAINT [PK_Logs] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Logs_Locations_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [Locations] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Logs_LocationId] ON [Logs] ([LocationId]);

GO
