import React from "react";
import { Button } from "antd";
function DangerButton(props) {
	return <Button type="danger" size="small" {...props} />;
}

DangerButton.propTypes = {};

export default DangerButton;
