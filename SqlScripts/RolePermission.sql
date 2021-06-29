if not exists (select * from sysobjects where name='RolePermission' and xtype='U')
BEGIN

 CREATE TABLE [dbo].[RolePermission](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[CPermissionId] [int] NOT NULL,
	
	CONSTRAINT [PK_RolePermission] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

	ALTER TABLE [dbo].[RolePermission]  WITH CHECK ADD  CONSTRAINT [FK_RolePermissione_Role] FOREIGN KEY([RoleId])
	REFERENCES [dbo].[Role] ([Id])

	ALTER TABLE [dbo].[RolePermission] CHECK CONSTRAINT [FK_RolePermission_Role]

	ALTER TABLE [dbo].[RolePermission]  WITH CHECK ADD  CONSTRAINT [FK_RolePermissione_Permission] FOREIGN KEY([PermissionId])
	REFERENCES [dbo].[Permission] ([Id])

	ALTER TABLE [dbo].[RolePermission] CHECK CONSTRAINT [FK_RolePermission_Permission]

	
END
