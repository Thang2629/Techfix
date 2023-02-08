<template>
  <div>
    <div class="fc-group-on-top">
      <div class="fc-group__left">
        <s-table-search
          v-on:onInputSearchSubmit="handleInputSearchSubmit"
          placeholder="Tên KH - SĐT - Phiếu biên nhận"
        />
      </div>
      <div class="fc-group__right">
        <nuxt-link to="/warranty/new">
          <a-button type="primary" class="fc-group-button blue">
            + Tạo mới
          </a-button>
        </nuxt-link>
      </div>
    </div>
    <div class="fc-group-on-top-v2">
      <s-single-select
        :dataOptions="dataCustomerType"
        v-on:onSelectOption="onSelectCustomerType"
        placeholder="Nhóm khách hàng"
      />
      <s-date-range-picker
        v-on:onChangeRangePicker="onChangeRangePicker"
        v-on:onChangeTypeMoment="onChangeTypeMoment"
      />
    </div>
    <s-table
      :columns="columns"
      :dataSource="warranties"
      v-on:onTableChange="onTableChange"
      :loading="loading"
      :pagination="pagination"
      rowKey="oW_code"
    >
      <template slot="customer_type" slot-scope="text, record">
        <span v-if="record.customer_type_name == 'Retail_customer'"
          >Khách lẻ</span
        >
        <span v-else>Khách sỉ</span>
      </template>
      <template slot="received_time" slot-scope="text, record">
        <span>{{
          moment(record.received_time).format("DD/MM/YYYY HH:mm:ss")
        }}</span>
      </template>

      <template slot="order_repair_code" slot-scope="text, record">
        <a-tooltip title="Nhấn để cập nhật">
          <a @click="handleClickEdit(record.oW_code)" v-if="record.oW_code"
            >{{ record.order_repair_code }}
          </a>
        </a-tooltip>
      </template>
    </s-table>
  </div>
</template>
<script>
import { mapState } from "vuex";
import moment from "moment";

import STableSearch from "../../../Global/STableSearch.vue";
import STableSelectEntries from "../../../Global/STableSelectEntries.vue";
import STable from "../../../Global/STable";
import SSingleSelect from "../../../Global/SSingleSelect.vue";

export default {
  components: {
    STable,
    STableSearch,
    STableSelectEntries,
    SSingleSelect,
  },
  props: {
    dataSource: {
      type: Array,
      default: [],
    },
    pagination: {
      type: Object,
    },
  },
  data() {
    return {
      columns: [
        {
          title: "Mã BNBH",
          dataIndex: "oW_code",
        },
        {
          title: "Điện thoại",
          dataIndex: "phone",
        },
        {
          title: "Khách hàng",
          dataIndex: "customer_name",
        },
        {
          title: "Nhóm Khách hàng",
          dataIndex: "customer_type_name",
          scopedSlots: { customRender: "customer_type" },
        },
        {
          title: "Ngày giờ nhận",
          dataIndex: "received_time",
          scopedSlots: { customRender: "received_time" },
        },
        {
          title: "Cửa hàng",
          dataIndex: "store_name",
        },
        {
          title: "Nhân viên",
          dataIndex: "user_name",
        },
        {
          dataIndex: "",
          scopedSlots: { customRender: "action" },
        },
      ],
      loading: false,
      perPage: 10,
      page: 1,
      q: "",
      customerTypeEnum: null,
      dateRange: [],
      dateFormatPayload: "YYYY/MM/DD",
    };
  },
  methods: {
    moment,
    fetch() {
      this.loading = true;

      const payload = {
        page: this.page,
        perPage: this.perPage,
        filter: this.q,
        e_customer_type: this.customerTypeEnum,
        fromDate: this.dateRange[0]?.format(this.dateFormatPayload),
        toDate: this.dateRange[1]?.format(this.dateFormatPayload),
      };

      this.$store
        .dispatch("order_repairs/getOrderRepairs", { ...payload })
        .then(() => (this.loading = false));
    },

    onTableChange(value) {
      if (this.page != value.pagination.current) {
        this.page = value.pagination.current;
      } else {
        this.page = 1;
      }
      this.fetch();
    },

    handleInputSearchSubmit(value) {
      this.page = 1;
      this.q = value;
      this.fetch();
    },
    onSelectCustomerType(id) {
      this.page = 1;
      this.customerTypeEnum = id;
      this.fetch();
    },
    onChangeRangePicker(dateRange) {
      this.page = 1;
      this.dateRange = dateRange;
      this.fetch();
    },
    onChangeTypeMoment(dateRange) {
      this.page = 1;
      this.dateRange = dateRange;
      this.fetch();
    },
    handleClickEdit(id) {
      this.$router.push({
        path: "/warranty/new",
      });
    },
  },
  computed: {
    ...mapState({
      warranties: (state) => state.warranties.warranties,
    }),
    dataCustomerType() {
      const cType = [
        {
          id: 0,
          name: "Khách lẻ",
        },
        {
          id: 1,
          name: "Khách sỉ",
        },
      ];
      return cType;
    },
  },
};
</script>

<style lang="less" scoped></style>
