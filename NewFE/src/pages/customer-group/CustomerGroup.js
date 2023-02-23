import React, { useState } from "react";
import { Button, Space, Modal, Form, Input, message } from "antd";

import PageWrapper from "components/Layout/PageWrapper";
import HeaderPage from "pages/home/header-page";
import Grid from "components/Grid";

import { useInjectReducer } from "utils/common/injectedReducers";
import { useInjectSaga } from "utils/common/injectSaga";
import saga from "./controllers/saga";
import reducer from "./controllers/reducer";
import { GET_CUSTOMER_GROUPS_ENDPOINT } from "services/customer/endpoints";
import { ExclamationCircleOutlined } from "@ant-design/icons";
import {
  updateCustomerGroups,
  createCustomerGroups,
  deleteCustomerGroups,
} from "services/customer/";
import {
  DELETE_ERROR,
  DELETE_SUCCESS,
  SAVE_SUCCESS,
} from "utils/common/messageContants";
import { useDispatch } from "react-redux";
import * as actions from "redux/global/actions";
import { ButtonDelete, ButtonEdit } from "common/components/Buttons";

const key = "customerGroups";

const CustomerGroup = (props) => {
  const [isLoading, setIsLoading] = useState(false);
  const dispatch = useDispatch();
  useInjectReducer({ key, reducer });
  useInjectSaga({ key, saga });

  const [isOpen, setIsopen] = useState(false);
  const [form] = Form.useForm();

  const readGrid = (refresh) => {
    dispatch(actions.refreshGrid(refresh));
  };

  const columns = [
    {
      title: "Mã nhóm khách hàng",
      dataIndex: "maNhomKhachHang",
    },
    {
      title: "Tên nhóm khách hàng",
      dataIndex: "tenNhomKhachHang",
    },
    {
      title: "Số lượng khách hàng",
      dataIndex: "soLuongKhachHang",
    },
    {
      title: "",
      dataIndex: "action",
      render: (_, values) => (
        <Space>
          <ButtonEdit onClick={() => onClickOpenModal(values)} />
          <ButtonDelete onClick={() => onClickDelete(values)} />
        </Space>
      ),
    },
  ];

  const handleCancel = () => {
    form.resetFields();
    setIsopen(false);
  };

  const finishAction = () => {
    handleCancel();
    readGrid(true);
  };

  const onFinish = (values) => {
    if (values.id) {
      handleUpdateCustomerGroup(values);
    } else {
      handleCreateCustomerGroup(values);
    }
  };
  const handleCreateCustomerGroup = async (values) => {
    setIsLoading(true);
    const result = await createCustomerGroups(values);
    if (result.data !== null && result.isSuccess) {
      finishAction();
      message.success(SAVE_SUCCESS);
    } else {
      message.error(result.message);
    }
    setIsLoading(false);
  };
  const handleUpdateCustomerGroup = async (values) => {
    setIsLoading(true);
    let res = await updateCustomerGroups(values);
    if (res.isSuccess) {
      finishAction();
      message.success(SAVE_SUCCESS);
    } else {
      message.error(res.message);
    }
    setIsLoading(false);
  };
  const handleDeleteCustomerGroup = (values) => {
    setIsLoading(true);

    deleteCustomerGroups([values.id])
      .then((res) => {
        if (res.isSuccess) {
          message.success(DELETE_SUCCESS);
          readGrid(true);
        } else {
          message.error(DELETE_ERROR);
        }
      })
      .catch(() => {
        message.error(DELETE_ERROR);
      })
      .finally(() => {
        setIsLoading(true);
      });
  };
  const onClickOpenModal = (record = {}) => {
    form.setFieldsValue(record);
    setIsopen(true);
  };
  const onClickDelete = (values) => {
    Modal.confirm({
      title: "Xác Nhận",
      icon: <ExclamationCircleOutlined />,
      content: "Bạn có chắc chắn muốn xóa nhóm khách hàng này không?",
      okText: "Xác Nhận",
      cancelText: "Hủy",
      onOk: () => handleDeleteCustomerGroup(values),
      confirmLoading: isLoading,
    });
  };
  return (
    <>
      <HeaderPage
        title="QUẢN LÝ NHÓM KHÁCH HÀNG"
        actions="default"
        onCreate={onClickOpenModal}
      />
      <div className="main__application">
        <PageWrapper>
          <Grid urlEndpoint={GET_CUSTOMER_GROUPS_ENDPOINT} columns={columns} />
        </PageWrapper>
      </div>
      <Modal
        title="Thông Tin"
        open={isOpen}
        form={form}
        onCancel={handleCancel}
        footer={[
          <Button form="myForm" key="back" onClick={handleCancel}>
            Hủy
          </Button>,
          <Button
            form="myForm"
            key="submit"
            type="primary"
            htmlType="submit"
            loading={isLoading}
          >
            Lưu
          </Button>,
        ]}
      >
        <Form
          id="myForm"
          form={form}
          labelCol={{ span: 24 }}
          wrapperCol={{ span: 24 }}
          onFinish={onFinish}
        >
          <Form.Item hidden={true} label="id" name="id">
            <Input />
          </Form.Item>

          <Form.Item
            label="Mã nhóm khách hàng"
            name="maNhomKhachHang"
            rules={[
              { required: true, message: "Xin Nhập Mã Nhóm Khách Hàng!" },
            ]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            label="Tên nhóm khách hàng"
            name="tenNhomKhachHang"
            rules={[{ required: true, message: "Xin Nhập Nhóm Khách Hàng!" }]}
          >
            <Input />
          </Form.Item>
        </Form>
      </Modal>
    </>
  );
};

CustomerGroup.propTypes = {};

export default CustomerGroup;
