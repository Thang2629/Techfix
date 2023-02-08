<template>
  <div>
    <div class="fc-group-on-top">
      <div class="fc-group__left">
        <s-table-search
          v-on:onInputSearchSubmit="handleInputSearchSubmit"
          placeholder="Nhập tên, mã hoặc SĐT"
        />
      </div>
      <div class="fc-group__right"></div>
    </div>
    <div class="fc-group-on-top-v2">
      <!-- <s-single-select
        :dataOptions="dataCustomerType"
        v-on:onSelectOption="onSelectCustomerType"
        placeholder="Nhóm khách hàng"
      />
      <s-date-range-picker
        v-on:onChangeRangePicker="onChangeRangePicker"
        v-on:onChangeTypeMoment="onChangeTypeMoment"
      /> -->
    </div>
    <s-table
      :columns="columns"
      :dataSource="dataSource"
      v-on:onTableChange="onTableChange"
      :loading="loading"
      :pagination="pagination"
      rowKey="id"
    >
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
import SDateRangePicker from "../../../Global/SDateRangePicker.vue";

export default {
  components: {
    STable,
    STableSearch,
    STableSelectEntries,
    SSingleSelect,
    SDateRangePicker,
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
        // {
        //   title: "Hình ảnh",
        //   dataIndex: "id",
        // },
        // {
        //   title: "Mã NCC",
        //   dataIndex: "id",
        // },
        {
          title: "Tên NCC",
          dataIndex: "name",
        },
        {
          title: "Điện thoại",
          dataIndex: "phone",
        },
        {
          title: "Địa chỉ",
          dataIndex: "address",
        },
        {
          title: "Action",
          dataIndex: "",
        },
      ],
      loading: false,
      perPage: 10,
      page: 1,
      q: "",
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
      };
      this.$store
        .dispatch("suppliers/getSuppliers", { ...payload })
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

    // onChangeRangePicker(dateRange) {
    //   this.page = 1;
    //   this.dateRange = dateRange;
    //   this.fetch();
    // },
    // onChangeTypeMoment(dateRange) {
    //   this.page = 1;
    //   this.dateRange = dateRange;
    //   this.fetch();
    // },
  },
  computed: {
    ...mapState({}),
  },
};
</script>

<style lang="less" scoped></style>
