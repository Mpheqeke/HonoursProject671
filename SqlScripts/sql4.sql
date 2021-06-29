if not exists (select * from sysobjects where name='UserJobApplication' and xtype='U')
BEGIN

 CREATE TABLE [dbo].[UserJobApplication](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VacancyId] [int] NOT NULL,
	[StatusId] [int] NOT NULL,
	[Motivation] varchar(max) NOT NULL,
	[CVUrl] varchar(max) NOT NULL,
	[SkillId] varchar(max) NOT NULL,
	[IsActive] bit default 1 NOT NULL,
	[CreatedBy] varchar(255) NOT NULL,
	[CreatedOn] DateTime NOT NULL,
	[ModifiedBy] varchar(255) NOT NULL,
	[ModifiedOn] DateTime NOT NULL,
	
	CONSTRAINT [PK_UserJobApplication] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

	
	ALTER TABLE [dbo].[UserJobApplication]  WITH CHECK ADD  CONSTRAINT [FK_UserJobApplication_Vacancy] FOREIGN KEY([VacancyId])
	REFERENCES [dbo].[Vacancy] ([Id])

	ALTER TABLE [dbo].[UserJobApplication] CHECK CONSTRAINT [FK_UserJobApplication_Vacancy]

	ALTER TABLE [dbo].[UserJobApplication]  WITH CHECK ADD  CONSTRAINT [FK_UserJobApplication_Skill] FOREIGN KEY([SkillId])
	REFERENCES [dbo].[Skill] ([Id])

	ALTER TABLE [dbo].[UserJobApplication] CHECK CONSTRAINT [FK_UserJobApplication_Skill]

	ALTER TABLE [dbo].[UserJobApplication]  WITH CHECK ADD  CONSTRAINT [FK_UserJobApplication_Status] FOREIGN KEY([StatusId])
	REFERENCES [dbo].[Status] ([Id])

	ALTER TABLE [dbo].[UserJobApplication] CHECK CONSTRAINT [FK_UserJobApplication_Status]
END