import {
  get,
  deleteData,
  postData,
  putData,
  combinePageInformation,
} from "../utils/store-utils";

export const state = () => ({
  suppliers: [],
  pagination: {
    page: 1,
    per_page: 10,
  },
});

export const mutations = {
  SET_ITEMS(state, items) {
    state.suppliers = items;
  },
  SET_PAGINATION(state, pagination) {
    state.pagination = pagination;
  },
};

export const actions = {
  async getSuppliers({ commit }, payload = {}) {
    let response = await get(`/api/supplier/suppliers`, this.$axios, {
      ...payload,
    });
    commit("SET_ITEMS", response.items);
    commit("SET_PAGINATION", combinePageInformation(response));
    return response;
  },
};
