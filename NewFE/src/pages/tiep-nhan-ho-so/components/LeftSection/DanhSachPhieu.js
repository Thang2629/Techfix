import React from "react";
import PhieuCard from "./PhieuCard";

import "./DanhSachPhieu.less";

const DanhSachPhieu = ({ listPhieu, doClickAction, doDoubleClickAction }) => {
	return (
		<div className="card-items">
			{listPhieu?.length > 0 &&
				listPhieu?.map((phieuInfo) => {
					return (
						<PhieuCard
							key={phieuInfo.id}
							phieuInfo={phieuInfo}
							handleClick={doClickAction}
							handleDoubleClick={doDoubleClickAction}
						/>
					);
				})}
		</div>
	);
};

DanhSachPhieu.propTypes = {};

export default DanhSachPhieu;
