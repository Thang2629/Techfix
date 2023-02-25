import React, { useState, useEffect } from "react";
import PropTypes from "prop-types";
import { useSelector, useDispatch } from "react-redux";
import {
  Input,
  Pagination,
  Space,
  Button,
  Dropdown,
  Menu,
  Typography,
  DatePicker,
} from "antd";
import moment from "moment";
import { useParams } from "react-router-dom";

import DanhSachMau from "./DanhSachMau";
import { FilterOutlined, PlusCircleOutlined } from "@ant-design/icons";
import Loading from "components/Loading/Loading";
import * as selectors from "redux/quyTrinh/selectors";
import * as actions from "redux/quyTrinh/actions";
import "./LeftSection.less";
import { useTable } from "hooks/useTable";
import { convertToISOTime, getPrevious30Days } from "utils/formatDate";

const { RangePicker } = DatePicker;

const LeftSection = (props) => {
  const { isDetailPhieuPage, phieuInfo, getDanhSachMauByPhieuTiepNhanId } =
    props;

  const { id } = useParams();
  const dispatch = useDispatch();
  const danhSachMau = useSelector(selectors.selectDanhSachMau());
  const loading = useSelector(selectors.selectDanhSachMauLoading());
  const { state, setState } = useTable();
  const [rangePicker, setRangePicker] = useState({
    dateStart: getPrevious30Days(),
    dateEnd: moment(),
    tempStart: getPrevious30Days(),
    tempEnd: moment(),
  });

  useEffect(() => {
    if (isDetailPhieuPage) {
      getDanhSachMauByPhieuTiepNhanId && getDanhSachMauByPhieuTiepNhanId(id);
      if (!phieuInfo || phieuInfo?.id !== id)
        dispatch(actions.getPhieuTiepNhan(id));
    } else {
      getDanhSachMauAll();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [dispatch, isDetailPhieuPage]);

  const getDanhSachMauAll = () => {
    dispatch(
      actions.getDanhSachMau({
        pageSize: state.pageSize,
        pageIndex: state.currentPage,
        TuNgay: convertToISOTime(rangePicker.dateStart),
        DenNgay: convertToISOTime(rangePicker.dateEnd),
        searchText: "",
      })
    );
  };

  const handleChangePage = (page, pageSize) => {
    setState({ currentPage: page });
  };

  const doClickAction = (sample) => {
    !isDetailPhieuPage &&
      dispatch(actions.getPhieuTiepNhan(sample?.tblPhieuTiepNhanMauId)); // for ds mau of phan chi chi tieu only
    dispatch(actions.getDetailMau(sample?.id));
  };

  const handleToggleCreateMau = () => {
    dispatch(actions.toggleCreateMau(true));
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

  console.log("danhSachMau ", danhSachMau);

  return (
    <section className="left-section">
      <Space>
        <Input.Search placeholder="Search input here" />
        <Dropdown menu={dropdownMenu} trigger={["click"]}>
          <Button type="default" icon={<FilterOutlined />}></Button>
        </Dropdown>
        {isDetailPhieuPage && (
          <Button
            type="primary"
            onClick={handleToggleCreateMau}
            icon={<PlusCircleOutlined />}
          >
            Tạo mẫu
          </Button>
        )}
      </Space>
      {loading ? (
        <Loading />
      ) : (
        <>
          <DanhSachMau
            isDetailPhieuPage={isDetailPhieuPage}
            danhSachMau={danhSachMau?.data || []}
            doClickAction={doClickAction}
            handleToggleCreateMau={handleToggleCreateMau}
          />
          <Pagination
            className="pagination"
            defaultCurrent={danhSachMau?.pageIndex}
            total={danhSachMau?.totalRecord}
            pageSize={state.pageSize}
            current={state.currentPage}
            showSizeChanger={false}
            hideOnSinglePage
            responsive
            showLessItems
            onChange={handleChangePage}
          />
        </>
      )}
    </section>
  );
};

LeftSection.propTypes = {};

export default LeftSection;
