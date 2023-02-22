import produce from 'immer';
import * as types from './constants';

export const initialState = {
  loading: false,
  error: null,
  data: [],
};

const customerGroupReducer = (state = initialState, action) =>
  produce(state, (draft) => {
    switch (action.type) {
      case types.GET_CUSTOMER_GROUP:
        draft.loading = true;
        draft.error = null;
        break;
      case types.GET_CUSTOMER_GROUP_SUCCESS:
        console.log("action ", action)
        draft.loading = false;
        draft.error = null;
        draft.data = action.payload;
        break;
      case types.GET_CUSTOMER_GROUP_FAIL:
        draft.loading = false;
        draft.error = action.payload;
        draft.data = [];
        break;
      default: return initialState
    }
  });

export default customerGroupReducer;
