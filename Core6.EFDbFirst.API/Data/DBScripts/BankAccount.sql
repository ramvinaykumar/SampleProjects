/****** Object:  Table [dbo].[BankAccount]    Script Date: 12-02-2023 8:44:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BankAccount](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TransactionNumber] [uniqueidentifier] NOT NULL,
	[BankName] [varchar](50) NOT NULL,
	[BankCode] [varchar](10) NULL,
	[BankAccountNumber] [varbinary](50) NOT NULL,
	[IFSCCode] [varchar](20) NULL,
	[PaymentMode] [varchar](20) NULL,
	[PartialSumAmount] [numeric](18, 2) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_BankAccount] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[AddBankAccount]    Script Date: 12-02-2023 8:44:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[AddBankAccount]
(
@TransactionNumber uniqueidentifier,
@BankName [varchar](50),
@BankCode [varchar](50),
@BankAccountNumber [varbinary](50),
@IFSCCode[varchar](20),
@PaymentMode [varchar](20),
@PartialSumAmount numeric(18,2),
@IsActive bit
)
AS
BEGIN
	INSERT INTO dbo.BankAccount
		(
			TransactionNumber
			,BankName
			,BankCode
			,BankAccountNumber
			,IFSCCode
			,PaymentMode
			,PartialSumAmount
			,IsActive
		)
    VALUES
		(
			@TransactionNumber,
			@BankName,
			@BankCode,
			@BankAccountNumber,
			@IFSCCode,
			@PaymentMode,
			@PartialSumAmount,
			@IsActive
		)
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteBankAccountByID]    Script Date: 12-02-2023 8:44:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[DeleteBankAccountByID]
@ID int
AS
BEGIN
	DELETE FROM dbo.BankAccount where ID = @ID
END
GO
/****** Object:  StoredProcedure [dbo].[GetBankAccountByID]    Script Date: 12-02-2023 8:44:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[GetBankAccountByID]
@ID int
AS
BEGIN
	SELECT
		ID
		,TransactionNumber
		,BankName
		,BankCode
		,BankAccountNumber
		,IFSCCode
		,PaymentMode
		,PartialSumAmount
		,IsActive
	FROM dbo.BankAccount where ID = @ID
END
GO
/****** Object:  StoredProcedure [dbo].[GetBankAccountList]    Script Date: 12-02-2023 8:44:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[GetBankAccountList]
AS
BEGIN
	SELECT * FROM dbo.BankAccount
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateBankAccount]    Script Date: 12-02-2023 8:44:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[UpdateBankAccount]
(
@ID int,
@TransactionNumber uniqueidentifier,
@BankName [varchar](50),
@BankCode [varchar](50),
@BankAccountNumber [varbinary](50),
@IFSCCode[varchar](20),
@PaymentMode [varchar](20),
@PartialSumAmount numeric(18,2),
@IsActive bit
)
AS
BEGIN
	UPDATE dbo.BankAccount
		SET
			TransactionNumber=@TransactionNumber
			,BankName=@BankName
			,BankCode=@BankCode
			,BankAccountNumber=@BankAccountNumber
			,IFSCCode=@IFSCCode
			,PaymentMode=@PaymentMode
			,PartialSumAmount=@PartialSumAmount
			,IsActive=@IsActive
			WHERE ID = @ID
		
END
GO
