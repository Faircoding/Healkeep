CREATE TABLE [dbo].[Diseases] (
    [DiseaseID]   BIGINT          IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50)   NOT NULL,
    [Description] NVARCHAR (1000) NULL,
    [URLwiki]     NVARCHAR (500)  NULL,
    CONSTRAINT [PK_Disease] PRIMARY KEY CLUSTERED ([DiseaseID] ASC)
);

