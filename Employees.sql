IF DB_ID('Employees') IS NULL
CREATE DATABASE Employees
GO
USE Employees;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblEmployee')
drop table tblEmployee;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblSector')
drop table tblSector;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblGender')
drop table tblGender;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'vwEmployee')
drop view vwEmployee;


Create table tblGender (
GenderID int identity (1,1) primary key,
Gender varchar(10)
)

Create table tblSector (
SectorID int identity (1,1) primary key,
SectorName varchar(10)
)

create table tblEmployee(
EmployeeID int identity(1,1) primary key,
FullName varchar(30) not null,
DateOfBirth date not null,
IdCardNumber char(9) check (LEN(IdCardNumber) = 9) not null unique,
JMBG varchar(30) check(len(JMBG) = 13)  not null unique,
GenderID int foreign key (GenderID) references tblGender(GenderID) not null,
PhoneNumber varchar(30) not null,
SectorID int foreign key (SectorID) references tblSector(SectorID) not null,
LocationOfEmployee varchar(30) not null,
Manager int not null,
constraint checkPhoneNumber check(PhoneNumber not like '%[^0-9]%'),
constraint checkJMBG check(JMBG not like '%[^0-9]%'))

insert into tblGender (Gender)
values('Male');

insert into tblGender (Gender)
values('Female');

insert into tblGender (Gender)
values('Other');

USE [Employees]
GO

/****** Object:  View [dbo].[vwEmployee]    Script Date: 01-Jul-20 10:20:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwEmployee]
AS
SELECT        dbo.tblEmployee.*
FROM            dbo.tblEmployee
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "tblEmployee"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 238
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwEmployee'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwEmployee'
GO





