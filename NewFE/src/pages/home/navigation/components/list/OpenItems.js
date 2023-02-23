import React from 'react';
import { List, Typography } from 'antd';
import './OpenItems.less';

const { Text } = Typography;
const data = [
  'Racing car sprays burning fuel into crowd.',
  'Japanese princess to wed commoner.',
  'Australian walks 100km after outback crash.'
];

const OpenItems = (props) => {
  const { usercls, styles, size, ...rest } = props;
  return (
    <div
      className={"open-items " + usercls}
    >
      <List
        header={
          <Text
            className="open-items__title"
            strong
          >
            Open
          </Text>
        }
        className="open-items__list"
        bordered
        dataSource={data}
        style={styles}
        size={size}
        {...rest}
        renderItem={item => (
          <List.Item
            className="open-items__list-item"
          >
            {item}
          </List.Item>
        )}
      />
    </div>

  );
};

export default OpenItems;