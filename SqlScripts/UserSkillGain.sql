if not exists (select * from sysobjects where name='UserSkillGain' and xtype='U')
BEGIN

 CREATE TABLE [dbo].[UserSkillGain](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[MoocsId] [int] NOT NULL,
	[SkillId] [int] NOT NULL,
	[DocumentId] [int] NOT NULL, 
	CONSTRAINT [PK_UserSkillGain] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

	ALTER TABLE [dbo].[UserSkillGain]  WITH CHECK ADD  CONSTRAINT [FK_UserSkillGain_User] FOREIGN KEY([UserId])
	REFERENCES [dbo].[User] ([Id])

	ALTER TABLE [dbo].[UserSkillGain] CHECK CONSTRAINT [FK_UserSkillGain_User]

	ALTER TABLE [dbo].[UserSkillGain]  WITH CHECK ADD  CONSTRAINT [FK_UserSkillGain_Moocs] FOREIGN KEY([MoocsId])
	REFERENCES [dbo].[Moocs] ([Id])

	ALTER TABLE [dbo].[UserSkillGain] CHECK CONSTRAINT [FK_UserSkillGain_Moocs]

	ALTER TABLE [dbo].[UserSkillGain]  WITH CHECK ADD  CONSTRAINT [FK_UserSkillGain_Skill] FOREIGN KEY([SkillId])
	REFERENCES [dbo].[Skill] ([Id])

	ALTER TABLE [dbo].[UserSkillGain] CHECK CONSTRAINT [FK_UserSkillGain_Skill]

	ALTER TABLE [dbo].[UserSkillGain]  WITH CHECK ADD  CONSTRAINT [FK_UserSkillGain_UserDocument] FOREIGN KEY([DocumentId])
	REFERENCES [dbo].[UserDocument] ([Id])

	ALTER TABLE [dbo].[UserSkillGain] CHECK CONSTRAINT [FK_UserSkillGain_Document]

	
END
