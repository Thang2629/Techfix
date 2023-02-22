import React, { useEffect, useState } from "react";
import { Button, Modal, Form, Input, message } from "antd";
import {
	createPhongBan,
	getPhongBanById,
	updatePhongBan,
} from "services/apartment-manage";
import { SAVE_SUCCESS } from "utils/common/messageContants";
import "./style.less";

const CreateAndUpdate = (props) => {
	const { isOpen, ID, onClose, title, form, reload } = props;
	const [isLoading, setIsLoading] = useState(false);

	const handleCancel = () => {
		form.resetFields();
		onClose();
	};

	const getDataUpdate = async () => {
		var res = await getPhongBanById(ID);
		if (res.data.isSuccess) {
			form.setFieldsValue(res.data.data);
		}
	};

	useEffect(() => {
		if (ID) {
			getDataUpdate();
		}
		// eslint-disable-next-line react-hooks/exhaustive-deps
	}, [ID]);

	const onFinish = (values) => {
		if (ID) {
			handleUpdatePhongBan(values);
		} else {
			handleCreatePhongBan(values);
		}
	};
	const handleCreatePhongBan = async (values) => {
		setIsLoading(true);
		const result = await createPhongBan(values);
		if (result.isSuccess) {
			handleCancel();
			reload(true);
			message.success(SAVE_SUCCESS);
		} else {
			message.error(result.message);
		}
		setIsLoading(false);
	};
	// const handleGetKhoa = async () => {
	// 	const result = await getAllKhoa();
	// 	if (result.isSuccess) {
	// 		setKhoa(result.data?.phongBanNames);
	// 	}
	// };
	const handleUpdatePhongBan = async (values) => {
		setIsLoading(true);
		const result = await updatePhongBan(values);
		if (result.isSuccess) {
			handleCancel();
			reload(true);
			message.success(SAVE_SUCCESS);
		} else {
			message.error(result.message);
		}
		setIsLoading(false);
	};

	return (
		<>
			<Modal
				title={title}
				open={isOpen}
				className="form-create-phong-ban"
				form={form}
				onCancel={handleCancel}
				footer={[
					<Button form="myForm" key="back" onClick={handleCancel}>
						Hủy
					</Button>,
					<Button
						form="myForm"
						key="submit"
						type="primary"
						htmlType="submit"
						loading={isLoading}
					>
						Lưu
					</Button>,
				]}
			>
				<Form
					id="myForm"
					labelCol={{ span: 8 }}
					form={form}
					wrapperCol={{ span: 16 }}
					onFinish={onFinish}
				>
					{ID && <Form.Item hidden={true} name="id"></Form.Item>}
					<Form.Item
						label="Mã Khoa/Phòng/Ban"
						name="maKhoaPhongBan"
						rules={[{ required: true, message: "Xin Nhập Mã Khoa/Phòng/Ban!" }]}
					>
						<Input />
					</Form.Item>
					<Form.Item
						label="Tên Khoa/Phòng/Ban"
						name="tenKhoaPhongBan"
						rules={[
							{ required: true, message: "Xin Nhập Tên Khoa/Phòng/Ban!" },
						]}
					>
						<Input />
					</Form.Item>
					<Form.Item
						label="Chức năng nhiệm vụ"
						name="chucNangNhiemVu"
					// rules={[
					// 	{ required: true, message: "Xin Nhập Chức năng nhiệm vụ!" },
					// ]}
					>
						<Input />
					</Form.Item>
					<Form.Item
						label="Vị trí"
						name="viTri"
					// rules={[{ required: true, message: "Xin Nhập Vị trí!" }]}
					>
						<Input />
					</Form.Item>
				</Form>
			</Modal>
		</>
	);
};

CreateAndUpdate.propTypes = {};
export default CreateAndUpdate;
