import React, { useState } from "react";
import { Layout } from "antd";
import HomeRoutes from "routes/HomeRoutes";
import Navigation from "./navigation";
import HeaderProject from "./header";
import "./style.less";
import { Footer } from "antd/lib/layout/layout";
import { PhoneTwoTone } from "@ant-design/icons";
const { Content, Header } = Layout;

/**
 * Homepage main view
 */
const HomePage = (props) => {
  const [collapsed, setCollapsed] = useState(false);
  return (
    <Layout>
      <Navigation
        usercls="main__navigation"
        branding={"abc"}
        collapsible
        collapsed={collapsed}
        collapsedWidth={80}
        width={250}
      />
      <Layout>
        <Header className="site-layout-background" style={{ padding: 0 }}>
          <HeaderProject
            handleCollapseSidebar={() => setCollapsed(!collapsed)}
            collapsed={collapsed}
          />
        </Header>
        <Content className="main__content">
          <HomeRoutes />
        </Content>
        {/* <Footer>
          <div className="col-sm-6">
            <strong className="float-left">
              Copyright © 2020-2022
              <a href="http://thienphucsci.com.vn/"> THIENPHUCSCI CO., LTD.</a>
            </strong>
            All rights reserved.
          </div>
          <div className="col-sm-6 text-right">
            Contact |
            <a href="tel:0938 453848">
              <PhoneTwoTone /> 0938 453848
            </a>
            <a href="mailto:len_nn@thienphucsci.com.vn">
              <i className="fa fa-envelope"></i>- len_nn@thienphucsci.com.vn
            </a>
          </div>
        </Footer> */}
      </Layout>
    </Layout>
  );
};

export default HomePage;
