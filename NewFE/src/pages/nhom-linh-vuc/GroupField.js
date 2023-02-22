import React, { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import * as actions from "redux/global/actions";
import { SEARCH_CRITERIA } from "static/Constants";
import { Button, Space, Modal, Form, Input, InputNumber, message } from "antd";
import { ExclamationCircleOutlined } from "@ant-design/icons";
import Grid from "components/Grid";
import HeaderPage from "pages/home/header-page";
import PageWrapper from "components/Layout/PageWrapper";
import { GET_FIELD_GROUPS_ENDPOINT } from "services/specimen-manage/endpoints";
import {
	createFieldGroups,
	updateFieldGroups,
	deleteFieldGroups,
	getFieldGroup,
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

const GroupField = (props) => {
	const dispatch = useDispatch();
	const [isOpen, setIsopen] = useState(false);
	const [tile, setTitle] = useState("Tạo mới nhóm lĩnh vực");
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
			handleUpdateFieldGroup(values);
		} else {
			handleCreateFieldGroup(values);
		}
	};

	const readGrid = (refresh) => {
		dispatch(actions.refreshGrid(refresh));
	};

	const onClickDelete = (values) => {
		Modal.confirm({
			title: "Xác Nhận",
			icon: <ExclamationCircleOutlined />,
			content: "Bạn có chắc chắn muốn xóa nhóm lĩnh vực này không?",
			okText: "Xác Nhận",
			cancelText: "Hủy",
			onOk: () => handleDeleteFieldGroup(values),
		});
	};

	const handleCreateFieldGroup = async (values) => {
		var response = await createFieldGroups(values);
		if (response.isSuccess) {
			setIsopen(false);
			form.resetFields();
			message.success(SAVE_SUCCESS);
			readGrid(true);
		} else {
			message.error(SAVE_ERROR);
		}
	};

	const handleUpdateFieldGroup = async (values) => {
		var response = await updateFieldGroups(values);
		if (response.isSuccess) {
			form.resetFields();
			setIsopen(false);
			message.success(SAVE_SUCCESS);
			readGrid(true);
		} else {
			message.error(SAVE_ERROR);
		}
	};

	const handleDeleteFieldGroup = async (values) => {
		var response = await deleteFieldGroups([values.id]);
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

	const handleUpdateFieldGroups = async (record) => {
		var response = await getFieldGroup(record.id);
		if (response.isSuccess) {
			onClickOpenModal(response.data, "Chỉnh sửa nhóm lĩnh vực");
		}
	};

	const columns = [
		{
			title: "Tên nhóm",
			dataIndex: "tenNhom",
			sorter: true,
		},
		{
			title: "Người phụ trách",
			dataIndex: "nguoiPhuTrach",
			sorter: true,
		},
		{
			title: "Số hiệu VILAS",
			dataIndex: "soHieuVILAS",
			sorter: true,
		},
		{
			title: "",
			dataIndex: "action",
			render: (_, value) => (
				<Space>
					<ButtonEdit onClick={() => handleUpdateFieldGroups(value)} />
					<ButtonDelete onClick={() => onClickDelete(value)} />
				</Space>
			),
		},
	];

	const onOpenModel = () => {
		onClickOpenModal({}, "Tạo mới nhóm lĩnh vực");
	};

	return (
		<>
			<HeaderPage
				title="NHÓM LĨNH VỰC"
				actions="default"
				onCreate={onOpenModel}
			/>
			<div className="main__application">
				<PageWrapper>
					{/* <Title level={5} style={{ marginBottom: '16px' }}>Nhóm lĩnh vực</Title> */}
					<Grid urlEndpoint={GET_FIELD_GROUPS_ENDPOINT} columns={columns} />
				</PageWrapper>
			</div>
			<Modal
				title={tile}
				open={isOpen}
				onCancel={handleCancel}
				footer={[
					<Button form="formNhomLinhVuc" key="back" onClick={handleCancel}>
						Hủy
					</Button>,
					<Button
						form="formNhomLinhVuc"
						key="submit"
						type="primary"
						htmlType="submit"
					>
						Lưu thông tin
					</Button>,
				]}
			>
				<Form
					id="formNhomLinhVuc"
					labelCol={{ span: 8 }}
					wrapperCol={{ span: 16 }}
					onFinish={onSave}
					form={form}
				>
					<Form.Item hidden={true} label="id" name="id">
						<Input />
					</Form.Item>
					<Form.Item
						label="Tên nhóm"
						name="tenNhom"
						rules={[{ required: true, message: "Vui lòng nhập Tên nhóm!" }]}
					>
						<Input />
					</Form.Item>
					<Form.Item label="Người phụ trách" name="nguoiPhuTrach">
						<Input />
					</Form.Item>
					<Form.Item label="Số hiệu VILAS" name="soHieuVILAS">
						<Input />
					</Form.Item>
				</Form>
			</Modal>
		</>
	);
};

GroupField.propTypes = {};

export default GroupField;
