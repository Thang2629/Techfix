import React, { useEffect } from "react";
import { useDispatch } from "react-redux";
import * as actions from "redux/global/actions";
import ImplementResult from "common/components/result/Implement";
import { SEARCH_CRITERIA } from "static/Constants";
import "./style.less";
const option = {};
const searchCriteria = SEARCH_CRITERIA.ALL;

const Dashboard = (props) => {
  const dispatch = useDispatch();
  useEffect(() => {
    dispatch(actions.changeRibbonActions(option));
  }, [dispatch]);

  useEffect(() => {
    dispatch(actions.updateSearchCriteria(searchCriteria));
  }, [dispatch]);

  return (
    <div>
      <ImplementResult module="Dashboard" />
    </div>
  );
};

Dashboard.propTypes = {};

export default Dashboard;
