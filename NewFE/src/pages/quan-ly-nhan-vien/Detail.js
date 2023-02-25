import {
  Button,
  Card,
  Col,
  DatePicker,
  Form,
  Input,
  message,
  Radio,
  Row,
  Select,
  Typography,
} from "antd";
import Loading from "components/Loading/Loading";
import HeaderPage from "pages/home/header-page";
import React, { useCallback, useEffect, useMemo, useState } from "react";
import { useParams } from "react-router-dom";
import {
  getAllPhongBan,
  getNhanVienById,
  updateNhanVien,
} from "services/apartment-manage";
import { getProductDetails } from "services/Products";
import { SaveOutlined } from "@ant-design/icons";
import { SAVE_SUCCESS } from "utils/common/messageContants";
import TabsSection from "common/components/TabsSection/TabsSection";
import LyLichKhoaHoc from "./components/LyLichKhoaHoc";
import DaoTao from "./components/DaoTao";
import LichSuHoatDong from "./components/LichSuHoatDong";
import ButtonBack from "common/components/Buttons/ButtonBack";
import PageWrapper from "components/Layout/PageWrapper";

const RowCustom = ({ span = 20, children }) => {
  return (
    <Row>
      <Col span={span}>{children}</Col>
    </Row>
  );
};
function Detail(props) {
  let { id } = useParams();
  const [dataDetail, setDataDetail] = useState({});
  const [loading, setLoading] = useState(false);
  const [isUpdate, setIsUpdate] = useState(false);
  const [khoaPhongBan, setKhoaPhongBan] = useState([]);
  const [value, setValue] = useState(1);
  const [form] = Form.useForm();

  const { Text } = Typography;
  const styleItem = {
    marginBottom: "15px",
  };

  const styleButton = {
    marginBottom: "5px",
    display: "flex",
    alignItems: "center",
    justifyContent: "end",
  };
  const getDataDetail = useCallback(async () => {
    setLoading(true);
    let result = await getProductDetails(id);
    if (result.isSuccess) {
      setDataDetail(result.data);
      form.setFieldsValue(result.data);
    }
    setLoading(false);
  }, [form, id]);

  useEffect(() => {
    // handleKhoaPhongBan();
    if (id) {
      getDataDetail();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [id]);

  const handleKhoaPhongBan = async () => {
    const result = await getAllPhongBan();
    if (result.isSuccess) {
      setKhoaPhongBan(result.data.phongBanNames);
    }
  };
  const handleUpdateNhanVien = async (values) => {
    const result = await updateNhanVien(values);
    if (result.isSuccess) {
      message.success(SAVE_SUCCESS);
      getDataDetail();
    } else {
      form.resetFields();
      message.error(result.message);
    }
  };

  const onFinish = (values) => {
    if (id) {
      handleUpdateNhanVien(values);
    }
  };
  const handleUpdate = () => {
    return (
      <div className="groupbtn" style={styleButton}>
        <ButtonBack url="/quan-ly-nhan-vien" />
      </div>
    );
  };

  const onChange = (e) => {
    setValue(e.target.value);
  };

  const renderTitle = useMemo(() => {
    if (dataDetail && dataDetail.tenNhanVien !== undefined) {
      return (
        <div style={{ display: "flex", justifyContent: "space-between" }}>
          <div>{"Thông tin nhân viên " + dataDetail.tenNhanVien}</div>
          <div span={9}>
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
                onClick={(values) => handleUpdateNhanVien(values)}
              >
                Lưu
              </Button>
            )}
          </div>
        </div>
      );
    } else {
      return "";
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [dataDetail, isUpdate]);
  const items = [
    { label: <>Lý lịch khoa học</>, key: "1", children: <LyLichKhoaHoc /> },
    { label: <>Đào tạo</>, key: "2", children: <DaoTao /> },
    { label: <>Lịch sử hoạt động</>, key: "3", children: <LichSuHoatDong /> },
  ];

  const onChangeDate = (date, dateString) => {
    console.log(date, dateString);
  };

  return (
    <div className="form-detail-nhan-su">
      <HeaderPage title="CHI TIẾT NHÂN VIÊN " actions={handleUpdate} />
      <div className="main__application">
        <PageWrapper>
          <Row>
            <Col span={9}>
              {loading ? (
                <Loading />
              ) : (
                <Card title={renderTitle}>
                  <Form
                    id="myForm"
                    form={form}
                    labelAlign="left"
                    labelCol={{ span: 11 }}
                    wrapperCol={{ span: 20 }}
                    onFinish={onFinish}
                  >
                    <RowCustom span={20}>
                      <Form.Item
                        style={styleItem}
                        hidden={true}
                        name="id"
                      ></Form.Item>
                      <Form.Item
                        style={styleItem}
                        label="Tên nhân viên: "
                        name="tenNhanVien"
                      >
                        {isUpdate ? (
                          <Input />
                        ) : (
                          <Text strong>{dataDetail.tenNhanVien}</Text>
                        )}
                      </Form.Item>
                    </RowCustom>
                    <RowCustom>
                      <Form.Item
                        style={styleItem}
                        label="Mã nhân viên"
                        name="maNhanVien"
                      >
                        {isUpdate ? (
                          <Input />
                        ) : (
                          <Text strong>{dataDetail.maNhanVien}</Text>
                        )}
                      </Form.Item>
                    </RowCustom>
                    <RowCustom>
                      <Form.Item
                        style={styleItem}
                        label="Địa chỉ: "
                        name="diaChi"
                      >
                        {isUpdate ? (
                          <Input />
                        ) : (
                          <Text strong>{dataDetail.diaChi}</Text>
                        )}
                      </Form.Item>
                    </RowCustom>
                    <RowCustom>
                      <Form.Item
                        style={styleItem}
                        label="Ngày sinh: "
                        name="ngaySinh"
                      >
                        {isUpdate ? (
                          <DatePicker
                            onChange={onChangeDate}
                            placeholder="Chọn ngày"
                          />
                        ) : (
                          <Text strong>
                            {dataDetail.ngaySinh ? dataDetail.ngaySinh : "-"}
                          </Text>
                        )}
                      </Form.Item>
                    </RowCustom>
                    <RowCustom>
                      <Form.Item
                        style={styleItem}
                        label="Giới tính: "
                        name="gioiTinh"
                      >
                        <Radio.Group onChange={onChange} value={value}>
                          <Radio value={1}>Nam</Radio>
                          <Radio value={2}>Nữ</Radio>
                        </Radio.Group>
                      </Form.Item>
                    </RowCustom>
                    <RowCustom>
                      <Form.Item
                        style={styleItem}
                        label="Tên Khoa/Phòng/Ban: "
                        name="khoaPhongBanId"
                        rules={[{ message: "Xin Nhập Khoa/Phòng/Ban!" }]}
                      >
                        {isUpdate ? (
                          <Select>
                            {khoaPhongBan &&
                              khoaPhongBan.map((item) => {
                                return (
                                  <Select.Option key={item.id} value={item.id}>
                                    {item.tenKhoaPhongBan}
                                  </Select.Option>
                                );
                              })}
                          </Select>
                        ) : (
                          <Text strong>{dataDetail.tenKhoaPhongBan}</Text>
                        )}
                      </Form.Item>
                    </RowCustom>
                    <RowCustom>
                      <Form.Item
                        style={styleItem}
                        label="Chức vụ: "
                        name="chucVu"
                      >
                        {isUpdate ? (
                          <Input />
                        ) : (
                          <Text strong>{dataDetail.chucVu}</Text>
                        )}
                      </Form.Item>
                    </RowCustom>
                    <RowCustom>
                      <Form.Item
                        style={styleItem}
                        label="Điện thoại: "
                        name="dienThoai"
                      >
                        {isUpdate ? (
                          <Input />
                        ) : (
                          <Text strong>{dataDetail.dienThoai}</Text>
                        )}
                      </Form.Item>
                    </RowCustom>
                    <RowCustom>
                      <Form.Item
                        style={styleItem}
                        label="Tài khoản: "
                        name="userName"
                      >
                        {isUpdate ? (
                          <Input />
                        ) : (
                          <Text strong>{dataDetail.userName}</Text>
                        )}
                      </Form.Item>
                    </RowCustom>
                  </Form>
                </Card>
              )}
            </Col>
            <Col span={15}>
              <TabsSection items={items} />
            </Col>
          </Row>
        </PageWrapper>
      </div>
    </div>
  );
}

Detail.propTypes = {};

export default Detail;
