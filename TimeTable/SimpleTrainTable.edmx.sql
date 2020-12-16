
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/17/2020 07:00:10
-- Generated from EDMX file: C:\Users\Cheng\Dropbox\TimeTable\TimeTable\SimpleTrainTable.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SimpleTimeTable];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_TrainInfoStopTimes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StopTimesSet] DROP CONSTRAINT [FK_TrainInfoStopTimes];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[StopTimesSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StopTimesSet];
GO
IF OBJECT_ID(N'[dbo].[TrainInfoSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TrainInfoSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'StopTimesSet'
CREATE TABLE [dbo].[StopTimesSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TrainId] int  NOT NULL,
    [StationID] smallint  NOT NULL,
    [StationName] nvarchar(max)  NOT NULL,
    [ArrivalTime] nvarchar(max)  NOT NULL,
    [DepartureTime] nvarchar(max)  NOT NULL,
    [TrainInfoId] int  NOT NULL
);
GO

-- Creating table 'TrainInfoSet'
CREATE TABLE [dbo].[TrainInfoSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TrainNo] smallint  NOT NULL,
    [TrainTypeName] nvarchar(max)  NOT NULL,
    [StartingStationId] smallint  NOT NULL,
    [EndingStationId] smallint  NOT NULL,
    [StartingStationName] nvarchar(max)  NOT NULL,
    [EndingStationName] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'StopTimesSet'
ALTER TABLE [dbo].[StopTimesSet]
ADD CONSTRAINT [PK_StopTimesSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TrainInfoSet'
ALTER TABLE [dbo].[TrainInfoSet]
ADD CONSTRAINT [PK_TrainInfoSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [TrainInfoId] in table 'StopTimesSet'
ALTER TABLE [dbo].[StopTimesSet]
ADD CONSTRAINT [FK_TrainInfoStopTimes]
    FOREIGN KEY ([TrainInfoId])
    REFERENCES [dbo].[TrainInfoSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TrainInfoStopTimes'
CREATE INDEX [IX_FK_TrainInfoStopTimes]
ON [dbo].[StopTimesSet]
    ([TrainInfoId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------