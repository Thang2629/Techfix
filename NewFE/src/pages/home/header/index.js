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
import { useLocalStorage } from "hooks/localStorage";
import "./header.less";

const HeaderProject = (props) => {
  const { handleCollapseSidebar, collapsed } = props;
  const [stores, setStores] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  const [storeId, setStoreId] = useLocalStorage(STORE_ID_KEY, {});
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
    setStoreId(value);
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

      const storeId = JSON.parse(localStorage.getItem(STORE_ID_KEY));
      if (!storeId) setStoreId(response[0].Id);
      setStores(response);
      setIsLoading(false);
    };
    initialize();
  }, []);

  return (
    <div className="header">
      <Row className="wrapper" justify="space-between">
        <Button
          onClick={handleCollapseSidebar}
          icon={collapsed ? <MenuUnfoldOutlined /> : <MenuFoldOutlined />}
        />
        <Col>
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
        <Col style={{ display: "flex" }}>
          <Select
            style={{ width: 200 }}
            loading={isLoading}
            onChange={HandleChangeStore}
            defaultValue={storeId}
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
          <Dropdown menu={dropdownMenu} trigger={["click"]}>
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
