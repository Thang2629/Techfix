/* eslint-disable no-unused-vars */
import React from 'react'
import { Button, Modal, Form, Input, message } from "antd";
import { createKhoa, createPhongBan, updateKhoa, updatePhongBan } from 'services/apartment-manage';
import { SAVE_SUCCESS } from 'utils/common/messageContants';


const CreateAndUpdate = (props) => {
    const { isOpen, ID, onClose, reload, title, form } = props;



    const handleCancel = () => {
        form.resetFields();
        onClose();
    };

    const onFinish = (values) => {
        if (ID) {
            handleUpdateKhoa(values);
        } else {
            handleCreateKhoa(values);
        }
    };
    const handleCreateKhoa = async (values) => {
        const result = await createKhoa(values)
        if (result.isSuccess) {
            handleCancel();
            reload();
            message.success(SAVE_SUCCESS)
        } else {
            handleCancel();
            reload();
            form.resetFields();
            message.error(result.message)
        }
    };
    const handleUpdateKhoa = async (values) => {
        const result = await updateKhoa(values);
        if (result.isSuccess) {
            handleCancel();
            reload();
            message.success(SAVE_SUCCESS)
        } else {
            handleCancel();
            reload();
            form.resetFields();
            message.error(result.message)
        }
    };

    return <>
        <Modal
            title={title}
            open={isOpen}
            form={form}
            onCancel={handleCancel}
            footer={[
                <Button form="myForm" key="back" onClick={handleCancel}>Hủy</Button>,
                <Button form="myForm" key="submit" type='primary' htmlType="submit">
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
                {ID && <Form.Item
                    name="id"
                >
                </Form.Item>}
                <Form.Item
                    label="Vị Trí"
                    name="viTri"
                    rules={[{ required: true, message: "Xin Nhập Vị Trí!" }]}
                >
                    <Input />
                </Form.Item>

                <Form.Item
                    label="Mã Khoa"
                    name="maKhoa"
                    rules={[{ required: true, message: "Xin Nhập Mã Khoa!" }]}
                >
                    <Input />
                </Form.Item>

                <Form.Item
                    label="Tên Khoa"
                    name="tenKhoa"
                    rules={[{ required: true, message: "Xin Nhập Tên Khoa!" }]}
                >
                    <Input />
                </Form.Item>
            </Form>
        </Modal>
    </>

}
CreateAndUpdate.propTypes = {};
export default CreateAndUpdate;
