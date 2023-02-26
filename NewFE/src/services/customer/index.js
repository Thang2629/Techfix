import * as api from "config/axios";
import * as endpoints from "./endpoints";

export const getCustomerGroups = (data) => {
  return api.sendPost(endpoints.GET_CUSTOMER_GROUPS_ENDPOINT, data);
};

export const createCustomerGroups = (payload) => {
  return api.sendPost(endpoints.CREATE_CUSTOMER_GROUPS_ENDPOINT, payload);
};

export const updateCustomerGroups = (payload) => {
  return api.sendPost(endpoints.UPDATE_CUSTOMER_GROUPS_ENDPOINT, payload);
};

export const deleteCustomerGroups = (payload) => {
  return api.sendPost(endpoints.DELETE_CUSTOMER_GROUPS_ENDPOINT, payload);
};
