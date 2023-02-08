<template>
  <div>
    <div class="fc-group-on-top">
      <div class="fc-group__left">
        <s-table-search
          v-on:onInputSearchSubmit="handleInputSearchSubmit"
          placeholder="Nhập tên, mã hoặc SĐT"
        />
      </div>
      <div class="fc-group__right">
        <customer-create v-on:fetchData="fetch" />
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
      :dataSource="dataSource"
      v-on:onTableChange="onTableChange"
      :loading="loading"
      :pagination="pagination"
      rowKey="c_code"
    >
      <template slot="c_code" slot-scope="text, record">
        <customer-edit :detail="record" />
      </template>
      <template slot="customer_type" slot-scope="text, record">
        <span v-if="record.customer_type === 0">Khách lẻ</span>
        <span v-else>Khách sỉ</span>
      </template>
      <template slot="action" slot-scope="text, record">
        <div style="display: flex; align-items: center">
          <customer-delete :customerId="record.id" v-on:fetchData="fetch" />
        </div>
      </template>
    </s-table>
  </div>
</template>
<script>
import { mapState } from "vuex";

import STableSearch from "../../../Global/STableSearch.vue";
import STableSelectEntries from "../../../Global/STableSelectEntries.vue";
import STable from "../../../Global/STable";
import SSingleSelect from "../../../Global/SSingleSelect.vue";
import SDateRangePicker from "../../../Global/SDateRangePicker.vue";

import CustomerCreate from "../Actions/CustomerCreate.vue";
import CustomerDelete from "../Actions/CustomerDelete.vue";
import CustomerEdit from "../Actions/CustomerEdit.vue";
import moment from "moment";

export default {
  components: {
    CustomerCreate,
    CustomerDelete,
    STable,
    STableSearch,
    STableSelectEntries,
    SSingleSelect,
    SDateRangePicker,
    CustomerEdit,
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
          title: "Mã KH",
          dataIndex: "c_code",
          scopedSlots: { customRender: "c_code" },
          sorter: true,
          width: 1,
        },
        {
          title: "Nhóm KH",
          dataIndex: "customer_type",
          sorter: true,
          scopedSlots: { customRender: "customer_type" },
          width: 1,
        },
        {
          title: "Tên KH",
          dataIndex: "c_name",
          sorter: true,
          width: 1.2,
        },
        {
          title: "Điện thoại",
          dataIndex: "c_phone",
          sorter: true,
          width: 1,
        },
        {
          title: "Địa chỉ",
          dataIndex: "c_address",
          sorter: true,
          width: 1,
        },
        {
          title: "Action",
          dataIndex: "",
          scopedSlots: { customRender: "action" },
          disabled: true,
          defaultChecked: true,
          width: 0.5,
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
        customerTypeEnum: this.customerTypeEnum,
        fromDate: this.dateRange[0]?.format(this.dateFormatPayload),
        toDate: this.dateRange[1]?.format(this.dateFormatPayload),
      };
      this.$store
        .dispatch("customers/getCustomers", { ...payload })
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
  },
  computed: {
    ...mapState({}),
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
