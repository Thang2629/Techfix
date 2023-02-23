import React from 'react'
import PropTypes from 'prop-types'
import { CaretRightOutlined } from '@ant-design/icons';
import { Collapse, Row, Typography, Col, Space } from 'antd';
import ThongTinPhieu from '../RightSection/ThongTinMau';
import { ExperimentOutlined, PicCenterOutlined } from '@ant-design/icons';
import "./TopInfo.less"

const { Panel } = Collapse;
const { Text, Paragraph } = Typography

const TopInfo = (props) => {


  return (
    <div className='top-info'>
      <Collapse
        expandIcon={({ isActive }) => <CaretRightOutlined rotate={isActive ? 90 : 0} />}
        className="site-collapse-custom-collapse"
        bordered={false}
        ghost={false}
      >
        <Panel header={<HeaderTop />} key="1" className="site-collapse-custom-panel">
          <ThongTinPhieu />
        </Panel>
      </Collapse>
    </div>
  )
}

TopInfo.propTypes = {}

export default TopInfo;

const HeaderTop = (props) => {

  return (
    <Row justify='space-between'>
      <Col>
        <Text strong>Thông tin phiếu </Text> <Text>Mã số phiếu</Text>
        <Paragraph>Thông tin khách hàng</Paragraph>
      </Col >
      <Col>
        <Space><Text><ExperimentOutlined /> 3 </Text> <Text><PicCenterOutlined /> </Text></Space>
      </Col>
    </Row >
  )
}

