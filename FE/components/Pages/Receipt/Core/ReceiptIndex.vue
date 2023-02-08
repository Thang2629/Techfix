<template>
  <div>
    <div class="fc-group-on-top">
      <div class="fc-group__left">
        <s-table-search
          v-on:onInputSearchSubmit="handleInputSearchSubmit"
          placeholder="Tên KH - SĐT - Phiếu biên nhận"
        />
      </div>
    </div>
    <div class="fc-group-on-top-v2">
      <s-date-range-picker
        v-on:onChangeRangePicker="onChangeRangePicker"
        v-on:onChangeTypeMoment="onChangeTypeMoment"
      />
    </div>
    <s-table
      :columns="columns"
      :dataSource="receipts"
      v-on:onTableChange="onTableChange"
      :loading="loading"
      :pagination="pagination"
      rowKey="id"
    />
  </div>
</template>
<script>
import { mapState } from 'vuex';
import moment from 'moment';

import STableSearch from '../../../Global/STableSearch.vue';
import STableSelectEntries from '../../../Global/STableSelectEntries.vue';
import STable from '../../../Global/STable';
import SSingleSelect from '../../../Global/SSingleSelect.vue';

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
          title: 'Mã phiếu thu',
          dataIndex: 'receipt_code',
        },
        {
          title: 'Mã phiếu xuất',
          dataIndex: '',
        },
        {
          title: 'Diện thoại',
          dataIndex: '',
        },
        {
          title: 'Tên KH - NCC',
          dataIndex: 'supplier_name',
        },
        {
          title: 'Chứng từ liên quan',
          dataIndex: '',
        },
        {
          title: 'Kho thu',
          dataIndex: 'store_name',
        },
        {
          title: 'Ngày thu',
          dataIndex: 'receipt_date',
        },
        {
          title: 'Người thu',
          dataIndex: 'user_name',
        },
        {
          title: 'Ghi chú',
          dataIndex: '',
        },
        {
          title: 'Nợ còn lại',
          dataIndex: 'debt',
        },
        {
          title: 'Tổng tiền',
          dataIndex: 'total_price',
        },
        {
          dataIndex: '',
          scopedSlots: { customRender: 'action' },
        },
      ],
      loading: false,
      perPage: 10,
      page: 1,
      q: '',
      customerTypeEnum: null,
      dateRange: [],
      dateFormatPayload: 'YYYY/MM/DD',
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
        fromDate: this.dateRange[0]?.format(this.dateFormatPayload),
        toDate: this.dateRange[1]?.format(this.dateFormatPayload),
      };

      this.$store
        .dispatch('receipts/getReceipts', { ...payload })
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
        path: '/warranty/new',
      });
    },
  },
  computed: {
    ...mapState({
      receipts: (state) => state.receipts.receipts,
    }),
  },
};
</script>

<style lang="less" scoped></style>
