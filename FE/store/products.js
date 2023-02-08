import {
  get,
  deleteData,
  post,
  putData,
  combinePageInformation,
} from '../utils/store-utils';

export const state = () => ({
  products: [],
  product_conditions: [],
  product_units: [],
  pagination: {
    page: 1,
    per_page: 10,
  },
});

export const mutations = {
  SET_ITEMS(state, items) {
    state.products = items;
  },
  SET_PAGINATION(state, pagination) {
    state.pagination = pagination;
  },
  SET_PRODUCT_CONDITIONS(state, items) {
    state.product_conditions = items;
  },
  SET_PRODUCT_UNITS(state, items) {
    state.product_units = items;
  },
};

export const actions = {
  async getProducts({ commit }, payload = {}) {
    let response = await get(`/api/products/products`, this.$axios, {
      ...payload,
    });
    commit('SET_ITEMS', response.data);
    commit('SET_PAGINATION', combinePageInformation(response));

    return response;
  },

  async getProductConditions({ commit }, payload = {}) {
    let response = await get(`/api/product-condition/products`, this.$axios, {
      ...payload,
    });
    commit('SET_PRODUCT_CONDITIONS', response.items);
    return response;
  },

  async getProductUnits({ commit }, payload = {}) {
    let response = await get(`/api/product-unit/products`, this.$axios, {
      ...payload,
    });
    commit('SET_PRODUCT_UNITS', response.items);
    return response;
  },

  async createProduct({ commit }, payload = {}) {
    let response = await post(`/api/products`, this.$axios, {
      ...payload,
    });
    return response;
  },
};
