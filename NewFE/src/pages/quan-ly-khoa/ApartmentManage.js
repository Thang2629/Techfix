import React, { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import { Button, Space, Typography, Modal, Form, message } from "antd";
import {
	DeleteOutlined,
	EditOutlined,
	ExclamationCircleOutlined,
} from "@ant-design/icons";
import PageWrapper from "components/Layout/PageWrapper";
import HeaderPage from "pages/home/header-page";
import Grid from "components/Grid";
import * as actions from "redux/global/actions";
import { SEARCH_CRITERIA } from "static/Constants";

import { GET_KHOA_ENDPOINT } from "services/apartment-manage/endpoint";
import { deleteKhoa } from "services/apartment-manage";
import CreateAndUpdate from "./crud-nhansu/CreateAndUpdate";
import { DELETE_ERROR, DELETE_SUCCESS } from "utils/common/messageContants";
import "./styles.less";

const { Title } = Typography;

const option = {};
const searchCriteria = SEARCH_CRITERIA.ALL;

const ApartmentManage = (props) => {
	const dispatch = useDispatch();
	useEffect(() => {
		dispatch(actions.changeRibbonActions(option));
	}, [dispatch]);

	useEffect(() => {
		dispatch(actions.updateSearchCriteria(searchCriteria));
	}, [dispatch]);

	const readGrid = (refresh) => {
		dispatch(actions.refreshGrid(refresh));
	};

	const [isOpen, setIsopen] = useState(false);
	const [maKhoa, setMaKhoa] = useState("");
	const [form] = Form.useForm();

	const columns = [
		{
			title: "STT",
			dataIndex: "",
			render: (row, _, index) => {
				return <div>{index + 1}</div>;
			},
			sorter: true,
		},
		{
			title: "Vị trí",
			dataIndex: "viTri",
			sorter: true,
		},
		{
			title: "Tên Khoa",
			dataIndex: "tenKhoa",
		},
		{
			title: "Mã Khoa",
			dataIndex: "maKhoa",
			sorter: true,
		},
		{
			title: "Số lượng phòng ban",
			dataIndex: "soLuongPhongBan",
			sorter: true,
		},
		{
			title: "Số lượng nhân viên",
			dataIndex: "soLuongNhanVien",
			sorter: true,
		},
		{
			title: "",
			dataIndex: "action",
			render: (_, values) => (
				<Space>
					<Button
						onClick={() => onClickUpdate(values)}
						type="default"
						icon={<EditOutlined />}
						size="small"
					>
						Chỉnh sửa
					</Button>
					<Button
						onClick={() => onClickDelete(values)}
						type="danger"
						icon={<DeleteOutlined />}
						size="small"
					>
						Xóa
					</Button>
				</Space>
			),
		},
	];

	const handleDelete = (values) => {
		deleteKhoa([values.id]).then((res) => {
			if (res.isSuccess) {
				message.success(DELETE_SUCCESS);
				readGrid(true);
			} else {
				message.success(DELETE_ERROR);
			}
		});
	};

	const onClickUpdate = (value) => {
		onClickOpenModal(value);
	};

	const onClickOpenModal = (record = {}) => {
		setMaKhoa(record.id);
		form.setFieldsValue(record);
		setIsopen(true);
	};

	const onClickDelete = (values) => {
		Modal.confirm({
			title: "Xác Nhận",
			icon: <ExclamationCircleOutlined />,
			content: "Bạn có chắc chắn muốn xóa trường này không?",
			okText: "Xác Nhận",
			cancelText: "Hủy",
			onOk: () => handleDelete(values),
		});
	};

	const onOpenModel = () => {
		onClickOpenModal({});
	};
	return (
		<>
			<HeaderPage
				title="QUẢN LÝ KHOA"
				actions="default"
				onCreate={onOpenModel}
			/>
			<div className="main__application">
				<PageWrapper>
					<Title level={5} style={{ marginBottom: "16px" }}>
						Danh mục Khoa
					</Title>
					<Grid columns={columns} urlEndpoint={GET_KHOA_ENDPOINT} />
				</PageWrapper>
			</div>
			<CreateAndUpdate
				isOpen={isOpen}
				ID={maKhoa}
				form={form}
				reload={() => readGrid(true)}
				onClose={() => setIsopen(false)}
				title={maKhoa ? "Cập nhật dữ liệu" : "Thêm mới dữ liệu"}
			/>
		</>
	);
};

ApartmentManage.propTypes = {};

export default ApartmentManage;
