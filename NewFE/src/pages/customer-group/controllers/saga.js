import { put, call } from "redux-saga/effects";
import { takeLatest } from "redux-saga/effects";

import * as types from "./constants";
import * as actions from "./actions";
import * as services from "services/customer";

export function* getCustomerGroupSaga(payload) {
	try {
		const response = yield call(services.getCustomerGroups, payload);
		if (response.isSuccess) {
			yield put(actions.getCustomerGroupSuccess(response.data.data));
		} else {
			yield put(actions.getCustomerGroupFail(response.message));
		}
	} catch (error) {
		let message = error;
		if (typeof error === "object") message = error.message;
		yield put(actions.getCustomerGroupFail(message));
	}
}

export default function* watchCustomerGroup() {
	yield takeLatest(types.GET_CUSTOMER_GROUP, getCustomerGroupSaga);
}
