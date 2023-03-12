import * as api from "config/axios";

const MANUFACTURERS_ENDPOINT = "api/Manufacturers";

export const getListManufacturers = () => {
  return api.sendGet(MANUFACTURERS_ENDPOINT);
};

export const createManufacturer = (payload) => {
  return api.sendPost(`${MANUFACTURERS_ENDPOINT}`, payload);
};
export const updateManufacturer = (id, payload) => {
  return api.sendPut(`${MANUFACTURERS_ENDPOINT}/${id}`, payload);
};
export const deleteManufacturer = (id) => {
  return api.sendDelete(`${MANUFACTURERS_ENDPOINT}/${id}`);
};
