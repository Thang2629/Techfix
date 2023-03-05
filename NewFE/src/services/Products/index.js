import * as api from "config/axios";

export const PRODUCTS_ENDPOINT = "api/Products";

export const PRODUCTS_GRID_ENDPOINT = "api/Products/get-all";

export const getProducts = (payload) => {
  return api.sendPost(PRODUCTS_GRID_ENDPOINT, payload);
};

export const getProductDetails = (id) => {
  return api.sendPost(`${PRODUCTS_ENDPOINT}/detail/${id}`);
};

export const updateProduct = (id, payload) => {
  return api.sendPut(`${PRODUCTS_ENDPOINT}/${id}`, payload);
};

export const createProduct = (payload) => {
  return api.sendPost(`${PRODUCTS_ENDPOINT}`, payload);
};
export const deleteProduct = (id) => {
  return api.sendDelete(`${PRODUCTS_ENDPOINT}/${id}`);
};

export const changeStatusProduct = (id) => {
  return api.sendPut(`${PRODUCTS_ENDPOINT}/change-status/${id}`);
};
export const restoreProduct = (id) => {
  return api.sendPut(`${PRODUCTS_ENDPOINT}/restore/${id}`);
};
