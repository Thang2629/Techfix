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
        
          <a-button type="primary" class="fc-group-button blue">
            + Xuất hoá đơn
          </a-button>

      </div>
    </div>

    <div class="fc-group-on-top-v2">
      <s-single-select
        :dataOptions="dataCustomerType"
        v-on:onSelectOption="onSelectCustomerType"
        placeholder="Nhóm khách hàng"
      />
      <s-single-select
        :dataOptions="dataUsers"
        v-on:onSelectOption="onSelectUser"
        placeholder="Chọn nhân viên"
      />
      <s-single-select
        :dataOptions="dataProcessRepairs"
        v-on:onSelectOption="onSelectProcessRepair"
        placeholder="Quy trình sửa chữa"
      />
      <s-date-range-picker
        v-on:onChangeRangePicker="onChangeRangePicker"
        v-on:onChangeTypeMoment="onChangeTypeMoment"
      />
    </div>

    <div class="fc-group-on-top-v3">
      <a-row :gutter="16" type="flex">
        <a-col :flex="1">
          <s-statistic-card
            title="Sl đã sửa xong"
            :value="statistic.fixed_count"
          />
        </a-col>
        <a-col :flex="1">
          <s-statistic-card
            title="Sl trả khách không sửa được"
            :value="statistic.return_customer"
        /></a-col>

        <a-col :flex="1">
          <s-statistic-card
            title="Doanh số dự kiến"
            :value="statistic.expected_revenue"
          />
        </a-col>
      </a-row>
    </div>

    <s-table
      :columns="columns"
      :dataSource="dataSource"
      v-on:onTableChange="onTableChange"
      :loading="loading"
      :pagination="pagination"
      rowKey="id_process_repair"
    >
    </s-table>
  </div>
</template>

<script>
import { mapState } from "vuex";

import STableSearch from "../../../Global/STableSearch.vue";
import STable from "../../../Global/STable";
import SSingleSelect from "../../../Global/SSingleSelect.vue";
import SDateRangePicker from "../../../Global/SDateRangePicker.vue";
import SStatisticCard from "../../../Global/SStatisticCard.vue";

export default {
  name: "ProcessRepairsIndex",
  components: {
    STableSearch,
    STable,
    SSingleSelect,
    SDateRangePicker,
    SStatisticCard,
  },
  props: {
    dataSource: {
      type: Array,
      default: [],
    },
    pagination: {
      type: Object,
    },
    users: {
      type: Array,
      default: [],
    },
  },
  data() {
    return {
      columns: [
        {
          title: "Khách hàng",
          dataIndex: "cus_name",
        },
        {
          title: "Mã BN",
          dataIndex: "order_repair_code",
        },
        {
          title: "Mã máy",
          dataIndex: "product_repair_code",
        },
        {
          title: "Model",
          dataIndex: "model",
        },

        {
          title: "Tình trạng lỗi",
          dataIndex: "status_error",
        },
        {
          title: "Tình trạng sửa",
          dataIndex: "status_fix",
        },
        {
          title: "Ngày nhận",
          dataIndex: "received_date",
        },
        {
          title: "Ngày báo giá",
          dataIndex: "quote_date",
        },
        {
          title: "Giá sửa",
          dataIndex: "fix_price",
        },
        {
          title: "Ngày sửa xong",
          dataIndex: "date_finish",
        },
        {
          title: "Quy trình SC",
          dataIndex: "process_repair_type_name",
        },
        {
          title: "Tình trạng sửa",
          dataIndex: "c_code",
        },
        {
          title: "Nhân viên sửa",
          dataIndex: "user_name_fix",
        },
        {
          title: "Hoàn thành",
          dataIndex: "",
        },
      ],
      loading: false,
      perPage: 10,
      page: 1,
      q: "",
      userId: null,
      processRepairType: null,
      dateRange: [],
      dateFormatPayload: "YYYY/MM/DD",
    };
  },
  methods: {
    fetch() {
      this.loading = true;
      const payload = {
        page: this.page,
        perPage: this.perPage,
        filter: this.q,
        user_id: this.userId,
        eProcessRepairType: this.processRepairType,
        fromDate: this.dateRange[0]?.format(this.dateFormatPayload),
        toDate: this.dateRange[1]?.format(this.dateFormatPayload),
        customerTypeEnum: this.customerTypeEnum,
      };

      this.$store
        .dispatch("process_repairs/getProcessRepairs", { ...payload })
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

    onSelectUser(userId) {
      this.page = 1;
      this.userId = userId;
      this.fetch();
    },

    onSelectProcessRepair(processRepairType) {
      this.page = 1;
      this.processRepairType = processRepairType;
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
    onSelectCustomerType(id) {
      this.page = 1;
      this.customerTypeEnum = id;
      this.fetch();
    },
  },
  computed: {
    ...mapState({
      statistic: (state) => state.process_repairs.statistic,
    }),
    dataUsers() {
      return this.users.map(({ id, u_username }) => ({
        id,
        name: u_username,
      }));
    },
    dataProcessRepairs() {
      return this.dataSource.map(
        ({ process_repair_type, process_repair_type_name }) => ({
          id: process_repair_type,
          name: process_repair_type_name,
        })
      );
    },
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

<style></style>
