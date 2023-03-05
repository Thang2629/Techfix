import React from "react";
// import PropTypes from 'prop-types';
import { Link } from "react-router-dom";
import { Menu } from "antd";
import {
  DashboardOutlined,
  ToolOutlined,
  SafetyCertificateOutlined,
  GoldOutlined,
  TeamOutlined,
  HomeOutlined,
  BookOutlined,
  LineChartOutlined,
  SettingOutlined,
} from "@ant-design/icons";

import "./Navigation.less";

const { SubMenu, Item } = Menu;

const Navigation = (props) => {
  const {
    ability,
    // selectedKeys,
    onSelect,
    freshProduceCategory,
    seefoodCategory,
    flowerCategory,
    usercls,
    openKeys,
    onOpenChange,
    callbackCollapseEvent,
    callbackExpandEvent,
    // eslint-disable-next-line no-unused-vars
    ...rest
  } = props;

  // const onTitleClick = ({ key, domEvent }) => {
  //   fowardTo(key);
  //   if (
  //     domEvent &&
  //     domEvent.target &&
  //     domEvent.target.className === 'ant-menu-submenu-arrow' &&
  //     domEvent.currentTarget.getAttribute('aria-expanded') === 'true'
  //   ) {
  //     if (callbackCollapseEvent) callbackCollapseEvent(key);
  //   } else {
  //     if (callbackExpandEvent) callbackExpandEvent(key);
  //   }
  // };

  return (
    <Menu
      defaultSelectedKeys={["dashboard"]}
      // openKeys={openKeys}
      // onOpenChange={onOpenChange}
      // selectedKeys={selectedKeys}
      onSelect={onSelect}
      className="menu-navigation"
      mode="inline"
      // {...rest}
    >
      <Item
        key="/dashboard"
        className="menu-navigation__item"
        icon={<DashboardOutlined />}
      >
        <Link to="/dashboard">Trang chủ</Link>
      </Item>

      <SubMenu
        key="repairSub"
        icon={<ToolOutlined />}
        title="Sửa chữa"
        className="menu-navigation__sub-menu"
      >
        <Item key="repairOrder" className="menu-navigation__item">
          <Link to="/">Đơn sửa chữa</Link>
        </Item>
        <Item key="repairProcess" className="menu-navigation__item">
          <Link to="/">Quy trình sửa chữa</Link>
        </Item>
        <Item key="repairReport" className="menu-navigation__item">
          <Link to="/">Báo cáo sửa chữa</Link>
        </Item>
        <Item key="repairSale" className="menu-navigation__item">
          <Link to="/">Doanh số sửa chữa</Link>
        </Item>
      </SubMenu>

      <SubMenu
        key="insureSub"
        icon={<SafetyCertificateOutlined />}
        title="Bảo hành"
        className="menu-navigation__sub-menu"
      >
        <Item key="insureOrder" className="menu-navigation__item">
          <Link to="/">Đơn bảo hành</Link>
        </Item>
        <Item key="insureProcess" className="menu-navigation__item">
          <Link to="/">Quy trình bảo hành</Link>
        </Item>
        <Item key="insureReport" className="menu-navigation__item">
          <Link to="/">Báo cáo bảo hánh</Link>
        </Item>
      </SubMenu>

      <SubMenu
        key="productSub"
        icon={<GoldOutlined />}
        title="Sản phẩm"
        className="menu-navigation__sub-menu"
      >
        <Item key="productManage" className="menu-navigation__item">
          <Link to="/">Sản phẩm</Link>
        </Item>
        <Item key="orderManage" className="menu-navigation__item">
          <Link to="/">Đơn hàng</Link>
        </Item>
        <Item key="barcode" className="menu-navigation__item">
          <Link to="/">Mã vạch</Link>
        </Item>
      </SubMenu>

      <SubMenu
        key="custonmerSub"
        icon={<TeamOutlined />}
        title="Khách hàng"
        className="menu-navigation__sub-menu"
      >
        <Item key="customerManage" className="menu-navigation__item">
          <Link to="/">Khách hàng</Link>
        </Item>
        <Item key="supplierManage" className="menu-navigation__item">
          <Link to="/">Nhà cung cấp</Link>
        </Item>
      </SubMenu>

      <SubMenu
        key="warehouseSub"
        icon={<HomeOutlined />}
        title="Hàng kho"
        className="menu-navigation__sub-menu"
      >
        <Item key="warehouseInput" className="menu-navigation__item">
          <Link to="/">Nhập kho</Link>
        </Item>
        <Item key="warehouseMove" className="menu-navigation__item">
          <Link to="/">Chuyển kho</Link>
        </Item>
        <Item key="warehouseBacklog" className="menu-navigation__item">
          <Link to="/">Tồn kho</Link>
        </Item>
      </SubMenu>

      <SubMenu
        key="documentSub"
        icon={<BookOutlined />}
        title="Sổ sách"
        className="menu-navigation__sub-menu"
      >
        <Item key="receipt" className="menu-navigation__item">
          <Link to="/">Phiếu thu</Link>
        </Item>
        <Item key="payment" className="menu-navigation__item">
          <Link to="/">Phiếu chi</Link>
        </Item>
        <Item key="cashbook" className="menu-navigation__item">
          <Link to="/cashbook">Sổ quỹ</Link>
        </Item>
      </SubMenu>

      <SubMenu
        key="reportSub"
        icon={<LineChartOutlined />}
        title="Báo cáo"
        className="menu-navigation__sub-menu"
      >
        <Item key="profit" className="menu-navigation__item">
          <Link to="/">Lợi nhuận</Link>
        </Item>
        <Item key="totalSale" className="menu-navigation__item">
          <Link to="/">Doanh số tổng</Link>
        </Item>
        <Item key="totalReport" className="menu-navigation__item">
          <Link to="/">Báo cáo tổng hợp</Link>
        </Item>
      </SubMenu>

      <SubMenu
        key="setting"
        icon={<SettingOutlined />}
        title="Thiết lập"
        className="menu-navigation__sub-menu"
      >
        <Item key="staffManage" className="menu-navigation__item">
          <Link to="/san-pham">Nhân viên</Link>
        </Item>
        <Item key="authorizeManage" className="menu-navigation__item">
          <Link to="">Phân quyền</Link>
        </Item>
        <Item key="warehouseManage" className="menu-navigation__item">
          <Link to="/">Kho</Link>
        </Item>
      </SubMenu>
    </Menu>
  );
};

Navigation.propTypes = {};

Navigation.defaultProps = {};

export default Navigation;
