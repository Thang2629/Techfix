import * as api from "config/axios";
import * as endpoints from "./endpoints";

export const getListPhieuTiepNhan = (params) => {
	return api.sendPost(endpoints.GET_LIST_PHIEU_TIEP_NHAN_ENDPOINT, null, {
		params: params,
	});
};

export const createPhieuTiepNhan = (data) => {
	return api.sendPost(endpoints.CREATE_PHIEU_TIEP_NHAN_ENDPOINT, data);
};

export const updatePhieuTiepNhan = (data) => {
	return api.sendPost(endpoints.UPDATE_PHIEU_TIEP_NHAN_ENDPOINT, data);
};

export const getFilePhieuTiepNhan = (phieuTiepNhanId) => {
	const data = { phieuTiepNhanMauID: phieuTiepNhanId, isCreateNew: true };
	return api.sendPost(endpoints.GET_FILE_PHIEU_TIEP_NHAN_ENDPOINT, data);
};

export const getDetailPhieu = (id) => {
	return api.sendGet(`${endpoints.GET_DETAIL_PHIEU_TIEP_NHAN_ENDPOINT}/${id}`);
};

export const getDanhSachMau = (params) => {
	return api.sendPost(endpoints.GET_DANH_SACH_MAU_ENDPOINT, null, {
		params: params,
	});
};

export const createMau = (data) => {
	return api.sendPost(endpoints.CREATE_MAU_ENDPOINT, data);
};

export const updateMau = (data) => {
	return api.sendPost(endpoints.UPDATE_MAU_ENDPOINT, data);
};

export const getDetailMau = (id) => {
	return api.sendGet(`${endpoints.GET_DETAIL_MAU_ENDPOINT}/${id}`);
};

export const getListDetailMauByPhieuTiepNhanId = (id) => {
	return api.sendGet(`${endpoints.GET_DANH_SACH_CHI_TIET_MAU_ENDPOINT}/${id}`);
};

export const createChiTieuKiemNghiem = (data) => {
	return api.sendPost(endpoints.CREATE_CHI_TIEU_KIEM_NGHIEM_ENDPOINT, data);
};

export const updateChiTieuKiemNghiem = (data) => {
	return api.sendPost(endpoints.UPDATE_CHI_TIEU_KIEM_NGHIEM_ENDPOINT, data);
};

export const getChiTieuKiemNghiemById = (id) => {
	return api.sendGet(`${endpoints.GET_DV_KIEM_NGHIEM_BY_ID_ENDPOINT}/${id}`);
};

export const getDVKiemNghiemByMauId = (mauId) => {
	return api.sendGet(
		`${endpoints.GET_DV_KIEM_NGHIEM_BY_MAU_ID_ENDPOINT}/${mauId}`
	);
};

export const deleteChiTieuKiemNghiem = (id) => {
	return api.sendDelete(endpoints.DELETE_CHI_TIEU_KIEM_NGHIEM_ENDPOINT, { id });
};

export const inPhieuKiemNghiem = (chiTietMauId, createNew) => {
	const data = { chiTietMauId, createNew };
	return api.sendPost(
		endpoints.GET_FILE_IN_PHIEU_KIEM_NGHIEM_MAU_ENDPOINT,
		data
	);
};

export const getChiTieusGroupByLinhVucs = (params) => {
	return api.sendGet(endpoints.GET_CHI_TIEUS_GROUP_BY_LINHVUC, { params });
};

export const saveChiTieuKiemNghiems = (data) => {
	return api.sendPost(endpoints.SAVE_CHI_TIEU_KIEM_NGHIEMS, data);
};

export const deleteChiTieuKiemNghiems = (data) => {
	return api.sendDelete(endpoints.DELETE_CHI_TIEU_KIEM_NGHIEMS, { data });
};
