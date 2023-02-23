import { useState, useCallback, useEffect } from "react";
import * as services from "services/sample";
import * as customersManagerServices from "services/customer-manager";
import * as mauManagerServices from "services/specimen-manage";
import { API_ENDPOINT } from "config/env";

export const useGetFileTiepNhanById = (id) => {
	const [file, setFile] = useState(null);
	const [isLoading, setLoading] = useState(false);

	const getFile = (file) => {
		const url = `${API_ENDPOINT}api/Asset/DownloadFile/${file}`;
		setFile(url);
	};

	const getFilePhieuTiepNhan = useCallback(async () => {
		try {
			setLoading(true);
			const result = await services.getFilePhieuTiepNhan(id);
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

export const useCustomerServices = (searchText) => {
	const [customerList, setCustomerList] = useState([]);
	const [status, setStatus] = useState("idle");

	const getCustomers = useCallback(async () => {
		try {
			setStatus("loading");
			const result = await customersManagerServices.getCustomerService({
				pageIndex: 1,
				pageSize: 100,
			});
			if (result?.isSuccess) {
				setCustomerList(result?.data?.data);
				setStatus("success");
			} else {
				setCustomerList([]);
				setStatus("error");
			}
		} catch (err) {
			setStatus("error");
		}
	}, []);

	useEffect(() => {
		getCustomers();
		// eslint-disable-next-line react-hooks/exhaustive-deps
	}, []);
	return { customerList, status };
};

export const useGetLoaiMauTiepNhan = (searchText) => {
	const [loaiMauList, setLoaiMauList] = useState([]);
	const [status, setStatus] = useState("idle");

	const getLoaiMauList = useCallback(async () => {
		try {
			setStatus("loading");
			const result = await mauManagerServices.getLoaiMauTiepNhans({
				pageIndex: 1,
				pageSize: 100,
			});
			if (result?.isSuccess) {
				setLoaiMauList(result.data?.data);
				setStatus("success");
			} else {
				setLoaiMauList([]);
				setStatus("error");
			}
		} catch (err) {
			setStatus("error");
		}
	}, []);

	useEffect(() => {
		getLoaiMauList();
		// eslint-disable-next-line react-hooks/exhaustive-deps
	}, []);

	return { loaiMauList, status };
};
