import React from "react";
import PropTypes from "prop-types";
import { CaretRightOutlined } from "@ant-design/icons";
import { Collapse, Row, Typography, Col, Space, Button } from "antd";
import ThongTinPhieu from "pages/tiep-nhan-ho-so/components/RightSection/ThongTinPhieu";
import { ExperimentOutlined, DashOutlined } from "@ant-design/icons";
import { useSelector } from "react-redux";
import * as selectors from "redux/quyTrinh/selectors";

import "./TopInfo.less";

const { Panel } = Collapse;
const { Text } = Typography;

const TopInfo = (props) => {
	return (
		<div className="top-info">
			<Collapse
				expandIcon={({ isActive }) => (
					<CaretRightOutlined rotate={isActive ? 90 : 0} />
				)}
				className="site-collapse-custom-collapse"
				bordered={false}
				ghost={false}
			>
				<Panel
					header={<HeaderTop />}
					key="1"
					className="site-collapse-custom-panel"
				>
					<ThongTinPhieu canEdit={false} />
				</Panel>
			</Collapse>
		</div>
	);
};

TopInfo.propTypes = {};

export default TopInfo;

const HeaderTop = (props) => {
	const phieuInfo = useSelector(selectors.selectPhieuTiepNhan());

	return (
		<Row justify="space-between" style={{ fontWeight: 500 }}>
			<Col>
				<Text strong> {phieuInfo?.tblKhachHangId}</Text> _{" "}
				<Text>Mã số phiếu: {phieuInfo?.soPhieu}</Text>
			</Col>
			<Col>
				<Space>
					<Text>
						<ExperimentOutlined style={{ fontSize: 16 }} />{" "}
						{phieuInfo?.soChiTieu}{" "}
					</Text>{" "}
					<Button
						icon={<DashOutlined />}
						onClick={(e) => e.stopPropagation()}
					></Button>{" "}
				</Space>
			</Col>
		</Row>
	);
};
