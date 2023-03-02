import React, { useEffect, useState, useRef } from "react";

import { useDispatch } from "react-redux";
import { PlusCircleOutlined } from "@ant-design/icons";
import {
  Button,
  Input,
  Row,
  Form,
  message,
  Modal,
  Space,
  Select,
  Spin,
} from "antd";
import { ExclamationCircleOutlined, BarsOutlined } from "@ant-design/icons";
import PageWrapper from "components/Layout/PageWrapper";
import HeaderPage from "pages/home/header-page";
import { useHistory } from "react-router-dom";
import Grid from "components/Grid";

import * as actions from "redux/global/actions";
import { SEARCH_CRITERIA } from "static/Constants";
import { deleteNhanVien } from "services/apartment-manage";
import { DELETE_ERROR, DELETE_SUCCESS } from "utils/common/messageContants";
import { getListManufacturers } from "services/Manufacturers";
import { getListCategories } from "services/Categories";
import { ButtonDelete, PrimaryButton } from "common/components/Buttons";
import { Link } from "react-router-dom";
import {
  PRODUCTS_GRID_ENDPOINT,
  getProducts,
  deleteProduct,
} from "services/Products";
import pickBy from "lodash/pickBy";
import identity from "lodash/identity";
import { fowardTo } from "utils/common/route";
const option = {};
const searchCriteria = SEARCH_CRITERIA.ALL;
const { Search } = Input;

const ProductManagement = (props) => {
  const dispatch = useDispatch();
  const history = useHistory();
  const [isOpen, setIsopen] = useState(false);
  const [openDetail, setOpenDetail] = useState(false);
  const [nhanVien, setNhanVien] = useState("");
  const [isLoading, setIsLoading] = useState(false);
  const [categories, setCategories] = useState([]);
  const [manufacturers, setManufacturers] = useState([]);

  const [filterData, setFilterData] = useState([]);
  const [form] = Form.useForm();
  const refBtn = useRef();

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
    const response = await getListCategories();
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
          <Link to={`/san-pham/${values.Id}`}>
            <PrimaryButton
              icon={<BarsOutlined />}
              onClick={() => onClickDetail(values)}
            ></PrimaryButton>
          </Link>
          <ButtonDelete onClick={() => onClickDelete(values)} />
        </Space>
      ),
    },
  ];

  const onClickDetail = (values) => {
    setOpenDetail(!openDetail);
    setNhanVien(values.Id);
    // history.push(`/san-pham/${values.Id}`);
  };

  const readGrid = (refresh) => {
    dispatch(actions.refreshGrid(refresh));
  };
  const handleDelete = async (values) => {
    const response = await deleteProduct(values.Id);
    if (response.Success) {
      message.success(DELETE_SUCCESS);
      readGrid(true);
    } else {
      message.success(response.Message);
    }
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
      content: "Bạn có chắc chắn muốn xóa sản phẩm này không?",
      okText: "Xác Nhận",
      cancelText: "Hủy",
      onOk: () => handleDelete(values),
    });
  };

  const onOpenModel = () => {
    onClickOpenModal({});
  };
  const onSave = async (values) => {
    const filterParams = [];
    const formatFilter = pickBy(values, identity);
    if (formatFilter.hasOwnProperty("Discontinue")) {
      switch (values.Discontinue) {
        case "onsale":
          filterParams.push({
            PropertyName: "IsDeleted",
            Comparison: "==",
            Value: "false",
          });
          filterParams.push({
            PropertyName: "Discontinue",
            Comparison: "==",
            Value: "false",
          });
          break;
        case "onstop":
          filterParams.push({
            PropertyName: "IsDeleted",
            Comparison: "==",
            Value: "false",
          });
          filterParams.push({
            PropertyName: "Discontinue",
            Comparison: "==",
            Value: "true",
          });
          break;
        default:
          filterParams.push({
            PropertyName: "IsDeleted",
            Comparison: "==",
            Value: "true",
          });
          break;
      }
    }
    Object.keys(formatFilter).forEach((key) => {
      if (key !== "Discontinue")
        filterParams.push({
          PropertyName: key,
          Comparison: key === "SearchData" ? "Contains" : "==",
          Value: formatFilter[key],
        });
    });

    const temp = { FilterParams: filterParams, PageNumber: 1, PageSize: 10 };

    setIsLoading(true);
    const response = await getProducts(temp);
    setFilterData(response.Data);
    setIsLoading(false);
  };

  const renderToolbar = () => {
    return (
      <Row
        style={{
          display: "flex",
          flexWrap: "nowrap",
          gap: "1rem",
          justifyContent: "end",
        }}
      >
        <Form
          style={{
            display: "flex",
            gap: "1rem",
          }}
          id="queryForm"
          onFinish={onSave}
          layout="vertical"
        >
          <Form.Item
            initialvalues=""
            name="Discontinue"
            style={{ marginBottom: 0 }}
          >
            <Select placeholder="Chọn Trạng Thái" style={{ width: "10rem" }}>
              {states?.map((item, idx) => {
                return (
                  <Select.Option key={item.value} value={item.value}>
                    {item.label}
                  </Select.Option>
                );
              })}
            </Select>
          </Form.Item>
          <Form.Item
            initialvalues=""
            name="CategoryId"
            style={{ marginBottom: 0 }}
          >
            <Select placeholder="Chọn Danh Mục" style={{ width: "10rem" }}>
              {categories?.map((item, idx) => {
                return (
                  <Select.Option key={item.Id} value={item.Id}>
                    {item.Name}
                  </Select.Option>
                );
              })}
            </Select>
          </Form.Item>
          <Form.Item
            initialvalues=""
            name="ManufacturerId"
            style={{ marginBottom: 0 }}
          >
            <Select placeholder="Search to Select" style={{ width: "10rem" }}>
              {manufacturers?.map((item, idx) => {
                return (
                  <Select.Option key={item.Id} value={item.Id}>
                    {item.Name}
                  </Select.Option>
                );
              })}
            </Select>
          </Form.Item>
          <Form.Item name="SearchData" style={{ marginBottom: 0 }}>
            <Search
              className="header-page__search"
              placeholder="Tìm kiếm..."
              onSearch={() => onSearch()}
              enterButton
            />
          </Form.Item>
          <Form.Item hidden={true} style={{ marginBottom: 0 }}>
            <Button ref={refBtn} htmlType="submit"></Button>
          </Form.Item>

          <Button
            type="primary"
            onClick={() => onClickAddProduct()}
            icon={<PlusCircleOutlined />}
          >
            Thêm Sản Phẩm Mới
          </Button>
        </Form>
      </Row>
    );
  };
  const onSearch = () => {
    refBtn.current.click();
    return;
  };
  const onClickAddProduct = () => {
    fowardTo("/tao-san-pham");
  };
  return (
    <>
      <Spin spinning={isLoading} tip="Loading...">
        <HeaderPage title="DANH SÁCH SẢN PHẨM">{renderToolbar()}</HeaderPage>
        <div className="main__application">
          <PageWrapper>
            <Grid
              columns={columns}
              urlEndpoint={PRODUCTS_GRID_ENDPOINT}
              data={filterData}
            />
          </PageWrapper>
        </div>
        {/* <DetailAndUpdate
          isOpen={isOpen}
          ID={nhanVien}
          reloadTable={() => readGrid(true)}
          form={form}
          onClose={() => setIsopen(false)}
          title={nhanVien ? "Cập nhật dữ liệu" : "Thêm mới dữ liệu"}
        /> */}
      </Spin>
    </>
  );
};

ProductManagement.propTypes = {};

export default ProductManagement;
