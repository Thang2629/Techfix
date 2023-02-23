import * as types from './constants';

export const getCustomerGroup = () => {
  return ({
    type: types.GET_CUSTOMER_GROUP
  })
}

export const getCustomerGroupSuccess = (data) => {
  return ({
    type: types.GET_CUSTOMER_GROUP_SUCCESS,
    payload: data
  })
}

export const getCustomerGroupFail = (err) => {
  return ({
    type: types.GET_CUSTOMER_GROUP_FAIL,
    payload: err
  })
}