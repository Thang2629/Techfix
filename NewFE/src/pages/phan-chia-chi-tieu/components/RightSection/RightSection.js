import React from "react";
import PropTypes from "prop-types";
import { Button, Dropdown, Menu } from "antd";
import { useSelector } from "react-redux";

import TabsSection from "common/components/TabsSection/TabsSection";
import ThongTinMau from "./ThongTinMau";
import InPhieuTiepNhan from "./InPhieuTiepNhan";
import LichSuHoatDong from "./LichSuHoatDong";
import TopInfo from "../TopInfo/TopInfo";
import {
	InfoCircleOutlined,
	UnorderedListOutlined,
	LoginOutlined,
} from "@ant-design/icons";
import DanhSachChiTieu from "./DanhSachChiTieu";
import * as selectors from "redux/quyTrinh/selectors";

import "./RightSection.less";

function RightSection({ phieuTiepNhanId, reloadList, canEdit }) {
	const mauInfo = useSelector(selectors.selectDetailMau());

	const items = [
		{
			label: (
				<>
					<InfoCircleOutlined /> Thông tin mẫu
				</>
			),
			key: "1",
			children: (
				<ThongTinMau
					phieuTiepNhanId={phieuTiepNhanId}
					reloadList={reloadList}
					canEdit={canEdit}
				/>
			),
		},
		{
			label: (
				<>
					<InfoCircleOutlined /> Danh sách chỉ tiêu
				</>
			),
			key: "2",
			children: <DanhSachChiTieu mauId={mauInfo?.id} />,
			disabled: !mauInfo,
		},
		{
			label: (
				<>
					<InfoCircleOutlined /> In phiếu tiếp nhận mẫu
				</>
			),
			key: "3",
			children: <InPhieuTiepNhan chiTietMauId={mauInfo?.id} />,
			disabled: !mauInfo,
		},
		{
			label: (
				<>
					<InfoCircleOutlined /> Xuất kết quả
				</>
			),
			key: "4",
			children: "Xuat kq",
			disabled: !mauInfo,
		},
		{
			label: (
				<>
					<InfoCircleOutlined /> Lịch sử hoạt động
				</>
			),
			key: "5",
			children: <LichSuHoatDong />,
			disabled: !mauInfo,
		},
	];

	const menuItems = [
		{
			label: "Hành động 1",
			key: "userInfo",
			icon: <LoginOutlined />,
		},
		{
			label: "Hành động 2",
			key: "logout",
			icon: <LoginOutlined />,
		},
	];
	const dropdownMenu = <Menu className="menu-dropdown" items={menuItems} />;

	const operations = (
		<Dropdown overlay={dropdownMenu} trigger={["click"]}>
			<Button type="outlined" icon={<UnorderedListOutlined />} />
		</Dropdown>
	);

	return (
		<div className="right-section">
			<TopInfo />
			<TabsSection
				className="right-section__tabs-chi-tieu"
				items={items}
				tabBarExtraContent={operations}
			/>
		</div>
	);
}

RightSection.propTypes = {
	phieuTiepNhanId: PropTypes.string,
	reloadList: PropTypes.func,
};

export default RightSection;
