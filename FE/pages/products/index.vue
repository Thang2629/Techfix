<template>
  <product-index :dataSource="products" :pagination="pagination" />
</template>

<script>
import RSVP from "rsvp";
import { mapState } from "vuex";

import ProductIndex from "pagesComponent/Products/Core/ProductIndex.vue";
export default {
  head() {
    return {
      title: this.current_menu.title,
    };
  },
  components: {
    ProductIndex,
  },
  asyncData({ store, query }) {
    return RSVP.all([
      store.dispatch("products/getProducts", { page: 1, perPage: 10 }),
      store.dispatch("products/getProductConditions", {
        page: 1,
        perPage: 100,
      }),
      store.dispatch("manufacturers/getManufacturers", {
        page: 1,
        perPage: 100,
      }),
      store.dispatch("categories/getCategories", {
        page: 1,
        perPage: 100,
      }),
    ]);
  },
  computed: {
    ...mapState({
      products: (state) => state.products.products,
      pagination: (state) => state.products.pagination,
      current_menu: (state) => state.menu.current_menu,
    }),
  },
};
</script>

<style lang="less" scoped>
</style>
