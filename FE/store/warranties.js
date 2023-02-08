import {
  get,
  del,
  post,
  put,
  combinePageInformation,
} from "../utils/store-utils";

export const state = () => ({
  warranties: [],
  process_warranties: {},
  pagination: {
    page: 1,
    per_page: 10,
  },
});

export const mutations = {
  SET_ITEMS(state, items) {
    state.warranties = items;
  },

  SET_PROCESS(state, items) {
    state.process_warranties = items;
  },

  SET_PAGINATION(state, pagination) {
    state.pagination = pagination;
  },
};

export const actions = {
  async getWarranties({ commit }, payload = {}) {
    let response = await get(
      `/api/order-warranty/order-warranties`,
      this.$axios,
      {
        ...payload,
      }
    );
    commit("SET_ITEMS", response.data.data);
    commit("SET_PAGINATION", combinePageInformation(response));
    return response;
  },

  async createWarranty({ commit }, payload = {}) {
    let response = await post(`/api/order-warranty`, this.$axios, {
      ...payload,
    });
    return response;
  },

  async getProcessWarranties({ commit }, payload = {}) {
    let response = await get(
      `/api/order-warranty/process-warranties`,
      this.$axios,
      {
        ...payload,
      }
    );
    commit("SET_PROCESS", response.data);
    commit("SET_PAGINATION", combinePageInformation(response));
    return response;
  },
};
