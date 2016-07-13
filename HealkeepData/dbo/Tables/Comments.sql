CREATE TABLE [dbo].[Comments] (
    [CommentID]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [Description]        NVARCHAR (MAX) NOT NULL,
    [CreatedTime]        DATETIME       CONSTRAINT [DF_Comments] DEFAULT (getutcdate()) NOT NULL,
    [AspNetUserID]       NVARCHAR (128) NOT NULL,
    [NaturalTreatmentID] BIGINT         NOT NULL,
    [CommentCommentID]   BIGINT         NULL,
    [VoteUp]             INT            DEFAULT ((0)) NOT NULL,
    [VoteDown]           INT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED ([CommentID] ASC),
    CONSTRAINT [FK_AspNetUserComment] FOREIGN KEY ([AspNetUserID]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_CommentComment] FOREIGN KEY ([CommentCommentID]) REFERENCES [dbo].[Comments] ([CommentID]),
    CONSTRAINT [FK_NaturalTreatmentComment] FOREIGN KEY ([NaturalTreatmentID]) REFERENCES [dbo].[NaturalTreatments] ([NaturalTreatmentID])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_AspNetUserComment]
    ON [dbo].[Comments]([AspNetUserID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_NaturalTreatmentComment]
    ON [dbo].[Comments]([NaturalTreatmentID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_CommentComment]
    ON [dbo].[Comments]([CommentCommentID] ASC);

