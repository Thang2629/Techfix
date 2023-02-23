import React from "react";
import "./InPhieuTiepNhan.less";

const defaultUrl = "https://arxiv.org/pdf/quant-ph/0410100.pdf";

const InPhieuTiepNhan = ({ src = defaultUrl }) => {
	const Iframe = ({ url }) => {
		const iframe = `<iframe style="width: ${"100%"}; height: ${"100%"}" src="${url}" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>`;
		return (
			<div
				style={{
					height: "550px",
					width: "100%",
					display: "flex",
					alignItems: "center",
					justifyContent: "center",
					padding: "20px",
				}}
				dangerouslySetInnerHTML={{ __html: iframe ? iframe : "" }}
			/>
		);
	};

	return (
		<div className="in-phieu-tiep-nhan">
			<Iframe url={defaultUrl} />
		</div>
	);
};

export default InPhieuTiepNhan;
