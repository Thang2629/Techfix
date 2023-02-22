import React from 'react';
import { useDispatch } from 'react-redux';
import { Row, Col, Form, Input, Button, Typography } from 'antd';

import { useInjectReducer } from 'utils/common/injectedReducers';
import { useInjectSaga } from 'utils/common/injectSaga';
import saga from './controllers/saga';
import reducer from './controllers/reducer';
// import * as selectors from './controllers/selectors';
import * as actions from './controllers/actions';

import './style.less';

const { Item } = Form;
const { Password } = Input;
const { Title, Paragraph } = Typography;

const key = 'user';

const LogIn = (props) => {
  const { setRegisterShow } = props;

  useInjectReducer({ key, reducer });
  useInjectSaga({ key, saga });

  const dispatch = useDispatch();

  const onFinish = async (values) => {
    const user = {
      username: values.username,
      password: values.password,
    };

    dispatch(actions.login(user));
  };

  return (
    <div className='login-page'>
      <Row className='login-page__wrap'>
        <Col span={10} className='login-page__info'>
          <Title level={2} strong={true}>
            Welcome, my dear!!!
          </Title>
          <Button
            shape='round'
            className='login-page__btn'
            onClick={setRegisterShow}
          >
            Đăng kí
          </Button>
        </Col>
        <Col span={14} className='login-page__action'>
          <Title className='login-page__title'>ĐĂNG NHẬP vào ERP</Title>
          <Paragraph>Vui lòng nhập các thông tin sau</Paragraph>
          <Form
            className='login-page__form'
            name='register-form'
            onFinish={onFinish}
          >
            <Item
              name='username'
              rules={[
                {
                  type: 'username',
                  message: 'Username',
                },
                {
                  required: true,
                  message: 'username',
                },
              ]}
            >
              <Input placeholder='nhập username' />
            </Item>
            <Item
              name='password'
              rules={[
                {
                  required: true,
                  message: 'Password',
                },
              ]}
            >
              <Password placeholder='nhập password' />
            </Item>

            <Button
              shape='round'
              className='login-page__btn login-page__btn--login'
              htmlType='submit'
            >
              Đăng Nhập
            </Button>
          </Form>
        </Col>
      </Row>
    </div>
  );
};

export default LogIn;