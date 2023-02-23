import React, { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import * as actions from "redux/global/actions";
import { SEARCH_CRITERIA } from "static/Constants";
import { Button, Space, Modal, Form, Input, message } from "antd";
import { ExclamationCircleOutlined } from "@ant-design/icons";
import Grid from "components/Grid";
import HeaderPage from "pages/home/header-page";
import PageWrapper from "components/Layout/PageWrapper";
import { GET_DANGBAOCHE_ENDPOINT } from "services/specimen-manage/endpoints";
import {
	createDangBaoChe,
	updateDangBaoChe,
	deleteDangBaoChe,
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

const DangBaoChe = (props) => {
	const dispatch = useDispatch();
	const [isOpen, setIsopen] = useState(false);
	const [tile, setTitle] = useState("Tạo mới dạng bào chế");
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
			handleUpdateDangBaoChe(values);
		} else {
			handleCreateDangBaoChe(values);
		}
	};

	const onClickDelete = (values) => {
		Modal.confirm({
			title: "Xác Nhận",
			icon: <ExclamationCircleOutlined />,
			content: "Bạn có chắc chắn muốn xóa nhóm lĩnh vực này không?",
			okText: "Xác Nhận",
			cancelText: "Hủy",
			onOk: () => handleDeleteDangBaoChe(values),
		});
	};

	const handleCreateDangBaoChe = async (values) => {
		var response = await createDangBaoChe(values);
		if (response.isSuccess) {
			setIsopen(false);
			form.resetFields();
			message.success(SAVE_SUCCESS);
			readGrid(true);
		} else {
			message.error(SAVE_ERROR);
		}
	};

	const handleUpdateDangBaoChe = async (values) => {
		var response = await updateDangBaoChe(values);
		if (response.isSuccess) {
			form.resetFields();
			setIsopen(false);
			message.success(SAVE_SUCCESS);
			readGrid(true);
		} else {
			message.error(SAVE_ERROR);
		}
	};

	const handleDeleteDangBaoChe = async (values) => {
		var response = await deleteDangBaoChe([values.id]);
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

	const onClickUpdateDangBaoChe = (record) => {
		onClickOpenModal(record, "Chỉnh sửa dạng bào chế");
	};
	const columns = [
		{
			title: "Dạng bào chế",
			dataIndex: "tenLoaiKiemNghiem",
			sorter: true,
		},
		{
			title: "",
			dataIndex: "action",
			render: (_, value) => (
				<Space>
					<ButtonEdit onClick={() => onClickUpdateDangBaoChe(value)} />
					<ButtonDelete onClick={() => onClickDelete(value)} />
				</Space>
			),
		},
	];

	const onOpenModel = () => {
		onClickOpenModal({}, "Tạo mới dạng bào chế");
	};

	const readGrid = (refresh) => {
		dispatch(actions.refreshGrid(refresh));
	};

	return (
		<>
			<HeaderPage
				title="DẠNG BÀO CHẾ"
				actions="default"
				onCreate={onOpenModel}
			/>
			<div className="main__application">
				<PageWrapper>
					<Grid urlEndpoint={GET_DANGBAOCHE_ENDPOINT} columns={columns} />
				</PageWrapper>
			</div>
			<Modal
				title={tile}
				open={isOpen}
				onCancel={handleCancel}
				footer={[
					<Button form="formDangBaoChe" key="back" onClick={handleCancel}>
						Hủy
					</Button>,
					<Button
						form="formDangBaoChe"
						key="submit"
						type="primary"
						htmlType="submit"
					>
						Lưu thông tin
					</Button>,
				]}
			>
				<Form
					id="formDangBaoChe"
					labelCol={{ span: 6 }}
					wrapperCol={{ span: 16 }}
					onFinish={onSave}
					form={form}
				>
					<Form.Item hidden={true} label="id" name="id">
						<Input />
					</Form.Item>

					<Form.Item
						label="Dạng bào chế"
						name="tenLoaiKiemNghiem"
						rules={[{ required: true, message: "Vui lòng nhập Dạng bào chế!" }]}
					>
						<Input />
					</Form.Item>
				</Form>
			</Modal>
		</>
	);
};

DangBaoChe.propTypes = {};

export default DangBaoChe;
