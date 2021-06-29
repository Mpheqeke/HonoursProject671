if not exists (select * from sysobjects where name='UserRole' and xtype='U')
BEGIN

 CREATE TABLE [dbo].[UserRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL
	
	CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

	ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_Role] FOREIGN KEY([RoleId])
	REFERENCES [dbo].[Role] ([Id])

	ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_Role]
	
	ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_User] FOREIGN KEY([UserId])
	REFERENCES [dbo].[User] ([Id])

	ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_User]



END
