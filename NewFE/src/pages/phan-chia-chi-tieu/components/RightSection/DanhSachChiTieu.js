import React, { useEffect, useState } from "react";
import {
  Button,
  Space,
  Row,
  Form,
  TreeSelect,
  message,
  Table,
  Modal,
  Input,
  Typography,
} from "antd";

import { ButtonDelete, ButtonEdit } from "common/components/Buttons";
import { useSelector, useDispatch } from "react-redux";
import Grid from "components/Grid";
import PageWrapper from "components/Layout/PageWrapper";
import * as selectors from "redux/quyTrinh/selectors";
import "./DanhSachChiTieu.less";
import _, { clone } from "lodash";
import * as services from "services/sample";
import { ExclamationCircleOutlined } from "@ant-design/icons";
import {
  SAVE_SUCCESS,
  SAVE_ERROR,
  DELETE_SUCCESS,
  DELETE_ERROR,
} from "utils/common/messageContants";

const DanhSachChiTieu = (props) => {
  const [chiTieuKiemNghiems, setchiTieuKiemNghiems] = useState([]);
  const [loading, setLoading] = useState(false);
  const [readGrid, setReadGrid] = useState(false);
  const [isOpen, setIsopen] = useState(false);
  const { mauId } = props;
  const [form] = Form.useForm();

  const childColumns = [
    {
      title: "Tên dịch vụ",
      dataIndex: "tenDichVu",
    },
    {
      title: "Mức chất lượng",
      dataIndex: "mucChatLuong",
    },
    {
      title: "Đơn giá",
      dataIndex: "donGia",
    },
    {
      title: "Phương pháp thử",
      dataIndex: "phươngPhapThu",
    },
    {
      title: "",
      dataIndex: "action",
      render: (_, value) => (
        <Space>
          <Space>
            <ButtonEdit onClick={() => onClickUpdate(value)} />
            <ButtonDelete onClick={() => onClickDelete(value)} />
          </Space>
        </Space>
      ),
    },
  ];

  const onClickUpdate = (value) => {
    form.setFieldsValue(value);
    setIsopen(true);
  };

  const onClickDelete = (values) => {
    Modal.confirm({
      title: "Xác Nhận",
      icon: <ExclamationCircleOutlined />,
      content: "Bạn có chắc chắn muốn xóa chỉ tiêu này không?",
      okText: "Xác Nhận",
      cancelText: "Hủy",
      onOk: () => handleDelete(values),
    });
  };

  const handleDelete = async (values) => {
    var response = await services.deleteChiTieuKiemNghiems([values.key]);
    if (response.isSuccess) {
      setReadGrid(true);
      message.success(DELETE_SUCCESS);
    } else {
      message.error(DELETE_ERROR);
    }
  };
  useEffect(() => {
    if (readGrid) {
      getChiTieuKiemNghiemByMauId();
    }
  }, [readGrid]);

  useEffect(() => {
    getChiTieuKiemNghiemByMauId();
  }, [mauId]);

  const getChiTieuKiemNghiemByMauId = async () => {
    setLoading(true);
    var response = await services.getDVKiemNghiemByMauId(mauId);
    if (response.isSuccess) {
      setLoading(false);
      setchiTieuKiemNghiems(response.data);
    } else {
      setLoading(false);
    }
    setReadGrid(false);
  };

  const handleCancel = () => {
    form.resetFields();
    setIsopen(false);
  };

  const onUpdate = async (values) => {
    var params = {
      tblChiTietMauId: mauId,
      chiTieuKiemNghiems: [
        {
          id: values.key,
          tblDichVuId: values.tblDichVuId,
          mucChatLuong: values.mucChatLuong,
        },
      ],
    };
    var response = await services.saveChiTieuKiemNghiems(params);

    if (response.isSuccess) {
      setIsopen(false);
      setReadGrid(true);
      message.success(SAVE_SUCCESS);
    } else {
      message.error(response.message);
    }
  };

  const renderGridChiTieu = () => {
    const results = [];
    chiTieuKiemNghiems &&
      chiTieuKiemNghiems.forEach((item, index) => {
        item.childrens.map((el) => {
          Object.assign(el, { id: el.key });
        });
        results.push(
          <PageWrapper key={index}>
            <Typography
              level={5}
              style={{ marginBottom: "16px", fontWeight: "bold" }}
            >
              {index + 1}. {item.tenLinhVuc}
            </Typography>
            <Grid
              data={item.childrens}
              columns={childColumns}
              isHidePagination={true}
            />
          </PageWrapper>
        );
      });
    return <>{results}</>;
  };

  return (
    <div>
      <Row className="danh-sach-chi-tieu__options">
        <ServicesFormAdd reloadList={getChiTieuKiemNghiemByMauId} />
      </Row>
      <Row></Row>
      <div>{renderGridChiTieu()}</div>
      <Modal
        title="Chỉnh sửa nhóm chỉ tiêu"
        open={isOpen}
        onCancel={handleCancel}
        footer={[
          <Button
            form="formUpdateNhomChiTieu"
            key="back"
            onClick={handleCancel}
          >
            Hủy
          </Button>,
          <Button
            form="formUpdateNhomChiTieu"
            key="submit"
            type="primary"
            htmlType="submit"
          >
            Lưu thông tin
          </Button>,
        ]}
      >
        <Form id="formUpdateNhomChiTieu" onFinish={onUpdate} form={form}>
          <Form.Item hidden={true} label="id" name="key">
            <Input />
          </Form.Item>
          <Form.Item hidden={true} label="tblDichVuId" name="tblDichVuId">
            <Input />
          </Form.Item>
          <Form.Item label="Mức chất lương" name="mucChatLuong">
            <Input />
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

DanhSachChiTieu.propTypes = {};

export default DanhSachChiTieu;

export const ServicesFormAdd = ({ reloadList }) => {
  const [form] = Form.useForm();
  const groupChiTieus = useSelector(
    selectors.selectGetChiTieusGroupByLinhVuc()
  );
  const [chitieus, setChiTieus] = useState(groupChiTieus);
  const mauDetail = useSelector(selectors.selectDetailMau());
  // const dispatch = useDispatch();
  const formItemLayout = {
    labelCol: { span: 32 },
    wrapperCol: { span: 48 },
  };
  const onSave = async (values) => {
    var params = {
      tblChiTietMauId: mauDetail.id,
      chiTieuKiemNghiems: values["danhSachChiTieus"].map((item) => {
        return {
          tblDichVuId: item,
        };
      }),
    };
    var response = await services.saveChiTieuKiemNghiems(params);

    if (response.isSuccess) {
      // dispatch(actions.refreshGrid(true));
      message.success(SAVE_SUCCESS);
      reloadList && reloadList();
    } else {
      message.error(response.message);
    }
  };

  const getAllChiTieu = () => {
    let chitieuData = clone(groupChiTieus);
    const allChiTieuData = [];
    chitieuData.forEach((item) => {
      allChiTieuData.push(item);
      if (item.children) {
        item.children.forEach((el) => {
          allChiTieuData.push(el);
        });
      }
    });

    return allChiTieuData;
  };

  const findChiTieuByKey = (allChiTieuData, value) => {
    return allChiTieuData.find((p) => p.key === value);
  };

  const buildGroupChiTieus = (selected, disableCheckbox) => {
    const newChiTieuGroup = [];
    groupChiTieus.forEach((item) => {
      const childrens = [];
      if (item.children && selected) {
        item.children.forEach((el) => {
          if (el.title === selected.title && el.key !== selected.key) {
            if (disableCheckbox) {
              let newObject = {
                ...el,
                disableCheckbox: true,
              };
              childrens.push(newObject);
            } else {
              childrens.push(el);
            }
          } else {
            childrens.push(el);
          }
        });
      }
      let parent = clone(item);
      parent.children = childrens;
      newChiTieuGroup.push(parent);
    });

    return newChiTieuGroup;
  };

  const onSelect = (value) => {
    const allChiTieuData = getAllChiTieu();
    const selected = findChiTieuByKey(allChiTieuData, value);
    const newChiTieuGroup = buildGroupChiTieus(selected, true);
    setChiTieus(newChiTieuGroup);
  };

  const onDeselect = (value) => {
    const allChiTieuData = getAllChiTieu();
    const selected = findChiTieuByKey(allChiTieuData, value);
    const newChiTieuGroup = buildGroupChiTieus(selected);
    setChiTieus(newChiTieuGroup);
  };
  return (
    <Form
      {...formItemLayout}
      form={form}
      onFinish={onSave}
      id="formThemChiTieu"
    >
      <Row justify="end">
        <Form.Item name="danhSachChiTieus" label="Chỉ tiêu kiểm nghiệm:">
          <TreeSelect
            placeholder="Chọn chỉ tiêu..."
            treeCheckable={true}
            treeData={chitieus}
            treeDefaultExpandAll={true}
            style={{ width: 500 }}
            allowClear={true}
            filterTreeNode={(search, item) => {
              return (
                item.title.toLowerCase().indexOf(search.toLowerCase()) >= 0
              );
            }}
            onSelect={onSelect}
            onDeselect={onDeselect}
          />
        </Form.Item>
        <Form.Item style={{ marginLeft: "20px" }} label="">
          <Button
            form="formThemChiTieu"
            key="submit"
            type="primary"
            htmlType="submit"
          >
            Thêm
          </Button>
        </Form.Item>
      </Row>
    </Form>
  );
};
