import * as types from './constants';

/**
 * apply user information when user has logged in successfully
 * @param {object} user 
 */
export function login(user) {
  return {
    type: types.LOGIN,
    user: user
  };
}

export function reset() {
  return {
    type: types.RESET_LOGIN
  };
}