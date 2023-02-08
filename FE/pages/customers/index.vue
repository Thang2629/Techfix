<template>
  <customer-index :dataSource="customers" :pagination="pagination" />
</template>

<script>
import RSVP from "rsvp";
import { mapState } from "vuex";
import CustomerIndex from "pagesComponent/Customers/Core/CustomerIndex.vue";

export default {
  head() {
    return {
      title: this.current_menu.title,
    };
  },
  components: {
    CustomerIndex,
  },
  asyncData({ store, query }) {
    return RSVP.all([
      store.dispatch("customers/getCustomers", { page: 1, perPage: 10 }),
    ]);
  },
  computed: {
    ...mapState({
      customers: (state) => state.customers.customers,
      pagination: (state) => state.customers.pagination,
      current_menu: (state) => state.menu.current_menu,
    }),
  },
};
</script>

<style lang="less" scoped>
</style>
