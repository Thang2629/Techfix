<template>
  <div style="background: #fffff">
    <a-tabs type="card">
      <a-tab-pane key="1" tab="Chi tiết đơn hàng">
        <a-table
          :columns="columns"
          :dataSource="orderRepairDetail"
          :pagination="false"
          :loading="loading"
          rowKey="pR_code"
        >
          <template slot="description" slot-scope="text">
            <order-repair-row-description :description="text" />
          </template>
        </a-table>
      </a-tab-pane>
    </a-tabs>
  </div>
</template>

<script>
import { mapState } from "vuex";

import STable from "../../../Global/STable";
import OrderRepairRowDescription from "./OrderRepairRowDescription.vue";
export default {
  name: "OrderRepairRowDetail",
  components: {
    STable,
    OrderRepairRowDescription,
  },
  props: {
    rowId: String,
  },
  data() {
    return {
      columns: [
        {
          title: "Mã máy",
          dataIndex: "pR_code",
          scopedSlots: { customRender: "pR_code" },
        },
        {
          title: "Tên máy",
          dataIndex: "pR_Name",
        },
        {
          title: "Chi tiết máy",
          scopedSlots: { customRender: "description" },
        },
        {
          title: "Loại sửa",
          dataIndex: "pR_repair_type_name",
        },
        {
          title: "Tình trạng lỗi",
          dataIndex: "pR_status_error",
        },
      ],
      data: [],
      loading: false,
    };
  },
  mounted() {
    this.fetch();
  },
  methods: {
    fetch() {
      this.loading = true;
      this.$store
        .dispatch("order_repairs/getOrderRepair", { id: this.rowId })
        .then(() => (this.loading = false));
    },
  },
  computed: {
    ...mapState({
      order_repair: (state) => state.order_repairs.order_repair,
    }),
    orderRepairDetail() {
      return this.order_repair.product_repair;
    },
  },
};
</script>

<style></style>
