import React from "react";
import { Link } from "react-router-dom";
import PrimaryButton from "./PrimaryButton";
import { BarsOutlined } from "@ant-design/icons";

function ButtonDetail({ url, record }) {
  return (
    <div>
      <Link to={`/${url}/${record.id}`}>
        <PrimaryButton icon={<BarsOutlined />}>Xem chi tiết</PrimaryButton>
      </Link>
    </div>
  );
}

ButtonDetail.propTypes = {};

export default ButtonDetail;
