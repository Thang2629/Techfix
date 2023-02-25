import React from "react";
import PropTypes from "prop-types";
import { Row, Col, Input, Button, Typography } from "antd";
import { PlusCircleOutlined } from "@ant-design/icons";
import { useDispatch } from "react-redux";
import { updateSearch } from "../../../redux/global/actions";
import "./header.less";
const { Search } = Input;

const HeaderPage = ({ title = "", actions = "default", onCreate }) => {
  const d = useDispatch();
  const onSearch = (text) => {
    d(updateSearch(text));
  };

  return (
    <div className="header-page">
      <Row className="wrapper" justify="space-around" align="center">
        <Col className="header-page__title">
          <Typography.Title level={3}>{title}</Typography.Title>
        </Col>
        <Col flex={1}>
          {actions === "default" ? (
            <Row
              style={{
                display: "flex",
                flexWrap: "nowrap",
                gap: "16px",
                justifyContent: "end",
              }}
            >
              <Search
                className="header-page__search"
                placeholder="Tìm kiếm..."
                onSearch={onSearch}
                enterButton
              />
              <Button
                type="primary"
                onClick={() => onCreate()}
                icon={<PlusCircleOutlined />}
              >
                Thêm mới
              </Button>
            </Row>
          ) : typeof actions === "function" ? (
            <Row
              style={{
                display: "flex",
                flexWrap: "nowrap",
                gap: "1rem",
                justifyContent: "end",
              }}
            >
              {actions()}
              <Search
                className="header-page__search"
                placeholder="Tìm kiếm..."
                onSearch={onSearch}
                enterButton
              />
              <Button
                type="primary"
                onClick={() => onCreate()}
                icon={<PlusCircleOutlined />}
              >
                Thêm mới
              </Button>
            </Row>
          ) : null}
        </Col>
      </Row>
    </div>
  );
};

HeaderPage.propTypes = {
  /**
   * options data for ribbon
   */
  title: PropTypes.string,
  actions: PropTypes.oneOf([PropTypes.string, PropTypes.func, PropTypes.bool]),
  onCreate: PropTypes.func,
};

export default HeaderPage;
