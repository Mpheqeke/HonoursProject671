if not exists (select * from sysobjects where name='Vacany' and xtype='U')
BEGIN

 CREATE TABLE [dbo].[Vacancy](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NOT NULL,
	[JobTitle] varchar(255) NOT NULL,
	[JobDescription] varchar(max) NOT NULL,
	[RefCode] varchar(max) NOT NULL,
	[Location] varchar(max) NOT NULL,
	[IsActive] bit default 1 NOT NULL,
	[ApplicationClosingDate] DateTime NOT NULL,
	[ApplictionOpeningDate] DateTime NOT NULL,
	[StartDate] DateTime NOT NULL,
	[Responsibilities] varchar(max) NOT NULL,
	[SkillRequirementId] varchar(max) NOT NULL,
	[CreatedBy] varchar(255) NOT NULL,
	[CreatedOn] DateTime NOT NULL,
	[ModifiedBy] varchar(255) NOT NULL,
	[ModifiedOn] DateTime NOT NULL,
	[DocumentUploadUrl] varchar(max) NULL 
	CONSTRAINT [PK_Vacany] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

	
	ALTER TABLE [dbo].[Vacancy]  WITH CHECK ADD  CONSTRAINT [FK_Vacancy_Company] FOREIGN KEY([CompanyId])
	REFERENCES [dbo].[Company] ([Id])

	ALTER TABLE [dbo].[Vacancy] CHECK CONSTRAINT [FK_Vacancy_Company]

	ALTER TABLE [dbo].[Vacancy]  WITH CHECK ADD  CONSTRAINT [FK__Vacancy_Skill] FOREIGN KEY([SkillRequirementId])
	REFERENCES [dbo].[Skill] ([Id])

	ALTER TABLE [dbo].[Vacancy] CHECK CONSTRAINT [FK_Vacancy_Skill]
END
