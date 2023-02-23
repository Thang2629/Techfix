import React, { useState, useEffect } from 'react'
import PropTypes from 'prop-types'
import { Avatar, Button, List, Skeleton } from 'antd';
import "./LichSuHoatDong.less";


const count = 3;
const fakeDataUrl = `https://randomuser.me/api/?results=${count}&inc=name,gender,email,nat,picture&noinfo`;

const LichSuHoatDong = (props) => {
  const [initLoading, setInitLoading] = useState(true);
  const [loading, setLoading] = useState(false);
  const [data, setData] = useState([]);
  const [list, setList] = useState([]);

  useEffect(() => {
    fetch(fakeDataUrl)
      .then(res => res.json())
      .then(res => {
        setInitLoading(false);
        setData(res.results);
        setList(res.results);
      });
  }, []);

  const onLoadMore = () => {
    setLoading(true);
    setList(
      data.concat([...new Array(count)].map(() => ({ loading: true, name: {}, picture: {} }))),
    );
    fetch(fakeDataUrl)
      .then(res => res.json())
      .then(res => {
        const newData = data.concat(res.results);
        setData(newData);
        setList(newData);
        setLoading(false);
        // Resetting window's offsetTop so as to display react-virtualized demo underfloor.
        // In real scene, you can using public method of react-virtualized:
        // https://stackoverflow.com/questions/46700726/how-to-use-public-method-updateposition-of-react-virtualized
        window.dispatchEvent(new Event('resize'));
      });
  };

  const loadMore =
    !initLoading && !loading ? (
      <div
        style={{
          textAlign: 'center',
          marginTop: 12,
          height: 32,
          lineHeight: '32px',
        }}
      >
        <Button onClick={onLoadMore}>loading more</Button>
      </div>
    ) : null;
  return (
    <List
      className="lich-su-hoat-dong"
      loading={initLoading}
      itemLayout="horizontal"
      loadMore={loadMore}
      dataSource={list}
      renderItem={item => (
        <List.Item
          actions={["30/10/2022"]}
        >
          <Skeleton avatar title={false} loading={item.loading} active>
            <List.Item.Meta
              avatar={<Avatar src={item.picture.large} />}
              title={'Ghi chú'}
              description="Thay đổi Trạng Thái từ A sang B"
            />
          </Skeleton>
        </List.Item>
      )}
    />
  )
}

LichSuHoatDong.propTypes = {}

export default LichSuHoatDong;
