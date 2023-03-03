import * as api from "config/axios";

const PRODUCT_UNITS_ENDPOINT = "api/ProductUnits";

export const getListProductUnits = () => {
  return api.sendGet(PRODUCT_UNITS_ENDPOINT);
};
