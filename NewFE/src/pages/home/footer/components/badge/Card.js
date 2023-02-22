import React from 'react';
import PropTypes from 'prop-types';
import { Badge } from 'antd';
import { IdcardOutlined } from '@ant-design/icons';

const Card = props => {
    const { count } = props;
    return (
        <Badge
            count={count}
            offset={[10, 0]}
        >
            <IdcardOutlined style={{ fontSize: 18 }} />
        </Badge>
    );
};

Card.propTypes = {
    count: PropTypes.number
};

export default Card;