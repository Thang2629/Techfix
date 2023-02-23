import React, { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import * as actions from "redux/global/actions";
import { SEARCH_CRITERIA } from "static/Constants";
import {
	Row,
	Button,
	Space,
	Typography,
	Modal,
	Form,
	Input,
	InputNumber,
	message,
	Select,
} from "antd";
import { ExclamationCircleOutlined } from "@ant-design/icons";
import Grid from "components/Grid";
import HeaderPage from "pages/home/header-page";
import PageWrapper from "components/Layout/PageWrapper";
import { GET_FIELDS_ENDPOINT } from "services/specimen-manage/endpoints";
import {
	getField,
	createFields,
	updateFields,
	deleteFields,
	getFieldGroups,
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

const Field = (props) => {
	const dispatch = useDispatch();
	const [isOpen, setIsopen] = useState(false);
	const [record, setRecord] = useState(undefined);
	const [tile, setTitle] = useState("Tạo mới lĩnh vực");
	const [fieldgroups, setFiledGroups] = useState([]);
	const [form] = Form.useForm();

	useEffect(() => {
		dispatch(actions.changeRibbonActions(option));
	}, [dispatch]);

	useEffect(() => {
		dispatch(actions.updateSearchCriteria(searchCriteria));
	}, [dispatch]);

	useEffect(() => {
		getFieldGroup();
	}, []);

	const handleCancel = () => {
		form.resetFields();
		setIsopen(false);
	};

	const onSave = (values) => {
		if (values.id) {
			handleUpdateFields(values);
		} else {
			handleCreateFields(values);
		}
	};

	const onClickDelete = (values) => {
		Modal.confirm({
			title: "Xác Nhận",
			icon: <ExclamationCircleOutlined />,
			content: "Bạn có chắc chắn muốn xóa lĩnh vực này không?",
			okText: "Xác Nhận",
			cancelText: "Hủy",
			onOk: () => handleDeleteFields(values),
		});
	};

	const getFieldGroup = async () => {
		const params = {
			pageSize: 10000000,
			pageIndex: 1,
		};

		var response = await getFieldGroups(params);
		if (response.isSuccess) {
			setFiledGroups(response.data.data);
		}
	};

	const handleCreateFields = async (values) => {
		var response = await createFields(values);
		if (response.isSuccess) {
			form.resetFields();
			setIsopen(false);
			message.success(SAVE_SUCCESS);
			readGrid(true);
		} else {
			message.error(SAVE_ERROR);
		}
	};

	const handleUpdateFields = async (values) => {
		var response = await updateFields(values);
		if (response.isSuccess) {
			form.resetFields();
			setIsopen(false);
			message.success(SAVE_SUCCESS);
			readGrid(true);
		} else {
			message.error(SAVE_ERROR);
		}
	};

	const handleDeleteFields = async (values) => {
		var response = await deleteFields([values.id]);
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

	const onClickUpdateFields = async (record) => {
		var response = await getField(record.id);
		if (response.data.isSuccess) {
			onClickOpenModal(response.data.data, "Chỉnh sửa lĩnh vực");
		}
	};

	const columns = [
		{
			title: "Nhóm lĩnh vực",
			dataIndex: "tenNhomLinhVuc",
			sorter: true,
		},
		{
			title: "Tên lĩnh vực/Sản phẩm, vật liệu được thử",
			dataIndex: "tenLinhVuc",
			sorter: true,
		},
		{
			title: "Khối lượng mẫu",
			dataIndex: "khoiLuongMau",
			sorter: true,
		},
		{
			title: "Thời gian trả kết quả",
			dataIndex: "thoiGianTraKetQua",
			sorter: true,
		},
		{
			title: "",
			dataIndex: "action",
			render: (_, value) => (
				<Space>
					<ButtonEdit onClick={() => onClickUpdateFields(value)} />
					<ButtonDelete onClick={() => onClickDelete(value)} />
				</Space>
			),
		},
	];

	const onOpenModel = () => {
		onClickOpenModal({}, "Tạo mới lĩnh vực");
	};

	const readGrid = (refresh) => {
		dispatch(actions.refreshGrid(refresh));
	};

	return (
		<>
			<HeaderPage title="LĨNH VỰC" actions="default" onCreate={onOpenModel} />
			<div className="main__application">
				<PageWrapper>
					<Grid urlEndpoint={GET_FIELDS_ENDPOINT} columns={columns} />
				</PageWrapper>
			</div>
			<Modal
				title={tile}
				open={isOpen}
				onCancel={handleCancel}
				footer={[
					<Button form="formLinhVuc" key="back" onClick={handleCancel}>
						Hủy
					</Button>,
					<Button
						form="formLinhVuc"
						key="submit"
						type="primary"
						htmlType="submit"
					>
						Lưu thông tin
					</Button>,
				]}
			>
				<Form id="formLinhVuc" layout="vertical" form={form} onFinish={onSave}>
					<Form.Item hidden={true} label="id" name="id" value={record?.id}>
						<Input />
					</Form.Item>
					<Form.Item
						label="Nhóm lĩnh vực"
						name="tblNhomLinhVucId"
						value={record?.tblNhomLinhVucId}
						rules={[
							{ required: true, message: "Vui lòng nhập Nhóm lĩnh vực!" },
						]}
					>
						<Select>
							{fieldgroups &&
								fieldgroups.map((item, index) => {
									return (
										<Select.Option key={index} value={item.id}>
											{item.tenNhom}
										</Select.Option>
									);
								})}
						</Select>
					</Form.Item>
					<Form.Item
						label="Tên lĩnh vực/ Sản phẩm, vật liệu được thử"
						name="tenLinhVuc"
						value={record?.tenLinhVuc}
						rules={[
							{
								required: true,
								message:
									"Vui lòng nhập Tên lĩnh vực/ Sản phẩm, vật liệu được thử!",
							},
						]}
					>
						<Input />
					</Form.Item>
					<Form.Item
						label="Khối lượng mẫu"
						name="khoiLuongMau"
						value={record?.khoiLuongMau}
					>
						<Input />
					</Form.Item>
					<Form.Item
						label="Thời gian trả mẫu"
						name="thoiGianTraKetQua"
						value={record?.thoiGianTraKetQua}
					>
						<InputNumber
							value={record?.thoiGianTraKetQua}
							min={0}
							defaultValue={0}
						/>
					</Form.Item>
				</Form>
			</Modal>
		</>
	);
};

Field.propTypes = {};

export default Field;
