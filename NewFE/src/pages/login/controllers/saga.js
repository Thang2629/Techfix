import { put, call } from "redux-saga/effects";

import * as types from "./constants";
import { takeLatest } from "redux-saga/effects";

import * as services from "services/login";
import { fowardTo } from "utils/common/route";
import { setSession } from "utils/common/session";
import { setData } from "utils/storage";
import { USER_KEY } from "static/Constants";

export function* loginSaga(payload) {
  try {
    const response = yield call(services.loginService, payload.user);
    if (response.Status !== "SUCCESS") {
      yield put({ type: types.LOGIN_ERROR, error: response.message });
    } else {
      const { Username, Token } = response;
      yield call(setSession, Token);
      yield call(setData, Username, USER_KEY);
      yield put({ type: types.LOGIN_SUCCESS, Username });

      yield call(fowardTo, "/dashboard");
    }
  } catch (error) {
    let message = error;
    if (typeof error === "object") message = error.message;
    yield put({ type: types.LOGIN_ERROR, message });
  }
}

export default function* watchUserAuthentication() {
  yield takeLatest(types.LOGIN, loginSaga);
}
