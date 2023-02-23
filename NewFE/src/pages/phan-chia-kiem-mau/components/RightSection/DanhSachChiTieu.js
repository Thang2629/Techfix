import React, { useState } from 'react';
import PropTypes from 'prop-types';
import Grid from 'components/Grid';
import { Button, Space, Row, Col, Form, Input, InputNumber } from 'antd';
import { EditOutlined, DeleteOutlined } from '@ant-design/icons';
import { GET_CUSTOMER_GROUPS_ENDPOINT } from "services/customer/endpoints";
import { AutoComplete } from 'antd';
import "./DanhSachChiTieu.less"

const DanhSachChiTieu = (props) => {
  const columns = [
    {
      title: 'STT',
      dataIndex: '',
      render: (row, _, index) => {
        return <div>{index}</div>
      }
    },
    {
      title: 'Tên dịch vụ',
      dataIndex: 'tenDichVu',
    },
    {
      title: 'Mức chất lượng',
      dataIndex: 'mucChatLuong',
    },
    {
      title: 'Đơn giá',
      dataIndex: 'donGia',
    },
    {
      title: '',
      dataIndex: 'action',
      render: () => (
        <Space>
          <Button type="danger" icon={<DeleteOutlined />} size='small' />
          <Button type="default" icon={<EditOutlined />} size='small' />
        </Space>
      ),
    }
  ];

  const options = [
    { value: 'Burns Bay Road' },
    { value: 'Downing Street' },
    { value: 'Wall Street' },
  ];

  return (
    <div>
      <Row className='danh-sach-chi-tieu__options'>
        <Col span={8}>
          <Space>
            <span>Chọn nhóm chỉ tiêu:</span>
            <AutoComplete
              style={{ width: 200 }}
              options={options}
              filterOption={(inputValue, option) =>
                option?.value.toUpperCase().indexOf(inputValue.toUpperCase()) !== -1
              }

            />
          </Space>
        </Col>
        <Col span={24} style={{ marginTop: 16 }}>
          <ServicesFormAdd />
        </Col>
      </Row>
      <Row>

      </Row>
      <Grid columns={columns} urlEndpoint={GET_CUSTOMER_GROUPS_ENDPOINT} />
    </div>
  )
}

DanhSachChiTieu.propTypes = {}

export default DanhSachChiTieu;

export const ServicesFormAdd = () => {
  const [form] = Form.useForm();
  const [formLayout, setFormLayout] = useState('vertical');


  const onFormLayoutChange = ({ layout }) => {
    setFormLayout(layout);
  };

  const formItemLayout =
  {
    labelCol: { span: 12 },
    wrapperCol: { span: 24 },
  }

  return (
    <Form
      {...formItemLayout}
      layout={"vertical"}
      form={form}
      // initialValues={{ layout: formLayout }}
      onValuesChange={onFormLayoutChange}
    >
      <Row gutter={12} align="bottom">
        <Col span={8}>
          <Form.Item label="Tên sản phẩm/ Vật liệu:">
            <Input placeholder="Nhập thông tin" />
          </Form.Item>
        </Col>
        <Col span={8}>
          <Form.Item label="Chọn dịch vụ kiểm nghiệm:">
            <Input placeholder="Nhập dịch vụ" />
          </Form.Item>
        </Col>
        <Col span={6}>
          <Form.Item label="Mức chất lượng:">
            <InputNumber placeholder="Nhập mức chất lượng" />
          </Form.Item>
        </Col>
        <Col span={2}>
          <Form.Item label="">
            <Button type="primary">Thêm </Button>
          </Form.Item>
        </Col>
      </Row>
    </Form>
  )
}
