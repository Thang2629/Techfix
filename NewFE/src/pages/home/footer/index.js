import React, { useState } from 'react';
import PropTypes from 'prop-types';
import { Row, Col } from 'antd';
import { FooterLayout } from 'common/components';
import {
	UsernameText,
	MrmText,
	DownloadQueueText,
	QueueProgress,
	ChatBadge,
	CardBadge,
	EmailBadge,
	LoggingBadge,
	ModalLog
} from './components';
import { USER_KEY } from 'static/Constants';
import { useLocalStorage } from 'hooks/localStorage';
import { useSelector } from 'react-redux';
import * as globalSelectors from 'redux/global/selectors';

const Footer = (props) => {
	const { usercls, controlOptions, optionType, children, ...rest } = props;
	const [ visible, setVisible ] = useState(false);
	const [ user ] = useLocalStorage(USER_KEY, {});
	const { email } = user;

	//const dispatch = useDispatch();
	const logging = useSelector(globalSelectors.selectLogging());

	const onClickLogging = () => {
		setVisible(true);
	};

	const onClickCancelModal = () =>{
		setVisible(false);
	}

	return (
		<FooterLayout className={'footer ' + usercls} {...rest}>
			<Row justify="start " align="middle">
				<Col xs={2} sm={2} md={2} lg={2} justify="start">
					<LoggingBadge count={(logging && logging.length) || ''} onClick={onClickLogging} />
					<ModalLog title="Logging" visible={visible} listData={logging} onCancel={onClickCancelModal}/>
				</Col>
				<Col xs={6} sm={6} md={4} lg={2}>
					<UsernameText text={email} />
				</Col>
				<Col xs={8} sm={8} md={10} lg={12}>
					<MrmText name={email} phone={email} email={email} />
				</Col>
				<Col xs={4} sm={4} md={4} lg={3}>
					<Row justify="space-around" align="middle">
						<ChatBadge count={99} />
						<CardBadge count={99} />
						<EmailBadge count={99} offset={[ 10, 0 ]} />
					</Row>
				</Col>
				<Col xs={4} sm={4} md={4} lg={5}>
					<Row justify="center" align="middle">
						<Col xs={0} sm={0} md={0} lg={10} style={{ margin: '0px 4px' }}>
							<DownloadQueueText />
						</Col>
						<Col flex="auto">
							<QueueProgress percent={30} />
						</Col>
					</Row>
				</Col>
			</Row>
			{children}
		</FooterLayout>
	);
};

Footer.propTypes = {
	/**
     * options data for ribbon
     */
	controlOptions: PropTypes.array,
	optionType: PropTypes.string
};

export default Footer;
