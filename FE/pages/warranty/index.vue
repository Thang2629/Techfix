<template>
  <warranty-index :dataSource="warranties" :pagination="pagination" />
</template>

<script>
import RSVP from "rsvp";
import { mapState } from "vuex";

import WarrantyIndex from "../../components/Pages/Warranty/Core/WarrantyIndex.vue";
export default {
  name: "WarrantyPage",
  components: {
    WarrantyIndex,
  },
  head() {
    return {
      title: this.current_menu.title,
    };
  },
  asyncData({ store, query }) {
    return RSVP.all([
      store.dispatch("warranties/getWarranties", { page: 1, perPage: 10 }),
    ]);
  },
  computed: {
    ...mapState({
      warranties: (state) => state.warranties.warranties,
      pagination: (state) => state.warranties.pagination,
      current_menu: (state) => state.menu.current_menu,
    }),
  },
};
</script>

<style></style>
