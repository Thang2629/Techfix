import React from 'react';
import PropTypes from 'prop-types';
import { Menu, Row, Col, Button, Dropdown } from 'antd';
import {
  MenuFoldOutlined, MenuUnfoldOutlined
} from '@ant-design/icons';
import { fowardTo } from 'utils/common/route';
import { cleanUp } from 'utils/storage';
import { USER_KEY, SESSION_KEY } from 'static/Constants';
import {
  LoginOutlined,
  LogoutOutlined,
  UserOutlined,
} from '@ant-design/icons';

import './header.less';

const Header = props => {
  const { handleCollapseSidebar, collapsed, } = props;

  const logout = () => {
    cleanUp([USER_KEY, SESSION_KEY]);
    fowardTo('/login');
  }

  const handleClickMenu = (item) => {
    switch (item.key) {
      case "userInfo": break;
      case "logout": logout(); break;
      default: break;
    }
  }

  const menuItems = [
    {
      label: "Thông tin user",
      key: 'userInfo',
      icon: <LoginOutlined />,
    },
    {
      label: "Đăng xuất",
      key: 'logout',
      icon: <LogoutOutlined />,
    },
  ]

  const dropdownMenu = (
    <Menu className='menu-dropdown' items={menuItems} onClick={handleClickMenu} />
  );

  return (
    <div className='header'>
      <Row className='wrapper' justify='space-between'>
        <Button onClick={handleCollapseSidebar} icon={collapsed ? <MenuUnfoldOutlined /> : <MenuFoldOutlined />} />
        <Col>
          <Dropdown overlay={dropdownMenu} trigger={['click']}>
            <Button className='header__btn' type='outlined' icon={<UserOutlined />}>
              Hello! User
            </Button>
          </Dropdown>
        </Col>
      </Row>
    </div>
  );
};

Header.propTypes = {
  /**
   * options data for ribbon
   */
  controlOptions: PropTypes.array,
  optionType: PropTypes.string
};

export default Header;