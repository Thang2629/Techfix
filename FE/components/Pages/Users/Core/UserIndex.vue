<template>
  <div>
    <s-table
      :columns="columns"
      :dataSource="dataSource"
      v-on:onTableChange="onTableChange"
      :loading="loading"
      :pagination="pagination"
      rowKey="id"
    >
      <template slot="index" slot-scope="text, record, index">
        {{ index + 1 }}
      </template>
      <template slot="status" slot-scope="text, record">
        <user-status :status="record.u_status" />
      </template>
    </s-table>
  </div>
</template>
<script>
import { mapState } from "vuex";

import STableSelectEntries from "../../../Global/STableSelectEntries.vue";
import STable from "../../../Global/STable";

import UserStatus from "./UserStatus.vue";

export default {
  components: {
    STable,
    STableSelectEntries,
    UserStatus,
  },
  props: {
    dataSource: {
      type: Array,
      default: [],
    },
    pagination: {
      type: Object,
      default: {},
    },
  },
  data() {
    return {
      columns: [
        {
          title: "#",
          dataIndex: "id",
          scopedSlots: { customRender: "index" },
        },
        {
          title: "Mã NV",
          dataIndex: "u_code",
        },
        {
          title: "Tên NV",
          dataIndex: "u_name",
        },
        {
          title: "Email",
          dataIndex: "u_email",
        },
        {
          title: "Hệ số thưởng",
          dataIndex: "u_commission",
        },
        {
          title: "Nhóm người sử dụng",
          dataIndex: "id_roles",
        },
        {
          title: "Trạng thái",
          dataIndex: "u_status",
          scopedSlots: { customRender: "status" },
        },
        {
          title: "Action",
          dataIndex: "",
          scopedSlots: { customRender: "action" },
        },
      ],
      loading: false,
      perPage: 10,
      page: 1,
    };
  },
  methods: {
    fetch() {
      this.loading = true;

      const payload = {
        page: this.page,
        perPage: this.perPage,
      };
      this.$store
        .dispatch("users/getUsers", { ...payload })
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
  },
  computed: {
    ...mapState({}),
  },
};
</script>

<style lang="less" scoped></style>
