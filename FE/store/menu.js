import { findNavItemsByRole } from '~/utils/meta_data';

export const state = () => ({
  current_menu: {},
  nav_items: [],
  current_request_gutter: {},
});

export const mutations = {
  SET_CURRENT_MENU(state, current_menu) {
    state.current_menu = current_menu;
  },
  SET_NAV_ITEMS(state, nav_items) {
    state.nav_items = nav_items;
  },
  SET_CURRENT_REQUEST_GUTTER(state, current_request_gutter) {
    state.current_request_gutter = current_request_gutter;
  },
};

export const actions = {
  setCurrentMenu({ commit }, current_menu) {
    console.log(current_menu);
    commit('SET_CURRENT_MENU', current_menu);
  },
  findNavItems({ commit }, role) {
    commit('SET_NAV_ITEMS', findNavItemsByRole(role));
  },
  setCurrentRequestGutter({ commit }, current_request_gutter) {
    commit('SET_CURRENT_REQUEST_GUTTER', current_request_gutter);
  },
};
