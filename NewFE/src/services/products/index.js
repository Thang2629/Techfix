import * as api from "config/axios";
import * as endpoint from "./endpoint";

export const getProducts = (params) => {
  return api.sendPost(endpoint.URL_PRODUCTS, params);
};
