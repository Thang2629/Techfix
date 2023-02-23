import React from 'react';
import PropTypes from 'prop-types';
import { Typography } from 'antd';

const { Text } = Typography;
const Username = props => {
    const { text, usercls } = props;
    return (
        <Text
            className={usercls}
        >
            {text}
        </Text>
    );
};

Username.propTypes = {
    text: PropTypes.string
};

export default Username;