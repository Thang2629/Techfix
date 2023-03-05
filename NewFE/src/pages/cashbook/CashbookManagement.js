import React, { useState } from "react";
import { Button, Row, Form, Spin, Space, Tabs, DatePicker } from "antd";
import {
  PlusCircleOutlined,
  UserOutlined,
  SettingOutlined,
  SearchOutlined,
} from "@ant-design/icons";
import HeaderPage from "pages/home/header-page";
import { GridCashbook } from "components/Grid";
import PageWrapper from "components/Layout/PageWrapper";
import { CASHBOOK_GRID_ENDPOINT, getCashbooks } from "services/cashbook";

const { RangePicker } = DatePicker;

export default function CashbookManagement() {
  const [isLoading, setIsLoading] = useState(false);
  const [data, setData] = useState(null);
  const [tabActive, setTabActive] = useState(1);
  const [dateFilter, setDateFilter] = useState("");
  const [dataFilter, setDataFilter] = useState({});

  const getFullDate = (date) => {
    const dateAndTime = date.split("T");
    return dateAndTime[0].split("-").reverse().join("-");
  };
  const columns = [
    {
      title: "Mã phiếu thu",
      dataIndex: "Code",
    },
    {
      title: "Người thu",
      dataIndex: "FullName",
    },
    // {
    //   title: "Chứng từ liên quan",
    //   dataIndex: "Quantity",
    // },
    {
      title: "Kho thu",
      dataIndex: "StoreName",
    },
    {
      title: "Ngày thu",
      dataIndex: "PaymentDate",
      render: (date) => getFullDate(date),
    },
    {
      title: "Ghi chú",
      dataIndex: "Note",
    },
    {
      title: "Hình thức thu",
      dataIndex: "PaymentMethod",
    },
    {
      title: "Tổng tiền",
      dataIndex: "Amount",
    },
    {
      title: "",
      dataIndex: "action",
      render: (_, values) => (
        <Space>
          {/* <Link to={`/san-pham/${values.Id}`}>
            <PrimaryButton
              icon={<BarsOutlined />}
              onClick={() => onClickDetail(values)}
            ></PrimaryButton>
          </Link> */}
          {/* <ButtonDelete onClick={() => onClickDelete(values)} /> */}
        </Space>
      ),
    },
  ];
  const tabs = [
    {
      key: 1,
      label: (
        <span>
          <UserOutlined />
          Thu tiền quỹ
        </span>
      ),
    },
    {
      key: 2,
      label: (
        <span>
          <SettingOutlined />
          Chi tiền quỹ
        </span>
      ),
    },
  ];
  const onTabChange = (key) => {
    setTabActive(key);
  };
  const onDateChange = (_, dateString) => {
    setDateFilter(dateString);
  };
  const onSave = async () => {
    setDataFilter({ dateFilter: dateFilter });
  };
  const renderToolbar = () => {
    return (
      <Row
        style={{
          display: "flex",
          flexWrap: "nowrap",
          gap: "1rem",
          justifyContent: "end",
        }}
      >
        <Form
          style={{
            display: "flex",
            alignItems: "center",
            gap: "1rem",
          }}
          id="queryForm"
          layout="vertical"
          onFinish={onSave}
        >
          <Form.Item>
            <Tabs
              type="card"
              defaultActiveKey={tabActive}
              items={tabs}
              onChange={onTabChange}
              style={{ height: "17px" }}
            />
          </Form.Item>
          <Form.Item style={{ marginBottom: 0 }}>
            <RangePicker onChange={onDateChange} />
            <Button
              type="primary"
              icon={<SearchOutlined />}
              htmlType="submit"
            />
          </Form.Item>

          <Button
            type="primary"
            // onClick={() => onClickAddProduct()}
            icon={<PlusCircleOutlined />}
          >
            Tạo Mới
          </Button>
        </Form>
      </Row>
    );
  };
  return (
    <>
      <Spin spinning={isLoading} tip="Loading...">
        <HeaderPage title="SỔ QUỸ">{renderToolbar()}</HeaderPage>
        <div className="main__application">
          <PageWrapper>
            <GridCashbook
              columns={columns}
              urlEndpoint={CASHBOOK_GRID_ENDPOINT}
              tabActive={tabActive}
              dataFilter={dataFilter}
              data={data}
            />
          </PageWrapper>
        </div>
      </Spin>
    </>
  );
}
