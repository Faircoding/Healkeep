CREATE TABLE [dbo].[NaturalTreatments] (
    [NaturalTreatmentID] BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]               NVARCHAR (50)  NOT NULL,
    [ScoreUsers]         DECIMAL (4, 2) NULL,
    [ScoreEntities]      DECIMAL (4, 2) NULL,
    [Description]        NVARCHAR (MAX) NULL,
    [DiseaseID]          BIGINT         NOT NULL,
    [Treatment]          BIT            NOT NULL,
    [Prevention]         BIT            NOT NULL,
    [Approved]           BIT            NOT NULL,
    [AspNetUserID]       NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_NaturalTreatment] PRIMARY KEY CLUSTERED ([NaturalTreatmentID] ASC),
    CONSTRAINT [FK_NaturalTreatment_AspNetUsers] FOREIGN KEY ([AspNetUserID]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_NaturalTreatment_Disease] FOREIGN KEY ([DiseaseID]) REFERENCES [dbo].[Diseases] ([DiseaseID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_NaturalTreatment_Disease]
    ON [dbo].[NaturalTreatments]([DiseaseID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_NaturalTreatment_AspNetUsers]
    ON [dbo].[NaturalTreatments]([AspNetUserID] ASC);

