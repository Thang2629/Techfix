import {
	message,
	Modal,
	Space,
} from "antd";
import PageWrapper from "components/Layout/PageWrapper";
import HeaderPage from "pages/home/header-page";
import React, { useCallback, useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import * as actions from "redux/global/actions";
import { SEARCH_CRITERIA } from "static/Constants";
import { ExclamationCircleOutlined } from "@ant-design/icons";
import Grid from "components/Grid";
import { GET_CUSTOMERS_ENDPOINT } from "services/customer-manager/endpoint";
import {
	deleteCustomerService,
} from "services/customer-manager/";
import { ButtonDelete } from "common/components/Buttons";
import ButtonDetail from "common/components/Buttons/ButtonDetail";
import CreateCustomer from "./CreateCustomer";
import { DELETE_ERROR, DELETE_SUCCESS } from "utils/common/messageContants";
const option = {};
const searchCriteria = SEARCH_CRITERIA.ALL;

const CustomerManager = (props) => {
	const dispatch = useDispatch();
	const [isOpen, setIsopen] = useState(false);
	const [idKhachHang, setIdKhachHang] = useState("");
	useEffect(() => {
		dispatch(actions.changeRibbonActions(option));
		dispatch(actions.updateSearchCriteria(searchCriteria));
	}, [dispatch]);

	const readGrid = (refresh) => {
		dispatch(actions.refreshGrid(refresh));
	};

	const columns = [
		{
			title: "Mã số thuế",
			dataIndex: "maSoThue",
		},
		{
			title: "Mã khách hàng",
			dataIndex: "maKhachHang",
		},
		{
			title: "Tên khách hàng",
			dataIndex: "tenKhachHang",
		},
		{
			title: "Nhóm khách hàng",
			dataIndex: "tenNhomKhachHang",
		},
		{
			title: "Số điện thoại",
			dataIndex: "dienThoai",
		},
		{
			title: "Địa chỉ đăng ký giấy phép ",
			dataIndex: "diaChiDangKyGiayPhep",
		},
		{
			title: "Địa chỉ kinh doanh",
			dataIndex: "diaChiKinhDoanh",
		},
		{
			title: "Ghi chú",
			dataIndex: "ghiChu",
		},
		{
			title: "",
			dataIndex: "action",
			render: (_, values) => (
				<Space>
					{/* <ButtonEdit onClick={() => onClickOpenModal(values)} /> */}
					<ButtonDelete onClick={() => onClickDelete(values)} />
					<ButtonDetail url="khach-hang" record={values} />
				</Space>
			),
		},
	];



	const handleDeleteCustomerGroup = (values) => {
		deleteCustomerService([values.id]).then(res => {
			if (res.isSuccess) {
				message.success(DELETE_SUCCESS);
				readGrid(true);
			} else {
				message.success(DELETE_ERROR);
			}
		});
	};


	const onClickOpenModal = useCallback((record = {}) => {
		setIdKhachHang(record.id)
		setIsopen(true);
	}, []);

	const onClickDelete = (values) => {
		Modal.confirm({
			title: "Xác Nhận",
			icon: <ExclamationCircleOutlined />,
			content: "Bạn có chắc chắn muốn xóa nhóm khách hàng này không?",
			okText: "Xác Nhận",
			cancelText: "Hủy",
			onOk: () => handleDeleteCustomerGroup(values),
		});
	};

	return (
		<>
			<HeaderPage
				title="KHÁCH HÀNG"
				actions="default"
				onCreate={onClickOpenModal}
			/>
			<div className="main__application">
				<PageWrapper>
					<Grid urlEndpoint={GET_CUSTOMERS_ENDPOINT} columns={columns} />
				</PageWrapper>
			</div>
			<CreateCustomer
				isOpen={isOpen}
				handleClosed={() => setIsopen(false)}
				title={"Thông tin"}
				reloadTable={() => readGrid(true)}
				idKhachHang={idKhachHang}
			/>
		</>
	);
};

CustomerManager.propTypes = {};

export default CustomerManager;
