import { put, call, all } from "redux-saga/effects";
import { takeLatest } from "redux-saga/effects";

import * as types from "./constants";
import * as actions from "./actions";
import * as services from "services/sample";
import * as mauServices from "services/specimen-manage";

export function* getDanhSachMauSaga(action) {
	try {
		const response = yield call(services.getDanhSachMau, action.payload);

		if (response.isSuccess) {
			yield put(actions.getDanhSachMauSuccess(response.data));
		} else {
			yield put(actions.getDanhSachMauFail(response?.message));
		}
	} catch (error) {
		let message = error;
		if (typeof error === "object") message = error.message;
		yield put(actions.getDanhSachMauFail(message));
	}
}

export function* getDetailPhieuSaga(action) {
	try {
		const response = yield call(services.getDetailPhieu, action.payload);
		if (response.isSuccess) {
			yield put(actions.getPhieuTiepNhanSuccess(response.data)); // todo: need update when api update
		} else {
			yield put(actions.getPhieuTiepNhanFail(response?.message));
		}
	} catch (error) {
		yield put(actions.getPhieuTiepNhanFail(error));
	}
}

export function* getDetailMauSaga(action) {
	try {
		const data = yield call(services.getDetailMau, action.payload);
		if (data?.isSuccess) {
			yield put(actions.getDetailMauSuccess(data.data));
		} else {
			yield put(actions.getDetailMauFail(data?.message));
		}
	} catch (error) {
		let message = error;
		if (typeof error === "object") message = error.message;
		yield put(actions.getDetailMauFail(message));
	}
}

export function* getListPhieuSaga(action) {
	try {
		const response = yield call(services.getListPhieuTiepNhan, action.payload);
		if (response.isSuccess) {
			yield put(actions.getListPhieuSuccess(response.data));
		} else {
			yield put(actions.getListPhieuFail(response?.message));
		}
	} catch (error) {
		yield put(actions.getListPhieuFail(error));
	}
}

export function* getListMauByPhieuTiepNhanIdSaga(action) {
	try {
		const data = yield call(
			services.getListDetailMauByPhieuTiepNhanId,
			action.payload
		);
		if (data.isSuccess) {
			yield put(actions.getListMauByPhieuTiepNhanIdSuccess(data));
		} else {
			yield put(actions.getListMauByPhieuTiepNhanIdFail(data?.message));
		}
	} catch (error) {
		yield put(actions.getListMauByPhieuTiepNhanIdFail(error));
	}
}

export function* getListLinhVucSaga(action) {
	try {
		const data = yield call(mauServices.getFields, action.payload);
		if (data.isSuccess) {
			yield put(actions.getListLinhVucSuccess(data?.data));
		} else {
			yield put(actions.getListLinhVucFail(data?.message));
		}
	} catch (error) {
		yield put(actions.getListLinhVucFail(error));
	}
}

export function* getistChiTieusGroupByLinhVucSaga(action) {
	try {
		const { data } = yield call(services.getChiTieusGroupByLinhVucs, action.payload);
		if (data) {
			yield put(actions.getListChiTieusGroupByLinhVucSuccess(data));
		}
	} catch (error) {
		yield put(actions.getListChiTieusGroupByLinhVucFail(error));
	}
}

export default function* watchPhanChiaChiTieu() {
	yield all([
		takeLatest(types.GET_DANH_SACH_MAU, getDanhSachMauSaga),
		takeLatest(types.GET_PHIEU_TIEP_NHAN, getDetailPhieuSaga),
		takeLatest(types.GET_DETAIL_MAU, getDetailMauSaga),
		takeLatest(types.GET_LIST_PHIEU, getListPhieuSaga),
		takeLatest(
			types.GET_LIST_MAU_BY_PHIEU_TIEP_NHAN_ID,
			getListMauByPhieuTiepNhanIdSaga
		),
		takeLatest(types.GET_LIST_LINH_VUC, getListLinhVucSaga),
		takeLatest(types.GET_LIST_LINH_VUC, getistChiTieusGroupByLinhVucSaga),
	]);
}
