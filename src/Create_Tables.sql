CREATE TABLE [dbo].[t_cbr_data](
	[Date] [date] NOT NULL,
	[CharCode] [nvarchar](3) NOT NULL,
	[Value] [float] NOT NULL
) ON [PRIMARY]

CREATE TABLE [dbo].[t_cbr_items](
	[Date] [date] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[EngName] [nvarchar](50) NOT NULL,
	[Nominal] [int] NOT NULL,
	[ParentCode] [nvarchar](10) NOT NULL,
	[ISO_Num_Code] [int] NOT NULL,
	[ISO_Char_Code] [nvarchar](3) NOT NULL
) ON [PRIMARY]

