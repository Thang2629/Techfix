import React, { useEffect } from "react";
import { Row, Col } from "antd";
import { useDispatch, useSelector } from "react-redux";

import PageWrapper from "components/Layout/PageWrapper";
import RightSection from "./components/RightSection/RightSection";

import LeftSection from "pages/phan-chia-chi-tieu/components/LeftSection/LeftSection";
import * as actions from "redux/quyTrinh/actions";
import "./styles.less";

const PhanChiaKiemMau = (props) => {
	const dispatch = useDispatch();

	// useEffect(() => {
	// 	dispatch(
	// 		actions.getDanhSachMau({
	// 			pageSize: 100,
	// 			pageIndex: 1,
	// 			TuNgay: "2022-10-15T17:58:14.359",
	// 			DenNgay: "2022-10-22T17:58:14.359",
	// 			searchText: "",
	// 		})
	// 	);
	// }, [dispatch]);

	return (
		<>
			{/* <HeaderPage title="PHÂN CHIA CHỈ TIÊU" actions={false} /> */}
			<div className="main__application">
				<PageWrapper className="ho-so-wrapper">
					<Row className="content">
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

PhanChiaKiemMau.propTypes = {};

export default PhanChiaKiemMau;
