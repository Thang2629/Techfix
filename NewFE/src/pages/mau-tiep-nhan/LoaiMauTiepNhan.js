import React, { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import * as actions from "redux/global/actions";
import { SEARCH_CRITERIA } from "static/Constants";
import { Button, Space, Typography, Modal, Form, Input, message } from "antd";
import { ExclamationCircleOutlined } from "@ant-design/icons";
import Grid from "components/Grid";
import HeaderPage from "pages/home/header-page";
import PageWrapper from "components/Layout/PageWrapper";
import { GET_LOAIMAUTIEPNHANS_ENDPOINT } from "services/specimen-manage/endpoints";
import {
	getLoaiMauTiepNhan,
	createLoaiMauTiepNhan,
	updateLoaiMauTiepNhan,
	deleteLoaiMauTiepNhan,
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

const LoaiMauTiepNhan = (props) => {
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
			handleUpdateLoaiMauTiepNhan(values);
		} else {
			handleCreateLoaiMauTiepNhan(values);
		}
	};

	const onClickDelete = (values) => {
		Modal.confirm({
			title: "Xác Nhận",
			icon: <ExclamationCircleOutlined />,
			content: "Bạn có chắc chắn muốn xóa loại mẫu tiếp nhận này không?",
			okText: "Xác Nhận",
			cancelText: "Hủy",
			onOk: () => handleDeleteLoaiMauTiepNhan(values),
		});
	};

	const handleCreateLoaiMauTiepNhan = async (values) => {
		var response = await createLoaiMauTiepNhan(values);
		if (response.isSuccess) {
			setIsopen(false);
			form.resetFields();
			message.success(SAVE_SUCCESS);
			readGrid(true);
		} else {
			message.error(SAVE_ERROR);
		}
	};

	const handleUpdateLoaiMauTiepNhan = async (values) => {
		var response = await updateLoaiMauTiepNhan(values);
		if (response.isSuccess) {
			form.resetFields();
			setIsopen(false);
			message.success(SAVE_SUCCESS);
			readGrid(true);
		} else {
			message.error(SAVE_ERROR);
		}
	};

	const handleDeleteLoaiMauTiepNhan = async (values) => {
		var response = await deleteLoaiMauTiepNhan([values.id]);
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

	const onUpdateLoaiMauTiepNhan = async (record) => {
		var response = await getLoaiMauTiepNhan(record.id);
		if (response.isSuccess) {
			onClickOpenModal(response.data, "Chỉnh sửa loại mẫu tiếp nhận");
		}
	};

	const columns = [
		{
			title: "Tên loại mẫu",
			dataIndex: "tenLoaiMau",
			sorter: true,
		},
		{
			title: "Mã loại mẫu",
			dataIndex: "maLoaiMau",
			sorter: true,
		},
		{
			title: "Loại phiếu",
			dataIndex: "tenLoaiMau",
			sorter: true,
		},
		{
			title: "Mẫu in",
			dataIndex: "mauIn",
			sorter: true,
		},
		{
			title: "",
			dataIndex: "action",
			render: (_, value) => (
				<Space>
					<ButtonEdit onClick={() => onUpdateLoaiMauTiepNhan(value)} />
					<ButtonDelete onClick={() => onClickDelete(value)} />
				</Space>
			),
		},
	];

	const readGrid = (refresh) => {
		dispatch(actions.refreshGrid(refresh));
	};

	const onOpenModel = () => {
		onClickOpenModal({}, "Tạo mới loại mẫu tiếp nhận");
	};

	return (
		<>
			<HeaderPage
				title="LOẠI MẪU TIẾP NHẬN"
				actions="default"
				onCreate={onOpenModel}
			/>
			<div className="main__application">
				<PageWrapper>
					{/* <Title level={5} style={{ marginBottom: '16px' }}>Mẫu tiếp nhận</Title> */}
					<Grid urlEndpoint={GET_LOAIMAUTIEPNHANS_ENDPOINT} columns={columns} />
				</PageWrapper>
			</div>
			<Modal
				title={tile}
				open={isOpen}
				onCancel={handleCancel}
				footer={[
					<Button form="formLoaiMauTiepNhan" key="back" onClick={handleCancel}>
						Hủy
					</Button>,
					<Button
						form="formLoaiMauTiepNhan"
						key="submit"
						type="primary"
						htmlType="submit"
					>
						Lưu thông tin
					</Button>,
				]}
			>
				<Form
					id="formLoaiMauTiepNhan"
					layout="vertical"
					form={form}
					onFinish={onSave}
				>
					<Form.Item hidden={true} label="id" name="id">
						<Input />
					</Form.Item>
					<Form.Item
						label="Mã loại mẫu"
						name="maLoaiMau"
						rules={[{ required: true, message: "Vui lòng nhập Mã loại mẫu!" }]}
					>
						<Input />
					</Form.Item>
					<Form.Item
						label="Tên loại mẫu"
						name="tenLoaiMau"
						rules={[{ required: true, message: "Vui lòng nhập Tên loại mẫu!" }]}
					>
						<Input />
					</Form.Item>

					<Form.Item
						label="Loại phiếu"
						name="loaiPhieu"
						rules={[{ required: true, message: "Vui lòng nhập Loại phiếu!" }]}
					>
						<Input />
					</Form.Item>
					<Form.Item
						label="Mẫu in"
						name="mauIn"
						rules={[{ required: true, message: "Vui lòng nhập Mẫu in!" }]}
					>
						<Input />
					</Form.Item>
				</Form>
			</Modal>
		</>
	);
};

LoaiMauTiepNhan.propTypes = {};

export default LoaiMauTiepNhan;
