import React, { useEffect, useCallback, useState } from "react";
import PropTypes from "prop-types";
import {
  Button,
  Form,
  Input,
  Row,
  Col,
  Space,
  DatePicker,
  InputNumber,
  Checkbox,
  message,
  Spin,
} from "antd";
import { SaveOutlined } from "@ant-design/icons";
import { useDispatch, useSelector } from "react-redux";
import * as selectors from "redux/quyTrinh/selectors";
import * as actions from "redux/quyTrinh/actions";
import { getDateFormat, convertToISOTime } from "utils/formatDate";
import * as services from "services/sample";
import "./ThongTinMau.less";

const initialValues = {
  dangBaoChe: "",
  deadline: null,
  dieuKienBaoQuan: "",
  heSoLamGap: 0,
  hoatChat: "",
  khoiLuongMau: "",
  lamGap: false,
  lyDoKhongDuyet: "",
  maSoMau: "",
  ngayHetHan: null,
  ngayNhanMau: null,
  ngayNhapKhau: null,
  ngaySanXuat: null,
  ngayTra: null,
  nguoiGiaoMau: "",
  nguoiLayMau: "",
  nguoiNhanMau: "",
  nhaPhanPhoi: "",
  noiGuiMau: "",
  noiLayMau: "",
  noiSanXuat: "",
  soDangKy: "",
  soLo: "",
  soLuongMau: "",
  soThuTuMau: 0,
  stt: 0,
  tblPhieuTiepNhanMauId: "",
  tenMau: "",
  thoiGianLuuMau: "",
  tieuChuanApDung: "",
  tinhTrangMau: "",
  trangThai: 1,
  trangThaiDuyetMau: 1,
  viTriMau: 1,
};

const ThongTinMau = (props) => {
  const dispatch = useDispatch();
  const { canEdit, phieuTiepNhanId, reloadList, callbackFunc, isCreating } =
    props;

  const [isEdit, setEdit] = useState(false);
  const [isLoading, setLoading] = useState(false);

  const mauInfo = useSelector(selectors.selectDetailMau());
  const mauInfoLoading = useSelector(selectors.selectDetailMauLoading());
  const isCreatingMau = useSelector(selectors.selectToggleCreatemau());
  const [form] = Form.useForm();
  const creatingMau = isCreating || isCreatingMau;
  const disabledForm = !creatingMau && !isEdit;

  useEffect(() => {
    if (creatingMau) {
      form.resetFields();
    } else {
      if (mauInfo) {
        setFormValues(mauInfo);
      }
    }

    return () => {
      setEdit(false);
    };

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [mauInfo, creatingMau]);

  const setFormValues = (info) => {
    form.setFieldsValue({
      ...info,
      ngaySanXuat: getDateFormat(info?.ngaySanXuat),
      ngayHetHan: getDateFormat(info?.ngayHetHan),
      ngayNhanMau: getDateFormat(info?.ngayNhanMau),
      ngayTra: getDateFormat(info?.ngayTra),
      ngayNhapKhau: getDateFormat(info?.ngayNhapKhau),
    });
  };

  useEffect(() => {
    getDanhSachChiTieuKiemNghiem();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const getDanhSachChiTieuKiemNghiem = () => {
    dispatch(
      actions.getListChiTieusGroupByLinhVuc({
        pageSize: 1000,
        pageIndex: 1,
        searchText: "",
      })
    );
  };

  const handleCreateSuccess = (id) => {
    dispatch(actions.toggleCreateMau(false));
    dispatch(actions.getDetailMau(id));
  };

  const handleCreateMau = useCallback(async (values) => {
    try {
      setLoading(true);
      const result = await services.createMau(values);
      if (result.isSuccess) {
        if (creatingMau && callbackFunc) {
          callbackFunc(result);
        } else if (reloadList) {
          reloadList();
          handleCreateSuccess(result.data.id);
        }
        message.success("Tạo mẫu thành công!");
      } else {
        message.error("Lỗi trong quá trình tạo! Vui lòng thử lại");
      }
    } catch (err) {
      console.log("loi roai >> ", err);
      message.error("Lỗi trong quá trình tạo! Vui lòng thử lại");
    } finally {
      setLoading(false);
    }
  }, []);

  const handleEditMau = useCallback(async (values) => {
    try {
      setLoading(true);
      const result = await services.updateMau(values);

      if (result.isSuccess) {
        setEdit(false);
        message.success("Cập nhật mẫu thành công!");
      } else {
        message.error("Lỗi trong quá trình xử lý! Vui lòng thử lại");
      }
    } catch (err) {
      console.log("loi roai >> ", err);
      message.error("Lỗi trong quá trình xử lý! Vui lòng thử lại");
    } finally {
      setLoading(false);
    }
  }, []);

  const onFinish = (values) => {
    const data = {
      ...values,
      ngaySanXuat: values.ngaySanXuat
        ? convertToISOTime(values.ngaySanXuat)
        : null,
      ngayHetHan: values.ngayHetHan
        ? convertToISOTime(values.ngayHetHan)
        : null,
      ngayNhanMau: values.ngayNhanMau
        ? convertToISOTime(values.ngayNhanMau)
        : null,
      ngayTra: values.ngayTra ? convertToISOTime(values.ngayTra) : null,
      ngayNhapKhau: values.ngayNhapKhau
        ? convertToISOTime(values.ngayNhapKhau)
        : null,
      tblPhieuTiepNhanMauId: phieuTiepNhanId,
      maSoMau:
        String(values.maSoMau?.trim()) + String(values.postfix_maSoMau?.trim()),
    };

    if (creatingMau) {
      handleCreateMau({ ...data });
    } else if (isEdit) {
      handleEditMau({ ...data, id: mauInfo?.id });
    }
  };

  const onCancelCreate = () => {
    if (creatingMau || isCreatingMau) dispatch(actions.toggleCreateMau(false));
    else if (isEdit) {
      setEdit(false);
      setFormValues(mauInfo);
    }
  };

  const renderActionButtons = () => {
    return (
      <Space>
        <Button onClick={onCancelCreate}>Hủy bỏ</Button>
        <Button
          htmlType="submit"
          type="primary"
          form="mauForm"
          icon={<SaveOutlined />}
        >
          {creatingMau ? "Tạo mẫu" : "Cập nhật"}
        </Button>
      </Space>
    );
  };

  const renderEditButton = () => {
    if (!canEdit) return;
    return (
      <>
        {isEdit ? (
          renderActionButtons()
        ) : (
          <Button type="primary" onClick={() => setEdit(true)}>
            Chỉnh sửa
          </Button>
        )}
      </>
    );
  };

  return (
    <>
      {mauInfo || creatingMau ? (
        <Spin spinning={isLoading || mauInfoLoading}>
          <MauForm
            id="mauForm"
            formInstance={form}
            onFinish={onFinish}
            disabled={disabledForm}
          />
          <Row style={{ marginTop: "16px" }}>
            {creatingMau ? renderActionButtons() : renderEditButton()}
          </Row>
        </Spin>
      ) : (
        <p>Chọn để xem mẫu!!</p>
      )}
    </>
  );
};

ThongTinMau.propTypes = {
  phieuTiepNhanId: PropTypes.string,
  canEdit: PropTypes.bool,
};

export default ThongTinMau;

export const MauForm = ({
  formInstance,
  onFinish,
  disabled = true,
  ...rest
}) => {
  const formItemLayout = {
    labelCol: { span: 24 },
    wrapperCol: { span: 24 },
  };

  return (
    <Form
      name="form-mau-info"
      {...formItemLayout}
      layout={"vertical"}
      form={formInstance}
      onFinish={onFinish}
      initialValues={initialValues}
      disabled={disabled}
      {...rest}
    >
      <Row gutter={12}>
        <Col span={4}>
          <Form.Item
            label="Dạng bào chế:"
            name="dangBaoChe"
            rules={[{ required: true, message: "Chọn Dạng bào chế" }]}
          >
            <Input placeholder="Nhập nơi lấy mẫu" />
          </Form.Item>
        </Col>
        <Col span={12}>
          <Form.Item
            label="Tên mẫu:"
            name="tenMau"
            rules={[{ required: true, message: "Nhập tên mẫu" }]}
          >
            <Input placeholder="Nhập tên mẫu" />
          </Form.Item>
        </Col>
        <Col span={4}>
          <Form.Item label="Hoạt chất:" name="hoatChat">
            <Input placeholder="Nhập hoạt chất" />
          </Form.Item>
        </Col>
        <Col span={4}>
          <Row align="end" style={{ alignItems: "end" }}>
            <Col span={12}>
              <Form.Item label="Mã số mẫu:" name="maSoMau">
                <Input placeholder="Nhập mã số mẫu" />
              </Form.Item>
            </Col>
            <Col span={12}>
              <Form.Item name="postfix_maSoMau">
                <Input placeholder="1" />
              </Form.Item>
            </Col>
          </Row>
        </Col>
        <Col span={8}>
          <Form.Item label="Nơi sản xuất:" name="noiSanXuat">
            <Input placeholder="Nhập nơi sản xuất" />
          </Form.Item>
        </Col>
        <Col span={4}>
          <Form.Item label="Số lô:" name="soLo">
            <Input placeholder="Nhập nơi lấy mẫu" />
          </Form.Item>
        </Col>
        <Col span={4}>
          <Form.Item label="Ngày sản xuất:" name="ngaySanXuat">
            <DatePicker format="DD-MM-YYYY" />
          </Form.Item>
        </Col>
        <Col span={4}>
          <Form.Item label="Hạn sử dụng:" name="ngayHetHan">
            <DatePicker format="DD-MM-YYYY" />
          </Form.Item>
        </Col>
        <Col span={4}>
          <Form.Item label="Số đăng kí:" name="soDangKy">
            <Input placeholder="Nhập số đăng kí" />
          </Form.Item>
        </Col>
        <Col span={8}>
          <Form.Item label="Nơi nhận mẫu:" name="noiGuiMau">
            <Input placeholder="Nhập nơi nhận mẫu" />
          </Form.Item>
        </Col>
        <Col span={8}>
          <Form.Item label="Tiêu chuẩn áp dụng:" name="tieuChuanApDung">
            <Input placeholder="Nhập nơi nhận mẫu" />
          </Form.Item>
        </Col>
        <Col span={4}>
          <Form.Item label="Khối lượng mẫu:" name="khoiLuongMau">
            <Input placeholder="Nhập khối lượng mẫu" />
          </Form.Item>
        </Col>
        <Col span={4}>
          <Form.Item label="Người giao mẫu:" name="nguoiGiaoMau">
            <Input placeholder="Nhập nơi lấy mẫu" />
          </Form.Item>
        </Col>
        <Col span={4}>
          <Form.Item label="Người nhận mẫu:" name="nguoiNhanMau">
            <Input placeholder="Nhập nơi lấy mẫu" />
          </Form.Item>
        </Col>
        <Col span={4}>
          <Form.Item
            label="Ngày nhận mẫu:"
            name="ngayNhanMau"
            rules={[{ required: true, message: "Nhập thông tin" }]}
          >
            <DatePicker format="DD-MM-YYYY" />
          </Form.Item>
        </Col>
        <Col span={4}>
          <Form.Item
            label="Ngày trả:"
            name="ngayTra"
            rules={[{ required: true, message: "Nhập thông tin" }]}
          >
            <DatePicker format="DD-MM-YYYY" />
          </Form.Item>
        </Col>
        <Col span={8}>
          <Form.Item label="Cơ sở nhập khẩu:" name="coSoNhapKhau">
            <Input placeholder="Nhập" />
          </Form.Item>
        </Col>
        <Col span={4}>
          <Form.Item label="Ngày nhập khẩu:" name="ngayNhapKhau">
            <DatePicker placeholder="Nhập ngày trả" />
          </Form.Item>
        </Col>
        <Col span={8}>
          <Form.Item label="Tình trạng mẫu:" name="tinhTrangMau">
            <Input placeholder="Nhập tình trạng" />
          </Form.Item>
        </Col>
        <Col span={8} style={{ display: "flex" }}>
          <Row gutter={12}>
            <Col flex={1} className="item-checkbox">
              <Form.Item
                label="Yêu cầu làm gấp:"
                name="lamGap"
                valuePropName="checked"
              >
                <Checkbox />
              </Form.Item>
            </Col>
            <Col span={10}>
              <Form.Item label="Hệ số:" name="heSoLamGap">
                <InputNumber placeholder="Nhập hệ số" />
              </Form.Item>
            </Col>
          </Row>
        </Col>
        <Col span={8}>
          <Form.Item label="Điều kiện bảo quản:" name="dieuKienBaoQuan">
            <Input placeholder="Nhập điều kiện" />
          </Form.Item>
        </Col>
      </Row>
    </Form>
  );
};
