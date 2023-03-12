import * as api from "config/axios";

const PRODUCT_UNITS_ENDPOINT = "api/ProductUnits";
const config = {
  headers: { "Content-Type": "text/plain" },
};
export const getListProductUnits = () => {
  return api.sendGet(PRODUCT_UNITS_ENDPOINT);
};

export const updateProductUnit = (id, payload) => {
  return api.sendPut(`${PRODUCT_UNITS_ENDPOINT}/${id}`, payload);
};

export const createProductUnit = (payload) => {
  return api.sendPost(`${PRODUCT_UNITS_ENDPOINT}`, payload);
};

export const deleteProductUnit = (id) => {
  return api.sendDelete(`${PRODUCT_UNITS_ENDPOINT}/${id}`);
};
