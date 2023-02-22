const prefixAPIQuyTrinh = "/api/QuanLyQuyTrinhTiepMauThu";
const prefixAPILichSu = `${prefixAPIQuyTrinh}/LichSu`;

export const CREATE_PHIEU_TIEP_NHAN_ENDPOINT =
	"/api/QuanLyQuyTrinhTiepMauThu/PhieuTiepNhanMau/Create";
export const UPDATE_PHIEU_TIEP_NHAN_ENDPOINT =
	"/api/QuanLyQuyTrinhTiepMauThu/PhieuTiepNhanMau/Update";
export const GET_FILE_PHIEU_TIEP_NHAN_ENDPOINT =
	"/api/QuanLyQuyTrinhTiepMauThu/PhieuTiepNhanMau/InPhieuTiepNhanMau";
export const GET_DETAIL_PHIEU_TIEP_NHAN_ENDPOINT =
	"/api/QuanLyQuyTrinhTiepMauThu/PhieuTiepNhanMau/GetPhieuTiepNhanMau";
export const GET_LIST_PHIEU_TIEP_NHAN_ENDPOINT =
	"/api/QuanLyQuyTrinhTiepMauThu/PhieuTiepNhanMau/LoadDanhSachPhieuTiepNhanMau";

export const GET_DANH_SACH_MAU_ENDPOINT =
	"/api/QuanLyQuyTrinhTiepMauThu/ChiTietMau/LoadDanhSachChiTietMau";
export const GET_DETAIL_MAU_ENDPOINT =
	"/api/QuanLyQuyTrinhTiepMauThu/ChiTietMau/GetChiTietMau";
export const CREATE_MAU_ENDPOINT =
	"/api/QuanLyQuyTrinhTiepMauThu/ChiTietMau/Create";
export const UPDATE_MAU_ENDPOINT =
	"/api/QuanLyQuyTrinhTiepMauThu/ChiTietMau/Update";

export const GET_DANH_SACH_CHI_TIET_MAU_ENDPOINT =
	"/api/QuanLyQuyTrinhTiepMauThu/ChiTietMau/LoadDanhSachChiTietMauByPhieuTiepNhanId";
export const GET_FILE_IN_KET_QUA_KIEM_NGHIEM_ENDPOINT =
	"/api/QuanLyQuyTrinhTiepMauThu/ChiTietMau/InKetQuaKiemNghiem";
export const GET_FILE_IN_PHIEU_YEU_CAU_PHAN_TICH_ENDPOINT =
	"/api/QuanLyQuyTrinhTiepMauThu/ChiTietMau/InPhieuYeuCauPhanTich";

export const CREATE_CHI_TIEU_KIEM_NGHIEM_ENDPOINT =
	"/api/QuanLyQuyTrinhTiepMauThu/DichVuKiemNghiem/CreateChiTieuKiemNghiem";
export const UPDATE_CHI_TIEU_KIEM_NGHIEM_ENDPOINT =
	"/api/QuanLyQuyTrinhTiepMauThu/DichVuKiemNghiem/UpdateChiTieuKiemNghiem";
export const DELETE_CHI_TIEU_KIEM_NGHIEM_ENDPOINT =
	"/api/QuanLyQuyTrinhTiepMauThu/DichVuKiemNghiem/DeleteChiTieuKiemNghiem";
export const GET_DV_KIEM_NGHIEM_BY_ID_ENDPOINT =
	"/api/QuanLyQuyTrinhTiepMauThu/DichVuKiemNghiem/GetDichVuKiemNghiemMauById";
export const GET_DV_KIEM_NGHIEM_BY_MAU_ID_ENDPOINT =
	"/api/QuanLyQuyTrinhTiepMauThu/DichVuKiemNghiem/GetDichVuKiemNghiemGroupByLinhVucByChiTietMauID";
export const GET_FILE_IN_PHIEU_KIEM_NGHIEM_MAU_ENDPOINT =
	"/api/QuanLyQuyTrinhTiepMauThu/DichVuKiemNghiem/InPhieuKiemNghiem";
export const GET_CHI_TIEUS_GROUP_BY_LINHVUC =
	"/api/QuanLyQuyTrinhTiepMauThu/ChiTietMau/GetChiTieusGroupByLinhVuc";
export const SAVE_CHI_TIEU_KIEM_NGHIEMS =
	"/api/QuanLyQuyTrinhTiepMauThu/DichVuKiemNghiem/SaveChiTieuKiemNghiems";

export const DELETE_CHI_TIEU_KIEM_NGHIEMS =
	"/api/QuanLyQuyTrinhTiepMauThu/DichVuKiemNghiem/DeleteChiTieuKiemNghiems";

export const PHAN_CHIA_CHI_TIEU_KIEM_NGHIEM_ENDPOINT =
	"/api/QuanLyQuyTrinhTiepMauThu/DichVuKiemNghiem/PhanChiaChiTieuKiemNghiem";
export const GET_FILE_IN_PHIEU_KIEM_NGHIEM_ENDPOINT =
	"/api/QuanLyQuyTrinhTiepMauThu/DichVuKiemNghiem/InPhieuKiemNghiem";

export const GET_LICH_SU_PHIEU_TIEP_NHAN_MAU_ID_ENDPOINT = `${prefixAPILichSu}/GetLichSuPhieuTiepNhanMauId`;
export const GET_LICH_SU_HOAT_DONG_BY_PHIEU_TIEP_NHAN_ID_ENDPOINT = `${prefixAPILichSu}/GetLichSuHoatDongByPhieuTiepNhanId`;
