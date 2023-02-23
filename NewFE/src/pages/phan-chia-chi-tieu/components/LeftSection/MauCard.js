import React from "react";
import PropTypes from "prop-types";
import { Card, Row, Col, Image, Avatar, Tag } from "antd";
import { formatMMDDYYYY } from "utils/formatDate";
import { ExperimentOutlined } from "@ant-design/icons";
import clsx from "classnames";
import { useSelector } from "react-redux";
import * as selectors from "redux/quyTrinh/selectors";

import "./DanhSachPhieu.less";

const MauCard = ({ sample, handleClick, handleDoubleClick }) => {
	const currentMau = useSelector(selectors.selectDetailMau());

	return (
		<Row
			className={clsx({
				item: true,
				"item-active": currentMau?.id === sample?.id,
			})}
			onClick={(e) => handleClick(sample, e)}
		>
			<Col className="image">
				<ExperimentOutlined style={{ fontSize: 30, color: "#47c0e3" }} />
			</Col>
			<Col flex={1}>
				<Card className="phieu">
					<Row className="">
						<Col span={12} className="title">
							{sample.tenMau}
						</Col>
						<Col span={12} className="name">
							{sample.maSoMau}
						</Col>
					</Row>
					<Row className="description-container">
						<Col span={24}>
							<p>Description</p>
						</Col>
					</Row>
					<Row>
						<Col span={24} className="avatar-group">
							<Avatar.Group>
								<Avatar src="https://joeschmoe.io/api/v1/random" />
								<Avatar style={{ backgroundColor: "#f56a00" }}>K</Avatar>
							</Avatar.Group>
						</Col>
					</Row>
					<Row className="">
						<Col span={8}>
							<Tag className="progress" color="#2db7f5">
								Đang thực hiện{" "}
								<strong>
									{sample.dangThucHien}/{sample.tongSoChiTieu}
								</strong>
							</Tag>
						</Col>
						<Col className="created-date" span={16}>
							{sample?.ngayNhanMau ? formatMMDDYYYY(sample?.ngayNhanMau) : ""}
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
