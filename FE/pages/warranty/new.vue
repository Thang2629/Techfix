<template>
  <div class="page-container">
    <div class="fc-breadcrumb">
      <a-breadcrumb>
        <span slot="separator">/</span>
        <a-breadcrumb-item>
          <nuxt-link to="/warranty"> Đơn bảo hành </nuxt-link>
        </a-breadcrumb-item>
        <a-breadcrumb-item> Tạo đơn bảo hành </a-breadcrumb-item>
      </a-breadcrumb>
    </div>
    <div>
      <warranty-form />
    </div>
  </div>
</template>

<script>
import RSVP from "rsvp";

import WarrantyForm from "../../components/Pages/Warranty/Core/WarrantyForm.vue";

export default {
  components: {
    WarrantyForm,
  },
  asyncData({ store, query }) {
    return RSVP.all([
      store.dispatch("customers/getCustomers", { page: 1, perPage: 10 }),
    ]);
  },
};
</script>

<style></style>
