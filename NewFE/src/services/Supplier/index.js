import * as api from "config/axios";

const SUPPLIERS_ENDPOINT = "api/Suppliers";

export const SUPPLIERS_GRID_ENDPOINT = `${SUPPLIERS_ENDPOINT}/get-all`;

export const getListSuppliers = (
  payload = {
    FilterParams: [],
    PageNumber: 1,
    PageSize: 10,
  }
) => {
  return api.sendPost(SUPPLIERS_GRID_ENDPOINT, payload);
};
