import React from "react";
import PropTypes from "prop-types";
import { Modal, List, Row, Typography } from "antd";
import moment from "moment";
const ModalLogging = (props) => {
  const { title, visible, onOk, onCancel, listData } = props;
  return (
    <Modal
      centered
      title={title}
      visible={visible}
      footer={null}
      destroyOnClose={true}
      onCancel={onCancel}
      onOk={onOk}
      width="60vw"
    >
      <div style={{ overflowY: "scroll", height: "80vh" }}>
        <List
          itemLayout="vertical"
          // size="large"
          pagination={{
            onChange: (page) => {
              console.log(page);
            },
            // pageSize: 10
            showQuickJumper: true,
          }}
          dataSource={listData}
          renderItem={(item, index) => (
            <List.Item key={index}>
              {item.type === "url" && (
                <List.Item.Meta
                  // avatar={<Avatar src={item.avatar} />}
                  title="User route access"
                  description={
                    <div>
                      <Row>
                        <Typography.Text
                          style={{ wordBreak: "break-all" }}
                        >{`Username: ${item.email}`}</Typography.Text>
                      </Row>
                      <Row>
                        <Typography.Text
                          style={{ wordBreak: "break-all" }}
                        >{`Timestamp: ${moment(item.date).format(
                          "MM/DD/YYYY h:mm:ss a"
                        )}`}</Typography.Text>
                      </Row>
                      <Row>
                        <Typography.Text
                          style={{ wordBreak: "break-all" }}
                        >{`Browser: ${item.browser}`}</Typography.Text>
                      </Row>
                      <Row>
                        <Typography.Text
                          style={{ wordBreak: "break-all" }}
                        >{`Route path: ${item.pathName}`}</Typography.Text>
                      </Row>
                    </div>
                  }
                />
              )}
              {item.type === "redux" && (
                <List.Item.Meta
                  // avatar={<Avatar src={item.avatar} />}
                  title="Redux State"
                  description={
                    <div>
                      <Row>
                        <Typography.Text
                          style={{ wordBreak: "break-all" }}
                        >{`Username: ${item.email}`}</Typography.Text>
                      </Row>
                      <Row>
                        <Typography.Text
                          style={{ wordBreak: "break-all" }}
                        >{`Timestamp: ${moment(item.date).format(
                          "MM/DD/YYYY h:mm:ss a"
                        )}`}</Typography.Text>
                      </Row>
                      <Row>
                        <Typography.Text
                          style={{ wordBreak: "break-all" }}
                        >{`Dispathching: ${item.dispatching}`}</Typography.Text>
                      </Row>
                      {item.nextState && (
                        <Row>
                          <Typography.Text
                            style={{ wordBreak: "break-all" }}
                          >{`Next State: ${item.nextState}`}</Typography.Text>
                        </Row>
                      )}
                    </div>
                  }
                />
              )}
              {item.type === "axios" && (
                <List.Item.Meta
                  // avatar={<Avatar src={item.avatar} />}
                  title="Error happen"
                  description={
                    <div>
                      <Row>
                        <Typography.Text
                          style={{ wordBreak: "break-all" }}
                        >{`Username: ${item.email}`}</Typography.Text>
                      </Row>
                      <Row>
                        <Typography.Text
                          style={{ wordBreak: "break-all" }}
                        >{`Timestamp: ${moment(item.date).format(
                          "MM/DD/YYYY h:mm:ss a"
                        )}`}</Typography.Text>
                      </Row>
                      <Row>
                        <Typography.Text
                          style={{ wordBreak: "break-all" }}
                        >{`Status: ${item.status}`}</Typography.Text>
                      </Row>
                      <Row>
                        <Typography.Text
                          style={{ wordBreak: "break-all" }}
                        >{`Status Text: ${item.statusText}`}</Typography.Text>
                      </Row>
                      <Row>
                        <Typography.Text
                          style={{ wordBreak: "break-all" }}
                        >{`Error: ${item.errorMessage}`}</Typography.Text>
                      </Row>
                    </div>
                  }
                />
              )}
              {item.type === "render" && (
                <List.Item.Meta
                  // avatar={<Avatar src={item.avatar} />}
                  title="Error render"
                  description={
                    <div>
                      <Row>
                        <Typography.Text
                          style={{ wordBreak: "break-all" }}
                        >{`Username: ${item.email}`}</Typography.Text>
                      </Row>
                      <Row>
                        <Typography.Text
                          style={{ wordBreak: "break-all" }}
                        >{`Timestamp: ${moment(item.date).format(
                          "MM/DD/YYYY h:mm:ss a"
                        )}`}</Typography.Text>
                      </Row>
                      <Row>
                        <Typography.Text
                          style={{ wordBreak: "break-all" }}
                        >{`Error: ${item.errorMessage}`}</Typography.Text>
                      </Row>
                    </div>
                  }
                />
              )}
            </List.Item>
          )}
        />
      </div>
    </Modal>
  );
};

ModalLogging.propTypes = {
  title: PropTypes.string,
  visible: PropTypes.bool.isRequired,
  onOk: PropTypes.func,
  onCancel: PropTypes.func.isRequired,
  listData: PropTypes.array.isRequired,
};

export default ModalLogging;
