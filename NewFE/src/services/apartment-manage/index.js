import * as api from "config/axios";
import * as endpoint from "./endpoint";

export const getKhoa = (params) => {
	return api.sendGet(endpoint.GET_KHOA_ENDPOINT, params);
};

export const getAllKhoa = (params) => {
	return api.sendGet(endpoint.GET_ALL_KHOA_ENDPOINT, params);
};

export const createKhoa = (payload) => {
	return api.sendPost(endpoint.CREATE_KHOA_ENDPOINT, payload);
};

export const updateKhoa = (payload) => {
	return api.sendPost(endpoint.UPDATE_KHOA_ENDPOINT, payload);
};

export const deleteKhoa = (payload) => {
	return api.sendPost(endpoint.DELETE_KHOA_ENDPOINT, payload);
};
///////////PHONG BAN///////////
export const getPhongBan = (params) => {
	return api.sendGet(endpoint.GET_PHONG_BAN, params);
};
export const getPhongBanById = (id) => {
	return api.sendGet(endpoint.GET_PHONG_BAN_BY_ID + id);
};

export const getAllPhongBan = (params) => {
	return api.sendGet(endpoint.GET_ALL_PHONG_BAN, params);
};

export const createPhongBan = (payload) => {
	return api.sendPost(endpoint.CREATE_PHONG_BAN, payload);
};

export const updatePhongBan = (payload) => {
	return api.sendPost(endpoint.UPDATE_PHONG_BAN, payload);
};

export const deletePhongBan = (payload) => {
	return api.sendPost(endpoint.DELETE_PHONG_BAN, payload);
};

///////////NHAN VIEN///////////
export const getNhanVien = (params) => {
	return api.sendGet(endpoint.GET_NHANVIEN_ENDPOINT, params);
};

export const getNhanVienById = (Id, params) => {
	return api.sendGet(endpoint.GET_NHANVIENBYID_ENDPOINT + Id, params);
};

export const createNhanVien = (payload) => {
	return api.sendPost(endpoint.CREATE_NHANVIEN_ENDPOINT, payload);
};

export const updateNhanVien = (payload) => {
	return api.sendPost(endpoint.UPDATE_NHANVIEN_ENDPOINT, payload);
};

export const deleteNhanVien = (payload) => {
	return api.sendPost(endpoint.DELETE_NHANVIEN_ENDPOINT, payload);
};
