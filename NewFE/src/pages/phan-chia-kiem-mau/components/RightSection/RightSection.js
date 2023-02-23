import React from 'react'
import PropTypes from 'prop-types'
import TabsSection from 'common/components/TabsSection/TabsSection';
import ThongTinMau from './ThongTinMau';
import InPhieuTiepNhan from './InPhieuTiepNhan';
import LichSuHoatDong from './LichSuHoatDong';
import TopInfo from '../TopInfo/TopInfo';
import { InfoCircleOutlined } from '@ant-design/icons';
import DanhSachChiTieu from './DanhSachChiTieu'

function RightSection(props) {
  const items = [
    { label: <> <InfoCircleOutlined /> Thông tin mẫu</>, key: '1', children: <ThongTinMau /> },
    { label: <> <InfoCircleOutlined /> Danh sách chỉ tiêu</>, key: '2', children: <DanhSachChiTieu /> },
    { label: <> <InfoCircleOutlined /> In phiếu tiếp nhận</>, key: '3', children: <InPhieuTiepNhan /> },
    { label: <> <InfoCircleOutlined /> Xuất kết quả</>, key: '4', children: "Xuat kq" },
    { label: <> <InfoCircleOutlined /> Lịch sử hoạt động</>, key: '5', children: <LichSuHoatDong /> },
  ];

  return (
    <div>
      <TabsSection items={items} />
    </div>
  )
}

RightSection.propTypes = {}

export default RightSection
