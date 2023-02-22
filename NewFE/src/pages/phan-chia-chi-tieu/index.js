import React, { useEffect } from "react";
import { Row, Col } from "antd";
import { useParams, useLocation } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";

import PageWrapper from "components/Layout/PageWrapper";
import RightSection from "./components/RightSection/RightSection";
import LeftSection from "./components/LeftSection/LeftSection";

import * as selectors from "redux/quyTrinh/selectors";
import * as actions from "redux/quyTrinh/actions";
import "./styles.less";

const PhanChiaChiTieu = (props) => {
	const dispatch = useDispatch();

	const { pathname } = useLocation();
	const { id } = useParams();
	const isDetailPhieuPage = pathname.includes("/ho-so") && id;
	const phieuInfo = useSelector(selectors.selectPhieuTiepNhan());

	useEffect(() => {
		const params = {
			pageIndex: 1,
			pageSize: 100000,
		};
		dispatch(actions.getListLinhVuc(params));
	}, []);

	const getDanhSachMauByPhieuTiepNhanId = () => {
		dispatch(actions.getListMauByPhieuTiepNhanId(id));
	};

	return (
		<>
			<div className="main__application">
				<PageWrapper className="ho-so-wrapper">
					<Row className="content" gutter={0}>
						<Col span={6}>
							<LeftSection
								isDetailPhieuPage={isDetailPhieuPage}
								phieuInfo={phieuInfo}
								getDanhSachMauByPhieuTiepNhanId={
									getDanhSachMauByPhieuTiepNhanId
								}
							/>
						</Col>
						<Col span={18}>
							<RightSection
								phieuTiepNhanId={id}
								reloadList={getDanhSachMauByPhieuTiepNhanId}
								canEdit={true}
							/>
						</Col>
					</Row>
				</PageWrapper>
			</div>
		</>
	);
};

PhanChiaChiTieu.propTypes = {};

export default PhanChiaChiTieu;
