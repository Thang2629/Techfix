import * as api from "config/axios";

export const PRODUCTS_ENDPOINT = "api/Products";

export const PRODUCTS_GRID_ENDPOINT = "api/Products/get-all";

export const getProducts = (payload) => {
  return api.sendPost(PRODUCTS_GRID_ENDPOINT, payload);
};

export const getProductDetails = (Id) => {
  return api.sendPost(`${PRODUCTS_ENDPOINT}/detail/${Id}`);
};
