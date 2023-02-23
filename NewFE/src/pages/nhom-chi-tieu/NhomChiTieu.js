import React, { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import * as actions from "redux/global/actions";
import { SEARCH_CRITERIA } from "static/Constants";
import {
	Row,
	Button,
	Space,
	Modal,
	Form,
	Input,
	message,
	TreeSelect,
} from "antd";
import { ExclamationCircleOutlined } from "@ant-design/icons";
import Grid from "components/Grid";
import HeaderPage from "pages/home/header-page";
import PageWrapper from "components/Layout/PageWrapper";
import { GET_NHOMCHITIEU_ENDPOINT } from "services/specimen-manage/endpoints";
import {
	getNhomChiTieu,
	createNhomChiTieu,
	updateNhomChiTieu,
	deleteNhomChiTieu,
	getChiTieuKiemNghiems,
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
const { TextArea } = Input;
const NhomChiTieu = (props) => {
	const dispatch = useDispatch();
	const [isOpen, setIsopen] = useState(false);
	const [tile, setTitle] = useState("Tạo mới nhóm chỉ tiêu");
	const [chitieukiemnghiem, setChiTieuKiemNghiem] = useState([]);
	const [form] = Form.useForm();

	useEffect(() => {
		dispatch(actions.changeRibbonActions(option));
	}, [dispatch]);

	useEffect(() => {
		dispatch(actions.updateSearchCriteria(searchCriteria));
	}, [dispatch]);

	useEffect(() => {
		getChiTieuKiemNghiem();
	}, []);

	const readGrid = (refresh) => {
		dispatch(actions.refreshGrid(refresh));
	};

	const onClickDelete = (values) => {
		Modal.confirm({
			title: "Xác Nhận",
			icon: <ExclamationCircleOutlined />,
			content: "Bạn có chắc chắn muốn xóa nhóm chỉ tiêu này không?",
			okText: "Xác Nhận",
			cancelText: "Hủy",
			onOk: () => handleDeleteNhomChiTieu(values),
		});
	};

	const handleDeleteNhomChiTieu = async (values) => {
		var response = await deleteNhomChiTieu([values.id]);
		if (response.isSuccess) {
			message.success(DELETE_SUCCESS);
			readGrid(true);
		} else {
			message.error(DELETE_ERROR);
		}
	};

	const getChiTieuKiemNghiem = async () => {
		const params = {
			pageSize: 100000000,
			pageIndex: 1,
		};

		var response = await getChiTieuKiemNghiems(params);
		if (response.isSuccess) {
			buildTreeData(response.data.data);
		}
	};

	const buildTreeData = (data) => {
		let treeData = data.map((item) => {
			return {
				title: item.tenDichVu,
				value: item.id,
				key: item.id,
			};
		});

		setChiTieuKiemNghiem(treeData);
	};

	const onClickOpenModal = (record = {}, title) => {
		setTitle(title);
		form.setFieldsValue(record);
		setIsopen(true);
	};

	const handleCreateNhomChiTieu = async (values) => {
		var response = await createNhomChiTieu(values);
		if (response.isSuccess) {
			setIsopen(false);
			form.resetFields();
			message.success(SAVE_SUCCESS);
			readGrid(true);
		} else {
			message.error(SAVE_ERROR);
		}
	};

	const handleUpdateNhomChiTieu = async (values) => {
		var response = await updateNhomChiTieu(values);
		if (response.isSuccess) {
			setIsopen(false);
			form.resetFields();
			message.success(SAVE_SUCCESS);
			readGrid(true);
		} else {
			message.error(SAVE_ERROR);
		}
	};

	const handleCancel = () => {
		form.resetFields();
		setIsopen(false);
	};

	const onSave = (values) => {
		if (values.id) {
			handleUpdateNhomChiTieu(values);
		} else {
			handleCreateNhomChiTieu(values);
		}
	};

	const onUpdateNhomChiTieu = async (record) => {
		var response = await getNhomChiTieu(record.id);
		if (response.isSuccess) {
			if (response.data && response.data.danhSachChiTieus) {
				var chitieus = response.data.danhSachChiTieus.map((item) => {
					return item.id;
				});

				response.data.danhSachChiTieus = chitieus;
			}
			onClickOpenModal(response.data, "Chỉnh sửa nhóm chỉ tiêu");
		}
	};

	const columns = [
		{
			title: "Tên nhóm",
			dataIndex: "tenNhom",
			sorter: true,
		},
		{
			title: "Số lượng chỉ tiêu",
			dataIndex: "soLuongChiTieu",
			sorter: true,
		},
		{
			title: "Ghi chú",
			dataIndex: "ghiChu",
			sorter: true,
		},
		{
			title: "",
			dataIndex: "action",
			render: (_, value) => (
				<Space>
					<ButtonEdit onClick={() => onUpdateNhomChiTieu(value)} />
					<ButtonDelete onClick={() => onClickDelete(value)} />
				</Space>
			),
		},
	];

	const onOpenModel = () => {
		onClickOpenModal({}, "Tạo mới nhóm chỉ tiêu");
	};

	return (
		<>
			<HeaderPage
				title="NHÓM CHỈ TIÊU"
				actions="default"
				onCreate={onOpenModel}
			/>
			<div className="main__application">
				<PageWrapper>
					{/* <Title level={5} style={{ marginBottom: '16px' }}>Nhóm chỉ tiêu</Title> */}
					<Grid urlEndpoint={GET_NHOMCHITIEU_ENDPOINT} columns={columns} />
				</PageWrapper>
			</div>
			<Modal
				title={tile}
				open={isOpen}
				onCancel={handleCancel}
				footer={[
					<Button form="formNhomChiTieu" key="back" onClick={handleCancel}>
						Hủy
					</Button>,
					<Button
						form="formNhomChiTieu"
						key="submit"
						type="primary"
						htmlType="submit"
					>
						Lưu thông tin
					</Button>,
				]}
			>
				<Form
					id="formNhomChiTieu"
					layout="vertical"
					onFinish={onSave}
					form={form}
				>
					<Form.Item hidden={true} label="id" name="id">
						<Input />
					</Form.Item>
					<Form.Item
						label="Tên nhóm chỉ tiêu"
						name="tenNhom"
						rules={[
							{ required: true, message: "Vui lòng nhập Tên nhóm chỉ tiêu!" },
						]}
					>
						<Input />
					</Form.Item>
					<Form.Item
						label="Chỉ tiêu"
						name="danhSachChiTieus"
						rules={[{ required: true, message: "Vui lòng nhập Chỉ tiêu!" }]}
					>
						<TreeSelect treeCheckable={true} treeData={chitieukiemnghiem} />
					</Form.Item>
					<Form.Item label="Ghi chú" name="ghiChu">
						<TextArea rows={3} />
					</Form.Item>
				</Form>
			</Modal>
		</>
	);
};

NhomChiTieu.propTypes = {};

export default NhomChiTieu;
