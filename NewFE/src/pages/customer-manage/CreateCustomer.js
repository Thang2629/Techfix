import { Button, Card, Col, DatePicker, Form, Input, message, Row, Select } from 'antd'
import TextArea from 'antd/lib/input/TextArea'
import Loading from 'components/Loading/Loading'
import ModalFullScreen from 'components/modal-fullscreen/ModalFullScreen'
import React, { useCallback, useEffect, useState } from 'react'
import { createCustomerService, getAllCustomerGroupService, getCustomerByIdService } from 'services/customer-manager'
import { SAVE_SUCCESS } from 'utils/common/messageContants'
import './style.less'

const EditCustomer = props => {
    const { isOpen, handleClosed, title, idKhachHang, reloadTable } = props

    const [dataKH, setDataKH] = useState([]);
    const [loading, setLoading] = useState(false);
    const [form] = Form.useForm();
    // const [value, setValue] = useState(1);


    const onClose = () => {
        form.resetFields();
        handleClosed();
    };

    // const onChange = (e) => {
    //     setValue(e.target.value);
    // };

    const getDataNhomKH = useCallback(async () => {
        const data = await getAllCustomerGroupService();
        if (data.isSuccess) {
            setDataKH(data.data.nhomKhachHangNames);
        }
    }, []);

    const getCustomerById = useCallback(async () => {
        setLoading(true)
        const result = await getCustomerByIdService(idKhachHang);
        if (result.isSuccess) {
            form.setFieldsValue(result.data)
        }
        setLoading(false)
    }, [form, idKhachHang])

    useEffect(() => {
        if (idKhachHang) { getCustomerById() }
        getDataNhomKH();
    }, [getCustomerById, getDataNhomKH, idKhachHang]);


    const onFinish = (values) => {
        createCustomer(values);
    };
    const createCustomer = async (values) => {
        const data = await createCustomerService(values);
        if (data.isSuccess) {
            handleClosed();
            reloadTable();
            message.success(SAVE_SUCCESS);
        } else {
            handleClosed();
            reloadTable();
            message.error(data.message);
        }
    };
    // const updateCustomer = async (values) => {
    //     const data = await updateCustomerService(values);
    //     if (data.isSuccess) {
    //         handleClosed();
    //         reloadTable();
    //         message.success(SAVE_SUCCESS);
    //     } else {
    //         message.error(SAVE_ERROR);
    //     }
    // };


    return (
        <ModalFullScreen
            title={title}
            open={isOpen}
            onCancel={onClose}
            footer={[
                <Button form="myForm" key="back" type="danger" onClick={handleClosed}>
                    Hủy
                </Button>,
                <Button form="myForm" key="submit" type="primary" htmlType="submit">
                    Lưu
                </Button>,
            ]}
        >
            {loading ? <Loading /> :
                <Form
                    id="myForm"
                    labelCol={{ span: 8 }}
                    wrapperCol={{ span: 16 }}
                    form={form}
                    layout='vertical'
                    onFinish={onFinish}
                >
                    <Row>
                        <Col span={24}>
                            <Card className="cardGroup">
                                <div className='wrapperText'>Thông tin cá nhân</div>
                                <Row>
                                    <Col span={8}>
                                        {
                                            idKhachHang && <Form.Item hidden={true} label="id" name="id" />
                                        }
                                        <Form.Item
                                            label="Tên Khách Hàng"
                                            name="tenKhachHang"
                                            rules={[{ required: true, message: "Nhập Khách Hàng!" }]}
                                        >
                                            <Input />
                                        </Form.Item>
                                    </Col>
                                    <Col span={8}>
                                        <Form.Item
                                            label="Mã Khách Hàng"
                                            name="maKhachHang"
                                            rules={[{ required: true, message: "Nhập Mã Khách Hàng!" }]}
                                        >
                                            <Input />
                                        </Form.Item>
                                    </Col>
                                    <Col span={8}>
                                        <Form.Item
                                            label="Nhóm Khách Hàng"
                                            name="nhomKhachHangId"
                                            rules={[{ required: true, message: "Nhập Nhóm Khách Hàng!" }]}
                                        >
                                            <Select allowClear>
                                                {dataKH &&
                                                    dataKH.map((item) => (
                                                        <Select.Option key={item.id} values={item.id}>{item.tenNhomKhachHang}</Select.Option>
                                                    ))}
                                            </Select>
                                        </Form.Item>
                                    </Col>
                                </Row>
                                <Row>
                                    <Col span={8}>
                                        <Form.Item
                                            label="Số điện thoại"
                                            name="dienThoai"
                                            rules={[{ required: true, message: "Nhập SĐT!" }]}
                                        >
                                            <Input />
                                        </Form.Item>
                                    </Col>
                                    <Col span={8}>
                                        <Form.Item
                                            label="Ngày sinh"
                                            name="ngaySinh"
                                            rules={[{ required: true, message: "Nhập ngày sinh!" }]}
                                        >
                                            <DatePicker />
                                        </Form.Item>
                                    </Col>
                                    {/* <Col span={8}>
                                        <Form.Item
                                            label="Giới tính"
                                            name="ngaySinh"
                                            rules={[{ required: true, message: "Nhập giới tính!" }]}
                                        >
                                            <Radio.Group onChange={onChange} value={value}>
                                                <Radio value={1}>Nam</Radio>
                                                <Radio value={2}>Nữ</Radio>
                                            </Radio.Group>
                                        </Form.Item>
                                    </Col> */}
                                </Row>
                            </Card>
                        </Col>
                    </Row>

                    <Row>
                        <Col span={24}>
                            <Card className="cardGroup">
                                <div className='wrapperText'>Địa chỉ đăng ký giấy phép</div>
                                <Row>
                                    <Col span={12}>
                                        <Form.Item
                                            label="Địa chỉ đăng ký giấy phép"
                                            name="diaChiDangKyGiayPhep"
                                        // rules={[{ required: true, message: "Nhập địa chỉ!" }]}
                                        >
                                            <Input />
                                        </Form.Item>
                                    </Col>
                                    <Col span={12}>
                                        <Form.Item
                                            label="Địa chỉ kinh doanh"
                                            name="diaChiKinhDoanh"
                                        // rules={[{ required: true, message: "Nhập địa chỉ!" }]}
                                        >
                                            <Input />
                                        </Form.Item>
                                    </Col>
                                </Row>
                            </Card>
                        </Col>
                    </Row>
                    <Row>
                        <Col span={24}>
                            <Card className="cardGroup">
                                <div className='wrapperText'>Thông tin doanh nghiệp</div>
                                <Row>
                                    <Col span={12}>
                                        <Form.Item
                                            label="Mã số thuế"
                                            name="maSoThue"
                                        // rules={[{ required: true, message: "Nhập Mã Số Thuế!" }]}
                                        >
                                            <Input />
                                        </Form.Item>
                                    </Col>
                                    <Col span={12}>
                                        <Form.Item
                                            label="Địa chỉ"
                                            name="diaChi"
                                        // rules={[{ required: true, message: "Nhập Địa Chỉ!" }]}
                                        >
                                            <Input />
                                        </Form.Item>
                                    </Col>
                                </Row>
                                <Row>
                                    <Col>
                                    </Col>
                                </Row>
                            </Card>
                        </Col>
                    </Row>
                    {/* Người liên hệ 1 */}
                    <Row>
                        <Col span={24}>
                            <Card className="cardGroup">
                                <div className='wrapperText'>Người liên hệ 1</div>
                                <Row>
                                    <Col span={8}>
                                        <Form.Item
                                            label="Tên người liên hệ 1"
                                            name="tenNguoiLienHe1"
                                        // rules={[{ required: true, message: "Nhập tên người liên hệ!" }]}
                                        >
                                            <Input />
                                        </Form.Item>
                                    </Col>
                                    <Col span={8}>
                                        <Form.Item
                                            label="SĐT người liên hệ 1"
                                            name="dienThoaiNguoiLienHe1"
                                        // rules={[{ required: true, message: "Nhập SĐT!" }]}
                                        >
                                            <Input />
                                        </Form.Item>
                                    </Col>
                                    <Col span={8}>
                                        <Form.Item
                                            label="Chức vụ người liên hệ 1"
                                            name="chucVuNguoiLienHe1"
                                        // rules={[{ required: true, message: "Nhập chức vụ!" }]}
                                        >
                                            <Input />
                                        </Form.Item>
                                    </Col>
                                </Row>
                                <Row>
                                    <Col>
                                    </Col>
                                </Row>
                            </Card>
                        </Col>
                    </Row>
                    {/* Người liên hệ 2 */}
                    <Row>
                        <Col span={24}>
                            <Card className="cardGroup">
                                <div className='wrapperText'>Người liên hệ 2</div>
                                <Row>
                                    <Col span={8}>
                                        <Form.Item
                                            label="Tên người liên hệ 2"
                                            name="tenNguoiLienHe2"
                                        // rules={[{ required: true, message: "Nhập tên người liên hệ!" }]}
                                        >
                                            <Input />
                                        </Form.Item>
                                    </Col>
                                    <Col span={8}>
                                        <Form.Item
                                            label="SĐT người liên hệ 2"
                                            name="dienThoaiNguoiLienHe2"
                                        // rules={[{ required: true, message: "Nhập SĐT!" }]}
                                        >
                                            <Input />
                                        </Form.Item>
                                    </Col>
                                    <Col span={8}>
                                        <Form.Item
                                            label="Chức vụ người liên hệ 2"
                                            name="chucVuNguoiLienHe2"
                                        // rules={[{ required: true, message: "Nhập chức vụ!" }]}
                                        >
                                            <Input />
                                        </Form.Item>
                                    </Col>
                                </Row>
                                <Row>
                                    <Col>
                                    </Col>
                                </Row>
                            </Card>
                        </Col>
                    </Row>
                    <Row>
                        <Col span={24}>
                            <Card className="cardGroup">
                                <div className='wrapperText'>Ghi chú</div>
                                <Form.Item
                                    label="Ghi chú"
                                    name="ghiChu"
                                // rules={[{ required: true, message: "Nhập ghi chú!" }]}
                                >
                                    <TextArea />
                                </Form.Item>
                            </Card>
                        </Col>
                    </Row>
                </Form>
            }

        </ModalFullScreen>
    )
}

EditCustomer.propTypes = {}

export default EditCustomer