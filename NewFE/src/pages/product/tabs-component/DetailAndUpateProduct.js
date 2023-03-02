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
  Popconfirm,
  InputNumber,
} from "antd";
import Loading from "components/Loading/Loading";
import React, {
  useCallback,
  useEffect,
  useMemo,
  useState,
  useRef,
} from "react";
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
import {
  getListManufacturers,
  createManufacturer,
} from "services/Manufacturers";
import { getListCategories, createCategory } from "services/Categories";
import {
  getListProductUnits,
  updateProductUnit,
  createProductUnit,
} from "services/ProductUnits";
import {
  getListProductConditions,
  createProductCondition,
} from "services/ProductConditions";
import { getListSuppliers } from "services/Supplier";
import { PlusSquareOutlined } from "@ant-design/icons";
import CreateDialog from "./components/CreateDialog";
import PageWrapper from "components/Layout/PageWrapper";
import Grid from "components/Grid";
import { ButtonDelete, PrimaryButton } from "common/components/Buttons";
import { formatNumber } from "utils/formatNumber";

const CREATE_TYPE = {
  PRODUCTS_UNIT: "PRODUCTS_UNIT",
  CATEGORY: "CATEGORY",
  MANUFACTURER: "MANUFACTURER",
  PRODUCTS_CONDITION: "PRODUCTS_CONDITION",
};
const ThongTinSanPham = (props) => {
  const { id } = useParams();

  const [dataCustomer, setDataCustomer] = useState([]);
  // eslint-disable-next-line no-unused-vars
  const [isOpen, setIsopen] = useState(false);
  const [value, setValue] = useState("");
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
  const [formTab] = Form.useForm();
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
    form.setFieldsValue(data);
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

  const onClickAddButton = (type) => {
    setTypeCreate(type);
    debugger;
    // renderBodyByType(type);
    setIsopen(true);
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
                  <Form.Item label="Đơn vị tính" name="ProductUnit">
                    {isUpdate ? (
                      <div style={{ display: "flex" }}>
                        <Select allowClear>
                          {productUnits &&
                            productUnits.map((item) => (
                              <Select.Option key={item.Id} values={item.Id}>
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
                      <Text strong>{productDetails?.ProductUnit || ""}</Text>
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
                        <Select allowClear>
                          {categories &&
                            categories.map((item) => (
                              <Select.Option key={item.Id} values={item.Id}>
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
                        <Select allowClear>
                          {manufacturers &&
                            manufacturers.map((item) => (
                              <Select.Option key={item.Id} values={item.Id}>
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
                        <Select allowClear>
                          {suppliers &&
                            suppliers.map((item) => (
                              <Select.Option key={item.Id} values={item.Id}>
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
                        <Select allowClear>
                          {productConditions &&
                            productConditions.map((item) => (
                              <Select.Option key={item.Id} values={item.Id}>
                                {item.Name}
                              </Select.Option>
                            ))}
                        </Select>
                        <Button
                          type="primary"
                          icon={<PlusSquareOutlined />}
                          onClick={() =>
                            onClickAddButton(CREATE_TYPE.PRODUCTS_CONDITION)
                          }
                        />
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
  }, [dataCustomer, form, btnEdit, id, isUpdate, onFinish, dataKH]);
  // grid
  const dataUnit = [
    { Id: 1, Code: "Code1", Name: "Name1" },
    { Id: 2, Code: "Code2", Name: "Name1" },
  ];
  const [data, setData] = useState(dataUnit);
  const [editingKey, setEditingKey] = useState("");
  const isEditing = (record) => record.Id === editingKey;
  const edit = (record) => {
    form.setFieldsValue({
      Name: "",
      ...record,
    });
    setEditingKey(record.Id);
  };
  const cancel = () => {
    setEditingKey("");
  };
  const save = async (record) => {
    try {
      const row = await form.validateFields();
      const value = inputTable.current.input.value;

      const newData = [...productUnits];
      const response = await updateProductUnit(id, value);
      debugger;
      const index = newData.findIndex((item) => record.id === item.Id);

      if (index > -1) {
        const item = newData[index];
        newData.splice(index, 1, {
          ...item,
          ...row,
        });
        setData(newData);
        setEditingKey("");
      } else {
        newData.push(row);
        setData(newData);
        setEditingKey("");
      }
    } catch (errInfo) {
      console.log("Validate Failed:", errInfo);
    }
  };

  const bodyDialog1 = () => {
    debugger;
    switch (typeCreate) {
      case CREATE_TYPE.CATEGORY:
        return renderBodyByType(typeCreate, categories);
      case CREATE_TYPE.MANUFACTURER:
        return renderBodyByType(typeCreate, manufacturers);
      case CREATE_TYPE.PRODUCTS_CONDITION:
        return renderBodyByType(typeCreate, productConditions);
      case CREATE_TYPE.PRODUCTS_UNIT:
        return renderBodyByType(typeCreate, productUnits);
      default:
        return;
    }
  };
  const renderBodyByType = (type, listData) => {
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
  const testColumn = [
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
              Save
            </Typography.Link>
            <Popconfirm title="Sure to cancel?" onConfirm={cancel}>
              <a>Cancel</a>
            </Popconfirm>
          </span>
        ) : (
          <Typography.Link
            disabled={editingKey !== ""}
            onClick={() => edit(record)}
          >
            Edit
          </Typography.Link>
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
  const mergedColumns = testColumn.map((col) => {
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
  const bodyDialog2 = () => {
    debugger;
    switch (typeCreate) {
      case CREATE_TYPE.CATEGORY:
        return renderCreateFormByType("CategoryName");
      case CREATE_TYPE.MANUFACTURER:
        return renderCreateFormByType("ManufacturerName");
      case CREATE_TYPE.PRODUCTS_CONDITION:
        return renderCreateFormByType("ProductConditionName");
      case CREATE_TYPE.PRODUCTS_UNIT:
        return renderCreateFormByType("ProductUnit");
      default:
        return;
    }
  };
  const onCreateByType = (values, typeCreate) => {
    debugger;
  };
  const renderCreateFormByType = (typeCreate) => {
    return (
      <PageWrapper>
        <Form
          form={formTab}
          id="tabForm1"
          onFinish={(values, typeCreate) => onCreateByType(values, typeCreate)}
        >
          <Row>
            <Col
              span={12}
              style={{
                display: "flex",
                alignItems: "center",
                gap: "0.5rem",
              }}
            >
              <Form.Item label="" name={typeCreate}>
                <Input placeholder="Nhập tên" />
              </Form.Item>
              <Button
                size="medium"
                type="primary"
                key="submit"
                htmlType="submit"
                form="tabForm1"
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
        bodyDialog={bodyDialog1}
        bodyDialog2={bodyDialog2}
      />
    </div>
  );
};

ThongTinSanPham.propTypes = {};

export default ThongTinSanPham;
