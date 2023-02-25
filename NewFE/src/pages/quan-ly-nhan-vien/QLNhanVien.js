import React, { useEffect, useState } from "react";

import { useDispatch } from "react-redux";
import { Form, message, Modal, Space, Select } from "antd";
import { ExclamationCircleOutlined, BarsOutlined } from "@ant-design/icons";
import PageWrapper from "components/Layout/PageWrapper";
import HeaderPage from "pages/home/header-page";

import Grid from "components/Grid";
import Loading from "components/Loading/Loading";
import * as actions from "redux/global/actions";
import { SEARCH_CRITERIA } from "static/Constants";
import { deleteNhanVien } from "services/apartment-manage";
import { DELETE_ERROR, DELETE_SUCCESS } from "utils/common/messageContants";
import { PRODUCTS_GRID_ENDPOINT } from "services/Products";
import { getListManufacturers } from "services/Manufacturers";
import { getListCatagories } from "services/Categories";
import CreateAndUpdate from "./CreateAndUpdate";
import { ButtonDelete, PrimaryButton } from "common/components/Buttons";
import { Link } from "react-router-dom";

const option = {};
const searchCriteria = SEARCH_CRITERIA.ALL;

const QLNhanVien = (props) => {
  const dispatch = useDispatch();
  const [isOpen, setIsopen] = useState(false);
  const [openDetail, setOpenDetail] = useState(false);
  const [nhanVien, setNhanVien] = useState("");
  const [isLoading, setIsLoading] = useState(false);
  const [categories, setCategories] = useState([]);
  const [manufacturers, setManufacturers] = useState([]);
  const [form] = Form.useForm();

  useEffect(() => {
    dispatch(actions.changeRibbonActions(option));
  }, [dispatch]);

  useEffect(() => {
    dispatch(actions.updateSearchCriteria(searchCriteria));
  }, [dispatch]);

  const getManufacturers = async () => {
    setIsLoading(true);
    const response = await getListManufacturers();
    setManufacturers(response);
    setIsLoading(false);
  };
  const getCatagories = async () => {
    setIsLoading(true);
    const response = await getListCatagories();
    setCategories(response);
    setIsLoading(false);
  };

  useEffect(() => {
    const initialize = () => {
      getManufacturers();
      getCatagories();
    };
    initialize();
  }, []);

  const states = [
    {
      label: "Đang Kinh Doanh",
      value: "onsale",
    },
    {
      label: "Ngừng Kinh Doanh",
      value: "onstop",
    },
    {
      label: "Đã Xóa",
      value: "deleted",
    },
  ];
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
    {
      title: "",
      dataIndex: "action",
      render: (_, values) => (
        <Space>
          <Link to={`/quan-ly-nhan-vien/${values.Id}`}>
            <PrimaryButton
              icon={<BarsOutlined />}
              onClick={() => onClickDetail(values)}
            ></PrimaryButton>
          </Link>
          <ButtonDelete />
          {/* <ButtonDelete onClick={() => onClickDelete(values)} /> */}
        </Space>
      ),
    },
  ];

  const onClickDetail = (values) => {
    debugger;
    setOpenDetail(!openDetail);
    setNhanVien(values.Id);
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

  const renderToolbar = () => {
    return (
      <>
        <Select
          defaultValue="onsale"
          style={{ width: "10rem" }}
          options={states}
        />
        <Select defaultValue="" style={{ width: "10rem" }}>
          {categories?.map((item, idx) => {
            return (
              <Select.Option key={item.Id} value={item.Id}>
                {item.Name}
              </Select.Option>
            );
          })}
        </Select>
        <Select defaultValue="" style={{ width: "10rem" }}>
          {manufacturers?.map((item, idx) => {
            return (
              <Select.Option key={item.Id} value={item.Id}>
                {item.Name}
              </Select.Option>
            );
          })}
        </Select>
      </>
    );
  };

  return (
    <>
      {isLoading && <Loading />}
      <HeaderPage
        title="DANH SÁCH SẢN PHẨM"
        actions={renderToolbar}
        onCreate={onOpenModel}
      />
      <div className="main__application">
        <PageWrapper>
          <Grid columns={columns} urlEndpoint={PRODUCTS_GRID_ENDPOINT} />
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
