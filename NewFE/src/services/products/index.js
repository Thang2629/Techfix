import * as api from "config/axios";

export const PRODUCTS_ENDPOINT = "api/Products";

export const PRODUCTS_GRID_ENDPOINT = "api/Products/get-all";

export const getProducts = (params) => {
  return api.sendPost(PRODUCTS_ENDPOINT, params);
};

export const getProductDetails = (Id) => {
  return api.sendPost(`${PRODUCTS_ENDPOINT}/detail/${Id}`);
};
