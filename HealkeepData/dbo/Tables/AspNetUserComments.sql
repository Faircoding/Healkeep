CREATE TABLE [dbo].[AspNetUserComments] (
    [AspNetUserCommentID] BIGINT         IDENTITY (1, 1) NOT NULL,
    [CheckedUp]           BIT            CONSTRAINT [DF_AspNetUserComments_Checked] DEFAULT ((0)) NULL,
    [AspNetUserID]        NVARCHAR (128) NULL,
    [CommentID]           BIGINT         NULL,
    [NaturalTreatmentID]  BIGINT         NULL,
    [CheckedDown]         BIT            NULL,
    CONSTRAINT [PK_AspNetUserComments] PRIMARY KEY CLUSTERED ([AspNetUserCommentID] ASC),
    CONSTRAINT [FK_AspNetUserComments_AspNetUsers] FOREIGN KEY ([AspNetUserID]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_AspNetUserComments_Comments] FOREIGN KEY ([CommentID]) REFERENCES [dbo].[Comments] ([CommentID]),
    CONSTRAINT [FK_AspNetUserComments_NaturalTreatment] FOREIGN KEY ([NaturalTreatmentID]) REFERENCES [dbo].[NaturalTreatments] ([NaturalTreatmentID])
);

