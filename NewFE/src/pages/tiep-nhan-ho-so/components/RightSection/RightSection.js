import React from "react";
import {
	InfoCircleOutlined,
	UnorderedListOutlined,
	LoginOutlined,
} from "@ant-design/icons";
import { useSelector } from "react-redux";
import { Menu, Dropdown, Button } from "antd";
import ThongTinPhieu from "./ThongTinPhieu";
import InPhieuTiepNhan from "./InPhieuTiepNhan";
import LichSuHoatDong from "./LichSuHoatDong";
import TabsSection from "common/components/TabsSection/TabsSection";
import * as selectors from "redux/quyTrinh/selectors";
import { useHistory } from "react-router-dom";
function RightSection(props) {
	const history = useHistory();
	const phieuInfo = useSelector(selectors.selectPhieuTiepNhan());

	const goToListMau = () => {
		history.push(`/ho-so/${phieuInfo.id}`);
	};

	const items = [
		{
			label: (
				<>
					<InfoCircleOutlined /> Thông tin phiếu
				</>
			),
			key: "1",
			children: <ThongTinPhieu canEdit={true} />,
		},
		{
			label: (
				<>
					<InfoCircleOutlined /> In thông tin tiếp nhận
				</>
			),
			key: "2",
			children: <InPhieuTiepNhan phieuTiepNhanId={phieuInfo?.id} />,
			disabled: false,
		},
		{
			label: (
				<>
					<InfoCircleOutlined /> Lịch sử hoạt động
				</>
			),
			key: "3",
			children: <LichSuHoatDong />,
			disabled: false,
		},
	];

	const menuItems = [
		{
			label: "Danh sách mẫu",
			key: "listMau",
			icon: <LoginOutlined />,
		},
	];

	const handleClickMenu = (item) => {
		switch (item.key) {
			case "listMau":
				goToListMau();
				break;

			default:
				break;
		}
	};

	const dropdownMenu = (
		<Menu
			className="menu-dropdown"
			items={menuItems}
			onClick={handleClickMenu}
		/>
	);

	const operations = (
		<Dropdown overlay={dropdownMenu} trigger={["click"]} disabled={!phieuInfo}>
			<Button type="outlined" icon={<UnorderedListOutlined />} />
		</Dropdown>
	);

	return (
		<>
			{phieuInfo ? (
				<TabsSection items={items} tabBarExtraContent={operations} />
			) : (
				<div>Chọn để xem thông tin phiếu tại đây</div>
			)}
		</>
	);
}

RightSection.propTypes = {};

export default RightSection;
