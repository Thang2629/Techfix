import React from "react";
import { Route, Switch } from "react-router-dom";
import { SIZE_LARGE } from "static/Constants";
import DelayedFallback from "common/components/indicator/SuspendIndicator";

const { lazy, Suspense } = React;
const Dashboard = lazy(() => import("pages/dashboard"));
const ApartmentManage = lazy(() => import("pages/quan-ly-khoa"));
const PhongBanManage = lazy(() => import("pages/quan-ly-phong-ban"));
const StaffManage = lazy(() => import("pages/quan-ly-nhan-vien"));
const DetailNhanVien = lazy(() => import("pages/quan-ly-nhan-vien/Detail"));
const CustomerGroup = lazy(() => import("pages/customer-group"));
const CustomerManage = lazy(() => import("pages/customer-manage"));
const CustomerManageDetail = lazy(() =>
  import("pages/customer-manage/DetailCustomer")
);
const MauTiepNhan = lazy(() => import("pages/mau-tiep-nhan"));
const FieldsGroup = lazy(() => import("pages/nhom-linh-vuc"));
const Field = lazy(() => import("pages/linh-vuc"));
const DangBaoChe = lazy(() => import("pages/dang-bao-che"));
const ChiTieuKiemNghiem = lazy(() => import("pages/chi-tieu-kiem-nghiem"));
const NhomChiTieu = lazy(() => import("pages/nhom-chi-tieu"));
const HoSoTiepNhan = lazy(() => import("pages/tiep-nhan-ho-so"));
const PhanChiaChiTieu = lazy(() => import("pages/phan-chia-chi-tieu"));
const PhanChiaKiemMau = lazy(() => import("pages/phan-chia-kiem-mau"));
const ThongKe = lazy(() => import("pages/thong-ke"));
const QuanTriNguoiDung = lazy(() => import("pages/quan-tri-nguoi-dung"));
const CreatePhieu = lazy(() =>
  import("pages/tiep-nhan-ho-so/components/CreatePhieu")
);
const CreateProduct = lazy(() => import("pages/product/ProductDetails"));
const ProductManagement = lazy(() => import("pages/product"));
const CashbookManagement = lazy(() => import("pages/cashbook"));

const HoatChat = lazy(() => import("pages/hoat-chat"));

function HomeRoutes(props) {
  // let { path, } = useRouteMatch();
  const containers = (
    <Switch>
      <Route path="/" exact component={Dashboard} />
      <Route path="/dashboard" exact component={Dashboard} />
      <Route path="/quan-ly-khoa" exact component={ApartmentManage} />
      <Route path="/quan-ly-phong-ban" exact component={PhongBanManage} />
      <Route path="/quan-ly-nhan-vien" exact component={StaffManage} />
      <Route path="/nhom-khach-hang" exact component={CustomerGroup} />
      <Route path="/khach-hang" exact component={CustomerManage} />
      <Route path="/loai-mau-tiep-nhan" exact component={MauTiepNhan} />
      <Route path="/nhom-linh-vuc" exact component={FieldsGroup} />
      <Route path="/linh-vuc" exact component={Field} />
      <Route path="/dang-bao-che" exact component={DangBaoChe} />
      <Route path="/hoat-chat" exact component={HoatChat} />
      <Route path="/chi-tieu-kiem-nghiem" exact component={ChiTieuKiemNghiem} />
      <Route path="/nhom-chi-tieu" exact component={NhomChiTieu} />
      <Route path="/tiep-nhan-ho-so" exact component={HoSoTiepNhan} />
      <Route path="/ho-so/:id" exact component={PhanChiaChiTieu} />
      <Route path="/phan-chia-chi-tieu" exact component={PhanChiaChiTieu} />
      <Route path="/phan-chia-kiem-mau" exact component={PhanChiaKiemMau} />
      <Route path="/ket-qua-kiem-nghiem" exact component={PhanChiaKiemMau} />
      <Route path="/nha-thau-phu" exact component={PhanChiaKiemMau} />
      <Route path="/tong-hop-ket-qua" exact component={PhanChiaKiemMau} />
      <Route path="/thong-ke/chi-tieu" exact component={ThongKe} />
      <Route path="/he-thong/tai-khoan" exact component={QuanTriNguoiDung} />
      <Route path="/he-thong/nguoi-dung" exact component={QuanTriNguoiDung} />
      <Route path="/tiep-nhan-ho-so/new" exact component={CreatePhieu} />
      <Route path="/quan-ly-nhan-vien/:id" exact component={DetailNhanVien} />
      <Route path="/khach-hang/:id" exact component={CustomerManageDetail} />
      {/* New route */}
      <Route path="/san-pham" exact component={ProductManagement} />
      <Route path="/tao-san-pham" exact component={CreateProduct} />
      <Route path="/san-pham/:id" exact component={CreateProduct} />
      <Route path="/cashbook" exact component={CashbookManagement} />
    </Switch>
  );
  return (
    <div className={props.cls} style={props.style}>
      <Suspense
        fallback={<DelayedFallback size={SIZE_LARGE} tip={"Loading"} />}
      >
        {containers}
      </Suspense>
    </div>
  );
}

export default HomeRoutes;
