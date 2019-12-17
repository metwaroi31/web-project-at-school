create database QuanLiSach
go
use QuanLiSach
go
create table Account 
(
	username varchar(100) PRIMARY KEY not null,
	password varchar(100),
)

go
create table KhachHang
(
	HoTen nvarchar(100),
	NgaySinh date,
	GioiTinh bit,
	DienThoai varchar(11),
	username varchar(100) FOREIGN KEY REFERENCES Account(username) not null 
	CONSTRAINT PK_KhachHang PRIMARY KEY (username)
)

go
create table NguoiQuanLy
(
	HoTen nvarchar(100),
	NgaySinh date,
	GioiTinh bit,
	DienThoai varchar(11),
	username varchar(100) FOREIGN KEY REFERENCES Account(username) not null,
	NguoiThem varchar(100) ,
	CONSTRAINT PK_NguoiQUanLy PRIMARY KEY (username),
	CONSTRAINT FK_NguoiQuanLy_NguoiQuanLy FOREIGN KEY (NguoiThem) REFERENCES NguoiQuanLy(username) 
)

go
create table TheLoai
(
	MaTheLoai int primary key,
	TheLoai nvarchar(100)
)

go
create table TacGia
(
	MaTacGia int primary key ,
	TenTacGia nvarchar(100),
	DiaChi nvarchar(100),
	DienThoai varchar(11)
)

go
 create table DonHang 
 (
	MaDonHang int primary key,
	DaThanhToan int not null,
	NgayDat date not null,
	NgayGiao date not null,
	username varchar(100) not null
	foreign key (username) references KhachHang(username),
 )
go


create table Sach
(
	MaSach int primary key,
	TenSach nvarchar(100),
	GiaBan money,
	Mota nvarchar(200),
	MaTheLoai int ,
	Soluong int ,
	MaTacGia int,

foreign key (MaTheLoai) references dbo.TheLoai(MaTheLoai) on delete set null,
foreign key (MaTacGia) references dbo.TacGia(MaTacGia) on delete set null,
)
go
create table ChiTietDon
(
	MaDonHang int not null,
	MaSach int not null,
	SoLuong int not null,
	DonGia nvarchar(10),
	primary key (MaDonHang, MaSach),
	foreign key (MaDonHang) references dbo.DonHang(MaDonHang)
)

go
