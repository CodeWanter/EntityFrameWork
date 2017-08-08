
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/03/2017 08:37:01
-- Generated from EDMX file: D:\1A张怀宇\SVNCode\WFGene\GeneModel\KWGene.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [guideDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CheckGuideArticleList]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ArticleList] DROP CONSTRAINT [FK_CheckGuideArticleList];
GO
IF OBJECT_ID(N'[dbo].[FK_WF_RoleUserInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WF_User] DROP CONSTRAINT [FK_WF_RoleUserInfo];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ArticleList]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ArticleList];
GO
IF OBJECT_ID(N'[dbo].[CheckGuide]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CheckGuide];
GO
IF OBJECT_ID(N'[dbo].[RoleToMenus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RoleToMenus];
GO
IF OBJECT_ID(N'[dbo].[WF_Menu]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WF_Menu];
GO
IF OBJECT_ID(N'[dbo].[WF_Role]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WF_Role];
GO
IF OBJECT_ID(N'[dbo].[WF_User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WF_User];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ArticleList'
CREATE TABLE [dbo].[ArticleList] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [FirstAuthor] nvarchar(max)  NOT NULL,
    [Corpartner] nvarchar(max)  NOT NULL,
    [Publisher] nvarchar(max)  NOT NULL,
    [Remark] nvarchar(max)  NOT NULL,
    [CheckGuideId] int  NOT NULL
);
GO

-- Creating table 'CheckGuide'
CREATE TABLE [dbo].[CheckGuide] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Account] nvarchar(max)  NOT NULL,
    [Department] nvarchar(max)  NOT NULL,
    [Campus] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Phone] nvarchar(max)  NOT NULL,
    [PayType] nvarchar(max)  NOT NULL,
    [Doc] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'WF_Menu'
CREATE TABLE [dbo].[WF_Menu] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [MenuName] nvarchar(max)  NOT NULL,
    [Url] nvarchar(max)  NOT NULL,
    [Icon] nvarchar(max)  NOT NULL,
    [PId] int  NOT NULL
);
GO

-- Creating table 'WF_Role'
CREATE TABLE [dbo].[WF_Role] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RoleName] nvarchar(max)  NOT NULL,
    [Introduce] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'WF_User'
CREATE TABLE [dbo].[WF_User] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(max)  NOT NULL,
    [TureName] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Createtime] datetime  NOT NULL,
    [WF_RoleId] int  NOT NULL
);
GO

-- Creating table 'RoleToMenus'
CREATE TABLE [dbo].[RoleToMenus] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [WF_RoleId] int  NOT NULL,
    [WF_MenuId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ArticleList'
ALTER TABLE [dbo].[ArticleList]
ADD CONSTRAINT [PK_ArticleList]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CheckGuide'
ALTER TABLE [dbo].[CheckGuide]
ADD CONSTRAINT [PK_CheckGuide]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WF_Menu'
ALTER TABLE [dbo].[WF_Menu]
ADD CONSTRAINT [PK_WF_Menu]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WF_Role'
ALTER TABLE [dbo].[WF_Role]
ADD CONSTRAINT [PK_WF_Role]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WF_User'
ALTER TABLE [dbo].[WF_User]
ADD CONSTRAINT [PK_WF_User]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RoleToMenus'
ALTER TABLE [dbo].[RoleToMenus]
ADD CONSTRAINT [PK_RoleToMenus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CheckGuideId] in table 'ArticleList'
ALTER TABLE [dbo].[ArticleList]
ADD CONSTRAINT [FK_CheckGuideArticleList]
    FOREIGN KEY ([CheckGuideId])
    REFERENCES [dbo].[CheckGuide]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CheckGuideArticleList'
CREATE INDEX [IX_FK_CheckGuideArticleList]
ON [dbo].[ArticleList]
    ([CheckGuideId]);
GO

-- Creating foreign key on [WF_RoleId] in table 'WF_User'
ALTER TABLE [dbo].[WF_User]
ADD CONSTRAINT [FK_WF_RoleUserInfo]
    FOREIGN KEY ([WF_RoleId])
    REFERENCES [dbo].[WF_Role]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WF_RoleUserInfo'
CREATE INDEX [IX_FK_WF_RoleUserInfo]
ON [dbo].[WF_User]
    ([WF_RoleId]);
GO

-- Creating foreign key on [WF_RoleId] in table 'RoleToMenus'
ALTER TABLE [dbo].[RoleToMenus]
ADD CONSTRAINT [FK_RoleToMenusWF_Role]
    FOREIGN KEY ([WF_RoleId])
    REFERENCES [dbo].[WF_Role]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoleToMenusWF_Role'
CREATE INDEX [IX_FK_RoleToMenusWF_Role]
ON [dbo].[RoleToMenus]
    ([WF_RoleId]);
GO

-- Creating foreign key on [WF_MenuId] in table 'RoleToMenus'
ALTER TABLE [dbo].[RoleToMenus]
ADD CONSTRAINT [FK_RoleToMenusWF_Menu]
    FOREIGN KEY ([WF_MenuId])
    REFERENCES [dbo].[WF_Menu]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoleToMenusWF_Menu'
CREATE INDEX [IX_FK_RoleToMenusWF_Menu]
ON [dbo].[RoleToMenus]
    ([WF_MenuId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------