import React from 'react';
import PropTypes from 'prop-types';
import {Badge} from 'antd';
import {MailOutlined} from '@ant-design/icons';

const Email = props => {
    const {count} = props;
    return (
        <Badge
            count={count}
            offset={[10, 0]}
        >
            <MailOutlined style={{ fontSize: 18 }} />
        </Badge>
    );
};

Email.propTypes = {
    count: PropTypes.number
};

export default Email;