import {
  get,
  deleteData,
  postData,
  putData,
  combinePageInformation,
} from "../utils/store-utils";

export const state = () => ({
  users: [],
  pagination: {
    page: 1,
    per_page: 10,
  },
});

export const mutations = {
  SET_ITEMS(state, items) {
    state.users = items;
  },
  SET_PAGINATION(state, pagination) {
    state.pagination = pagination;
  },
};

export const actions = {
  async getUsers({ commit }, payload = {}) {
    let response = await get(`/api/user/users`, this.$axios, {
      ...payload,
    });
    const pagination = {
      page: 1,
      per_page: 100,
      total_pages: 1,
      count: response.length,
    };
    commit("SET_ITEMS", response);
    commit("SET_PAGINATION", combinePageInformation(pagination));
    return response;
  },
};
