import * as api from "config/axios";
import * as endpoints from "./endpoints";

export const getFieldGroup = (id) => {
	return api.sendGet(endpoints.GET_FIELD_GROUP_ENDPOINT + id);
};

export const getFieldGroups = (data) => {
	return api.sendPost(endpoints.GET_FIELD_GROUPS_ENDPOINT, data);
};

export const createFieldGroups = (payload) => {
	return api.sendPost(endpoints.CREATE_FIELD_GROUPS_ENDPOINT, payload);
};

export const updateFieldGroups = (payload) => {
	return api.sendPost(endpoints.UPDATE_FIELD_GROUPS_ENDPOINT, payload);
};

export const deleteFieldGroups = (payload) => {
	return api.sendPost(endpoints.DELETE_FIELD_GROUPS_ENDPOINT, payload);
};

export const getField = (id) => {
	return api.sendGet(endpoints.GET_FIELD_ENDPOINT + id);
};

export const getFields = (data) => {
	return api.sendPost(endpoints.GET_FIELDS_ENDPOINT, data);
};

export const createFields = (payload) => {
	return api.sendPost(endpoints.CREATE_FIELDS_ENDPOINT, payload);
};

export const updateFields = (payload) => {
	return api.sendPost(endpoints.UPDATE_FIELDS_ENDPOINT, payload);
};

export const deleteFields = (payload) => {
	return api.sendPost(endpoints.DELETE_FIELDS_ENDPOINT, payload);
};

export const getLoaiMauTiepNhan = (id) => {
	return api.sendGet(endpoints.GET_LOAIMAUTIEPNHAN_ENDPOINT + id);
};

export const getLoaiMauTiepNhans = (data) => {
	return api.sendPost(endpoints.GET_LOAIMAUTIEPNHANS_ENDPOINT, data);
};

export const createLoaiMauTiepNhan = (payload) => {
	return api.sendPost(endpoints.CREATE_LOAIMAUTIEPNHAN_ENDPOINT, payload);
};

export const updateLoaiMauTiepNhan = (payload) => {
	return api.sendPost(endpoints.UPDATE_LOAIMAUTIEPNHAN_ENDPOINT, payload);
};

export const deleteLoaiMauTiepNhan = (payload) => {
	return api.sendPost(endpoints.DELETE_LOAIMAUTIEPNHAN_ENDPOINT, payload);
};

export const getNhomChiTieu = (id) => {
	return api.sendGet(endpoints.GET_NHOMCHITIEUBYID_ENDPOINT + id);
};

export const getNhomChiTieus = (params) => {
	return api.sendGet(endpoints.GET_NHOMCHITIEU_ENDPOINT, params);
};

export const createNhomChiTieu = (payload) => {
	return api.sendPost(endpoints.CREATE_NHOMCHITIEU_ENDPOINT, payload);
};

export const updateNhomChiTieu = (payload) => {
	return api.sendPost(endpoints.UPDATE_NHOMCHITIEU_ENDPOINT, payload);
};

export const deleteNhomChiTieu = (payload) => {
	return api.sendPost(endpoints.DELETE_NHOMCHITIEU_ENDPOINT, payload);
};

export const getDangBaoChes = (params) => {
	return api.sendGet(endpoints.GET_DANGBAOCHE_ENDPOINT, params);
};

export const createDangBaoChe = (payload) => {
	return api.sendPost(endpoints.CREATE_DANGBAOCHE_ENDPOINT, payload);
};

export const updateDangBaoChe = (payload) => {
	return api.sendPost(endpoints.UPDATE_DANGBAOCHE_ENDPOINT, payload);
};

export const deleteDangBaoChe = (payload) => {
	return api.sendPost(endpoints.DELETE_DANGBAOCHE_ENDPOINT, payload);
};

export const getChiTieuKiemNghiemId = (id) => {
	let url = endpoints.GET_CHITIEUKIEMNGHIEMID_ENDPOINT + id;
	return api.sendGet(url);
};

export const getChiTieuKiemNghiems = (data) => {
	return api.sendPost(endpoints.GET_CHITIEUKIEMNGHIEM_ENDPOINT, data);
};

export const createChiTieuKiemNghiem = (payload) => {
	return api.sendPost(endpoints.CREATE_CHITIEUKIEMNGHIEM_ENDPOINT, payload);
};

export const updateChiTieuKiemNghiem = (payload) => {
	return api.sendPost(endpoints.UPDATE_CHITIEUKIEMNGHIEM_ENDPOINT, payload);
};

export const deleteChiTieuKiemNghiem = (payload) => {
	return api.sendPost(endpoints.DELETE_CHITIEUKIEMNGHIEM_ENDPOINT, payload);
};

export const getHoatChatById = (id) => {
	return api.sendGet(endpoints.GET_HOAT_CHAT_BY_ID_ENDPOINT + id);
};

export const getHoatChat = (data) => {
	return api.sendPost(endpoints.GET_HOAT_CHAT_ENDPOINT, data);
};

export const createHoatChat = (payload) => {
	return api.sendPost(endpoints.CREATE_HOAT_CHAT_ENDPOINT, payload);
};

export const updateHoatChat = (payload) => {
	return api.sendPost(endpoints.UPDATE_HOAT_CHAT_ENDPOINT, payload);
};

export const deleteHoatChat = (payload) => {
	return api.sendPost(endpoints.DELETE_HOAT_CHAT_ENDPOINT, { Ids: payload });
};
