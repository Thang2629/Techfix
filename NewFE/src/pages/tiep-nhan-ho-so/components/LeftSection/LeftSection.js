import React, { useState, useEffect } from "react";
import PropTypes from "prop-types";
import {
  Input,
  Pagination,
  Space,
  Button,
  Dropdown,
  DatePicker,
  Menu,
  Typography,
} from "antd";
import { useDispatch, useSelector } from "react-redux";
import DanhSachPhieu from "./DanhSachPhieu";
import Loading from "components/Loading/Loading";
import * as selectors from "redux/quyTrinh/selectors";
import * as actions from "redux/quyTrinh/actions";
import { useHistory } from "react-router-dom";
import { FilterOutlined } from "@ant-design/icons";
import { useTable, withTable } from "hooks/useTable";
import moment from "moment";
import { convertToISOTime, getPrevious30Days } from "utils/formatDate";

import "./LeftSection.less";

const { RangePicker } = DatePicker;

const LeftSection = (props) => {
  const history = useHistory();
  const dispatch = useDispatch();
  const listPhieu = useSelector(selectors.selectListPhieu());
  const loading = useSelector(selectors.selectListPhieuLoading());
  const { state, setState } = useTable();
  const [rangePicker, setRangePicker] = useState({
    dateStart: getPrevious30Days(),
    dateEnd: moment(),
    tempStart: getPrevious30Days(),
    tempEnd: moment(),
  });

  useEffect(() => {
    dispatch(
      actions.getListPhieu({
        PageSize: state.pageSize,
        PageIndex: state.currentPage,
        TuNgay: convertToISOTime(rangePicker.dateStart),
        DenNgay: convertToISOTime(rangePicker.dateEnd),
        SearchText: "",
      })
    );
  }, [
    state.pageSize,
    state.currentPage,
    dispatch,
    rangePicker.dateStart,
    rangePicker.dateEnd,
  ]);

  const doClickAction = (phieuInfo) => {
    dispatch(actions.getPhieuTiepNhanSuccess(phieuInfo)); // temp
    dispatch(actions.getPhieuTiepNhan(phieuInfo.id));
  };

  const doDoubleClickAction = (phieuInfo) => {
    history.push(`/ho-so/${phieuInfo.id}`);
  };

  const handleChangePage = (page, pageSize) => {
    setState({ currentPage: page });
  };

  const onChangeTime = (dates, dateStrings) => {
    setRangePicker({ ...rangePicker, tempStart: dates[0], tempEnd: dates[1] });
  };

  const handleChoose = (dates, dateStrings) => {
    setRangePicker({
      ...rangePicker,
      dateStart: rangePicker.tempStart,
      dateEnd: rangePicker.tempEnd,
    });
  };

  const dropdownMenu = (
    <Menu style={{ width: "500px", padding: "16px" }}>
      <Space>
        <Typography.Text strong>Thời gian:</Typography.Text>
        <RangePicker
          picker="week"
          format={"DD-MM-YYYY"}
          value={[rangePicker.tempStart, rangePicker.tempEnd]}
          onChange={onChangeTime}
        />
        <Button type="primary" onClick={handleChoose}>
          OK
        </Button>
      </Space>
    </Menu>
  );

  return (
    <section className="left-section">
      <Space>
        <Input.Search placeholder="Search input here" />
        <Dropdown overlay={dropdownMenu} trigger={["click"]}>
          <Button type="default" icon={<FilterOutlined />}></Button>
        </Dropdown>
      </Space>
      {loading ? (
        <Loading />
      ) : (
        <>
          <DanhSachPhieu
            listPhieu={listPhieu?.data || []}
            doDoubleClickAction={doDoubleClickAction}
            doClickAction={doClickAction}
          />
          <Pagination
            className="pagination"
            defaultCurrent={state.currentPage}
            showSizeChanger={false}
            hideOnSinglePage
            responsive
            showLessItems
            onChange={handleChangePage}
            pageSize={state.pageSize}
            current={state.currentPage}
            total={listPhieu.totalRecord || 0}
          />
        </>
      )}
    </section>
  );
};

LeftSection.propTypes = {};

export default withTable(LeftSection);
