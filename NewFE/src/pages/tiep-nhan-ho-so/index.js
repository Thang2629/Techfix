import React from "react";
import { Row, Col, Button } from "antd";
import { useHistory } from "react-router-dom";
import { PlusCircleOutlined } from "@ant-design/icons";

import PageWrapper from "components/Layout/PageWrapper";
import HeaderPage from "pages/home/header-page";
import RightSection from "./components/RightSection/RightSection";
import LeftSection from "./components/LeftSection/LeftSection";
import "./styles.less";

const TiepNhanHoSo = (props) => {
	const history = useHistory();

	const handleAddNewPhieu = () => {
		history.push(`/tiep-nhan-ho-so/new`);
	};

	const btnAdd = () => {
		return (
			<Row
				style={{
					display: "flex",
					flexWrap: "nowrap",
					gap: "16px",
					justifyContent: "end",
				}}
			>
				<Button
					onClick={() => handleAddNewPhieu()}
					type="primary"
					icon={<PlusCircleOutlined />}
				>
					Tạo phiếu
				</Button>
			</Row>
		);
	};

	return (
		<>
			<HeaderPage title="TIẾP NHẬN HỒ SƠ" actions={btnAdd} />
			<div className="main__application">
				<PageWrapper className="ho-so-wrapper">
					<Row className="content" gutter={8}>
						<Col span={6}>
							<LeftSection />
						</Col>
						<Col span={18}>
							<RightSection />
						</Col>
					</Row>
				</PageWrapper>
			</div>
		</>
	);
};

TiepNhanHoSo.propTypes = {};

export default TiepNhanHoSo;
