if not exists (select * from sysobjects where name='UserJobApplication' and xtype='U')
BEGIN

 CREATE TABLE [dbo].[UserJobApplication](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[VacancyId] [int] NOT NULL,
	[StatusId] [int] NOT NULL,
	[SkillId] [int] NOT NULL,
	[Motivation] varchar(max) Not Null,
	[CVUrl] varchar(500) Not Null,
	[IsActive] bit Not Null,
	[CreatedBy] varchar(max) Not Null,
	[CreatedOn] DateTime Not Null,
	[ModifiedBy] varchar(max) Not Null,
	[ModifiedOn] DateTime Not Null,
	
	CONSTRAINT [PK_UserJobApplication] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

	ALTER TABLE [dbo].[UserJobApplication]  WITH CHECK ADD  CONSTRAINT [FK_UserJobApplication_User] FOREIGN KEY([UserId])
	REFERENCES [dbo].[User] ([Id])
	ALTER TABLE [dbo].[UserJobApplication] CHECK CONSTRAINT [FK_UserJobApplication_User]

	ALTER TABLE [dbo].[UserJobApplication]  WITH CHECK ADD  CONSTRAINT [FK_UserJobApplication_Vacancy] FOREIGN KEY([VacancyId])
	REFERENCES [dbo].[Vacancy] ([Id])

	ALTER TABLE [dbo].[UserJobApplication] CHECK CONSTRAINT [FK_UserJobApplication_Vacancy]
	
	
	ALTER TABLE [dbo].[UserJobApplication]  WITH CHECK ADD  CONSTRAINT [FK_UserJobApplication_Status] FOREIGN KEY([StatusId])
	REFERENCES [dbo].[Status] ([Id])

	ALTER TABLE [dbo].[UserJobApplication] CHECK CONSTRAINT [FK_UserJobApplication_Status]
	
	ALTER TABLE [dbo].[UserJobApplication]  WITH CHECK ADD  CONSTRAINT [FK_UserJobApplication_Skill] FOREIGN KEY([SkillId])
	REFERENCES [dbo].[Skill] ([Id])

	ALTER TABLE [dbo].[UserJobApplication] CHECK CONSTRAINT [FK_UserJobApplication_Skill]
	



END
