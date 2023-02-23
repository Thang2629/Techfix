import React from "react";
import { Result, Button } from "antd";
import { fowardTo } from "utils/common/route";
import { Col, Row } from "antd";
import {
  TeamOutlined,
  FileTextOutlined,
  ExperimentOutlined,
  UserSwitchOutlined,
  QuestionCircleOutlined,
  LogoutOutlined,
} from "@ant-design/icons";

const ImplementResult = (props) => {
  const { module } = props;
  return (
    <>
      {/* <Row>
        <Col span={24}>Banner</Col>
      </Row>
      <Row>
        <Col span={24}>Title</Col>
      </Row>
      <Row>
        <Col span={24}>
          <Row>
            <Col span={8}>
              <div class="pet">
                <span class="pet__icon">
                  <i>
                    <TeamOutlined />
                  </i>
                </span>
                <h5>Corgi</h5>
                <p>Lorem ipsum dolor sit amet consectetur adipisicing elit.</p>
              </div>
            </Col>
            <Col span={8}>
              <div class="pet">
                <span class="pet__icon">
                  <i>
                    <TeamOutlined />
                  </i>
                </span>
                <h5>Corgi</h5>
                <p>Lorem ipsum dolor sit amet consectetur adipisicing elit.</p>
              </div>
            </Col>
            <Col span={8}>
              <div class="pet">
                <span class="pet__icon">
                  <i>
                    <TeamOutlined />
                  </i>
                </span>
                <h5>Corgi</h5>
                <p>Lorem ipsum dolor sit amet consectetur adipisicing elit.</p>
              </div>
            </Col>
          </Row>
          <Row>
            <Col span={8}>
              <div class="pet">
                <span class="pet__icon">
                  <i>
                    <TeamOutlined />
                  </i>
                </span>
                <h5>Corgi</h5>
                <p>Lorem ipsum dolor sit amet consectetur adipisicing elit.</p>
              </div>
            </Col>
            <Col span={8}>
              <div class="pet">
                <span class="pet__icon">
                  <i>
                    <TeamOutlined />
                  </i>
                </span>
                <h5>Corgi</h5>
                <p>Lorem ipsum dolor sit amet consectetur adipisicing elit.</p>
              </div>
            </Col>
            <Col span={8}>
              <div class="pet">
                <span class="pet__icon">
                  <i>
                    <TeamOutlined />
                  </i>
                </span>
                <h5>Corgi</h5>
                <p>Lorem ipsum dolor sit amet consectetur adipisicing elit.</p>
              </div>
            </Col>
          </Row>
        </Col>
      </Row> */}
    </>
    // <Result
    //     status="warning"
    //     title={module + " is under development!"}
    //     extra={
    //         <Button
    //             type="primary"
    //             key="back-to-dashboard"
    //             onClick={() => fowardTo('/dashboard')}
    //         >
    //             Back to Dashboard
    //         </Button>
    //     }
    // />
  );
};

export default ImplementResult;
