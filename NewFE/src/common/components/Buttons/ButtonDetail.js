import React from 'react'
import { NavLink } from 'react-router-dom'
import PrimaryButton from './PrimaryButton'
import { BarsOutlined } from "@ant-design/icons";

function ButtonDetail({ url, record }) {
    return (
        <div>
            <NavLink to={`/${url}/${record.id}`}>
                <PrimaryButton icon={<BarsOutlined />}>
                    Xem chi tiết
                </PrimaryButton>
            </NavLink></div>
    )
}

ButtonDetail.propTypes = {}

export default ButtonDetail
