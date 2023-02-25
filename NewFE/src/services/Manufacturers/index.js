import * as api from "config/axios";

const MANUFACTURERS_ENDPOINT = "api/Manufacturers";

export const getListManufacturers = () => {
  return api.sendGet(MANUFACTURERS_ENDPOINT);
};
