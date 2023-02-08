import {
  get,
  del,
  post,
  put,
  combinePageInformation,
} from '../utils/store-utils';

export const state = () => ({
  receipts: [],
  receipt: {},
  pagination: {
    page: 1,
    per_page: 10,
  },
});

export const mutations = {
  SET_ITEMS(state, items) {
    state.receipts = items;
  },

  SET_ITEM(state, item) {
    state.receipt = item;
  },

  SET_PAGINATION(state, pagination) {
    state.pagination = pagination;
  },
};

export const actions = {
  async getReceipts({ commit }, payload = {}) {
    let response = await get(`/api/good-receipt/goods-receipts`, this.$axios, {
      ...payload,
    });
    commit('SET_ITEMS', response.data);
    commit('SET_PAGINATION', combinePageInformation(response));
    return response;
  },

  async createReceipt({ commit }, payload = {}) {
    let response = await post(`/api/good-receipt`, this.$axios, {
      ...payload,
    });
    return response;
  },

  async getReceipt({ commit }, payload = {}) {
    let response = await get(`/api/good-receipt/id`, this.$axios, {
      ...payload,
    });
    commit('SET_ITEM', response);
    return response;
  },
};
