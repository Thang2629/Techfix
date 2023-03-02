import * as api from "config/axios";

const PRODUCT_UNITS_ENDPOINT = "api/ProductUnits";

export const getListProductUnits = () => {
  return api.sendGet(PRODUCT_UNITS_ENDPOINT);
};

export const updateProductUnit = (id, payload) => {
  return api.sendPut(`${PRODUCT_UNITS_ENDPOINT}/${id}`, payload);
};

export const createProductUnit = (id, payload) => {
  return api.sendPost(`${PRODUCT_UNITS_ENDPOINT}/${id}`, payload);
};
