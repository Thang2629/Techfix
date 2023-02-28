import ButtonBack from "common/components/Buttons/ButtonBack";
import ImplementResult from "common/components/result/Implement";
import TabsSection from "common/components/TabsSection/TabsSection";
import PageWrapper from "components/Layout/PageWrapper";
import HeaderPage from "pages/home/header-page";
import React from "react";
import DetailAndUpateProduct from "./tabs-component/DetailAndUpateProduct";
import CreateAndCopyProduct from "./tabs-component/DetailAndUpateProduct";
import { useParams } from "react-router-dom";

const DetailCustomer = (props) => {
  const { id } = useParams();
  const styleButton = {
    marginBottom: "5px",
    display: "flex",
    alignItems: "center",
    justifyContent: "end",
  };

  const handleBack = () => {
    return (
      <div className="groupbtn" style={styleButton}>
        <ButtonBack url="/san-pham" />
      </div>
    );
  };

  return (
    <div>
      <HeaderPage title="Tạo Phản Phẩm">{handleBack()}</HeaderPage>
      <div className="main__application">
        <PageWrapper>
          {id ? <DetailAndUpateProduct /> : <CreateAndCopyProduct />}
          {/* <TabsSection items={items} /> */}
        </PageWrapper>
      </div>
    </div>
  );
};

DetailCustomer.propTypes = {};

export default DetailCustomer;
