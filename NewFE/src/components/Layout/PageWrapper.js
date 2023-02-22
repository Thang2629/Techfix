import React from 'react'
import classnames from 'classnames';

import "./PageWrapper.less"

function PageWrapper({ className, children }) {
  return (
    <div className={classnames("section-wrapper", className)} >{children}</div>
  )
}

PageWrapper.propTypes = {}

export default PageWrapper;
