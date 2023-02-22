import { createSelector } from 'reselect';
import { initialState } from './reducer';

const customerGroups = (state) => state.customerGroups || initialState;

const selectCustomerGroups = () => createSelector(customerGroups, (groupsState) => groupsState.data);
const selectCustomerErr = () => createSelector(customerGroups, (groupsState) => groupsState.error);
const selectCustomerLoading = () => createSelector(customerGroups, (groupsState) => groupsState.loading);

export { selectCustomerGroups, selectCustomerErr, selectCustomerLoading };
