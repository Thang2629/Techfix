import * as api from "config/axios";

const CATEGORIES_ENDPOINT = "api/Categories";

export const getListCategories = () => {
  return api.sendGet(CATEGORIES_ENDPOINT);
};

export const createCategory = (id, payload) => {
  return api.sendPost(`${CATEGORIES_ENDPOINT}/${id}`, payload);
};
