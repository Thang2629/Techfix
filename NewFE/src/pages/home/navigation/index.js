/* eslint-disable no-unused-vars */
import React, { useContext, useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import { useLocation } from "react-router-dom";
import { Layout, Typography } from "antd";
import { AbilityContext } from "context/Can";
import { Navigation, OpenItems } from "./components";
import imageLogo from "../../../assets/techfix.png";

import "./style.less";

const { Sider } = Layout;

const LeftNavigation = (props) => {
  const { usercls, branding, ...rest } = props;
  const [openKeys, setOpenKeys] = useState([]);

  const ability = useContext(AbilityContext);
  let { pathname } = useLocation();
  const dispatch = useDispatch();

  const callbackCollapseEvent = (key) => {
    if (key === "/all") {
      setOpenKeys([]);
    } else {
      let result = openKeys.filter((item) => item !== key);
      setOpenKeys(result);
    }
  };
  const callbackExpandEvent = (key) => {
    setOpenKeys([...openKeys, key]);
  };

  return (
    <Sider className={"sider " + usercls} {...rest}>
      <div className="logo">
        <img src={imageLogo} alt="logo" />
      </div>
      <Navigation
        usercls="sider__navigation"
        mode="inline"
        ability={ability}
        selectedKeys={pathname + "-key"}
        openKeys={openKeys}
        callbackCollapseEvent={callbackCollapseEvent}
        callbackExpandEvent={callbackExpandEvent}
      />
    </Sider>
  );
};

export default LeftNavigation;
