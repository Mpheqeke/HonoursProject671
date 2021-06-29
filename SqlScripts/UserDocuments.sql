if not exists (select * from sysobjects where name='UserDocument' and xtype='U')
BEGIN

 CREATE TABLE [dbo].[UserDocument](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[DocumentTypeId] [int] NOT NULL,
	[DocumentUrl] varchar(255) NOT NULL,
	[StatusId] int NULL,
	
	CONSTRAINT [PK_UserDocument] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
	ALTER TABLE [dbo].[UserDocument]  WITH CHECK ADD  CONSTRAINT [FK_UserDocument_User] FOREIGN KEY([UserId])
	REFERENCES [dbo].[User] ([Id])

	ALTER TABLE [dbo].[UserDocument] CHECK CONSTRAINT [FK_UserDocument_User]

	ALTER TABLE [dbo].[UserDocument]  WITH CHECK ADD  CONSTRAINT [FK_UserDocument_DocumentType] FOREIGN KEY([DocumentTypeId])
	REFERENCES [dbo].[DocumentType] ([Id])

	ALTER TABLE [dbo].[UserDocument] CHECK CONSTRAINT [FK_UserDocument_DocumentType]
END
