import React, { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import * as actions from "redux/global/actions";
import { SEARCH_CRITERIA } from "static/Constants";
import {
	Button,
	Space,
	Modal,
	Form,
	Input,
	InputNumber,
	Select,
	message,
} from "antd";
import { ExclamationCircleOutlined } from "@ant-design/icons";
import Grid from "components/Grid";
import HeaderPage from "pages/home/header-page";
import PageWrapper from "components/Layout/PageWrapper";
import { GET_CHITIEUKIEMNGHIEM_ENDPOINT } from "services/specimen-manage/endpoints";
import {
	getChiTieuKiemNghiemId,
	createChiTieuKiemNghiem,
	updateChiTieuKiemNghiem,
	deleteChiTieuKiemNghiem,
	getFields,
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

const ChiTieu = (props) => {
	const dispatch = useDispatch();
	const [isOpen, setIsopen] = useState(false);
	const [tile, setTitle] = useState("Tạo mới Chỉ tiêu kiểm nghiệm");
	const [form] = Form.useForm();
	const [nhomchitieu, setNhomChiTieu] = useState([]);

	useEffect(() => {
		dispatch(actions.changeRibbonActions(option));
	}, [dispatch]);

	useEffect(() => {
		dispatch(actions.updateSearchCriteria(searchCriteria));
	}, [dispatch]);

	useEffect(() => {
		getDataTenSanPhamVatLieu();
	}, []);

	const getDataTenSanPhamVatLieu = async () => {
		const params = {
			pageSize: 10000000,
			pageIndex: 1,
		};

		var response = await getFields(params);
		if (response.isSuccess) {
			setNhomChiTieu(response.data.data);
		}
	};

	const readGrid = (refresh) => {
		dispatch(actions.refreshGrid(refresh));
	};

	const handleCancel = () => {
		form.resetFields();
		setIsopen(false);
	};

	const onSave = (values) => {
		if (values.id) {
			handleUpdateChiTieuKiemNghiem(values);
		} else {
			handleCreateChiTieuKiemNghiem(values);
		}
	};

	const onClickDelete = (values) => {
		Modal.confirm({
			title: "Xác Nhận",
			icon: <ExclamationCircleOutlined />,
			content: "Bạn có chắc chắn muốn xóa chỉ tiêu kiểm nghiệm này không?",
			okText: "Xác Nhận",
			cancelText: "Hủy",
			onOk: () => handleDeleteChiTieuKiemNghiem(values),
		});
	};

	const handleCreateChiTieuKiemNghiem = async (values) => {
		var response = await createChiTieuKiemNghiem(values);
		if (response.isSuccess) {
			setIsopen(false);
			form.resetFields();
			message.success(SAVE_SUCCESS);
			readGrid(true);
		} else {
			message.error(SAVE_ERROR);
		}
	};

	const handleUpdateChiTieuKiemNghiem = async (values) => {
		var response = await updateChiTieuKiemNghiem(values);
		if (response.isSuccess) {
			form.resetFields();
			setIsopen(false);
			message.success(SAVE_SUCCESS);
			readGrid(true);
		} else {
			message.error(SAVE_ERROR);
		}
	};

	const handleDeleteChiTieuKiemNghiem = async (values) => {
		var response = await deleteChiTieuKiemNghiem([values.id]);
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

	const onClickUpdateChiTieuKiemNghiem = async (record) => {
		var response = await getChiTieuKiemNghiemId(record.id);
		if (response.isSuccess) {
			onClickOpenModal(response.data, "Chỉnh sửa chỉ tiêu kiểm nghiệm");
		}
	};

	const columns = [
		{
			title: "Tên sản phẩm/vật liệu",
			dataIndex: "tenLinhVuc",
			sorter: true,
		},
		{
			title: "Giới hạn định lượng",
			dataIndex: "gioiHanDinhLuong",
			sorter: true,
		},
		{
			title: "Phương pháp thử",
			dataIndex: "phuongPhapThu",
			sorter: true,
		},
		{
			title: "Đơn giá",
			dataIndex: "donGia",
			sorter: true,
		},
		{
			title: "Đơn giá gấp",
			dataIndex: "donGiaGap",
			sorter: true,
		},
		{
			title: "Mô tả",
			dataIndex: "moTa",
			sorter: true,
		},
		{
			title: "",
			dataIndex: "action",
			render: (_, value) => (
				<Space>
					<ButtonEdit onClick={() => onClickUpdateChiTieuKiemNghiem(value)} />
					<ButtonDelete onClick={() => onClickDelete(value)} />
				</Space>
			),
		},
	];

	const columns1 = [
		{
			title: "Name",
			dataIndex: "name",
			key: "name",
		},
		{
			title: "Age",
			dataIndex: "age",
			key: "age",
			width: "12%",
		},
		{
			title: "Address",
			dataIndex: "address",
			width: "30%",
			key: "address",
		},
	];

	const onOpenModel = () => {
		onClickOpenModal({}, "Tạo mới chỉ tiêu kiểm nghiệm");
	};

	return (
		<>
			<HeaderPage
				title="CHỈ TIÊU KIỂM NGHIỆM"
				actions="default"
				onCreate={onOpenModel}
			/>
			<div className="main__application">
				<PageWrapper>
					{/* <Title level={5} style={{ marginBottom: '16px' }}>Chỉ tiêu kiểm nghiệm</Title> */}
					<div className="grid">
						{/* <Table
              columns={columns1}
              // rowSelection={{
              //   ...rowSelection,
              //   checkStrictly,
              // }}
              dataSource={data1}
            /> */}
						{/* <Table
            columns={columns}
            dataSource={data1}
            onChange={onChange}
            bordered
            pagination={tableParams.pagination}
            loading={loading}
            rowKey="id"
            {...rest}
          /> */}
					</div>
					<Grid
						urlEndpoint={GET_CHITIEUKIEMNGHIEM_ENDPOINT}
						columns={columns}
					/>
				</PageWrapper>
			</div>
			<Modal
				title={tile}
				open={isOpen}
				onCancel={handleCancel}
				footer={[
					<Button form="formChiTieuKiemNgiem" key="back" onClick={handleCancel}>
						Hủy
					</Button>,
					<Button
						form="formChiTieuKiemNgiem"
						key="submit"
						type="primary"
						htmlType="submit"
					>
						Lưu thông tin
					</Button>,
				]}
			>
				<Form
					id="formChiTieuKiemNgiem"
					onFinish={onSave}
					form={form}
					layout="vertical"
				>
					<Form.Item hidden={true} label="id" name="id">
						<Input />
					</Form.Item>
					<Form.Item
						label="Tên sản phẩm/vật liệu"
						name="linhVucId"
						rules={[
							{
								required: true,
								message: "Vui lòng nhập Tên sản phẩm/vật liệu!",
							},
						]}
					>
						<Select>
							{nhomchitieu &&
								nhomchitieu.map((item) => {
									return (
										<Select.Option key={item.id} value={item.id}>
											{item.tenLinhVuc}
										</Select.Option>
									);
								})}
						</Select>
					</Form.Item>
					<Form.Item
						label="Tên dịch vụ/phép thử"
						name="tenDichVu"
						rules={[
							{
								required: true,
								message: "Vui lòng nhập Tên dịch vụ/phép thử!",
							},
						]}
					>
						<Input />
					</Form.Item>
					<Form.Item
						label="Tên dịch vụ/phép thử rút gọn"
						name="tenDichVuRutGon"
					>
						<Input />
					</Form.Item>
					<Form.Item label="Giới hạn định lượng" name="gioiHanDinhLuong">
						<Input />
					</Form.Item>
					<Form.Item
						label="Đơn giá"
						name="donGia"
						style={{
							display: "inline-block",
							marginRight: "8px",
							width: "calc(50% - 8px)",
						}}
					>
						<InputNumber min={0} style={{ width: "100%" }} defaultValue={0} />
					</Form.Item>
					<Form.Item
						label="Đơn giá gấp"
						name="donGiaGap"
						style={{
							display: "inline-block",
							width: "calc(50% - 8px)",
							marginLeft: "8px",
						}}
					>
						<InputNumber min={0} style={{ width: "100%" }} defaultValue={0} />
					</Form.Item>
					<Form.Item label="Mô tả" name="moTa">
						<TextArea rows={3} />
					</Form.Item>
				</Form>
			</Modal>
		</>
	);
};

ChiTieu.propTypes = {};

export default ChiTieu;
