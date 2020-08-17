IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200729175752_Initial')
BEGIN
    CREATE TABLE [Autores] (
        [id] int NOT NULL IDENTITY,
        [nombre] nvarchar(max) NULL,
        CONSTRAINT [PK_Autores] PRIMARY KEY ([id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200729175752_Initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200729175752_Initial', N'3.1.6');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200729204210_Libros')
BEGIN
    CREATE TABLE [Libros] (
        [id] int NOT NULL IDENTITY,
        [titulo] nvarchar(max) NOT NULL,
        [autorId] int NOT NULL,
        CONSTRAINT [PK_Libros] PRIMARY KEY ([id]),
        CONSTRAINT [FK_Libros_Autores_autorId] FOREIGN KEY ([autorId]) REFERENCES [Autores] ([id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200729204210_Libros')
BEGIN
    CREATE INDEX [IX_Libros_autorId] ON [Libros] ([autorId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200729204210_Libros')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200729204210_Libros', N'3.1.6');
END;

GO

