import ButtonBack from 'common/components/Buttons/ButtonBack'
import ImplementResult from 'common/components/result/Implement'
import TabsSection from 'common/components/TabsSection/TabsSection'
import PageWrapper from 'components/Layout/PageWrapper'
import HeaderPage from 'pages/home/header-page'
import React from 'react'
import ThongTinKhachHang from './tabs-component/ThongTinKhachHang'

const DetailCustomer = props => {
    const styleButton = {
        marginBottom: "5px",
        display: "flex",
        alignItems: "center",
        justifyContent: "end",
    }

    const items = [
        { label: <>Thông tin khách hàng</>, key: '1', children: <ThongTinKhachHang /> },
        { label: <>Lịch sử mẫu</>, key: '2', children: <ImplementResult module="module" /> },
        { label: <>Khác</>, key: '3', children: <ImplementResult module="module" /> },
    ];

    const handleUpdate = () => {
        return <div className='groupbtn' style={styleButton}>
            <ButtonBack url="/khach-hang" />
        </div>
    }

    return (
        <div>
            <HeaderPage title="CHI TIẾT KHÁCH HÀNG" actions={handleUpdate} />
            <div className="main__application">
                <PageWrapper>
                    <TabsSection items={items} />
                </PageWrapper>
            </div>

        </div>
    )
}

DetailCustomer.propTypes = {}

export default DetailCustomer