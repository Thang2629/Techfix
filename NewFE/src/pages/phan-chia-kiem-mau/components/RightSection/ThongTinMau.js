import React, { useState } from 'react'
import { Button, Form, Input, Row, Col, Space, DatePicker, InputNumber, Checkbox } from 'antd';
import { SaveOutlined } from '@ant-design/icons';
import "./ThongTinPhieu.less"

const ThongTinPhieu = (props) => {
  const [form] = Form.useForm();
  const [formLayout, setFormLayout] = useState('horizontal');

  const onFormLayoutChange = ({ layout }) => {
    setFormLayout(layout);
  };

  const formItemLayout =
  {
    labelCol: { span: 24 },
    wrapperCol: { span: 24 },
  }

  return (
    <Form
      {...formItemLayout}
      layout={"vertical"}
      form={form}
      initialValues={{ layout: formLayout }}
      onValuesChange={onFormLayoutChange}
    >
      <Row gutter={12}>
        <Col span={4}>
          <Form.Item label="Dạng bào chế:">
            <Input placeholder="Nhập nơi lấy mẫu" />
          </Form.Item>
        </Col>
        <Col span={12}>
          <Form.Item label="Tên mẫu:">
            <Input placeholder="Nhập tên mẫu" />
          </Form.Item>
        </Col>
        <Col span={4}>
          <Form.Item label="Mã số mẫu:">
            <Input placeholder="Nhập mã số mẫu" />
          </Form.Item>
        </Col>
        <Col span={8}>
          <Form.Item label="Nơi sản xuất:">
            <Input placeholder="Nhập nơi sản xuất" />
          </Form.Item>
        </Col>
        <Col span={4}>
          <Form.Item label="Số lô:">
            <Input placeholder="Nhập nơi lấy mẫu" />
          </Form.Item>
        </Col>
        <Col span={4}>
          <Form.Item label="Ngày sản xuất:">
            <DatePicker />
          </Form.Item>
        </Col>
        <Col span={4}>
          <Form.Item label="Hạn sử dụng:">
            <DatePicker />
          </Form.Item>
        </Col>
        <Col span={8}>
          <Form.Item label="Số đăng kí:">
            <InputNumber min={0} defaultValue={0} />
          </Form.Item>
        </Col>
        <Col span={8}>
          <Form.Item label="Nơi nhận mẫu:">
            <Input placeholder="Nhập nơi nhận mẫu" />
          </Form.Item>
        </Col>
        <Col span={8}>
          <Form.Item label="Tiêu chuẩn áp dụng:">
            <Input placeholder="Nhập nơi nhận mẫu" />
          </Form.Item>
        </Col>
        <Col span={4}>
          <Form.Item label="Khối lượng mẫu:">
            <Input placeholder="Nhập khối lượng mẫu" />
          </Form.Item>
        </Col>
        <Col span={4}>
          <Form.Item label="Người giao mẫu:">
            <Input placeholder="Nhập nơi lấy mẫu" />
          </Form.Item>
        </Col>
        <Col span={4}>
          <Form.Item label="Người nhận mẫu:">
            <Input placeholder="Nhập nơi lấy mẫu" />
          </Form.Item>
        </Col>
        <Col span={4}>
          <Form.Item label="Ngày nhận mẫu:">
            <DatePicker placeholder="Nhập nơi lấy mẫu" />
          </Form.Item>
        </Col>
        <Col span={4}>
          <Form.Item label="Ngày trả:">
            <DatePicker placeholder="Nhập ngày trả" />
          </Form.Item>
        </Col>
        <Col span={8}>
          <Form.Item label="Cơ sở nhập khẩu:">
            <InputNumber min={0} defaultValue={0} />
          </Form.Item>
        </Col>
        <Col span={8}>
          <Form.Item label="Ngày nhập khẩu:">
            <DatePicker placeholder="Nhập ngày trả" />
          </Form.Item>
        </Col>
        <Col span={8}>
          <Form.Item label="Tình trạng mẫu:">
            <Input placeholder="Nhập tình trạng" />
          </Form.Item>
        </Col>
        <Col span={4}>
          <Form.Item label="Yêu cầu làm gấp:">
            <Checkbox />
          </Form.Item>
        </Col>
        <Col span={4}>
          <Form.Item label="Hệ số:">
            <Input placeholder="Nhập hệ số" />
          </Form.Item>
        </Col>
      </Row>
      <Row style={{ marginTop: '16px' }}>
        <Space>
          <Button>Hủy bỏ</Button>
          <Button type="primary" icon={<SaveOutlined />}>Cập nhật chỉ tiêu</Button>
        </Space>
      </Row>
    </Form>
  )
}

ThongTinPhieu.propTypes = {}

export default ThongTinPhieu
