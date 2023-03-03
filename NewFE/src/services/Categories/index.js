import * as api from "config/axios";

const CATEGORIES_ENDPOINT = "api/Categories";

export const getListCategories = () => {
  return api.sendGet(CATEGORIES_ENDPOINT);
};

export const createCategory = (payload) => {
  return api.sendPost(`${CATEGORIES_ENDPOINT}`, payload);
};

export const updateCategory = (id, payload) => {
  return api.sendPut(`${CATEGORIES_ENDPOINT}/${id}`, payload);
};

export const deleteCategory = (id) => {
  return api.sendDelete(`${CATEGORIES_ENDPOINT}/${id}`);
};
