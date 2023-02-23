import React, { useCallback, useEffect, useState } from "react";
import PropTypes from "prop-types";
import {
	Button,
	Form,
	Input,
	Row,
	Col,
	Space,
	DatePicker,
	InputNumber,
	message,
	Select,
	Spin,
} from "antd";
import { useHistory } from "react-router-dom";
import { useSelector } from "react-redux";
import * as selectors from "redux/quyTrinh/selectors";
import { SaveOutlined } from "@ant-design/icons";
import { getDateFormat, convertToISOTime } from "utils/formatDate";
import * as services from "services/sample";
import {
	useCustomerServices,
	useGetLoaiMauTiepNhan,
} from "pages/tiep-nhan-ho-so/utils";
import GridPaticipant from "./GridPaticipant";
import "./ThongTinPhieu.less";

const { Option } = Select;

const initialValues = {
	id: null,
	diaDiemLayMau: "",
	loaiMau: -1,
	loaiPhieu: -1,
	ngayLapPhieu: null,
	ngayPhanMau: null,
	ngayVaoSo: null,
	ngayYeuCauLayMau: null,
	nguoiDaiDien: "",
	nguoiLayMau: "",
	soChiTieu: "",
	soDienThoai: "",
	soLuongLay: 0,
	soLuongLuu: 0,
	soLuongPhieuKetQua: 0,
	soPhieu: "",
	tblKhachHangId: null,
	tblLoaiMauTiepNhanId: null,
	thoiGianLayMau: null,
	tienUngTruoc: 0,
	yeuCauKiemNghiem: "",
	yeuCauNhanXet: true,
	ngayLayMau: null,
	nguoiNhanMaus: [],
};

const ThongTinPhieu = (props) => {
	const { canEdit = true, isCreating, callbackFunc } = props;
	const history = useHistory();

	const [isEdit, setEdit] = useState(false);
	const [isLoading, setLoading] = useState(false);
	const phieuInfo = useSelector(selectors.selectPhieuTiepNhan());
	const phieuInfoLoading = useSelector(selectors.selectPhieuTiepNhanLoading());
	const [form] = Form.useForm();

	useEffect(() => {
		if (phieuInfo) {
			setFormValues(phieuInfo);
		}

		return () => {
			setEdit(false);
		};
		// eslint-disable-next-line react-hooks/exhaustive-deps
	}, [phieuInfo]);

	const setFormValues = (info) => {
		form.setFieldsValue({
			...info,
			ngayYeuCauLayMau: getDateFormat(info?.ngayYeuCauLayMau), // today: need check
			ngayVaoSo: getDateFormat(info?.ngayVaoSo),
			ngayPhanMau: getDateFormat(info?.ngayPhanMau),
			ngayLapPhieu: getDateFormat(info?.ngayLapPhieu),
			ngayLayMau: getDateFormat(info?.ngayLayMau),
			ngayCapGiayGioiThieu: getDateFormat(info?.ngayCapGiayGioiThieu),
		});
	};

	const onCancel = () => {
		if (isCreating) {
			history.push(`/tiep-nhan-ho-so`);
		} else if (isEdit) {
			setEdit(false);
			setFormValues(phieuInfo);
		}
	};

	const handleCreatePhieu = useCallback(async (values) => {
		try {
			setLoading(true);
			const result = await services.createPhieuTiepNhan(values);
			console.log("result ", result);
			if (result.isSuccess) {
				callbackFunc && callbackFunc(result.data);
			} else {
				message.error("Có lỗi xảy ra! Vui lòng thử lại");
				console.log("loi roai");
			}
		} catch (err) {
			message.error("Có lỗi xảy ra! Vui lòng thử lại");
			console.log("loi roai >> ", err);
		} finally {
			setLoading(false);
		}
	}, []);

	const handleEditPhieu = useCallback(async (values) => {
		try {
			setLoading(true);
			const result = await services.updatePhieuTiepNhan(values);
			if (result.isSuccess) {
				message.success("Cập nhật thành công");
				setEdit(false);
			} else {
				console.log("loi roai");
				message.error("Cập nhật thất bại, vui lòng thử lại");
			}
		} catch (err) {
			message.error("Cập nhật thất bại, vui lòng thử lại");
		} finally {
			setLoading(false);
		}
	}, []);

	const onFinish = (values) => {
		const data = {
			...values,
			ngayVaoSo: values.ngayVaoSo ? convertToISOTime(values.ngayVaoSo) : null,
			ngayLayMau: values.ngayLayMau
				? convertToISOTime(values.ngayLayMau)
				: null,
			ngayCapGiayGioiThieu: values.ngayCapGiayGioiThieu // todo: Ngày cấp
				? convertToISOTime(values.ngayCapGiayGioiThieu)
				: null,
			// ngayPhanMau: values.ngayPhanMau
			// 	? convertToISOTime(values.ngayPhanMau)
			// 	: null,
			// ngayYeuCauLayMau: values.ngayYeuCauLayMau
			// 	? convertToISOTime(values.ngayYeuCauLayMau)
			// 	: null,
			// ngayLapPhieu: values.ngayLapPhieu
			// 	? convertToISOTime(values.ngayLapPhieu)
			// 	: null,
		};

		if (isCreating) {
			handleCreatePhieu(data);
		} else if (isEdit) {
			handleEditPhieu({ ...data, id: phieuInfo.id });
		}
	};

	const setPaticipants = (paticipants) => {
		form.setFieldsValue({
			nguoiNhanMaus: paticipants,
		});
	};

	const renderActionsButton = () => {
		return (
			<Space>
				<Button onClick={onCancel}>Hủy bỏ</Button>{" "}
				<Button
					htmlType="submit"
					form="phieuForm"
					type="primary"
					icon={<SaveOutlined />}
				>
					{isEdit ? "Lưu lại" : " Lưu và tiếp tục"}
				</Button>
			</Space>
		);
	};

	const renderEditButton = () => {
		return (
			<>
				{canEdit ? (
					isEdit ? (
						renderActionsButton()
					) : (
						<Button type="primary" onClick={() => setEdit(true)}>
							Chỉnh sửa
						</Button>
					)
				) : null}
			</>
		);
	};

	const disabledForm = !isEdit && !isCreating;

	return (
		<>
			{phieuInfo || isCreating ? (
				<Spin spinning={isLoading || phieuInfoLoading}>
					<PhieuForm
						id="phieuForm"
						formInstance={form}
						onFinish={onFinish}
						setPaticipants={setPaticipants}
						phieuInfo={phieuInfo}
						disabled={disabledForm}
					/>
					{isCreating ? renderActionsButton() : renderEditButton()}
				</Spin>
			) : (
				<p>Chọn để xem Phieu!!</p>
			)}
		</>
	);
};

ThongTinPhieu.propTypes = {
	canEdit: PropTypes.bool,
};

export default ThongTinPhieu;

export const PhieuForm = ({
	formInstance,
	onFinish,
	setPaticipants,
	phieuInfo,
	disabled = false,
	...rest
}) => {
	const formItemLayout = {
		labelCol: { span: 24 },
		wrapperCol: { span: 24 },
	};
	const { customerList } = useCustomerServices();
	const { loaiMauList } = useGetLoaiMauTiepNhan();

	const khachHangOptions =
		customerList?.length > 0
			? customerList?.map((c) => ({
					id: c.id,
					label: c.tenKhachHang,
			  }))
			: [];
	const loaiMauOptions =
		loaiMauList?.length > 0
			? loaiMauList?.map((c) => ({ label: c.tenLoaiMau, id: c.id }))
			: [];

	return (
		<Form
			{...formItemLayout}
			layout={"vertical"}
			form={formInstance}
			onFinish={onFinish}
			initialValues={initialValues}
			disabled={disabled}
			{...rest}
		>
			<Row gutter={12}>
				<Col span={12}>
					<Row gutter={12}>
						<Col span={8}>
							<Form.Item label="Nơi lấy mẫu:" name="diaDiemLayMau">
								<Input placeholder="Nhập nơi lấy mẫu" />
							</Form.Item>
						</Col>
						<Col span={8}>
							<Form.Item
								label="Ngày lấy mẫu:"
								name="ngayLayMau"
								rules={[{ required: true, message: "Nhập ngày lấy" }]}
							>
								<DatePicker format="DD-MM-YYYY" />
							</Form.Item>
						</Col>
						<Col span={8}>
							<Form.Item label="Số giấy giới thiệu:" name="soGiayGioiThieu">
								<Input placeholder="Nhập số giấy" />
							</Form.Item>
						</Col>
						<Col span={24}>
							<Form.Item
								label="Khách hàng:"
								name="tblKhachHangId"
								rules={[{ required: true, message: "Chọn khách hàng" }]}
							>
								<Select showSearch placeholder="Select a person">
									{khachHangOptions?.map((kh) => (
										<Option value={kh.id}>{kh.label}</Option>
									))}
								</Select>
							</Form.Item>
						</Col>
						<Col span={24}>
							<Form.Item label="Yêu cầu kiểm nghiệm:" name="yeuCauKiemNghiem">
								<Input placeholder="Nhập nơi lấy mẫu" />
							</Form.Item>
						</Col>
						<Col span={8}>
							<Form.Item label="Số phiếu kết quả:" name="soLuongPhieuKetQua">
								<InputNumber min={0} defaultValue={0} />
							</Form.Item>
						</Col>
						<Col span={8}>
							<Form.Item label="Ngày vào sổ:" name="ngayVaoSo">
								<DatePicker />
							</Form.Item>
						</Col>
						<Col span={8}>
							<Form.Item label="Tiền ứng trước:" name="tienUngTruoc">
								<InputNumber min={0} defaultValue={0} />
							</Form.Item>
						</Col>
						<Col span={24}>
							<Form.Item name="nguoiNhanMaus">
								<GridPaticipant
									originData={phieuInfo?.nguoiNhanMaus || []}
									handleChangeData={setPaticipants}
									rowKey="nguoiNhanMauId"
								/>
							</Form.Item>
						</Col>
					</Row>
				</Col>
				<Col span={12}>
					<Row gutter={12}>
						<Col span={8}>
							<Form.Item label="Ngày cấp:" name="ngayCapGiayGioiThieu">
								<DatePicker format={"DD-MM-YYYY"} />
							</Form.Item>
						</Col>
						<Col span={8}>
							<Form.Item label="Nơi cấp:" name="noiCapGiayGioiThieu">
								<Input placeholder="Nhập " />
							</Form.Item>
						</Col>
						<Col span={8}>
							<Form.Item label="Số biên bản:" name="soBienBan">
								<Input placeholder="Nhập " />
							</Form.Item>
						</Col>
						<Col span={12}>
							<Form.Item label="Người đại diện:" name="nguoiDaiDien">
								<Input placeholder="Nhập " />
							</Form.Item>
						</Col>
						<Col span={12}>
							<Form.Item label="Số điện thoại:" name="soDienThoai">
								<Input placeholder="Nhập " />
							</Form.Item>
						</Col>
						<Col span={8}>
							<Form.Item
								label="Loại mẫu:"
								name="tblLoaiMauTiepNhanId"
								rules={[{ required: true, message: "Chọn loại mẫu" }]}
							>
								<Select showSearch placeholder="Select a person">
									{loaiMauOptions?.map((m) => (
										<Option value={m.id}>{m.label}</Option>
									))}
								</Select>
							</Form.Item>
						</Col>
						<Col span={8}>
							<Form.Item label="Số lượng:" name="soLuongLay">
								<InputNumber min={0} defaultValue={0} />
							</Form.Item>
						</Col>
						<Col span={8}>
							<Form.Item label="Số lượng lưu:" name="soLuongLuu">
								<InputNumber min={0} defaultValue={0} />
							</Form.Item>
						</Col>
						<Col span={8}>
							<Form.Item
								label="Người nhận mẫu:"
								name="nguoiLayMau"
								rules={[
									{
										required: true,
										message: "Người nhận không để trống",
									},
								]}
							>
								<Input placeholder="Nhập nơi lấy mẫu" />
							</Form.Item>
						</Col>
					</Row>
				</Col>
			</Row>
		</Form>
	);
};
