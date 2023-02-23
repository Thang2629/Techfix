import React from 'react';
import PropTypes from 'prop-types';
import { Progress } from 'antd';
const Queue = props => {
    const { percent } = props;
    return (
        <Progress percent={percent} />
    );
};

Queue.propTypes = {
    percent: PropTypes.number
};

export default Queue;