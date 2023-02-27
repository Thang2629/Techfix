import * as api from "config/axios";

const PRODUCT_CONDITIONS_ENDPOINT = "api/ProductConditions";

export const getListProductConditions = () => {
  return api.sendGet(PRODUCT_CONDITIONS_ENDPOINT);
};
