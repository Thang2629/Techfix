import {
  get,
  del,
  post,
  put,
  combinePageInformation,
} from "../utils/store-utils";

export const state = () => ({
  order_repairs: [],
  order_repair: {},
  pagination: {
    page: 1,
    per_page: 10,
  },
});

export const mutations = {
  SET_ITEMS(state, items) {
    state.order_repairs = items;
  },
  SET_ITEM(state, item) {
    state.order_repair = item;
  },
  SET_PAGINATION(state, pagination) {
    state.pagination = pagination;
  },
};

export const actions = {
  async getOrderRepairs({ commit }, payload = {}) {
    let response = await get(`/api/order-repair/order-repairs`, this.$axios, {
      ...payload,
    });
    commit("SET_ITEMS", response.data);
    commit("SET_PAGINATION", combinePageInformation(response));
    return response;
  },

  async createOrderRepair({ commit }, payload = {}) {
    let response = await post(`/api/product-repair`, this.$axios, {
      ...payload,
    });
    return response;
  },

  async deleteOrderRepair({ commit }, payload = {}) {
    let response = await del(`/api/order-repair`, this.$axios, {
      ...payload,
    });
    return response;
  },

  async getOrderRepair({ commit }, payload = {}) {
    let response = await get(
      `/api/order-repair/order-repair/${payload.id}`,
      this.$axios,
      {}
    );
    commit("SET_ITEM", response);
    return response;
  },
};
