if not exists (select * from sysobjects where name='Company' and xtype='U')
BEGIN

 CREATE TABLE [dbo].[Company](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] varchar(255) NOT NULL,
	[Sector] varchar(max) NOT NULL,
	[Vision] varchar(max) NOT NULL,
	[Mission] varchar(max) NOT NULL,
	[ISActive] bit default 1 NOT NULL,
	[LogoUrl] varchar(max) NOT NULL,
	[CreatedBy] varchar(255) NOT NULL,
	[CreatedOn] DateTime NOT NULL,
	[ModifiedBy] varchar(255) NOT NULL,
	[ModifiedOn] DateTime NOT NULL,
	
	CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

	

END