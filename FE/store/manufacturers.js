import {
    get,
    deleteData,
    postData,
    putData,
    combinePageInformation
} from "../utils/store-utils";

export const state = () => ({
    manufacturers: [],
    pagination: {
        page: 1,
        per_page: 10
    },
});

export const mutations = {
    SET_ITEMS(state, items) {
        state.manufacturers = items;
    },
    SET_PAGINATION(state, pagination) {
        state.pagination = pagination;
    },

};

export const actions = {
    async getManufacturers({ commit }, payload = {}) {
        let response = await get(`/api/manufacturers/manufacturers`, this.$axios, {
            ...payload
        });
        commit("SET_ITEMS", response.items);
        return response;
    },
};