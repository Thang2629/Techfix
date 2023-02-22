import React, { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import * as actions from "redux/global/actions";
import { SEARCH_CRITERIA } from "static/Constants";
import { Button, Space, Modal, Form, Input, InputNumber, message } from "antd";
import { ExclamationCircleOutlined } from "@ant-design/icons";
import Grid from "components/Grid";
import HeaderPage from "pages/home/header-page";
import PageWrapper from "components/Layout/PageWrapper";
import { GET_HOAT_CHAT_ENDPOINT } from "services/specimen-manage/endpoints";
import {
    createHoatChat,
    updateHoatChat,
    deleteHoatChat,
    getHoatChatById,
} from "services/specimen-manage/";
import {
    SAVE_SUCCESS,
    SAVE_ERROR,
    DELETE_SUCCESS,
    DELETE_ERROR,
} from "utils/common/messageContants";
import { ButtonDelete, ButtonEdit } from "common/components/Buttons";

const option = {};
const searchCriteria = SEARCH_CRITERIA.ALL;

const HoatChat = (props) => {
    const dispatch = useDispatch();
    const [isOpen, setIsopen] = useState(false);
    const [tile, setTitle] = useState("Tạo mới hoạt chất");
    const [form] = Form.useForm();

    useEffect(() => {
        dispatch(actions.changeRibbonActions(option));
    }, [dispatch]);

    useEffect(() => {
        dispatch(actions.updateSearchCriteria(searchCriteria));
    }, [dispatch]);

    const handleCancel = () => {
        form.resetFields();
        setIsopen(false);
    };

    const onSave = (values) => {
        if (values.id) {
            handleUpdateHoatChat(values);
        } else {
            handleCreateHoatChat(values);
        }
    };

    const readGrid = (refresh) => {
        dispatch(actions.refreshGrid(refresh));
    };

    const onClickDelete = (values) => {
        Modal.confirm({
            title: "Xác Nhận",
            icon: <ExclamationCircleOutlined />,
            content: "Bạn có chắc chắn muốn xóa hoạt chất này không?",
            okText: "Xác Nhận",
            cancelText: "Hủy",
            onOk: () => handleDeleteHoatChat(values),
        });
    };

    const handleCreateHoatChat = async (values) => {
        var response = await createHoatChat(values);
        if (response.isSuccess) {
            setIsopen(false);
            form.resetFields();
            message.success(SAVE_SUCCESS);
            readGrid(true);
        } else {
            message.error(SAVE_ERROR);
        }
    };

    const handleUpdateHoatChat = async (values) => {
        var response = await updateHoatChat(values);
        if (response.isSuccess) {
            form.resetFields();
            setIsopen(false);
            message.success(SAVE_SUCCESS);
            readGrid(true);
        } else {
            message.error(SAVE_ERROR);
        }
    };

    const handleDeleteHoatChat = async (values) => {
        var response = await deleteHoatChat([values.id]);
        if (response.isSuccess) {
            message.success(DELETE_SUCCESS);
            readGrid(true);
        } else {
            message.error(DELETE_ERROR);
        }
    };

    const onClickOpenModal = (record = {}, title) => {
        setTitle(title);
        form.setFieldsValue(record);
        setIsopen(true);
    };

    const handleUpdateHoatChats = async (record) => {
        var response = await getHoatChatById(record.id);
        if (response.isSuccess) {
            onClickOpenModal(response.data, "Chỉnh sửa hoạt chất");
        }
    };

    const columns = [
        {
            title: "Tên hoạt chất",
            dataIndex: "tenHoatChat",
            sorter: true,
        },
        {
            title: "Loại hoạt chất",
            dataIndex: "loaiHoatChat",
            sorter: true,
        },
        {
            title: "Nhóm hoạt chất",
            dataIndex: "nhomHoatChat",
            sorter: true,
        },
        {
            title: "",
            dataIndex: "action",
            render: (_, value) => (
                <Space>
                    <ButtonEdit onClick={() => handleUpdateHoatChats(value)} />
                    <ButtonDelete onClick={() => onClickDelete(value)} />
                </Space>
            ),
        },
    ];

    const onOpenModel = () => {
        onClickOpenModal({}, "Tạo mới hoạt chất");
    };

    return (
        <>
            <HeaderPage
                title="HOẠT CHẤT"
                actions="default"
                onCreate={onOpenModel}
            />
            <div className="main__application">
                <PageWrapper>
                    {/* <Title level={5} style={{ marginBottom: '16px' }}>hoạt chất</Title> */}
                    <Grid urlEndpoint={GET_HOAT_CHAT_ENDPOINT} columns={columns} />
                </PageWrapper>
            </div>
            <Modal
                title={tile}
                open={isOpen}
                onCancel={handleCancel}
                footer={[
                    <Button form="formHoatChat" key="back" onClick={handleCancel}>
                        Hủy
                    </Button>,
                    <Button
                        form="formHoatChat"
                        key="submit"
                        type="primary"
                        htmlType="submit"
                    >
                        Lưu thông tin
                    </Button>,
                ]}
            >
                <Form
                    id="formHoatChat"
                    labelCol={{ span: 8 }}
                    wrapperCol={{ span: 16 }}
                    onFinish={onSave}
                    form={form}
                >
                    <Form.Item hidden={true} label="id" name="id">
                        <Input />
                    </Form.Item>
                    <Form.Item
                        label="Tên hoạt chất"
                        name="tenHoatChat"
                        rules={[{ required: true, message: "Vui lòng nhập Tên hoạt chất!" }]}
                    >
                        <Input />
                    </Form.Item>
                    <Form.Item
                        label="Loại hoạt chất"
                        name="loaiHoatChat"
                        rules={[{ required: true, message: "Vui lòng nhập Loại hoạt chất!" }]}
                    >
                        <Input />
                    </Form.Item>
                    <Form.Item
                        label="Nhóm hoạt chất"
                        name="nhomHoatChat"
                        rules={[{ required: true, message: "Vui lòng nhập Nhóm hoạt chất!" }]}
                    >
                        <Input />
                    </Form.Item>
                </Form>
            </Modal>
        </>
    );
};

HoatChat.propTypes = {};

export default HoatChat;
