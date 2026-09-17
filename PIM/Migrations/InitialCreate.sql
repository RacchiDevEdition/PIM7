IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Notifications] (
    [Id] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [Message] nvarchar(max) NOT NULL,
    [Read] bit NOT NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    CONSTRAINT [PK_Notifications] PRIMARY KEY ([Id])
);

CREATE TABLE [User] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(200) NOT NULL,
    [Email] nvarchar(256) NOT NULL,
    [PasswordHash] nvarchar(max) NOT NULL,
    [Role] int NOT NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [Discriminator] nvarchar(8) NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([Id])
);

CREATE TABLE [Courses] (
    [Id] uniqueidentifier NOT NULL,
    [Title] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Level] nvarchar(max) NOT NULL,
    [TeacherId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Courses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Courses_User_TeacherId] FOREIGN KEY ([TeacherId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
);

CREATE TABLE [Enrollments] (
    [Id] uniqueidentifier NOT NULL,
    [StudentId] uniqueidentifier NOT NULL,
    [CourseId] uniqueidentifier NOT NULL,
    [EnrolledAt] datetimeoffset NOT NULL,
    [Status] int NOT NULL,
    CONSTRAINT [PK_Enrollments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Enrollments_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Enrollments_User_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
);

CREATE TABLE [Lessons] (
    [Id] uniqueidentifier NOT NULL,
    [CourseId] uniqueidentifier NOT NULL,
    [Title] nvarchar(max) NOT NULL,
    [Order] int NOT NULL,
    CONSTRAINT [PK_Lessons] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Lessons_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Tags] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [CourseId] uniqueidentifier NULL,
    CONSTRAINT [PK_Tags] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Tags_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([Id])
);

CREATE TABLE [LessonContents] (
    [Id] uniqueidentifier NOT NULL,
    [Order] int NOT NULL,
    [Type] int NOT NULL,
    [Title] nvarchar(max) NOT NULL,
    [ContentType] nvarchar(13) NOT NULL,
    [LessonId] uniqueidentifier NULL,
    [Body] nvarchar(max) NULL,
    [Summary] nvarchar(max) NULL,
    [Url] nvarchar(max) NULL,
    [Duration] time NULL,
    [Transcript] nvarchar(max) NULL,
    CONSTRAINT [PK_LessonContents] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LessonContents_Lessons_LessonId] FOREIGN KEY ([LessonId]) REFERENCES [Lessons] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Progresses] (
    [Id] uniqueidentifier NOT NULL,
    [StudentId] uniqueidentifier NOT NULL,
    [LessonId] uniqueidentifier NOT NULL,
    [Completed] bit NOT NULL,
    [CompletedAt] datetimeoffset NULL,
    [Score] int NULL,
    CONSTRAINT [PK_Progresses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Progresses_Lessons_LessonId] FOREIGN KEY ([LessonId]) REFERENCES [Lessons] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Progresses_User_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
);

CREATE TABLE [Questions] (
    [Id] uniqueidentifier NOT NULL,
    [Text] nvarchar(max) NOT NULL,
    [Type] int NOT NULL,
    [QuizContentId] uniqueidentifier NULL,
    CONSTRAINT [PK_Questions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Questions_LessonContents_QuizContentId] FOREIGN KEY ([QuizContentId]) REFERENCES [LessonContents] ([Id])
);

CREATE TABLE [QuizResults] (
    [Id] uniqueidentifier NOT NULL,
    [StudentId] uniqueidentifier NOT NULL,
    [QuizContentId] uniqueidentifier NOT NULL,
    [Score] int NOT NULL,
    [TakenAt] datetimeoffset NOT NULL,
    CONSTRAINT [PK_QuizResults] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_QuizResults_LessonContents_QuizContentId] FOREIGN KEY ([QuizContentId]) REFERENCES [LessonContents] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_QuizResults_User_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
);

CREATE TABLE [Choices] (
    [Id] uniqueidentifier NOT NULL,
    [Text] nvarchar(max) NOT NULL,
    [IsCorrect] bit NOT NULL,
    [QuestionId] uniqueidentifier NULL,
    CONSTRAINT [PK_Choices] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Choices_Questions_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [Questions] ([Id])
);

CREATE TABLE [Answers] (
    [Id] uniqueidentifier NOT NULL,
    [QuestionId] uniqueidentifier NOT NULL,
    [SelectedChoiceIds] nvarchar(max) NOT NULL,
    [FreeTextAnswer] nvarchar(max) NULL,
    [QuizResultId] uniqueidentifier NULL,
    CONSTRAINT [PK_Answers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Answers_Questions_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [Questions] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Answers_QuizResults_QuizResultId] FOREIGN KEY ([QuizResultId]) REFERENCES [QuizResults] ([Id])
);

CREATE INDEX [IX_Answers_QuestionId] ON [Answers] ([QuestionId]);

CREATE INDEX [IX_Answers_QuizResultId] ON [Answers] ([QuizResultId]);

CREATE INDEX [IX_Choices_QuestionId] ON [Choices] ([QuestionId]);

CREATE INDEX [IX_Courses_TeacherId] ON [Courses] ([TeacherId]);

CREATE INDEX [IX_Enrollments_CourseId] ON [Enrollments] ([CourseId]);

CREATE INDEX [IX_Enrollments_StudentId] ON [Enrollments] ([StudentId]);

CREATE INDEX [IX_LessonContents_LessonId] ON [LessonContents] ([LessonId]);

CREATE INDEX [IX_Lessons_CourseId] ON [Lessons] ([CourseId]);

CREATE INDEX [IX_Progresses_LessonId] ON [Progresses] ([LessonId]);

CREATE INDEX [IX_Progresses_StudentId] ON [Progresses] ([StudentId]);

CREATE INDEX [IX_Questions_QuizContentId] ON [Questions] ([QuizContentId]);

CREATE INDEX [IX_QuizResults_QuizContentId] ON [QuizResults] ([QuizContentId]);

CREATE INDEX [IX_QuizResults_StudentId] ON [QuizResults] ([StudentId]);

CREATE INDEX [IX_Tags_CourseId] ON [Tags] ([CourseId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260917002140_InitialCreate', N'10.0.12');

COMMIT;
GO

