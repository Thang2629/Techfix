import * as api from "config/axios";

const CATEGORIES_ENDPOINT = "api/Categories";

export const getListCatagories = () => {
  return api.sendGet(CATEGORIES_ENDPOINT);
};
