import React from "react";
import { EditOutlined } from "@ant-design/icons";
import DefaultButton from "./DefaultButton";

function ButtonEdit({ title = "Chỉnh sửa", ...rest }) {
	return (
		<DefaultButton icon={<EditOutlined />} {...rest}>
			{title}
		</DefaultButton>
	);
}

ButtonEdit.propTypes = {};

export default ButtonEdit;
