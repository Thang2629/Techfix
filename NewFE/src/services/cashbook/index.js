import * as api from "config/axios";

export const CASHBOOK_GRID_ENDPOINT = "/api/Funds/get-all";
export const getCashbooks = (payload) => {
  return api.sendPost(CASHBOOK_GRID_ENDPOINT, payload);
};
