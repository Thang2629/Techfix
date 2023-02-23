import React from "react";
import { Route, Switch } from "react-router-dom";

import PrivateRoute from "./PrivateRoute"; // use for page have autorization
import { SIZE_LARGE } from "static/Constants";
import DelayedFallback from "common/components/indicator/SuspendIndicator";
import { isAuthed } from "utils/common/session";
import { QuestionCircleTwoTone } from "@ant-design/icons";
import ErrorBoundary from "common/components/errorboundary/ErrorBoundary";

const { lazy, Suspense } = React;
const HomePage = lazy(() => import("pages/home"));
const LogIn = lazy(() => import("pages/login"));
const NotFound = lazy(() => import("pages/error/NotFound"));

function Routes(props) {
	const containers = (
		<Switch>
			<Route path={"/login"} exact render={() => <LogIn />} />
			<Route path={"/register"} exact render={() => <LogIn />} />
			<Route
				path={"/notfound"}
				exact
				render={() => <NotFound customIcon={<QuestionCircleTwoTone />} />}
			/>
			<PrivateRoute path={"/"} component={HomePage} validateFn={isAuthed} />
		</Switch>
	);
	return (
		<div className={props.cls} style={props.style}>
			<Suspense
				fallback={<DelayedFallback size={SIZE_LARGE} tip={"Loading"} />}
			>
				<ErrorBoundary>{containers}</ErrorBoundary>
			</Suspense>
		</div>
	);
}

export default Routes;
