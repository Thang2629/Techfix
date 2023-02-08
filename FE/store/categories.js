import {
    get,
    deleteData,
    postData,
    putData,
    combinePageInformation
} from "../utils/store-utils";

export const state = () => ({
    categories: [],
    pagination: {
        page: 1,
        per_page: 10
    },
});

export const mutations = {
    SET_ITEMS(state, items) {
        state.categories = items;
    },
    SET_PAGINATION(state, pagination) {
        state.pagination = pagination;
    },

};

export const actions = {
    async getCategories({ commit }, payload = {}) {
        let response = await get(`/api/categories/categories`, this.$axios, {
            ...payload
        });
        commit("SET_ITEMS", response);
        return response;
    },
};