import React from "react";
import "./InPhieuTiepNhan.less";

import { useGetFileTiepNhanById } from "pages/tiep-nhan-ho-so/utils";
import Loading from "components/Loading/Loading";
import FileViewer from "react-file-viewer";

const type = "pdf";
const InPhieuTiepNhan = ({ phieuTiepNhanId }) => {
	const { file, isLoading } = useGetFileTiepNhanById(phieuTiepNhanId);

	const onError = (e) => {
		console.log("e ", e);
	};

	const CustomErrorComponent = () => {
		return <div>not support</div>;
	};

	return (
		<div className="in-phieu-tiep-nhan">
			{isLoading && !file ? (
				<Loading />
			) : file ? (
				<FileViewer
					fileType={type}
					filePath={file}
					errorComponent={CustomErrorComponent}
					onError={onError}
				/>
			) : (
				<p> Không có data</p>
			)}
		</div>
	);
};

export default InPhieuTiepNhan;
