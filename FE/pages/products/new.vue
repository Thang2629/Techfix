<template>
  <div class="page-container">
    <div class="fc-breadcrumb">
      <a-breadcrumb>
        <span slot="separator">/</span>
        <a-breadcrumb-item>
          <nuxt-link to="/products"> Sản phẩm </nuxt-link>
        </a-breadcrumb-item>
        <a-breadcrumb-item> Tạo sản phẩm </a-breadcrumb-item>
      </a-breadcrumb>
    </div>
    <div>
      <product-form />
    </div>
  </div>
</template>

<script>
import RSVP from "rsvp";

import ProductForm from "../../components/Pages/Products/Core/ProductForm.vue";
export default {
  components: {
    ProductForm,
  },
  asyncData({ store, query }) {
    return RSVP.all([
      store.dispatch("products/getProductUnits", {
        page: 1,
        perPage: 100,
      }),
      store.dispatch("categories/getCategories", {}),
      store.dispatch("suppliers/getSuppliers", {
        page: 1,
        perPage: 100,
      }),
      store.dispatch("manufacturers/getManufacturers", {
        page: 1,
        perPage: 100,
      }),
      store.dispatch("products/getProductConditions", {
        page: 1,
        perPage: 100,
      }),
    ]);
  },
};
</script>

<style>
</style>
ProductForm