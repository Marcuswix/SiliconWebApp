CREATE TABLE [dbo].[FeatureItems] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [FeatureId] INT            NOT NULL,
    [ImageUrl]  NVARCHAR (MAX) NULL,
    [Title]     NVARCHAR (MAX) NULL,
    [Text]      NVARCHAR (MAX) NULL,
    [AltText] NVARCHAR(MAX) NULL, 
    CONSTRAINT [PK_FeatureItems] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FeatureItems_Features_FeatureId] FOREIGN KEY ([FeatureId]) REFERENCES [dbo].[Features] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_FeatureItems_FeatureId]
    ON [dbo].[FeatureItems]([FeatureId] ASC);

