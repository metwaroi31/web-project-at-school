USE [QuanLiSach]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 12/17/2019 2:06:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Account](
	[username] [varchar](100) NOT NULL,
	[password] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ChiTietDon]    Script Date: 12/17/2019 2:06:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietDon](
	[MaDonHang] [int] NOT NULL,
	[MaSach] [int] NOT NULL,
	[SoLuong] [int] NOT NULL,
	[DonGia] [nvarchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaDonHang] ASC,
	[MaSach] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DonHang]    Script Date: 12/17/2019 2:06:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DonHang](
	[MaDonHang] [int] NOT NULL,
	[DaThanhToan] [int] NOT NULL,
	[NgayDat] [date] NOT NULL,
	[NgayGiao] [date] NOT NULL,
	[username] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaDonHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[KhachHang]    Script Date: 12/17/2019 2:06:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[KhachHang](
	[HoTen] [nvarchar](100) NULL,
	[NgaySinh] [date] NULL,
	[GioiTinh] [bit] NULL,
	[DienThoai] [varchar](11) NULL,
	[username] [varchar](100) NOT NULL,
 CONSTRAINT [PK_KhachHang] PRIMARY KEY CLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NguoiQuanLy]    Script Date: 12/17/2019 2:06:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NguoiQuanLy](
	[HoTen] [nvarchar](100) NULL,
	[NgaySinh] [date] NULL,
	[GioiTinh] [bit] NULL,
	[DienThoai] [varchar](11) NULL,
	[username] [varchar](100) NOT NULL,
	[NguoiThem] [varchar](100) NULL,
 CONSTRAINT [PK_NguoiQUanLy] PRIMARY KEY CLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Sach]    Script Date: 12/17/2019 2:06:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sach](
	[MaSach] [int] NOT NULL,
	[TenSach] [nvarchar](100) NULL,
	[GiaBan] [money] NULL,
	[Mota] [nvarchar](200) NULL,
	[MaTheLoai] [int] NULL,
	[Soluong] [int] NULL,
	[MaTacGia] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaSach] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TacGia]    Script Date: 12/17/2019 2:06:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TacGia](
	[MaTacGia] [int] NOT NULL,
	[TenTacGia] [nvarchar](100) NULL,
	[DiaChi] [nvarchar](100) NULL,
	[DienThoai] [varchar](11) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaTacGia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TheLoai]    Script Date: 12/17/2019 2:06:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TheLoai](
	[MaTheLoai] [int] NOT NULL,
	[TheLoai] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaTheLoai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[Account] ([username], [password]) VALUES (N'Admin', N'Admin')
INSERT [dbo].[Account] ([username], [password]) VALUES (N'AnhManager', N'01644312911')
INSERT [dbo].[Account] ([username], [password]) VALUES (N'metwaroi31', N'09112035')
INSERT [dbo].[Account] ([username], [password]) VALUES (N'metwaroi81', N'09112035')
INSERT [dbo].[Account] ([username], [password]) VALUES (N'TrungManager', N'09112035')
INSERT [dbo].[ChiTietDon] ([MaDonHang], [MaSach], [SoLuong], [DonGia]) VALUES (0, 2, 5, N'30.0000')
INSERT [dbo].[ChiTietDon] ([MaDonHang], [MaSach], [SoLuong], [DonGia]) VALUES (0, 3, 5, N'35.0000')
INSERT [dbo].[ChiTietDon] ([MaDonHang], [MaSach], [SoLuong], [DonGia]) VALUES (0, 4, 6, N'20.0000')
INSERT [dbo].[ChiTietDon] ([MaDonHang], [MaSach], [SoLuong], [DonGia]) VALUES (1, 2, 5, N'30.0000')
INSERT [dbo].[ChiTietDon] ([MaDonHang], [MaSach], [SoLuong], [DonGia]) VALUES (1, 3, 5, N'35.0000')
INSERT [dbo].[ChiTietDon] ([MaDonHang], [MaSach], [SoLuong], [DonGia]) VALUES (1, 4, 5, N'20.0000')
INSERT [dbo].[ChiTietDon] ([MaDonHang], [MaSach], [SoLuong], [DonGia]) VALUES (2, 1, 1, N'15.0000')
INSERT [dbo].[ChiTietDon] ([MaDonHang], [MaSach], [SoLuong], [DonGia]) VALUES (2, 5, 5, N'20.0000')
INSERT [dbo].[ChiTietDon] ([MaDonHang], [MaSach], [SoLuong], [DonGia]) VALUES (3, 0, 2, N'10.0000')
INSERT [dbo].[ChiTietDon] ([MaDonHang], [MaSach], [SoLuong], [DonGia]) VALUES (3, 2, 2, N'30.0000')
INSERT [dbo].[ChiTietDon] ([MaDonHang], [MaSach], [SoLuong], [DonGia]) VALUES (3, 3, 2, N'35.0000')
INSERT [dbo].[ChiTietDon] ([MaDonHang], [MaSach], [SoLuong], [DonGia]) VALUES (3, 4, 3, N'20.0000')
INSERT [dbo].[ChiTietDon] ([MaDonHang], [MaSach], [SoLuong], [DonGia]) VALUES (3, 5, 4, N'20.0000')
INSERT [dbo].[ChiTietDon] ([MaDonHang], [MaSach], [SoLuong], [DonGia]) VALUES (4, 2, 3, N'30.0000')
INSERT [dbo].[ChiTietDon] ([MaDonHang], [MaSach], [SoLuong], [DonGia]) VALUES (4, 3, 3, N'35.0000')
INSERT [dbo].[ChiTietDon] ([MaDonHang], [MaSach], [SoLuong], [DonGia]) VALUES (4, 4, 1, N'20.0000')
INSERT [dbo].[ChiTietDon] ([MaDonHang], [MaSach], [SoLuong], [DonGia]) VALUES (4, 5, 5, N'20.0000')
INSERT [dbo].[DonHang] ([MaDonHang], [DaThanhToan], [NgayDat], [NgayGiao], [username]) VALUES (0, 0, CAST(0x00000000 AS Date), CAST(0x00000000 AS Date), N'metwaroi81')
INSERT [dbo].[DonHang] ([MaDonHang], [DaThanhToan], [NgayDat], [NgayGiao], [username]) VALUES (1, 0, CAST(0x00000000 AS Date), CAST(0x00000000 AS Date), N'metwaroi81')
INSERT [dbo].[DonHang] ([MaDonHang], [DaThanhToan], [NgayDat], [NgayGiao], [username]) VALUES (2, 0, CAST(0x00000000 AS Date), CAST(0x00000000 AS Date), N'metwaroi81')
INSERT [dbo].[DonHang] ([MaDonHang], [DaThanhToan], [NgayDat], [NgayGiao], [username]) VALUES (3, 0, CAST(0x00000000 AS Date), CAST(0x00000000 AS Date), N'metwaroi81')
INSERT [dbo].[DonHang] ([MaDonHang], [DaThanhToan], [NgayDat], [NgayGiao], [username]) VALUES (4, 0, CAST(0x00000000 AS Date), CAST(0x00000000 AS Date), N'metwaroi31')
INSERT [dbo].[KhachHang] ([HoTen], [NgaySinh], [GioiTinh], [DienThoai], [username]) VALUES (N'Tan Trung', CAST(0x16230B00 AS Date), 1, N'089626269', N'metwaroi31')
INSERT [dbo].[KhachHang] ([HoTen], [NgaySinh], [GioiTinh], [DienThoai], [username]) VALUES (N'Anh Bui', CAST(0x1B230B00 AS Date), 1, N'0703354217', N'metwaroi81')
INSERT [dbo].[NguoiQuanLy] ([HoTen], [NgaySinh], [GioiTinh], [DienThoai], [username], [NguoiThem]) VALUES (N'111111', CAST(0x1B230B00 AS Date), 1, N'1111', N'Admin', NULL)
INSERT [dbo].[NguoiQuanLy] ([HoTen], [NgaySinh], [GioiTinh], [DienThoai], [username], [NguoiThem]) VALUES (N'Anh Bui', CAST(0x1B230B00 AS Date), 1, N'0703354217', N'AnhManager', N'TrungManager')
INSERT [dbo].[NguoiQuanLy] ([HoTen], [NgaySinh], [GioiTinh], [DienThoai], [username], [NguoiThem]) VALUES (N'Tan Trung24512451245', CAST(0x7B200B00 AS Date), 1, N'0703354217', N'TrungManager', N'Admin')
INSERT [dbo].[Sach] ([MaSach], [TenSach], [GiaBan], [Mota], [MaTheLoai], [Soluong], [MaTacGia]) VALUES (0, N'aaaaaaa', 10.0000, N'sihgasghiaufhg', 0, 1, 0)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [GiaBan], [Mota], [MaTheLoai], [Soluong], [MaTacGia]) VALUES (1, N'bbbbbb', 15.0000, N'ofhgaiofhg', 0, 1, 0)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [GiaBan], [Mota], [MaTheLoai], [Soluong], [MaTacGia]) VALUES (2, N'Black Hole', 30.0000, N'Lets discover the universe with stephen hawking', 2, 10, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [GiaBan], [Mota], [MaTheLoai], [Soluong], [MaTacGia]) VALUES (3, N'Universe in a nutshell', 35.0000, N'Lets discover the universe with stephen hawking', 2, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [GiaBan], [Mota], [MaTheLoai], [Soluong], [MaTacGia]) VALUES (4, N'Head first java', 20.0000, N'A comprehensive guide to java programming', 1, 0, 2)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [GiaBan], [Mota], [MaTheLoai], [Soluong], [MaTacGia]) VALUES (5, N'Linux bible', 20.0000, N'shell scripting and basic linux commands', 1, 16, 2)
INSERT [dbo].[TacGia] ([MaTacGia], [TenTacGia], [DiaChi], [DienThoai]) VALUES (0, N'asdf', N'asfd', N'312541615')
INSERT [dbo].[TacGia] ([MaTacGia], [TenTacGia], [DiaChi], [DienThoai]) VALUES (1, N'Stephen hawking', N'England', N'712467586')
INSERT [dbo].[TacGia] ([MaTacGia], [TenTacGia], [DiaChi], [DienThoai]) VALUES (2, N'O''Reaily', N'America', N'481521985')
INSERT [dbo].[TheLoai] ([MaTheLoai], [TheLoai]) VALUES (0, N'aaaaaa')
INSERT [dbo].[TheLoai] ([MaTheLoai], [TheLoai]) VALUES (1, N'Computer')
INSERT [dbo].[TheLoai] ([MaTheLoai], [TheLoai]) VALUES (2, N'Science')
ALTER TABLE [dbo].[ChiTietDon]  WITH CHECK ADD FOREIGN KEY([MaDonHang])
REFERENCES [dbo].[DonHang] ([MaDonHang])
GO
ALTER TABLE [dbo].[DonHang]  WITH CHECK ADD FOREIGN KEY([username])
REFERENCES [dbo].[KhachHang] ([username])
GO
ALTER TABLE [dbo].[KhachHang]  WITH CHECK ADD FOREIGN KEY([username])
REFERENCES [dbo].[Account] ([username])
GO
ALTER TABLE [dbo].[NguoiQuanLy]  WITH CHECK ADD FOREIGN KEY([username])
REFERENCES [dbo].[Account] ([username])
GO
ALTER TABLE [dbo].[NguoiQuanLy]  WITH CHECK ADD  CONSTRAINT [FK_NguoiQuanLy_NguoiQuanLy] FOREIGN KEY([NguoiThem])
REFERENCES [dbo].[NguoiQuanLy] ([username])
GO
ALTER TABLE [dbo].[NguoiQuanLy] CHECK CONSTRAINT [FK_NguoiQuanLy_NguoiQuanLy]
GO
ALTER TABLE [dbo].[Sach]  WITH CHECK ADD FOREIGN KEY([MaTacGia])
REFERENCES [dbo].[TacGia] ([MaTacGia])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Sach]  WITH CHECK ADD FOREIGN KEY([MaTheLoai])
REFERENCES [dbo].[TheLoai] ([MaTheLoai])
ON DELETE SET NULL
GO
