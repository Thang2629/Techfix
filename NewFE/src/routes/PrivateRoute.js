import React from "react";
import { Route, Redirect } from "react-router-dom";
/**
 * private route using cookie to check user has already login
 * @param {*} props
 */
const PrivateRoute = ({ component: Component, validateFn, ...rest }) => {
  return (
    <Route
      {...rest}
      render={(props) =>
        validateFn() ? (
          <Component {...props} />
        ) : (
          <Redirect
            to={{
              path: "/login",
              state: { from: props.location },
            }}
          />
        )
      }
    />
  );
};

export default PrivateRoute;
