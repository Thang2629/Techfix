import {
  get,
  del,
  post,
  put,
  combinePageInformation,
  combineStatistic,
} from "../utils/store-utils";

export const state = () => ({
  process_repairs: [],
  statistic: {},
  pagination: {
    page: 1,
    per_page: 10,
  },
});

export const mutations = {
  SET_ITEMS(state, items) {
    state.process_repairs = items;
  },
  SET_STATISTIC(state, statistic) {
    state.statistic = statistic;
  },
  SET_PAGINATION(state, pagination) {
    state.pagination = pagination;
  },
};

export const actions = {
  async getProcessRepairs({ commit }, payload = {}) {
    let response = await get(
      `/api/process_repair/order-process_repairs`,
      this.$axios,
      {
        ...payload,
      }
    );
    commit("SET_ITEMS", response.data.data_process_repair);
    commit("SET_PAGINATION", combinePageInformation(response));
    commit("SET_STATISTIC", combineStatistic(response.data));
    return response;
  },
};
