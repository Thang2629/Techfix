import React, { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import { Button, Row, Steps } from "antd";
import { useHistory, useParams } from "react-router-dom";
import ThongTinPhieu from "../RightSection/ThongTinPhieu";
import PageWrapper from "components/Layout/PageWrapper";
import HeaderPage from "pages/home/header-page";
import ThongTinMau from "pages/phan-chia-chi-tieu/components/RightSection/ThongTinMau";
import DanhSachChiTieu from "pages/phan-chia-chi-tieu/components/RightSection/DanhSachChiTieu";
import * as actions from "redux/quyTrinh/actions";
import "./CreatePhieu.less";
const { Step } = Steps;

const CreatePhieu = (props) => {
  const history = useHistory();
  let params = useParams();
  const [current, setCurrent] = useState(0);
  const [phieuId, setPhieuId] = useState(null);
  const [mauId, setMauId] = useState(null);

  const dispatch = useDispatch();

  const handleStepCreatingMau = (phieu) => {
    setPhieuId(phieu?.id);
    setCurrent(current + 1);
    // dispatch(actions.toggleCreateMau(true));
  };

  const handleStepCreatingDanhSachChiTieu = (mau) => {
    setMauId(mau?.id);
    setCurrent(current + 1);
  };

  const handleStepFinishCreate = () => {
    history.push(`/tiep-nhan-ho-so`);
  };

  const steps = [
    {
      title: "Tạo Thông tin phiếu",
      content: (
        <ThongTinPhieu isCreating={true} callbackFunc={handleStepCreatingMau} />
      ),
    },
    {
      title: "Tạo Thông tin mẫu",
      content: (
        <ThongTinMau
          isCreating={true}
          callbackFunc={handleStepCreatingDanhSachChiTieu}
          phieuTiepNhanId={phieuId}
        />
      ),
    },
    {
      title: "Tạo Danh sách chỉ tiêu",
      content: <DanhSachChiTieu />,
    },
  ];

  return (
    <>
      <HeaderPage title="Tạo phiếu" actions={false} />
      <div className="main__application">
        <PageWrapper>
          <Row className="content">
            <Steps current={current}>
              {steps.map((item) => (
                <Step key={item.title} title={item.title} />
              ))}
            </Steps>
            <div className="steps-content">{steps[current].content}</div>
          </Row>
          {current === 2 && (
            <Row>
              <Button type="default" onClick={handleStepFinishCreate}>
                Trở về
              </Button>
            </Row>
          )}
        </PageWrapper>
      </div>
    </>
  );
};

CreatePhieu.propTypes = {};

export default CreatePhieu;
