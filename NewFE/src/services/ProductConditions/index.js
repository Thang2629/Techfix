import * as api from "config/axios";

const PRODUCT_CONDITIONS_ENDPOINT = "api/ProductConditions";

export const getListProductConditions = () => {
  return api.sendGet(PRODUCT_CONDITIONS_ENDPOINT);
};

export const createProductCondition = (payload) => {
  return api.sendPost(`${PRODUCT_CONDITIONS_ENDPOINT}`, payload);
};

export const updateProductCondition = (id, payload) => {
  return api.sendPut(`${PRODUCT_CONDITIONS_ENDPOINT}/${id}`, payload);
};

export const deleteProductCondition = (id) => {
  return api.sendDelete(`${PRODUCT_CONDITIONS_ENDPOINT}/${id}`);
};
