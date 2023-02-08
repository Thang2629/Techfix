<template>
  <div>
    <div class="fc-group-on-top">
      <div class="fc-group__left">
        <s-table-search
          v-on:onInputSearchSubmit="handleInputSearchSubmit"
          placeholder="Nhập tên, mã hoặc SĐT"
        />
      </div>
    </div>

    <div class="fc-group-on-top-v2">
      <s-single-select
        :dataOptions="dataUsers"
        v-on:onSelectOption="onSelectUser"
        placeholder="Chọn nhân viên"
      />
      <s-single-select
        :dataOptions="dataProcess"
        v-on:onSelectOption="onSelectProcess"
        placeholder="Quy trình BH"
      />
      <s-single-select
        :dataOptions="dataType"
        v-on:onSelectOption="onSelectType"
        placeholder="Loại BH"
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
            title="Sl đang BH"
            :value="dataSource.warranty_count"
        /></a-col>
        <a-col :flex="1">
          <s-statistic-card
            title="Sl BH xong"
            :value="dataSource.warranty_done"
          />
        </a-col>
        <a-col :flex="1">
          <s-statistic-card
            title="Sl trả khách"
            :value="dataSource.warranty_return_customer"
          />
        </a-col>
      </a-row>
    </div>

    <s-table
      :columns="columns"
      :dataSource="dataSource.warranty_lst"
      v-on:onTableChange="onTableChange"
      :loading="loading"
      :pagination="pagination"
      rowKey="order_warranty_code"
    >
      <template slot="date_received" slot-scope="text, record">
        <span>{{
          moment(record.date_received).format("DD/MM/YYYY HH:mm:ss")
        }}</span>
      </template>
    </s-table>
  </div>
</template>

<script>
import moment from "moment";
import STableSearch from "../../../Global/STableSearch.vue";
import STable from "../../../Global/STable";
import SSingleSelect from "../../../Global/SSingleSelect.vue";
import SDateRangePicker from "../../../Global/SDateRangePicker.vue";
import SStatisticCard from "../../../Global/SStatisticCard.vue";

export default {
  name: "ProcessWarrantyIndex",
  components: {
    STableSearch,
    STable,
    SSingleSelect,
    SDateRangePicker,
    SStatisticCard,
  },
  props: {
    dataSource: {
      type: Object,
      default: {},
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
          dataIndex: "customer_name",
        },
        {
          title: "Mã BNBH",
          dataIndex: "order_warranty_code",
        },
        {
          title: "Mã máy",
          dataIndex: "pW_code",
        },
        {
          title: "Model",
          dataIndex: "pW_name",
        },
        {
          title: "Số lần BH",
          dataIndex: "warranty_times",
        },
        {
          title: "Loại BH",
          dataIndex: "product_warranty_type",
        },
        {
          title: "Tình trạng lỗi",
          dataIndex: "pW_status",
        },
        {
          title: "Tình trạng bảo hành",
          dataIndex: "warranty_status",
        },
        {
          title: "Ngày nhận",
          dataIndex: "date_received",
          scopedSlots: { customRender: "date_received" }
        },

        {
          title: "Quy trình BH",
          dataIndex: "warranty_process",
        },
        {
          title: "Nhân viên BH",
          dataIndex: "user_warranty",
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
      eProductWarrantyType: null,
      eWarrantyProcess: null,
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
        user_id: this.userId,
        eProductWarrantyType: this.eProductWarrantyType,
        eWarrantyProcess: this.eWarrantyProcess,
        fromDate: this.dateRange[0]?.format(this.dateFormatPayload),
        toDate: this.dateRange[1]?.format(this.dateFormatPayload),
      };

      this.$store
        .dispatch("warranties/getProcessWarranties", { ...payload })
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

    onSelectProcess(process) {
      this.page = 1;
      this.eWarrantyProcess = process;
      this.fetch();
    },

    onSelectType(type) {
      this.page = 1;
      this.eProductWarrantyType = type;
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
    dataUsers() {
      return this.users.map(({ id, u_username }) => ({
        id,
        name: u_username,
      }));
    },
    dataProcess() {
      return [
        {
          id: 1,
          name: "Đang bảo hành",
        },
        {
          id: 2,
          name: "Bảo hành xong",
        },
        {
          id: 3,
          name: "Trả khách không sửa được",
        },
      ];
    },
    dataType() {
      return [
        {
          id: 1,
          name: "SC",
        },
        {
          id: 2,
          name: "LK",
        },
        {
          id: 3,
          name: "LT",
        },
        {
          id: 4,
          name: "PC",
        },
      ];
    },
  },
};
</script>

<style></style>
