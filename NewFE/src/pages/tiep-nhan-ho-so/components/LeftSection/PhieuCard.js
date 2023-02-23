import React, { useRef } from "react";
import PropTypes from "prop-types";
import { Card, Row, Col } from "antd";
import useDoubleClick from "hooks/useDoubleClick";
import { formatMMDDYYYY } from "utils/formatDate";
import { ContactsFilled } from "@ant-design/icons";
import { useSelector } from "react-redux";
import clsx from "classnames";
import * as selectors from "redux/quyTrinh/selectors";

import "./DanhSachPhieu.less";

const MauCard = ({ phieuInfo, handleClick, handleDoubleClick }) => {
	const ref = useRef();
	const curremtPhieu = useSelector(selectors.selectPhieuTiepNhan());

	useDoubleClick({
		onSingleClick: (e) => {
			handleClick && handleClick(phieuInfo, e);
		},
		onDoubleClick: (e) => {
			handleDoubleClick && handleDoubleClick(phieuInfo, e);
		},
		ref: ref,
	});

	return (
		<Row
			ref={ref}
			className={clsx({
				item: true,
				"item-active": phieuInfo?.id === curremtPhieu?.id,
			})}
		>
			<Col className="image">
				<ContactsFilled style={{ fontSize: 30, color: "#47c0e3" }} />
			</Col>
			<Col flex={1}>
				<Card className="phieu">
					<Row className="">
						<Col span={15} className="title">
							{phieuInfo?.tenKhachHang}
						</Col>
						<Col span={9} className="name">
							{phieuInfo?.soPhieu}
						</Col>
					</Row>
					<Row>
						<Col span={18} className="avatar-group">
							Người lấy: {phieuInfo?.nguoiLayMau}
						</Col>
						<Col className="created-date" span={6}>
							{formatMMDDYYYY(phieuInfo?.ngayLapPhieu)}
						</Col>
					</Row>
				</Card>
			</Col>
		</Row>
	);
};

MauCard.propTypes = {
	sample: PropTypes.object,
	handleClick: PropTypes.func,
	handleDoubleClick: PropTypes.func,
};

export default MauCard;
