if not exists (select * from sysobjects where name='User' and xtype='U')
BEGIN

 CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] varchar(255) NOT NULL,
	[LastName] varchar(255) NOT NULL,
	[Mobile] varchar(12) NOT NULL,
	[ThumbnailUrl] varchar(max) NULL,
	[ImageUrl] varchar(max),
	[IsActive] bit default 1,
	[UUID] varchar(max) NOT NULL,
	[CreatedOn] DateTime NOT NULL,
	[CreatedBy] varchar(255) NOT NULL,
	[ModifiedBy] varchar(255) NOT NULL,
	[ModifiedOn] DateTime NOT NULL,
	[IsSuperAdmin] bit default 0
	
	CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


END
