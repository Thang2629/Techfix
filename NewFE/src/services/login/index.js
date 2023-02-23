import * as api from "config/axios";
import * as endpoints from "./endpoints";

/**
 * User login service
 * @param {object} data user login information
 */
export const loginService = (params) => {
  return api.sendPost(endpoints.USER_LOGIN_ENDPOINT, params);
};
