<template>
  <supplier-index :dataSource="suppliers" :pagination="pagination" />
</template>

<script>
import RSVP from "rsvp";
import { mapState } from "vuex";
import SupplierIndex from "../../components/Pages/Suppliers/Core/SupplierIndex.vue";

export default {
  head() {
    return {
      title: this.current_menu.title,
    };
  },
  components: {
    SupplierIndex,
  },
  asyncData({ store, query }) {
    return RSVP.all([
      store.dispatch("suppliers/getSuppliers", {
        page: 1,
        perPage: 10,
      }),
    ]);
  },
  computed: {
    ...mapState({
      suppliers: (state) => state.suppliers.suppliers,
      pagination: (state) => state.suppliers.pagination,
      current_menu: (state) => state.menu.current_menu,
    }),
  },
};
</script>

<style lang="less" scoped></style>
