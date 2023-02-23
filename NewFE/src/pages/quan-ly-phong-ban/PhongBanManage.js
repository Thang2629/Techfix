import { Form, message, Modal, Space } from "antd";
import React, { useState } from "react";
import { ExclamationCircleOutlined } from "@ant-design/icons";
import { DELETE_ERROR, DELETE_SUCCESS } from "utils/common/messageContants";
import { deletePhongBan } from "services/apartment-manage";
import HeaderPage from "pages/home/header-page";
import PageWrapper from "components/Layout/PageWrapper";
import Grid from "components/Grid";
import { GET_PHONG_BAN } from "services/apartment-manage/endpoint";
import CreateAndUpdate from "./CreateAndUpdate";
import { useDispatch } from "react-redux";
import * as actions from "redux/global/actions";
import { ButtonDelete, ButtonEdit } from "common/components/Buttons";

const PhongBanManage = (props) => {
	const dispatch = useDispatch();
	const readGrid = (refresh) => {
		dispatch(actions.refreshGrid(refresh));
	};

	const [isOpen, setIsopen] = useState(false);
	const [maPhongBan, setMaPhongBan] = useState("");
	const [form] = Form.useForm();

	const columns = [
		{
			title: "Mã Khoa/Phòng/Ban",
			dataIndex: "maKhoaPhongBan",
		},
		{
			title: "Tên Khoa/Phòng/Ban",
			dataIndex: "tenKhoaPhongBan",
		},
		{
			title: "Chức năng nhiệm vụ",
			dataIndex: "chucNangNhiemVu",
		},
		{
			title: "Vị trí",
			dataIndex: "viTri",
		},
		{
			title: "Số lượng nhân viên",
			dataIndex: "soLuongNhanVien",
		},
		{
			title: "",
			dataIndex: "action",
			render: (_, values) => (
				<Space>
					<ButtonEdit onClick={() => onClickUpdate(values)} />
					<ButtonDelete onClick={() => onClickDelete(values)} />
				</Space>
			),
		},
	];

	const handleDelete = (values) => {
		deletePhongBan([values.id])
			.then((res) => {
				if (res.isSuccess) {
					message.success(DELETE_SUCCESS);
					readGrid(true);
				} else {
					message.error(DELETE_ERROR);
				}
			})
			.catch((err) => {
				message.error("Xóa thất bại! Vui lòng thử lại");
			});
	};

	const onClickUpdate = (value) => {
		onClickOpenModal(value);
	};

	const onClickOpenModal = (record = {}) => {
		setMaPhongBan(record.id);
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
				title="QUẢN LÝ PHÒNG BAN"
				actions="default"
				onCreate={onOpenModel}
			/>
			<div className="main__application">
				<PageWrapper>
					<Grid columns={columns} urlEndpoint={GET_PHONG_BAN} />
				</PageWrapper>
			</div>
			<CreateAndUpdate
				isOpen={isOpen}
				ID={maPhongBan}
				form={form}
				reload={() => readGrid(true)}
				onClose={() => setIsopen(false)}
				title={maPhongBan ? "Cập nhật dữ liệu" : "Thêm mới dữ liệu"}
			/>
		</>
	);
};

PhongBanManage.propTypes = {};

export default PhongBanManage;
