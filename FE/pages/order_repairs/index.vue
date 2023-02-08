<template>
  <order-repair-index :dataSource="order_repairs" :pagination="pagination" />
</template>

<script>
import RSVP from "rsvp";
import { mapState } from "vuex";
import OrderRepairIndex from "../../components/Pages/OrderRepairs/Core/OrderRepairIndex.vue";

export default {
  name: "OrderRepairPage",
  head() {
    return {
      title: this.current_menu.title,
    };
  },
  components: {
    OrderRepairIndex,
  },
  asyncData({ store, query }) {
    return RSVP.all([
      store.dispatch("order_repairs/getOrderRepairs", { page: 1, perPage: 10 }),
    ]);
  },
  computed: {
    ...mapState({
      order_repairs: (state) => state.order_repairs.order_repairs,
      pagination: (state) => state.order_repairs.pagination,
      current_menu: (state) => state.menu.current_menu,
    }),
  },
};
</script>

<style lang="less" scoped></style>
