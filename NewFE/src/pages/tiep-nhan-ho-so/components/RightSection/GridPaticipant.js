import React, { useEffect, useState } from "react";
import {
	Table,
	Form,
	Input,
	InputNumber,
	Typography,
	Button,
	Row,
	Space,
} from "antd";
import "./GridPaticipant.less";

const GridPaticipants = ({
	originData = [],
	rowKey = "id",
	handleChangeData,
	...rest
}) => {
	const [formInstance] = Form.useForm();
	const [data, setData] = useState(originData);
	const [editingKey, setEditingKey] = useState("");
	const [isAdding, setAdding] = useState(false);

	useEffect(() => {
		setData(originData);
	}, [originData]);

	const isEditing = (record) => record[rowKey] === editingKey;

	const onEditRecord = (record) => {
		formInstance.setFieldsValue({ tenNguoiNhan: "", chucVu: "", ...record });
		setEditingKey(record[rowKey]);
	};

	const onCancel = () => {
		if (isAdding) {
			setData((prev) => prev.slice(0, -1));
			setAdding(false);
		} else {
			setEditingKey("");
		}
	};

	const onSave = async (index, record) => {
		try {
			const row = await formInstance.validateFields();
			const newData = [...data];

			if (index > -1) {
				const item = newData[index];
				newData.splice(index, 1, {
					...item,
					...row,
				});
				setData(newData);
				setEditingKey("");
				setAdding(false);
				handleChangeData && handleChangeData(newData);
			} else {
				newData.push(row);
				setData(newData);
				setEditingKey("");
				setAdding(false);
			}
		} catch (errInfo) {
			console.log("Validate Failed:", errInfo);
		}
	};

	const handleAddRow = () => {
		const newData = {
			tenNguoiNhan: ``,
			chucVu: "",
		};
		setData([...data, newData]);
		setAdding(true);
		formInstance.setFieldsValue({ ...newData });
	};

	const onDeleteRecord = (record, index) => {
		const newData = [...data];
		newData.splice(index, 1);
		setData(newData);
		handleChangeData && handleChangeData(newData);
	};

	const columns = [
		{
			title: "Tên người tham gia",
			dataIndex: "tenNguoiNhan",
			width: "40%",
			editable: true,
		},
		{
			title: "Chức vụ",
			dataIndex: "chucVu",
			width: "40%",
			editable: true,
		},
		{
			title: "",
			dataIndex: "",
			render: (_, record, index) => {
				const editable =
					isEditing(record) || (isAdding && index === data.length - 1);
				return editable ? (
					<Space>
						<Button
							type="primary"
							onClick={() => onSave(record?.[rowKey] || index, record)}
							size="small"
						>
							Lưu
						</Button>
						<Button type="default" onClick={onCancel} size="small">
							Hủy
						</Button>
					</Space>
				) : (
					<Space>
						<Button
							type="default"
							disabled={editingKey !== "" || isAdding}
							onClick={() => onEditRecord(record)}
							size="small"
						>
							Sửa
						</Button>
						<Button
							type="danger"
							onClick={() => onDeleteRecord(record, index)}
							disabled={editingKey !== "" || isAdding}
							size="small"
						>
							Xóa
						</Button>
					</Space>
				);
			},
		},
	];

	const columnsWithEdit = columns.map((col) => {
		if (!col.editable) {
			return col;
		}
		return {
			...col,
			onCell: (record, index) => ({
				index,
				record,
				inputType: "text",
				dataIndex: col.dataIndex,
				title: col.title,
				editing: isEditing(record),
				isAdding: isAdding,
				rowLength: data.length,
			}),
		};
	});

	return (
		<Form className="grid-paticipant" form={formInstance}>
			<Table
				size="small"
				components={{
					body: {
						cell: EditableCell,
					},
				}}
				bordered
				dataSource={data}
				columns={columnsWithEdit}
				rowClassName="editable-row"
				pagination={false}
				scroll={{ y: 200 }}
				onRow={(record, rowIndex) => {
					return {
						onClick: (event) => {}, // click row
						onDoubleClick: (event) => {}, // double click row
					};
				}}
				title={() => (
					<Row className="grid-paticipant__title" align="space-between">
						<Typography.Text>Người tham gia lấy mẫu:</Typography.Text>
						<Button onClick={handleAddRow} type="primary" size="small">
							Thêm mới
						</Button>
					</Row>
				)}
				rowKey="id"
			/>
		</Form>
	);
};

GridPaticipants.propTypes = {};
export default GridPaticipants;

const EditableCell = ({
	editing,
	dataIndex,
	title,
	inputType,
	record,
	index,
	children,
	isAdding,
	rowLength,
	...restProps
}) => {
	const inputNode = inputType === "number" ? <InputNumber /> : <Input />;

	return (
		<td {...restProps}>
			{editing || (isAdding && index === rowLength - 1) ? (
				<>
					<Form.Item
						name={dataIndex}
						style={{ margin: 0 }}
						rules={[
							{
								required: true,
								message: `Nhập ${title}!`,
							},
						]}
					>
						{inputNode}
					</Form.Item>
				</>
			) : (
				children
			)}
		</td>
	);
};
