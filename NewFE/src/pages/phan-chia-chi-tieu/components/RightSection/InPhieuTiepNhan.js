import React from "react";
import "./InPhieuTiepNhan.less";
import { useGetFileKiemNghiem } from "pages/phan-chia-chi-tieu/utils";
import FileViewer from "react-file-viewer";
import Loading from "components/Loading/Loading";

const type = "pdf";
const InPhieuTiepNhanMau = ({ chiTietMauId }) => {
	const { file, isLoading } = useGetFileKiemNghiem(chiTietMauId);

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

export default InPhieuTiepNhanMau;
