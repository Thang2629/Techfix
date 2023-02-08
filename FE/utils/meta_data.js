import {
  ProductIcon,
  CustomerIcon,
  OrderRepairIcon,
  ConfigurationIcon,
  OverviewIcon,
  WarrantyIcon,
  WarehouseIcon,
  ClipboardIcon,
  ChartIcon,
} from '~/components/Icons/index.js';

export const NAV_ITEMS = {
  overview: {
    name: 'Tổng quan',
    path: '/overview',
    icon: OverviewIcon,
    children: [],
  },

  order_repairs: {
    name: 'Sửa chữa',
    path: '/order_repairs',
    icon: OrderRepairIcon,
    children: [
      {
        name: 'Đơn sửa chữa',
        path: '/order_repairs',
        icon: OrderRepairIcon,
      },
      {
        name: 'Quy trình sửa chữa',
        path: '/process_repairs',
        icon: OrderRepairIcon,
      },
      {
        name: 'Báo cáo sửa chữa',
        path: '/report_repairs',
        icon: OrderRepairIcon,
      },
      {
        name: 'Doanh số sửa chữa',
        path: '/repair_revenue',
        icon: OrderRepairIcon,
      },
    ],
  },

  warranty: {
    name: 'Bảo hành',
    path: '/warranty',
    icon: WarrantyIcon,
    children: [
      {
        name: 'Đơn bảo hành',
        path: '/warranty',
        icon: WarrantyIcon,
      },
      {
        name: 'Quy trình bảo hành',
        path: '/process_warranty',
        icon: WarrantyIcon,
      },
      {
        name: 'Báo cáo bảo hành',
        path: '/report_warranty',
        icon: WarrantyIcon,
      },
    ],
  },

  orders: {
    name: 'Đơn hàng',
    path: '/orders',
    icon: ProductIcon,
    children: [],
  },

  products: {
    name: 'Sản phẩm',
    path: '/products',
    icon: ProductIcon,
    children: [
      {
        name: 'Sản phẩm',
        path: '/products',
        icon: ProductIcon,
      },
      {
        name: 'Mã vạch',
        path: '/barcode',
        icon: ProductIcon,
      },
    ],
  },

  customers: {
    name: 'Khách hàng',
    path: '/customers',
    icon: CustomerIcon,
    children: [
      {
        name: 'Khách hàng',
        path: '/customers',
        icon: CustomerIcon,
      },

      {
        name: 'Nhà cung cấp',
        path: '/suppliers',
        icon: CustomerIcon,
      },
    ],
  },

  warehouse: {
    name: 'Hàng kho',
    path: '/warehouse_in',
    icon: WarehouseIcon,
    children: [
      {
        name: 'Nhập kho',
        path: '/warehouse_in',
        icon: WarehouseIcon,
      },
      {
        name: 'Chuyển kho',
        path: '/warehouse_out',
        icon: WarehouseIcon,
      },
      {
        name: 'Tồn kho',
        path: '/warehouse_transfer',
        icon: WarehouseIcon,
      },
    ],
  },

  profit: {
    name: 'Báo cáo',
    path: '/profit',
    icon: ChartIcon,
    children: [
      {
        name: 'Lợi nhuận',
        path: '/profit',
        icon: ChartIcon,
      },
      {
        name: 'Doanh số tổng',
        path: '/revenue',
        icon: ChartIcon,
      },
      {
        name: 'Báo cáo tổng hợp',
        path: '/report',
        icon: ChartIcon,
      },
    ],
  },

  paper: {
    name: 'Sổ sách',
    path: '/receipt',
    icon: ClipboardIcon,
    children: [
      {
        name: 'Phiếu thu',
        path: '/receipt',
        icon: ClipboardIcon,
      },
      {
        name: 'Phiếu chi',
        path: '/payment',
        icon: ClipboardIcon,
      },
      {
        name: 'Sổ quỷ',
        path: '/cashbook',
        icon: ClipboardIcon,
      },
    ],
  },

  configuration: {
    name: 'Thiết lập',
    path: '/users',
    icon: ConfigurationIcon,
    children: [
      {
        name: 'Nhân viên',
        path: '/users',
        icon: OrderRepairIcon,
      },
      {
        name: 'Phân quyền',
        path: '/roles',
        icon: OrderRepairIcon,
      },
      {
        name: 'Kho',
        path: '/stores',
        icon: OrderRepairIcon,
      },
    ],
  },
};

export const findNavItemsByRole = (role) => {
  let items = [];
  const {
    customers,
    products,
    order_repairs,
    configuration,
    overview,
    warranty,
    warehouse,
    paper,
    profit,
    orders,
  } = NAV_ITEMS;
  items = [
    overview,
    order_repairs,
    warranty,
    orders,
    products,
    customers,
    warehouse,
    profit,
    paper,
    configuration,
  ];
  return items;
};
