if not exists (select * from sysobjects where name='Moocs' and xtype='U')
BEGIN

 CREATE TABLE [dbo].[Moocs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] varchar(255) NOT NULL,
	[Url] varchar(255) NOT NULL
	CONSTRAINT [PK_Moocs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
	
END
