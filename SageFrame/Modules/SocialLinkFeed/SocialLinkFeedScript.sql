/****** Object:  StoredProcedure [dbo].[usp_SocialLinkModify]    Script Date: 07/30/2012 16:39:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_SocialLinkModify]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_SocialLinkModify]
GO
/****** Object:  StoredProcedure [dbo].[usp_SocialLinkDelete]    Script Date: 07/30/2012 16:39:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_SocialLinkDelete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_SocialLinkDelete]
GO
/****** Object:  StoredProcedure [dbo].[usp_SocialLinkSelect]    Script Date: 07/30/2012 16:39:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_SocialLinkSelect]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_SocialLinkSelect]
GO
/****** Object:  Table [dbo].[SocialLink]    Script Date: 07/30/2012 16:39:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocialLink]') AND type in (N'U'))
DROP TABLE [dbo].[SocialLink]
GO
/****** Object:  Table [dbo].[SocialLink]    Script Date: 07/30/2012 16:39:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocialLink]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocialLink](
	[LinkID] [int] IDENTITY(1,1) NOT NULL,
	[Link] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Type] [varchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UserName] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PortalID] [int] NOT NULL,
	[UserModuleID] [int] NOT NULL,
 CONSTRAINT [PK_SocialLink] PRIMARY KEY CLUSTERED 
(
	[LinkID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_SocialLinkSelect]    Script Date: 07/30/2012 16:39:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_SocialLinkSelect]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[usp_SocialLinkSelect] 
@userName varchar(max),
@portalID int,
@userModuleID int

AS
BEGIN
SELECT LinkID,Link,Type from dbo.SocialLink
WHERE 
UserName = @userName and 
PortalID = @portalID and
UserModuleID = @userModuleID 
END


set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_SocialLinkDelete]    Script Date: 07/30/2012 16:39:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_SocialLinkDelete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[usp_SocialLinkDelete]
@linkID int
AS
BEGIN

DELETE
	FROM dbo.SocialLink
Where LinkID = @linkID
END



set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_SocialLinkModify]    Script Date: 07/30/2012 16:39:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_SocialLinkModify]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[usp_SocialLinkModify]
@linkID int,
@link varchar(max),
@type varchar(50),
@userName varchar(max),
@portalID int,
@userModuleID int

AS
BEGIN

IF(@linkID=0)
BEGIN
	DECLARE @rowCount as int 
	DECLARE @counter as int 
	DECLARE @tblLink TABLE(RowNum int identity(1,1),Link varchar(256))
	INSERT INTO @tblLink SELECT rtrim(ltrim(items)) from split(@link,'','')
	SELECT @rowCount=count(RowNum) from @tblLink
	set @counter=1
	while(@counter<=@rowCount or @counter=1)
		BEGIN
		DECLARE @link_ as varchar(256)
		SET @link_ = (SELECT Link from @tblLink where RowNum = @counter)

		INSERT
			INTO dbo.SocialLink
			Values(@link_,@type,@userName,@portalID,@userModuleID)

		SET @counter= @counter+1; print @counter
		END
END

ELSE
BEGIN
UPDATE
	dbo.SocialLink
	SET
	Link=@link
	WHERE
	LinkID = @linkID
END
END



' 
END
GO
