import { useState, useCallback, useEffect } from "react";
import * as services from "services/sample";
import { API_ENDPOINT } from "config/env";

export const useGetFileKiemNghiem = (id) => {
	const [file, setFile] = useState(null);
	const [isLoading, setLoading] = useState(false);

	const getFile = (file) => {
		const url = `${API_ENDPOINT}api/Asset/DownloadFile/${file}`;
		setFile(url);
	};

	const getFilePhieuTiepNhan = useCallback(async () => {
		try {
			setLoading(true);
			const result = await services.inPhieuKiemNghiem(id, true);
			if (result.isSuccess) {
				getFile(encodeURI(result?.data?.virtualPath));
			} else {
				setFile(null);
			}
		} catch (err) {
			console.log("err ", err);
			setFile(null); // todo: need update
		} finally {
			setLoading(false);
		}
	}, [id]);

	useEffect(() => {
		if (id) getFilePhieuTiepNhan();
	}, [getFilePhieuTiepNhan, id]);

	return { file, isLoading };
};
