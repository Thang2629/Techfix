import React from 'react';
import PropTypes from 'prop-types';
import { Typography } from 'antd';

const { Text } = Typography;
const Mrm = props => {
    const { name, phone, email, usercls } = props;
    return (
        <Text
            className={usercls}
        >
            {name} / {phone} / {email}
        </Text>
    );
};

Mrm.propTypes = {
    name: PropTypes.string,
    phone: PropTypes.string,
    email: PropTypes.string
};

export default Mrm;