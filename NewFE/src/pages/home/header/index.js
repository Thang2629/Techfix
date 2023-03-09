import React, { useEffect, useState } from "react";
import PropTypes from "prop-types";
import { Menu, Row, Col, Button, Dropdown, Space, Select } from "antd";
import { MenuFoldOutlined, MenuUnfoldOutlined } from "@ant-design/icons";
import { fowardTo } from "utils/common/route";
import { cleanUp } from "utils/storage";
import { USER_KEY, SESSION_KEY } from "static/Constants";
import { LoginOutlined, LogoutOutlined, UserOutlined } from "@ant-design/icons";
import { getProductAssicatedByType } from "services/ProductAssociated";
import { PRODUCT_ASSOCIATED, STORE_ID_KEY } from "static/Constants";
import "./header.less";
import * as actions from "redux/global/actions";
import { useSelector, useDispatch } from "react-redux";

const HeaderProject = (props) => {
  const { handleCollapseSidebar, collapsed } = props;
  const [stores, setStores] = useState([]);
  const dispatch = useDispatch();
  const storeId = useSelector((state) => state.global.storeId);
  const [isLoading, setIsLoading] = useState(false);
  const logout = () => {
    cleanUp([USER_KEY, SESSION_KEY]);
    fowardTo("/login");
  };

  const handleClickMenu = (item) => {
    switch (item.key) {
      case "userInfo":
        break;
      case "logout":
        logout();
        break;
      default:
        break;
    }
  };
  const HandleChangeStore = (value) => {
    dispatch(actions.selectStore(value));
  };
  const menuItems = [
    {
      label: "Thông tin user",
      key: "userInfo",
      icon: <LoginOutlined />,
    },
    {
      label: "Đăng xuất",
      key: "logout",
      icon: <LogoutOutlined />,
    },
  ];

  const dropdownMenu = (
    <Menu
      className="menu-dropdown"
      items={menuItems}
      onClick={handleClickMenu}
    />
  );

  useEffect(() => {
    const initialize = async () => {
      setIsLoading(true);
      const response = await getProductAssicatedByType(
        PRODUCT_ASSOCIATED.STORE
      );
      if (!storeId) dispatch(actions.selectStore(response[0].Id));
      setStores(response);
      setIsLoading(false);
    };
    initialize();
  }, [storeId]);

  return (
    <div className="header">
      <Row className="wrapper" justify="space-between">
        <Button
          onClick={handleCollapseSidebar}
          icon={collapsed ? <MenuUnfoldOutlined /> : <MenuFoldOutlined />}
        />
        <Col></Col>
        <Col style={{ display: "flex" }}>
          <Select
            style={{ width: 200 }}
            loading={isLoading}
            onChange={HandleChangeStore}
            value={storeId}
          >
            {stores &&
              stores.map((item) => {
                return (
                  <Select.Option key={`item_${item.Id}`} value={item.Id}>
                    {item.Name}
                  </Select.Option>
                );
              })}
          </Select>
          <Dropdown overlay={dropdownMenu} trigger={["click"]}>
            <Button
              className="header__btn"
              type="outlined"
              icon={<UserOutlined />}
            >
              Hello! User
            </Button>
          </Dropdown>
        </Col>
      </Row>
    </div>
  );
};

HeaderProject.propTypes = {
  /**
   * options data for ribbon
   */
  controlOptions: PropTypes.array,
  optionType: PropTypes.string,
};

export default HeaderProject;
