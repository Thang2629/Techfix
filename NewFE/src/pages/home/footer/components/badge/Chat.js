import React from 'react';
import PropTypes from 'prop-types';
import {Badge} from 'antd';
import {MessageOutlined} from '@ant-design/icons';

const Chat = props => {
    const {count, ...rest} = props;
    return (
        <Badge 
            count={count}
            offset={[10, 0]}
            {...rest}
        >
            <MessageOutlined style={{ fontSize: 18 }} />
        </Badge>
    );
};

Chat.propTypes = {
    count: PropTypes.number
};

export default Chat;