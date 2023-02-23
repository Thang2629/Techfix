import React from 'react';
import PropTypes from 'prop-types';
import {Badge} from 'antd';
import {ReadOutlined } from '@ant-design/icons';

const Logging = props => {
    const {count, ...rest} = props;
    return (
        <Badge 
            count={count}
            offset={[10, 0]}
            overflowCount={9999999999}
            {...rest}
        >
            <ReadOutlined  style={{ fontSize: 18 }} />
        </Badge>
    );
};

Logging.propTypes = {
    count: PropTypes.number
};

export default Logging;