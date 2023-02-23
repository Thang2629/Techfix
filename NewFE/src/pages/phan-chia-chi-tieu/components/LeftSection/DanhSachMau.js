import React from "react";
import { Empty } from "antd";

import MauCard from "./MauCard";
import "./DanhSachPhieu.less";

const DanhSachMau = (props) => {
	const { danhSachMau, doClickAction } = props;

	return (
		<>
			<div className="mau-items">
				{danhSachMau?.length > 0 ? (
					<div>
						{danhSachMau?.map((sample) => {
							return (
								<MauCard
									key={sample?.id}
									sample={sample}
									handleClick={doClickAction}
								/>
							);
						})}
					</div>
				) : (
					<Empty />
				)}
			</div>
		</>
	);
};

DanhSachMau.propTypes = {};

export default DanhSachMau;
