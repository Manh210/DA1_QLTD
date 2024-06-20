create database QLTD20232
create table LinhVuc
(
	ID_LV int identity(1,1) primary key,
	TenLV nvarchar(50) not null,
	MotaLV nvarchar(50) null	
)
alter table LinhVuc add Xoa bit default 0

create table NganhNghe
(
	ID_NN int identity(1,1) primary key,
	TenNN nvarchar(100) not null,
	MoTaNN nvarchar(100) null,
	ID_LV int foreign key references LinhVuc(ID_LV)
)
alter table NganhNghe add Xoa bit default 0


create table DiaDiem
(
	ID_DD int identity(1,1) primary key,
	TenDD nvarchar(50) not null,
	CapDD nvarchar(20) not null
)
alter table DiaDiem add Xoa bit default 0

create table TaiKhoan
(
	ID_TK int identity(1,1) primary key,
	TenDN nvarchar(50) not null,
	MK nvarchar(10) not null,
	NgayTao date null
)
alter table TaiKhoan add Xoa bit default 0
alter table TaiKhoan add ID_Admin int foreign key references ND_Admin(ID_Admin)

create table ND_Admin
(
	ID_Admin int identity(1,1) primary key,
	HoTen nvarchar(50) not null,
	SDT nchar(10) not null,
	DOB date not null,
	GioiTinh nvarchar(3) not null,
	Email nvarchar(50) null,
	TgLamViec date null
)
alter table ND_Admin add Xoa bit default 0

alter table  ND_Admin add ID_TK int foreign key references TaiKhoan(ID_TK)

create table UngVien
(
	ID_UV int identity(1,1) primary key,
	HoTen nvarchar(50) not null,
	SDT nchar(10) not null,
	DOB date not null,
	GioiTinh nvarchar(3) not null,
	Email nvarchar(50) null,
	SYLL nvarchar(MAX) null,
	CV nvarchar(MAX) null,
	ID_DD int foreign key references DiaDiem(ID_DD),
	ID_TK int foreign key references TaiKhoan(ID_TK)
)
alter table UngVien add Xoa bit default 0
alter table UngVien add AnhDD nvarchar(MAX) null

create table NhaTuyenDung
(
	ID_NTD int identity(1,1) primary key,
	TenNTD nvarchar(100) not null,
	QuocGia nvarchar(10) null,
	QuyMo nvarchar(10) null,
	GPKD nvarchar(10) not null,
	ID_TK int foreign key references TaiKhoan(ID_TK)
)
alter table NhaTuyenDung add Xoa bit default 0
alter table NhaTuyenDung add AnhDD nvarchar(MAX) null

create table CongViec
(
	ID_CV int identity(1,1) primary key,
	TenCV nvarchar(100) not null,
	MoTaCV nvarchar(100) null,
	LoaiViec nvarchar(50) null,
	ID_NN int foreign key references NganhNghe(ID_NN),
	ID_Admin int foreign key references ND_Admin(ID_Admin),
	ID_NTD int foreign key references NhaTuyenDung(ID_NTD),
	ID_DD int foreign key references DiaDiem(ID_DD),
	ID_LV int foreign key references LinhVuc(ID_LV)
)
alter table CongViec add Xoa bit default 0
alter table CongViec add SoLuongUT int 

select count(ID_CV) from UngTuyen where ID_CV = '9'

create table TinTuyenDung
(
	ID_TTD int identity(1,1) primary key,
	TieuDe nvarchar(100) not null,
	YeuCau nvarchar(100) null,
	TgLam nvarchar(50) null,
	HanUT date null,
	SoLuongTD int null,
	PhucLoi nvarchar(100) null,
	MucLuong nvarchar(100) null,
	CapBac nvarchar(50) null,
	TgDang datetime null,
	TrangThaiTTD nvarchar(50) default N'Chờ xác nhận',
	TgianCapNhatTT datetime null,
	ID_Admin int foreign key references ND_Admin(ID_Admin),
	ID_NTD int foreign key references NhaTuyenDung(ID_NTD),
	ID_CV int foreign key references CongViec(ID_CV)
)
alter table TinTuyenDung add Xoa bit default 0

create table TinUngTuyen
(
	ID_TUT int identity(1,1) primary key,
	HoTen nvarchar(50) not null,
	SDT nchar(10) not null,
	DOB date not null,
	GioiTinh nvarchar(3) not null,
	Email nvarchar(50) null,
	PhucLoi nvarchar(100) null,
	MucLuong nvarchar(100) null,
	CapBac nvarchar(50) null,
	KinhNghiemLV nvarchar(200) null,
	TrinhDoHocVan nvarchar(100) null,
	KyNangChuyenMon nvarchar(200) null,
	TgDang datetime null,
	TrangThaiTUT nvarchar(50) default N'Chờ xác nhận',
	TgianCapNhatTT datetime null,
	ID_NN int foreign key references NganhNghe(ID_NN),
	ID_Admin int foreign key references ND_Admin(ID_Admin),
	ID_UV int foreign key references UngVien(ID_UV),
	ID_DD int foreign key references DiaDiem(ID_DD),
	ID_LV int foreign key references LinhVuc(ID_LV)
)
alter table TinUngTuyen add Xoa bit default 0


create table Thuoc
(
	ID_LV int not null,
	ID_NTD int not null,
	primary key(ID_LV,ID_NTD),
	foreign key (ID_LV) references LinhVuc(ID_LV),
	foreign key (ID_NTD) references NhaTuyenDung(ID_NTD)
)
 
create table O
(
	ID_DD int not null,
	ID_NTD int not null,
	primary key(ID_DD,ID_NTD),
	foreign key (ID_DD) references DiaDiem(ID_DD),
	foreign key (ID_NTD) references NhaTuyenDung(ID_NTD)
)

create table UngTuyen
(
	ID_UT int identity(1,1) primary key,
	TrangThaiUT nvarchar(50) default N'Chờ xác nhận',
	TgianCapNhatTT datetime not null,
	ID_UV int foreign key references UngVien(ID_UV),
	ID_CV int foreign key references CongViec(ID_CV)
)
alter table UngTuyen add Xoa bit default 0
alter table UngTuyen add TgianUngTuyen datetime
alter table UngTuyen add ID_TTD int foreign key references TinTuyenDung(ID_TTD)

create table Tim
(
	ID_UV int not null,
	ID_NTD int not null,
	primary key(ID_UV,ID_NTD),
	foreign key (ID_UV) references UngVien(ID_UV),
	foreign key (ID_NTD) references NhaTuyenDung(ID_NTD)
)

/*
/*cau lenh truy van ten cua rang buoc khoa ngoai*/
SELECT CONSTRAINT_NAME
FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
WHERE TABLE_NAME = 'TinTuyenDung' AND COLUMN_NAME = 'ID_BD'

ALTER TABLE TinTuyenDung DROP CONSTRAINT FK__TinTuyenD__ID_BD__68487DD7

SELECT CONSTRAINT_NAME
FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
WHERE TABLE_NAME = 'TinUngTuyen' AND COLUMN_NAME = 'ID_BD'

ALTER TABLE TinUngTuyen DROP CONSTRAINT FK__TinUngTuy__ID_BD__6E01572D

ALTER TABLE TinTuyenDung ADD TgDang DATETIME not null
ALTER TABLE TinTuyenDung ADD TgCapNhat DATETIME not null
ALTER TABLE TinTuyenDung DROP COLUMN ID_BD

ALTER TABLE TinUngTuyen ADD TgDang DATETIME not null
ALTER TABLE TinUngTuyen ADD TgCapNhat DATETIME not null
ALTER TABLE TinUngTuyen DROP COLUMN ID_BD
*/

/*Tạo trigger để thời gian đăng được cập nhật sau khi bài đăng được phê duyệt*/
CREATE TRIGGER UpdateTgDang
ON TinTuyenDung
AFTER UPDATE
AS
BEGIN
    IF UPDATE(TrangThaiTTD)
    BEGIN
        UPDATE TinTuyenDung
        SET TgDang = GETDATE()
        FROM TinTuyenDung t
        INNER JOIN inserted i ON t.ID_TTD = i.ID_TTD
        WHERE i.TrangThaiTTD = N'Xác nhận' AND NOT EXISTS (
            SELECT 1 FROM deleted d WHERE d.ID_TTD = i.ID_TTD AND d.TrangThaiTTD = N'Xác nhận'
        );
    END
END;

/*trigger cập nhật giá trị cho cột TgCapNhatTT khi giá trị ở cột TrangThaiTTD thay đổi*/
CREATE TRIGGER UpdateTgCapNhatTT
ON TinTuyenDung
AFTER UPDATE
AS
BEGIN
    IF UPDATE(TrangThaiTTD)
    BEGIN
        UPDATE TinTuyenDung
        SET TgianCapNhatTT = GETDATE()  -- Cập nhật thời gian hiện tại
        FROM TinTuyenDung t
        INNER JOIN inserted i ON t.ID_TTD = i.ID_TTD
        WHERE t.ID_TTD = i.ID_TTD;  -- Chỉ cập nhật cho dòng dữ liệu đã thay đổi
    END
END;

/*Tạo trigger để thời gian đăng được cập nhật sau khi bài đăng được phê duyệt*/
CREATE TRIGGER Update_TgDangTUT
ON TinUngTuyen
AFTER UPDATE
AS
BEGIN
    IF UPDATE(TrangThaiTUT)
    BEGIN
        UPDATE TinUngTuyen
        SET TgDang = GETDATE()
        FROM TinUngTuyen t
        INNER JOIN inserted i ON t.ID_TUT = i.ID_TUT
        WHERE i.TrangThaiTUT = N'Xác nhận' AND NOT EXISTS (
            SELECT 1 FROM deleted d WHERE d.ID_TUT = i.ID_TUT AND d.TrangThaiTUT = N'Xác nhận'
        );
    END
END;

/*trigger cập nhật giá trị cho cột TgCapNhatTT khi giá trị ở cột TrangThaiTUT thay đổi*/
CREATE TRIGGER UpdateTgCapNhatTT_TUT
ON TinUngTuyen
AFTER UPDATE
AS
BEGIN
    IF UPDATE(TrangThaiTUT)
    BEGIN
        UPDATE TinUngTuyen
        SET TgianCapNhatTT = GETDATE()  -- Cập nhật thời gian hiện tại
        FROM TinUngTuyen t
        INNER JOIN inserted i ON t.ID_TUT = i.ID_TUT
        WHERE t.ID_TUT = i.ID_TUT;  -- Chỉ cập nhật cho dòng dữ liệu đã thay đổi
    END
END;

/*trigger cập nhật thời gian thay đổi trạng thái ứng tuyển ở bảng ứng tuyển*/
CREATE TRIGGER UpdateTgianCapNhatTT_UngTuyen
ON UngTuyen
AFTER UPDATE
AS
BEGIN
    IF UPDATE(TrangThaiUT)
    BEGIN
        UPDATE UngTuyen
        SET TgianCapNhatTT = GETDATE()  -- Cập nhật thời gian hiện tại
        FROM UngTuyen u
        INNER JOIN inserted i ON u.ID_UT = i.ID_UT 
        WHERE u.ID_UT = i.ID_UT ;  -- Chỉ cập nhật cho dòng dữ liệu đã thay đổi
    END
END;

/* trigger tạo giá trị cho cột NgayTao khi tạo dòng mới trong bảng TaiKhoan*/
CREATE TRIGGER UpdateNgayTao_TaiKhoan
ON TaiKhoan
AFTER INSERT
AS
BEGIN
    UPDATE TaiKhoan
    SET NgayTao = GETDATE()  -- Cập nhật thời gian hiện tại
    FROM TaiKhoan t
    INNER JOIN inserted i ON t.ID_TK = i.ID_TK;  -- Chỉ cập nhật cho dòng dữ liệu mới được thêm vào
END;

/* trigger tạo giá trị cho cột TgianUngTuyen khi tạo dòng mới trong bảng UngTuyen*/
CREATE TRIGGER UpdateTgianUngTuyen
ON UngTuyen
AFTER INSERT
AS
BEGIN
    UPDATE UngTuyen
    SET TgianUngTuyen = GETDATE()  -- Cập nhật thời gian hiện tại
    FROM UngTuyen t
    INNER JOIN inserted i ON t.ID_UT = i.ID_UT;  -- Chỉ cập nhật cho dòng dữ liệu mới được thêm vào
END;


CREATE TRIGGER UpdateSoLuongUT
ON UngTuyen
AFTER INSERT, DELETE, UPDATE
AS
BEGIN
    -- Cập nhật số lượng ứng tuyển khi có INSERT
    IF EXISTS (SELECT 1 FROM inserted)
    BEGIN
        UPDATE CongViec
        SET SoLuongUT = (
            SELECT COUNT(ID_CV)
            FROM UngTuyen
            WHERE CongViec.ID_CV = UngTuyen.ID_CV
        )
        WHERE ID_CV IN (SELECT DISTINCT ID_CV FROM inserted);
    END

    -- Cập nhật số lượng ứng tuyển khi có DELETE
    IF EXISTS (SELECT 1 FROM deleted)
    BEGIN
        UPDATE CongViec
        SET SoLuongUT = (
            SELECT COUNT(ID_CV)
            FROM UngTuyen
            WHERE CongViec.ID_CV = UngTuyen.ID_CV
        )
        WHERE ID_CV IN (SELECT DISTINCT ID_CV FROM deleted);
    END

    -- Cập nhật số lượng ứng tuyển khi có UPDATE (trường hợp ID_CV thay đổi)
    IF EXISTS (SELECT 1 FROM inserted) AND EXISTS (SELECT 1 FROM deleted)
    BEGIN
        UPDATE CongViec
        SET SoLuongUT = (
            SELECT COUNT(ID_CV)
            FROM UngTuyen
            WHERE CongViec.ID_CV = UngTuyen.ID_CV
        )
        WHERE ID_CV IN (
            SELECT DISTINCT ID_CV FROM inserted
            UNION
            SELECT DISTINCT ID_CV FROM deleted
        );
    END
END;