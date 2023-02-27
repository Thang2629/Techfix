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
  Checkbox,
  Tag,
} from "antd";
import Loading from "components/Loading/Loading";
import React, { useCallback, useEffect, useMemo, useState } from "react";
import { useParams } from "react-router-dom";
import {
  getAllCustomerGroupService,
  getCustomerByIdService,
  updateCustomerService,
} from "services/customer-manager";
import { getProductDetails } from "services/Products";
import { SaveOutlined } from "@ant-design/icons";
import "../style.less";
import { SAVE_ERROR, SAVE_SUCCESS } from "utils/common/messageContants";
import isEmpty from "lodash/isEmpty";
import TextArea from "antd/lib/input/TextArea";
import { updateProduct } from "services/Products";
import { getListManufacturers } from "services/Manufacturers";
import { getListCatagories } from "services/Categories";
import { getListProductUnits } from "services/ProductUnits";
import { getListProductConditions } from "services/ProductConditions";
import { getListSuppliers } from "services/Supplier";

const ThongTinSanPham = (props) => {
  const { id } = useParams();

  const [dataCustomer, setDataCustomer] = useState([]);
  // eslint-disable-next-line no-unused-vars
  const [dataKH, setDataKH] = useState([]);
  const [productDetails, setProductDetails] = useState({});
  const [categories, setCategories] = useState([]);
  const [manufacturers, setManufacturers] = useState([]);
  const [productUnits, setProductUnits] = useState([]);
  const [productConditions, setProductConditions] = useState([]);
  const [suppliers, setSuppliers] = useState([]);
  const [loading, setLoading] = useState(false);
  const [isUpdate, setIsUpdate] = useState(false);
  const [form] = Form.useForm();
  const { Text } = Typography;

  const getManufacturers = async () => {
    const response = await getListManufacturers();
    setManufacturers(response);
  };
  const getCatagories = async () => {
    const response = await getListCatagories();
    setCategories(response);
  };
  const getProductUnits = async () => {
    const response = await getListProductUnits();
    setProductUnits(response);
  };
  const getProductConditions = async () => {
    const response = await getListProductConditions();
    setProductConditions(response);
  };
  const getSuppliers = async () => {
    const response = await getListSuppliers();
    setSuppliers(response.Data);
  };

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
  const getProductDetail = async () => {
    const data = await getProductDetails(id);
    setProductDetails(data);
  };

  useEffect(() => {
    if (id) getProductDetail();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [id]);
  useEffect(() => {
    getManufacturers();
    getCatagories();
    getProductUnits();
    getProductConditions();
    getSuppliers();
  }, []);

  const onFinish = async (values) => {
    const data = await updateProduct(id, values);

    if (data.isSuccess) {
      message.success(SAVE_SUCCESS);
    } else {
      message.error(SAVE_ERROR);
    }
  };

  const btnEdit = useMemo(() => {
    return (
      <div style={{ position: "absolute", right: "0", top: "0" }}>
        {id && (
          <Button
            style={{ marginRight: "5px" }}
            size="small"
            type={isUpdate ? "default" : "primary"}
            onClick={() => setIsUpdate(!isUpdate)}
          >
            {isUpdate ? "Hủy" : "Chỉnh sửa"}
          </Button>
        )}

        {isUpdate && (
          <Button
            size="small"
            type="primary"
            key="submit"
            htmlType="submit"
            form="myForm"
            icon={<SaveOutlined />}
          >
            {id ? "Lưu" : "Tạo Mới"}
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
                <Col span={12}>
                  {id && (
                    <Form.Item
                      hidden={true}
                      label="Id"
                      name="Id"
                      value={productDetails.Id}
                    >
                      <Input />
                    </Form.Item>
                  )}
                  <Form.Item label="Tên Sản Phẩm" name="Name">
                    {isUpdate ? (
                      <Input />
                    ) : (
                      <Text strong>
                        {isEmpty(productDetails.Name)
                          ? "-"
                          : productDetails.Name}
                      </Text>
                    )}
                  </Form.Item>
                </Col>
              </Row>
              {/*  */}
              <Row>
                <Col span={12}>
                  <Form.Item label="Số Lượng" name="Quantity">
                    {isUpdate ? (
                      <Input />
                    ) : (
                      <Text strong>
                        {isEmpty(productDetails.Quantity)
                          ? "-"
                          : productDetails.Quantity}
                      </Text>
                    )}
                  </Form.Item>
                </Col>
                <Col span={12}>
                  <Form.Item label="Mã Sản Phẩm" name="Code">
                    {isUpdate ? (
                      <Input />
                    ) : (
                      <Text strong>
                        {isEmpty(productDetails.Code)
                          ? "-"
                          : productDetails.Code}
                      </Text>
                    )}
                  </Form.Item>
                </Col>
              </Row>
              <Row>
                <Col span={12}>
                  <Form.Item label="Đơn vị tính" name="ProductUnit">
                    {isUpdate ? (
                      <Select allowClear>
                        {productUnits &&
                          productUnits.map((item) => (
                            <Select.Option key={item.Id} values={item.Id}>
                              {item.Name}
                            </Select.Option>
                          ))}
                      </Select>
                    ) : (
                      <Text strong>
                        {isEmpty(productDetails.ProductUnit)
                          ? "-"
                          : productDetails.ProductUnit}
                      </Text>
                    )}
                  </Form.Item>
                </Col>
                <Col span={12}>
                  <Row>
                    <Col span={8}>
                      <Form.Item
                        label="Theo dõi tồn kho?"
                        name="IsInventoryTracking"
                        valuePropName="checked"
                      >
                        {isUpdate ? (
                          <Checkbox />
                        ) : (
                          <Text strong>
                            {isEmpty(productDetails.IsInventoryTracking) ? (
                              <Tag color="red">Không</Tag>
                            ) : (
                              <Tag color="blue">Có</Tag>
                            )}
                          </Text>
                        )}
                      </Form.Item>
                    </Col>
                    <Col span={8}>
                      <Form.Item
                        label="Cho phép bán âm?"
                        name="AllowNegativeSell"
                        valuePropName="checked"
                      >
                        {isUpdate ? (
                          <Checkbox />
                        ) : (
                          <Text strong>
                            {isEmpty(productDetails.AllowNegativeSell) ? (
                              <Tag color="red">Không</Tag>
                            ) : (
                              <Tag color="blue">Có</Tag>
                            )}
                          </Text>
                        )}
                      </Form.Item>
                    </Col>
                  </Row>
                </Col>
              </Row>
              <Row>
                <Col span={12}>
                  <Form.Item label="Giá Nhập" name="FakePrice">
                    {isUpdate ? (
                      <Input />
                    ) : (
                      <Text strong>
                        {isEmpty(productDetails.FakePrice)
                          ? "-"
                          : productDetails.FakePrice}
                      </Text>
                    )}
                  </Form.Item>
                </Col>
                <Col span={12}>
                  <Form.Item label="Giá Web" name="WebPrice">
                    {isUpdate ? (
                      <Input />
                    ) : (
                      <Text strong>
                        {isEmpty(productDetails.WebPrice)
                          ? "-"
                          : productDetails.WebPrice}
                      </Text>
                    )}
                  </Form.Item>
                </Col>
              </Row>
              <Row>
                <Col span={12}>
                  <Form.Item label="Bảo Hành NCC" name="Warranty">
                    {isUpdate ? (
                      <Input />
                    ) : (
                      <Text strong>
                        {isEmpty(productDetails.Warranty)
                          ? "-"
                          : productDetails.Warranty}
                      </Text>
                    )}
                  </Form.Item>
                </Col>
                <Col span={12}>
                  <Form.Item label="Giá Vốn" name="OriginalPrice">
                    {isUpdate ? (
                      <Input />
                    ) : (
                      <Text strong>
                        {isEmpty(productDetails.OriginalPrice)
                          ? "-"
                          : productDetails.OriginalPrice}
                      </Text>
                    )}
                  </Form.Item>
                </Col>
              </Row>
              <Row>
                <Col span={12}>
                  <Form.Item label="Danh Mục" name="CategoryId">
                    {isUpdate ? (
                      <Select allowClear>
                        {categories &&
                          categories.map((item) => (
                            <Select.Option key={item.Id} values={item.Id}>
                              {item.Name}
                            </Select.Option>
                          ))}
                      </Select>
                    ) : (
                      <Text strong>
                        <Text strong>
                          {isEmpty(productDetails.CategoryId)
                            ? "-"
                            : productDetails.CategoryName}
                        </Text>
                      </Text>
                    )}
                  </Form.Item>
                </Col>
                <Col span={12}>
                  <Form.Item label="Nhà Sản Xuất" name="ManufacturerId">
                    {isUpdate ? (
                      <Select allowClear>
                        {manufacturers &&
                          manufacturers.map((item) => (
                            <Select.Option key={item.Id} values={item.Id}>
                              {item.Name}
                            </Select.Option>
                          ))}
                      </Select>
                    ) : (
                      <Text strong>
                        {isEmpty(productDetails.ManufacturerId)
                          ? "-"
                          : productDetails.ManufacturerName}
                      </Text>
                    )}
                  </Form.Item>
                </Col>
              </Row>
              <Row>
                <Col span={12}>
                  <Form.Item label="Định Mức Tối Thiểu" name="MinimumNorm">
                    {isUpdate ? (
                      <Input />
                    ) : (
                      <Text strong>
                        {isEmpty(productDetails.MinimumNorm)
                          ? "-"
                          : productDetails.MinimumNorm}
                      </Text>
                    )}
                  </Form.Item>
                </Col>
                <Col span={12}>
                  <Form.Item label="Định Mức Tối Đa" name="MaximumNorm">
                    {isUpdate ? (
                      <Input />
                    ) : (
                      <Text strong>
                        {isEmpty(productDetails.MaximumNorm)
                          ? "-"
                          : productDetails.MaximumNorm}
                      </Text>
                    )}
                  </Form.Item>
                </Col>
              </Row>
              <Row>
                <Col span={12}>
                  <Form.Item label="Nhà Cung Cấp" name="SupplierId">
                    {isUpdate ? (
                      <Select allowClear>
                        {suppliers &&
                          suppliers.map((item) => (
                            <Select.Option key={item.Id} values={item.Id}>
                              {item.Name}
                            </Select.Option>
                          ))}
                      </Select>
                    ) : (
                      <Text strong>
                        {isEmpty(productDetails.SupplierId)
                          ? "-"
                          : productDetails.SupplierName}
                      </Text>
                    )}
                  </Form.Item>
                </Col>
                <Col span={12}>
                  <Form.Item
                    label="Tình Trạng Sản Phẩm"
                    name="ProductConditionId"
                  >
                    {isUpdate ? (
                      <Select allowClear>
                        {productConditions &&
                          productConditions.map((item) => (
                            <Select.Option key={item.Id} values={item.Id}>
                              {item.Name}
                            </Select.Option>
                          ))}
                      </Select>
                    ) : (
                      <Text strong>
                        {isEmpty(productDetails.ProductConditionId)
                          ? "-"
                          : productDetails.ProductConditionName}
                      </Text>
                    )}
                  </Form.Item>
                </Col>
              </Row>
            </Card>
          </Col>
        </Row>
      </Form>
    );
  }, [dataCustomer, form, btnEdit, id, isUpdate, onFinish, dataKH]);

  return (
    <div className="main__application">
      {loading ? <Loading /> : renderForm}
    </div>
  );
};

ThongTinSanPham.propTypes = {};

export default ThongTinSanPham;
