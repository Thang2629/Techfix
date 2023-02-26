// eslint-disable-next-line no-unused-vars
import {
  Button,
  Card,
  Col,
  Form,
  Input,
  message,
  Row,
  Select,
  Typography,
} from "antd";
import Loading from "components/Loading/Loading";
import React, { useCallback, useEffect, useMemo, useState } from "react";
import { useParams } from "react-router-dom";
import {
  getAllCustomerGroupService,
  getCustomerByIdService,
  updateCustomerService,
} from "services/customer-manager";
import { SaveOutlined } from "@ant-design/icons";
import "../style.less";
import { SAVE_ERROR, SAVE_SUCCESS } from "utils/common/messageContants";
import TextArea from "antd/lib/input/TextArea";
const ThongTinKhachHang = (props) => {
  let { id } = useParams();
  const [dataCustomer, setDataCustomer] = useState([]);
  // eslint-disable-next-line no-unused-vars
  const [dataKH, setDataKH] = useState([]);
  const [loading, setLoading] = useState(false);
  const [isUpdate, setIsUpdate] = useState(false);
  const [form] = Form.useForm();
  const { Text } = Typography;

  const getCustomerById = async () => {
    setLoading(true);
    const result = await getCustomerByIdService(id);
    if (result.isSuccess) {
      setDataCustomer(result.data);
      form.setFieldsValue(result.data);
    }
    setLoading(false);
  };

  //   const getDataNhomKH = useCallback(async () => {
  //     const data = await getAllCustomerGroupService();
  //     if (data.isSuccess) {
  //       setDataKH(data.data.nhomKhachHangNames);
  //     }
  //   }, []);

  // useEffect(() => {
  //     getDataNhomKH();
  //     if (id)
  //         getCustomerById();
  //     // eslint-disable-next-line react-hooks/exhaustive-deps
  // }, [getDataNhomKH])

  const onFinish = useCallback(
    (values) => {
      if (id) {
        handleUpdate(values);
      }
    },
    [id]
  );

  const handleUpdate = async (values) => {
    const data = await updateCustomerService(values);
    if (data.isSuccess) {
      message.success(SAVE_SUCCESS);
    } else {
      message.error(SAVE_ERROR);
    }
  };

  const btnEdit = useMemo(() => {
    return (
      <div style={{ position: "absolute", right: "0", top: "0" }}>
        <Button
          style={{ marginRight: "5px" }}
          size="small"
          type={isUpdate ? "default" : "primary"}
          onClick={() => setIsUpdate(!isUpdate)}
        >
          {isUpdate ? "Hủy" : "Chỉnh sửa"}
        </Button>
        {isUpdate && (
          <Button
            size="small"
            type="primary"
            key="submit"
            htmlType="submit"
            form="myForm"
            icon={<SaveOutlined />}
            onClick={(values) => handleUpdate(values)}
          >
            Lưu
          </Button>
        )}
      </div>
    );
  }, [isUpdate]);

  const renderForm = useMemo(() => {
    return (
      <Form
        id="myForm"
        form={form}
        labelCol={{ span: 15 }}
        labelAlign={"left"}
        layout="vertical"
        labelWrap={true}
        wrapperCol={{ span: 18 }}
        onFinish={onFinish}
      >
        <Row>
          <Col span={24}>
            <Card className="cardGroup" title={btnEdit}>
              <div className="wrapperText">Thông tin sản phẩm</div>
              <Row>
                <Col span={24}>
                  {id && <Form.Item hidden={true} label="Id" name="Id" />}
                  <Form.Item label="Tên Sản Phẩm" name="tensanpham">
                    {isUpdate ? (
                      <Input />
                    ) : (
                      <Text strong>
                        {dataCustomer.tenKhachHang === "string"
                          ? "-"
                          : dataCustomer.tenKhachHang}
                      </Text>
                    )}
                  </Form.Item>
                </Col>
              </Row>
              {/*  */}
              <Row>
                <Col span={12}>
                  {id && <Form.Item hidden={true} label="id" name="id" />}
                  <Form.Item label="Số Lượng" name="Quantity">
                    {isUpdate ? (
                      <Input />
                    ) : (
                      <Text strong>
                        {dataCustomer.tenKhachHang === "string"
                          ? "-"
                          : dataCustomer.tenKhachHang}
                      </Text>
                    )}
                  </Form.Item>
                </Col>
                <Col span={12}>
                  <Form.Item label="Mã Sản Phẩm" name="ProductCode">
                    {isUpdate ? (
                      <Input />
                    ) : (
                      <Text strong>
                        {dataCustomer.maKhachHang === "string"
                          ? "-"
                          : dataCustomer.maKhachHang}
                      </Text>
                    )}
                  </Form.Item>
                </Col>
              </Row>
              <Row>
                <Col span={8}>
                  <Form.Item label="Đơn vị tính" name="dienThoai">
                    {isUpdate ? (
                      <Select allowClear>
                        {[
                          { Id: 1, Label: "text" },
                          { Id: 2, Label: "tex2" },
                        ] &&
                          [
                            { Id: 1, Label: "text" },
                            { Id: 2, Label: "tex2" },
                          ].map((item) => (
                            <Select.Option key={item.Id} values={item.Id}>
                              {item.Label}
                            </Select.Option>
                          ))}
                      </Select>
                    ) : (
                      <Text strong>
                        {dataCustomer.tenNhomKhachHang === "string"
                          ? "-"
                          : dataCustomer.tenNhomKhachHang}
                      </Text>
                    )}
                  </Form.Item>
                </Col>
              </Row>
              <Row>
                <Col span={12}>
                  <Form.Item label="Giá Nhập" name="dienThoai">
                    {isUpdate ? (
                      <Input />
                    ) : (
                      <Text strong>
                        {dataCustomer.dienThoai === "string"
                          ? "-"
                          : dataCustomer.dienThoai}
                      </Text>
                    )}
                  </Form.Item>
                </Col>
                <Col span={12}>
                  <Form.Item label="Giá Web" name="dienThoai">
                    {isUpdate ? (
                      <Input />
                    ) : (
                      <Text strong>
                        {dataCustomer.dienThoai === "string"
                          ? "-"
                          : dataCustomer.dienThoai}
                      </Text>
                    )}
                  </Form.Item>
                </Col>
              </Row>
              <Row>
                <Col span={12}>
                  <Form.Item label="Bảo Hành NCC" name="dienThoai">
                    {isUpdate ? (
                      <Input />
                    ) : (
                      <Text strong>
                        {dataCustomer.dienThoai === "string"
                          ? "-"
                          : dataCustomer.dienThoai}
                      </Text>
                    )}
                  </Form.Item>
                </Col>
                <Col span={12}>
                  <Form.Item label="Giá Vốn" name="dienThoai">
                    {isUpdate ? (
                      <Input />
                    ) : (
                      <Text strong>
                        {dataCustomer.dienThoai === "string"
                          ? "-"
                          : dataCustomer.dienThoai}
                      </Text>
                    )}
                  </Form.Item>
                </Col>
              </Row>
              <Row>
                <Col span={12}>
                  <Form.Item label="Danh Mục" name="dienThoai">
                    {isUpdate ? (
                      <Select allowClear>
                        {[
                          { Id: 1, Label: "text" },
                          { Id: 2, Label: "tex2" },
                        ] &&
                          [
                            { Id: 1, Label: "text" },
                            { Id: 2, Label: "tex2" },
                          ].map((item) => (
                            <Select.Option key={item.Id} values={item.Id}>
                              {item.Label}
                            </Select.Option>
                          ))}
                      </Select>
                    ) : (
                      <Text strong>
                        {dataCustomer.tenNhomKhachHang === "string"
                          ? "-"
                          : dataCustomer.tenNhomKhachHang}
                      </Text>
                    )}
                  </Form.Item>
                </Col>
                <Col span={12}>
                  <Form.Item label="Nhà Sản Xuất" name="dienThoai">
                    {isUpdate ? (
                      <Select allowClear>
                        {[
                          { Id: 1, Label: "text" },
                          { Id: 2, Label: "tex2" },
                        ] &&
                          [
                            { Id: 1, Label: "text" },
                            { Id: 2, Label: "tex2" },
                          ].map((item) => (
                            <Select.Option key={item.Id} values={item.Id}>
                              {item.Label}
                            </Select.Option>
                          ))}
                      </Select>
                    ) : (
                      <Text strong>
                        {dataCustomer.tenNhomKhachHang === "string"
                          ? "-"
                          : dataCustomer.tenNhomKhachHang}
                      </Text>
                    )}
                  </Form.Item>
                </Col>
              </Row>
              <Row>
                <Col span={12}>
                  <Form.Item label="Định Mức Tối Thiểu" name="dienThoai">
                    {isUpdate ? (
                      <Input />
                    ) : (
                      <Text strong>
                        {dataCustomer.dienThoai === "string"
                          ? "-"
                          : dataCustomer.dienThoai}
                      </Text>
                    )}
                  </Form.Item>
                </Col>
                <Col span={12}>
                  <Form.Item label="Định Mức Tối Đa" name="dienThoai">
                    {isUpdate ? (
                      <Input />
                    ) : (
                      <Text strong>
                        {dataCustomer.dienThoai === "string"
                          ? "-"
                          : dataCustomer.dienThoai}
                      </Text>
                    )}
                  </Form.Item>
                </Col>
              </Row>
              <Row>
                <Col span={12}>
                  <Form.Item label="Nhà Cung Cấp" name="dienThoai">
                    {isUpdate ? (
                      <Select allowClear>
                        {[
                          { Id: 1, Label: "text" },
                          { Id: 2, Label: "tex2" },
                        ] &&
                          [
                            { Id: 1, Label: "text" },
                            { Id: 2, Label: "tex2" },
                          ].map((item) => (
                            <Select.Option key={item.Id} values={item.Id}>
                              {item.Label}
                            </Select.Option>
                          ))}
                      </Select>
                    ) : (
                      <Text strong>
                        {dataCustomer.tenNhomKhachHang === "string"
                          ? "-"
                          : dataCustomer.tenNhomKhachHang}
                      </Text>
                    )}
                  </Form.Item>
                </Col>
                <Col span={12}>
                  <Form.Item label="Tình Trạng Sản Phẩm" name="dienThoai">
                    {isUpdate ? (
                      <Select allowClear>
                        {[
                          { Id: 1, Label: "text" },
                          { Id: 2, Label: "tex2" },
                        ] &&
                          [
                            { Id: 1, Label: "text" },
                            { Id: 2, Label: "tex2" },
                          ].map((item) => (
                            <Select.Option key={item.Id} values={item.Id}>
                              {item.Label}
                            </Select.Option>
                          ))}
                      </Select>
                    ) : (
                      <Text strong>
                        {dataCustomer.tenNhomKhachHang === "string"
                          ? "-"
                          : dataCustomer.tenNhomKhachHang}
                      </Text>
                    )}
                  </Form.Item>
                </Col>
              </Row>
            </Card>
          </Col>
        </Row>
        {/* Địa chỉ đăng ký giấy phép */}
        {/* <Row>
          <Col span={24}>
            <Card className="cardGroup">
              <div className="wrapperText">Địa chỉ đăng ký giấy phép</div>
              <Row>
                <Col span={12}>
                  <Form.Item
                    label="Địa chỉ đăng ký giấy phép"
                    name="diaChiDangKyGiayPhep"
                  >
                    {isUpdate ? (
                      <Input />
                    ) : (
                      <Text strong>
                        {dataCustomer.diaChiDangKyGiayPhep === "string"
                          ? "-"
                          : dataCustomer.diaChiDangKyGiayPhep}
                      </Text>
                    )}
                  </Form.Item>
                </Col>
                <Col span={12}>
                  <Form.Item label="Địa chỉ kinh doanh" name="diaChiKinhDoanh">
                    {isUpdate ? (
                      <Input />
                    ) : (
                      <Text strong>
                        {dataCustomer.diaChiKinhDoanh === "string"
                          ? "-"
                          : dataCustomer.diaChiKinhDoanh}
                      </Text>
                    )}
                  </Form.Item>
                </Col>
              </Row>
            </Card>
          </Col>
        </Row> */}
        {/* Thông tin doanh nghiệp */}
        {/* <Row>
          <Col span={24}>
            <Card className="cardGroup">
              <div className="wrapperText">Thông tin doanh nghiệp</div>
              <Row>
                <Col span={12}>
                  <Form.Item label="Mã số thuế" name="maSoThue">
                    {isUpdate ? (
                      <Input />
                    ) : (
                      <Text strong>
                        {dataCustomer.maSoThue === "string"
                          ? "-"
                          : dataCustomer.maSoThue}
                      </Text>
                    )}
                  </Form.Item>
                </Col>
                <Col span={12}>
                  <Form.Item label="Địa chỉ" name="diaChi">
                    {isUpdate ? (
                      <Input />
                    ) : (
                      <Text strong>
                        {dataCustomer.diaChi === "string"
                          ? "-"
                          : dataCustomer.diaChi}
                      </Text>
                    )}
                  </Form.Item>
                </Col>
              </Row>
            </Card>
          </Col>
        </Row> */}
        {/* Người liên hệ 1 */}
        {/* <Row>
          <Col span={24}>
            <Card className="cardGroup">
              <div className="wrapperText">Người liên hệ 1</div>
              <Row>
                <Col span={8}>
                  <Form.Item label="Tên người liên hệ 1" name="tenNguoiLienHe1">
                    {isUpdate ? (
                      <Input />
                    ) : (
                      <Text strong>
                        {dataCustomer.tenNguoiLienHe1 === "string"
                          ? "-"
                          : dataCustomer.tenNguoiLienHe1}
                      </Text>
                    )}
                  </Form.Item>
                </Col>
                <Col span={8}>
                  <Form.Item
                    label="Điện thoại người liên hệ 1"
                    name="dienThoaiNguoiLienHe1"
                  >
                    {isUpdate ? (
                      <Input />
                    ) : (
                      <Text strong>
                        {dataCustomer.dienThoaiNguoiLienHe1 === "string"
                          ? "-"
                          : dataCustomer.dienThoaiNguoiLienHe1}
                      </Text>
                    )}
                  </Form.Item>
                </Col>
                <Col span={8}>
                  <Form.Item
                    label="Chức vụ người liên hệ 1"
                    name="chucVuNguoiLienHe1"
                  >
                    {isUpdate ? (
                      <Input />
                    ) : (
                      <Text strong>
                        {dataCustomer.chucVuNguoiLienHe1 === "string"
                          ? "-"
                          : dataCustomer.chucVuNguoiLienHe1}
                      </Text>
                    )}
                  </Form.Item>
                </Col>
              </Row>
            </Card>
          </Col>
        </Row> */}
        {/* Người liên hệ 2 */}
        {/* <Row>
          <Col span={24}>
            <Card className="cardGroup">
              <div className="wrapperText">Người liên hệ 2</div>
              <Row>
                <Col span={8}>
                  <Form.Item label="Tên người liên hệ 2" name="tenNguoiLienHe2">
                    {isUpdate ? (
                      <Input />
                    ) : (
                      <Text strong>
                        {dataCustomer.tenNguoiLienHe2 === "string"
                          ? "-"
                          : dataCustomer.tenNguoiLienHe2}
                      </Text>
                    )}
                  </Form.Item>
                </Col>
                <Col span={8}>
                  <Form.Item
                    label="Điện thoại người liên hệ 2"
                    name="dienThoaiNguoiLienHe2"
                  >
                    {isUpdate ? (
                      <Input />
                    ) : (
                      <Text strong>
                        {dataCustomer.dienThoaiNguoiLienHe2 === "string"
                          ? "-"
                          : dataCustomer.dienThoaiNguoiLienHe2}
                      </Text>
                    )}
                  </Form.Item>
                </Col>
                <Col span={8}>
                  <Form.Item
                    label="Chức vụ người liên hệ 2"
                    name="chucVuNguoiLienHe2"
                  >
                    {isUpdate ? (
                      <Input />
                    ) : (
                      <Text strong>
                        {dataCustomer.chucVuNguoiLienHe2 === "string"
                          ? "-"
                          : dataCustomer.chucVuNguoiLienHe2}
                      </Text>
                    )}
                  </Form.Item>
                </Col>
              </Row>
            </Card>
          </Col>
        </Row> */}
      </Form>
    );
  }, [dataCustomer, form, btnEdit, id, isUpdate, onFinish, dataKH]);

  return (
    <div className="main__application">
      {loading ? <Loading /> : renderForm}
    </div>
  );
};

ThongTinKhachHang.propTypes = {};

export default ThongTinKhachHang;
