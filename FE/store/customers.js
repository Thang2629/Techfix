import {
  get,
  del,
  post,
  put,
  combinePageInformation,
} from "../utils/store-utils";

export const state = () => ({
  customers: [],
  pagination: {
    page: 1,
    per_page: 10,
  },
});

export const mutations = {
  SET_ITEMS(state, items) {
    state.customers = items;
  },
  SET_PAGINATION(state, pagination) {
    state.pagination = pagination;
  },
};

export const actions = {
  async getCustomers({ commit }, payload = {}) {
    let response = await get(`/api/customers/customers`, this.$axios, {
      ...payload,
    });
    commit("SET_ITEMS", response.data);
    commit("SET_PAGINATION", combinePageInformation(response));
    return response;
  },

  async createCustomer({ commit }, payload = {}) {
    let response = await post(`/api/customers`, this.$axios, {
      ...payload,
    });
    return response;
  },

  async deleteCustomer({ commit }, payload = {}) {
    let response = await del(`/api/customers`, this.$axios, {
      ...payload,
    });
    return response;
  },

  async updateCustomer({ commit }, payload = {}) {
    let response = await put(`/api/customers?id=${payload.id}`, this.$axios, {
      ...payload.payload,
    });
    return response;
  },
};
