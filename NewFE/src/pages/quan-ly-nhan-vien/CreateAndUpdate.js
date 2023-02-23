import React, { useEffect, useState } from "react";
import { Button, Modal, Form, Input, message, Select, Row, Col } from "antd";
import {
  createNhanVien,
  getAllPhongBan,
  updateNhanVien,
} from "services/apartment-manage";
import { SAVE_SUCCESS } from "utils/common/messageContants";

const CreateAndUpdate = (props) => {
  const { isOpen, ID, onClose, title, form, reloadTable } = props;

  const [phongBan, setPhongBan] = useState([]);
  const handleCancel = () => {
    form.resetFields();
    onClose();
  };

  useEffect(() => {
    // handlePhongBan();
  }, []);

  const handlePhongBan = async () => {
    const result = await getAllPhongBan();
    if (result.isSuccess) {
      setPhongBan(result.data.phongBanNames);
    }
  };

  const handleReload = () => {
    handleCancel();
    reloadTable();
  };

  const onFinish = (values) => {
    if (ID) {
      handleUpdateNhanVien(values);
    } else {
      handleCreateNhanVien(values);
    }
  };
  const handleCreateNhanVien = async (values) => {
    const result = await createNhanVien(values);
    if (result.isSuccess) {
      handleReload();
      message.success(SAVE_SUCCESS);
    } else {
      handleReload();
      message.error(result.message);
      form.resetFields();
    }
  };

  // const handleGetNhanVienById = useCallback(async () => {
  // 	const result = await getNhanVienById(ID);
  // }, [ID]);

  // useEffect(() => {
  // 	ID && handleGetNhanVienById(); // todo: need check
  // }, [ID, handleGetNhanVienById]);

  const handleUpdateNhanVien = async (values) => {
    const result = await updateNhanVien(values);
    if (result.isSuccess) {
      handleReload();
      message.success(SAVE_SUCCESS);
    } else {
      handleReload();
      form.resetFields();
      message.error(result.message);
    }
  };

  return (
    <>
      <Modal
        title={title}
        open={isOpen}
        form={form}
        width={1000}
        onCancel={handleCancel}
        footer={[
          <Button form="myForm" key="back" onClick={handleCancel}>
            Hủy
          </Button>,
          <Button form="myForm" key="submit" type="primary" htmlType="submit">
            Lưu
          </Button>,
        ]}
      >
        <Form
          id="myForm"
          labelCol={{ span: 8 }}
          form={form}
          wrapperCol={{ span: 16 }}
          onFinish={onFinish}
        >
          {ID && <Form.Item name="id"></Form.Item>}
          <Row>
            <Col span={12}>
              <Form.Item
                label="Mã nhân viên"
                name="maNhanVien"
                rules={[{ required: true, message: "Xin Nhập Mã nhân viên!" }]}
              >
                <Input />
              </Form.Item>
              <Form.Item
                label="Tên nhân viên"
                name="tenNhanVien"
                rules={[{ required: true, message: "Xin Nhập Tên nhân viên!" }]}
              >
                <Input />
              </Form.Item>
              <Form.Item
                label="Địa chỉ"
                name="diaChi"
                // rules={[{ required: true, message: "Xin Nhập Địa chỉ!" }]}
              >
                <Input />
              </Form.Item>
              <Form.Item
                label="Điện thoại"
                name="dienThoai"
                // rules={[{ required: true, message: "Xin Nhập Điện thoại!" }]}
              >
                <Input />
              </Form.Item>
            </Col>
            <Col span={12}>
              <Form.Item
                label="Chức vụ"
                name="chucVu"
                // rules={[{ required: true, message: "Xin Nhập Chức vụ!" }]}
              >
                <Input />
              </Form.Item>
              <Form.Item
                label="Tên Khoa/Phòng/Ban"
                name="khoaPhongBanId"
                // rules={[{ message: "Xin Nhập Khoa/Phòng/Ban!" }]}
              >
                <Select>
                  {phongBan &&
                    phongBan.map((item) => {
                      return (
                        <Select.Option key={item.id} value={item.id}>
                          {item.tenKhoaPhongBan}
                        </Select.Option>
                      );
                    })}
                </Select>
              </Form.Item>

              <Form.Item
                label="Tên người dùng"
                name="userName"
                rules={
                  [
                    // { required: true, message: "Xin Nhập Tên người dùng!" },
                  ]
                }
              >
                <Input />
              </Form.Item>
            </Col>
          </Row>
        </Form>
      </Modal>
    </>
  );
};

CreateAndUpdate.propTypes = {};
export default CreateAndUpdate;
