import React, { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import { Form, message, Modal, Space } from "antd";
import { ExclamationCircleOutlined, BarsOutlined } from "@ant-design/icons";
import PageWrapper from "components/Layout/PageWrapper";
import HeaderPage from "pages/home/header-page";

import Grid from "components/Grid";

import * as actions from "redux/global/actions";
import { SEARCH_CRITERIA } from "static/Constants";
import { deleteNhanVien } from "services/apartment-manage";
import { DELETE_ERROR, DELETE_SUCCESS } from "utils/common/messageContants";
import { GET_ALLPRODUCTS_ENDPOINT } from "services/products/endpoint";
import CreateAndUpdate from "./CreateAndUpdate";
import { ButtonDelete, PrimaryButton } from "common/components/Buttons";
import { NavLink } from "react-router-dom";

const option = {};
const searchCriteria = SEARCH_CRITERIA.ALL;

const QLNhanVien = (props) => {
  const dispatch = useDispatch();
  const [isOpen, setIsopen] = useState(false);
  const [openDetail, setOpenDetail] = useState(false);
  const [nhanVien, setNhanVien] = useState("");
  const [form] = Form.useForm();

  useEffect(() => {
    dispatch(actions.changeRibbonActions(option));
  }, [dispatch]);

  useEffect(() => {
    dispatch(actions.updateSearchCriteria(searchCriteria));
  }, [dispatch]);

  const columns = [
    {
      title: "Mã Sản Phẩm",
      dataIndex: "Code",
    },
    {
      title: "Tên Sản Phẩm",
      dataIndex: "Name",
    },
    {
      title: "SL",
      dataIndex: "Quantity",
    },
    {
      title: "Giá Vốn",
      dataIndex: "OriginalCost",
    },
    {
      title: "Giá Web",
      dataIndex: "dienThoai",
    },
    {
      title: "TTSP",
      dataIndex: "dienThoai",
    },
    {
      title: "Bảo hành NCC",
      dataIndex: "Warranty",
    },
    {
      title: "Danh Mục",
      dataIndex: "CategoryName",
    },
    {
      title: "Nhà Sản Xuất",
      dataIndex: "ManufacturerName",
    },
    {
      title: "Nhà Cung Cấp",
      dataIndex: "SupplierName",
    },
    // {
    // 	title: "Tên đăng nhập",
    // 	dataIndex: "userName",
    // 	sorter: true,
    // },
    {
      title: "",
      dataIndex: "action",
      render: (_, values) => (
        <Space>
          {/* <NavLink to={`/quan-ly-nhan-vien/${values.id}`}> */}
          <PrimaryButton
            icon={<BarsOutlined />}
            // onClick={() => onClickDetail(values)}
          ></PrimaryButton>
          {/* </NavLink> */}
          <ButtonDelete />
          {/* <ButtonDelete onClick={() => onClickDelete(values)} /> */}
        </Space>
      ),
    },
  ];

  const onClickDetail = (values) => {
    setOpenDetail(!openDetail);
    setNhanVien(values.id);
  };

  const readGrid = (refresh) => {
    dispatch(actions.refreshGrid(refresh));
  };
  const handleDelete = (values) => {
    deleteNhanVien([values.id]).then((res) => {
      if (res.isSuccess) {
        message.success(DELETE_SUCCESS);
        readGrid(true);
      } else {
        message.success(DELETE_ERROR);
      }
    });
  };

  const onClickOpenModal = (record = {}) => {
    setNhanVien(record.id);
    form.setFieldsValue(record);
    setIsopen(true);
  };

  const onClickDelete = (values) => {
    Modal.confirm({
      title: "Xác Nhận",
      icon: <ExclamationCircleOutlined />,
      content: "Bạn có chắc chắn muốn xóa trường này không?",
      okText: "Xác Nhận",
      cancelText: "Hủy",
      onOk: () => handleDelete(values),
    });
  };

  const onOpenModel = () => {
    onClickOpenModal({});
  };

  return (
    <>
      <HeaderPage
        title="QUẢN LÝ SẢN PHẨM"
        actions="default"
        onCreate={onOpenModel}
      />
      <div className="main__application">
        <PageWrapper>
          <Grid columns={columns} urlEndpoint={GET_ALLPRODUCTS_ENDPOINT} />
        </PageWrapper>
      </div>
      <CreateAndUpdate
        isOpen={isOpen}
        ID={nhanVien}
        reloadTable={() => readGrid(true)}
        form={form}
        onClose={() => setIsopen(false)}
        title={nhanVien ? "Cập nhật dữ liệu" : "Thêm mới dữ liệu"}
      />
    </>
  );
};

QLNhanVien.propTypes = {};

export default QLNhanVien;
