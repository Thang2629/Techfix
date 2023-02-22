import React from "react";
import { Button } from "antd";
import { NavLink } from "react-router-dom";
import { BackwardOutlined } from "@ant-design/icons";

function ButtonBack({ url, ...rest }) {
    return <NavLink style={style} to={url}>
        <Button icon={<BackwardOutlined />} type="primary" size="small" {...rest}>Quay lại</Button>
    </NavLink>
}

const style = {
    margin: "8px",
    marginBottom: "12px",
}

ButtonBack.propTypes = {};

export default ButtonBack;
