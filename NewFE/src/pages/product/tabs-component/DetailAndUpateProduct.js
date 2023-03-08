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
  InputNumber,
} from "antd";
import Loading from "components/Loading/Loading";
import React, { useEffect, useMemo, useState, useRef } from "react";
import { useParams } from "react-router-dom";

import { getProductDetails } from "services/Products";
import { SaveOutlined } from "@ant-design/icons";
import "../style.less";
import {
  DELETE_SUCCESS,
  SAVE_SUCCESS,
  CREATE_SUCCESS,
} from "utils/common/messageContants";
import { updateProduct } from "services/Products";
import {
  getListManufacturers,
  createManufacturer,
  updateManufacturer,
  deleteManufacturer,
} from "services/Manufacturers";
import {
  getListCategories,
  createCategory,
  updateCategory,
  deleteCategory,
} from "services/Categories";
import {
  getListProductUnits,
  updateProductUnit,
  createProductUnit,
  deleteProductUnit,
} from "services/ProductUnits";
import {
  getListProductConditions,
  createProductCondition,
  updateProductCondition,
  deleteProductCondition,
} from "services/ProductConditions";
import { getListSuppliers } from "services/Supplier";
import { PlusSquareOutlined } from "@ant-design/icons";
import CreateDialog from "./components/CreateDialog";
import PageWrapper from "components/Layout/PageWrapper";
import Grid from "components/Grid";
import { formatNumber } from "utils/formatNumber";

const CREATE_TYPE = {
  PRODUCTS_UNIT: "PRODUCTS_UNIT",
  CATEGORY: "CATEGORY",
  MANUFACTURER: "MANUFACTURER",
  PRODUCTS_CONDITION: "PRODUCTS_CONDITION",
};

const ThongTinSanPham = (props) => {
  const { id } = useParams();

  // eslint-disable-next-line no-unused-vars
  const [isOpen, setIsopen] = useState(false);
  const [productDetails, setProductDetails] = useState({});
  const [categories, setCategories] = useState([]);
  const [manufacturers, setManufacturers] = useState([]);
  const [productUnits, setProductUnits] = useState([]);
  const [productConditions, setProductConditions] = useState([]);
  const [suppliers, setSuppliers] = useState([]);
  const [loading, setLoading] = useState(false);
  const [isUpdate, setIsUpdate] = useState(false);
  const [submitForm] = Form.useForm();
  const [formTab] = Form.useForm();
  const [formTabCreate] = Form.useForm();
  const [typeCreate, setTypeCreate] = useState("");
  const { Text } = Typography;

  const getManufacturers = async () => {
    const response = await getListManufacturers();
    setManufacturers(response);
  };
  const getCatagories = async () => {
    const response = await getListCategories();
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

  const getProductDetail = async () => {
    const data = await getProductDetails(id);
    submitForm.setFieldsValue({ ...data });
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
    const response = await updateProduct(id, values);

    if (response.Success) {
      message.success(SAVE_SUCCESS);
    } else {
      message.error(response.Message);
    }
  };
  const onSelectFieldChange = (value, fieldName) => {
    submitForm.setFieldsValue({ [`${fieldName}`]: value });
  };
  const onClickAddButton = (type) => {
    setTypeCreate(type);
    // renderBodyByType(type);
    setIsopen(true);
  };
  const onClickUpdate = (state) => {
    setIsUpdate(state);
    submitForm.setFieldsValue({ ...productDetails });
  };
  const btnEdit = useMemo(() => {
    return (
      <div style={{ position: "absolute", right: "0", top: "0" }}>
        {id && (
          <Button
            style={{ marginRight: "5px" }}
            size="small"
            type={isUpdate ? "default" : "primary"}
            onClick={() => onClickUpdate(!isUpdate)}
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
        form={submitForm}
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
                      value={productDetails?.Id}
                    >
                      <Input />
                    </Form.Item>
                  )}
                  <Form.Item label="Tên Sản Phẩm" name="Name">
                    {isUpdate ? (
                      <Input />
                    ) : (
                      <Text strong>{productDetails?.Name || "-"}</Text>
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
                        {formatNumber(productDetails?.Quantity) || "-"}
                      </Text>
                    )}
                  </Form.Item>
                </Col>
                <Col span={12}>
                  <Form.Item label="Mã Sản Phẩm" name="Code">
                    {isUpdate ? (
                      <Input />
                    ) : (
                      <Text strong>{productDetails?.Code || ""}</Text>
                    )}
                  </Form.Item>
                </Col>
              </Row>
              <Row>
                <Col span={12}>
                  <Form.Item label="Đơn vị tính" name="ProductUnitId">
                    {isUpdate ? (
                      <div style={{ display: "flex" }}>
                        <Select
                          onChange={(value) =>
                            onSelectFieldChange(value, "ProductUnitId")
                          }
                          allowClear
                        >
                          {productUnits &&
                            productUnits.map((item) => (
                              <Select.Option
                                key={`item_${item.Id}`}
                                value={item.Id}
                              >
                                {item.Name}
                              </Select.Option>
                            ))}
                        </Select>
                        <Button
                          type="primary"
                          icon={<PlusSquareOutlined />}
                          onClick={() =>
                            onClickAddButton(CREATE_TYPE.PRODUCTS_UNIT)
                          }
                        />
                      </div>
                    ) : (
                      <Text strong>
                        {productDetails?.ProductUnitName || ""}
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
                            {productDetails?.IsInventoryTracking ? (
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
                            {productDetails?.AllowNegativeSell ? (
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
                        {formatNumber(productDetails?.FakePrice) || "-"}
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
                        {formatNumber(productDetails?.WebPrice) || "-"}
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
                      <Text strong>{productDetails?.Warranty || ""}</Text>
                    )}
                  </Form.Item>
                </Col>
                <Col span={12}>
                  <Form.Item label="Giá Vốn" name="OriginalPrice">
                    {isUpdate ? (
                      <Input />
                    ) : (
                      <Text strong>
                        {formatNumber(productDetails?.OriginalPrice) || ""}
                      </Text>
                    )}
                  </Form.Item>
                </Col>
              </Row>
              <Row>
                <Col span={12}>
                  <Form.Item label="Danh Mục" name="CategoryId">
                    {isUpdate ? (
                      <div style={{ display: "flex" }}>
                        <Select
                          onChange={(value) =>
                            onSelectFieldChange(value, "CategoryId")
                          }
                          allowClear
                        >
                          {categories &&
                            categories.map((item) => (
                              <Select.Option key={item.Id} value={item.Id}>
                                {item.Name}
                              </Select.Option>
                            ))}
                        </Select>
                        <Button
                          type="primary"
                          icon={<PlusSquareOutlined />}
                          onClick={() => onClickAddButton(CREATE_TYPE.CATEGORY)}
                        />
                      </div>
                    ) : (
                      <Text strong>
                        <Text strong>{productDetails?.CategoryName || ""}</Text>
                      </Text>
                    )}
                  </Form.Item>
                </Col>
                <Col span={12}>
                  <Form.Item label="Nhà Sản Xuất" name="ManufacturerId">
                    {isUpdate ? (
                      <div style={{ display: "flex" }}>
                        <Select
                          onChange={(value) =>
                            onSelectFieldChange(value, "ManufacturerId")
                          }
                          allowClear
                        >
                          {manufacturers &&
                            manufacturers.map((item) => (
                              <Select.Option key={item.Id} value={item.Id}>
                                {item.Name}
                              </Select.Option>
                            ))}
                        </Select>
                        <Button
                          type="primary"
                          icon={<PlusSquareOutlined />}
                          onClick={() =>
                            onClickAddButton(CREATE_TYPE.MANUFACTURER)
                          }
                        />
                      </div>
                    ) : (
                      <Text strong>
                        {productDetails?.ManufacturerName || ""}
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
                        {formatNumber(productDetails?.MinimumNorm) || ""}
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
                        {formatNumber(productDetails?.MaximumNorm) || ""}
                      </Text>
                    )}
                  </Form.Item>
                </Col>
              </Row>
              <Row>
                <Col span={12}>
                  <Form.Item label="Nhà Cung Cấp" name="SupplierId">
                    {isUpdate ? (
                      <div style={{ display: "flex" }}>
                        <Select
                          onChange={(value) =>
                            onSelectFieldChange(value, "SupplierId")
                          }
                          allowClear
                        >
                          {suppliers &&
                            suppliers.map((item) => (
                              <Select.Option key={item.Id} value={item.Id}>
                                {item.Name}
                              </Select.Option>
                            ))}
                        </Select>
                      </div>
                    ) : (
                      <Text strong>{productDetails?.SupplierName || ""}</Text>
                    )}
                  </Form.Item>
                </Col>
                <Col span={12}>
                  <Form.Item
                    label="Tình Trạng Sản Phẩm"
                    name="ProductConditionId"
                  >
                    {isUpdate ? (
                      <div style={{ display: "flex" }}>
                        <Select
                          onChange={(value) =>
                            onSelectFieldChange(value, "ProductConditionId")
                          }
                          allowClear
                        >
                          {productConditions &&
                            productConditions.map((item) => (
                              <Select.Option key={item.Id} values={item.Id}>
                                {item.Name}
                              </Select.Option>
                            ))}
                        </Select>
                      </div>
                    ) : (
                      <Text strong>
                        {productDetails?.ProductConditionName || ""}
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
  }, [submitForm, btnEdit, id, isUpdate, onFinish]);
  // grid

  const [editingKey, setEditingKey] = useState("");
  const isEditing = (record) => record.Id === editingKey;
  const edit = (record) => {
    formTab.setFieldsValue({
      ...record,
    });
    setEditingKey(record.Id);
  };
  const onDelete = async (record) => {
    const response = await deleteAction(record.Id);
    if (response.Success) {
      message.success(DELETE_SUCCESS);
    } else {
      message.error(response.Message);
    }
  };

  const cancel = () => {
    setEditingKey("");
  };
  const save = async (record) => {
    try {
      const value = JSON.stringify(inputTable.current.input.value);
      const response = await updateAction(record.Id, value);
      if (response.Success) {
        message.success(CREATE_SUCCESS);
      } else {
        message.error(response.Message);
      }
    } catch (errInfo) {
      console.log("Validate Failed:", errInfo);
    }
  };
  const updateAction = (itemId, value) => {
    switch (typeCreate) {
      case CREATE_TYPE.CATEGORY:
        return updateCategory(itemId, value);
      case CREATE_TYPE.MANUFACTURER:
        return updateManufacturer(itemId, value);
      case CREATE_TYPE.PRODUCTS_CONDITION:
        return updateProductCondition(itemId, value);
      case CREATE_TYPE.PRODUCTS_UNIT:
        return updateProductUnit(itemId, value);
      default:
        return;
    }
  };
  const deleteAction = () => {
    switch (typeCreate) {
      case CREATE_TYPE.CATEGORY:
        return deleteCategory(id);
      case CREATE_TYPE.MANUFACTURER:
        return deleteManufacturer(id);
      case CREATE_TYPE.PRODUCTS_CONDITION:
        return deleteProductCondition(id);
      case CREATE_TYPE.PRODUCTS_UNIT:
        return deleteProductUnit(id);
      default:
        return;
    }
  };

  const renderTablist = () => {
    switch (typeCreate) {
      case CREATE_TYPE.CATEGORY:
        return renderTabListByType(typeCreate, categories);
      case CREATE_TYPE.MANUFACTURER:
        return renderTabListByType(typeCreate, manufacturers);
      case CREATE_TYPE.PRODUCTS_CONDITION:
        return renderTabListByType(typeCreate, productConditions);
      case CREATE_TYPE.PRODUCTS_UNIT:
        return renderTabListByType(typeCreate, productUnits);
      default:
        return;
    }
  };
  const renderTabListByType = (type, listData) => {
    return (
      <PageWrapper>
        <Form form={formTab} component={false}>
          <Grid
            data={listData}
            components={{
              body: {
                cell: EditableCell,
              },
            }}
            columns={mergedColumns}
          />
        </Form>
      </PageWrapper>
    );
  };
  const tabColumns = [
    {
      title: "STT",
      dataIndex: "",
      width: "10%",
      render: (row, _, index) => {
        return <div>{index + 1}</div>;
      },
    },
    {
      title: "Danh Sách",
      dataIndex: "Name",
      width: "25%",
      editable: true,
    },
    {
      title: "",
      dataIndex: "operation",
      width: "40%",
      render: (_, record) => {
        const editable = isEditing(record);
        return editable ? (
          <span>
            <Typography.Link
              onClick={() => save(record)}
              style={{
                marginRight: 8,
              }}
            >
              Lưu
            </Typography.Link>
            <Typography.Link
              onClick={cancel}
              style={{
                marginRight: 8,
              }}
            >
              Hủy
            </Typography.Link>
          </span>
        ) : (
          <span>
            <Typography.Link
              disabled={editingKey !== ""}
              onClick={() => edit(record)}
              style={{
                marginRight: 8,
              }}
            >
              Chỉnh Sửa
            </Typography.Link>
            <Typography.Link
              disabled={editingKey !== ""}
              onClick={() => onDelete(record)}
              style={{
                marginRight: 8,
              }}
            >
              Xóa
            </Typography.Link>
          </span>
        );
      },
    },
  ];
  const inputTable = useRef();
  const EditableCell = ({
    editing,
    dataIndex,
    title,
    inputType,
    record,
    index,
    children,
    ...restProps
  }) => {
    const inputNode =
      inputType === "number" ? (
        <InputNumber ref={inputTable} />
      ) : (
        <Input ref={inputTable} />
      );
    return (
      <td {...restProps}>
        {editing ? (
          <Form.Item
            name={dataIndex}
            style={{
              margin: 0,
            }}
            rules={[
              {
                required: true,
                message: `Please Input ${title}!`,
              },
            ]}
          >
            {inputNode}
          </Form.Item>
        ) : (
          children
        )}
      </td>
    );
  };
  const mergedColumns = tabColumns.map((col) => {
    if (!col.editable) {
      return col;
    }
    return {
      ...col,
      onCell: (record) => ({
        record,
        inputType: col.dataIndex === "age" ? "number" : "text",
        dataIndex: col.dataIndex,
        title: col.title,
        editing: isEditing(record),
      }),
    };
  });
  // grid
  const renderTabCreate = () => {
    switch (typeCreate) {
      case CREATE_TYPE.CATEGORY:
        return renderCreateFormByType(createCategory);
      case CREATE_TYPE.MANUFACTURER:
        return renderCreateFormByType(createManufacturer);
      case CREATE_TYPE.PRODUCTS_CONDITION:
        return renderCreateFormByType(createProductCondition);
      case CREATE_TYPE.PRODUCTS_UNIT:
        return renderCreateFormByType(createProductUnit);
      default:
        return;
    }
  };
  const onCreateByType = async (values, action) => {
    const requestValue = JSON.stringify(values.Name);
    const response = await action(requestValue);
    if (response.Success) {
      message.success(SAVE_SUCCESS);
    } else {
      message.error(response.Message);
    }
  };
  const renderCreateFormByType = (action) => {
    return (
      <PageWrapper>
        <Form
          form={formTabCreate}
          id="formTabCreate"
          onFinish={(values) => onCreateByType(values, action)}
        >
          <Row gutter={16}>
            <Col span={18}>
              <Form.Item label="" name="Name">
                <Input placeholder="Nhập tên" />
              </Form.Item>
            </Col>
            <Col span={6}>
              <Button
                size="medium"
                type="primary"
                key="submit"
                htmlType="submit"
                form="formTabCreate"
                icon={<SaveOutlined />}
                style={{ alignSelf: "flex-start" }}
              >
                Lưu
              </Button>
            </Col>
          </Row>
        </Form>
      </PageWrapper>
    );
  };
  return (
    <div className="main__application">
      {loading ? <Loading /> : renderForm}
      <CreateDialog
        isOpen={isOpen}
        handleClosed={() => setIsopen(false)}
        title={`Thông tin`}
        bodyDialog={renderTablist}
        bodyDialog2={renderTabCreate}
      />
    </div>
  );
};

ThongTinSanPham.propTypes = {};

export default ThongTinSanPham;
