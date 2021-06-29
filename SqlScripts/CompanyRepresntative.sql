if not exists (select * from sysobjects where name='CompanyRepresentative' and xtype='U')
BEGIN

 CREATE TABLE [dbo].[CompanyRepresentative](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[CompanyId] [int] NOT NULL,
	
	CONSTRAINT [PK_CompanyRepresentative] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

	ALTER TABLE [dbo].[CompanyRepresentative]  WITH CHECK ADD  CONSTRAINT [FK_CompanyRepresentative_User] FOREIGN KEY([UserId])
	REFERENCES [dbo].[User] ([Id])

	ALTER TABLE [dbo].[CompanyRepresentative] CHECK CONSTRAINT [FK_CompanyRepresentative_User]

	ALTER TABLE [dbo].[CompanyRepresentative]  WITH CHECK ADD  CONSTRAINT [FK_CompanyRepresentative_Company] FOREIGN KEY([CompanyId])
	REFERENCES [dbo].[Company] ([Id])

	ALTER TABLE [dbo].[CompanyRepresentative] CHECK CONSTRAINT [FK_CompanyRepresentative_Company]

END
