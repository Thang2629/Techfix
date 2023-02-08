<template>
  <div class="page-container">
    <div class="fc-breadcrumb">
      <a-breadcrumb>
        <span slot="separator">/</span>
        <a-breadcrumb-item>
          <nuxt-link to="/order_repairs"> Đơn sửa chữa </nuxt-link>
        </a-breadcrumb-item>
        <a-breadcrumb-item> Tạo đơn sc </a-breadcrumb-item>
      </a-breadcrumb>
    </div>
    <div>
      <order-repair-form />
    </div>
  </div>
</template>

<script>
import RSVP from "rsvp";

import OrderRepairForm from "../../components/Pages/OrderRepairs/Core/OrderRepairForm.vue";

export default {
  components: {
    OrderRepairForm,
  },
  asyncData({ store, query }) {
    return RSVP.all([
      store.dispatch("customers/getCustomers", { page: 1, perPage: 10 }),
    ]);
  },
};
</script>

<style></style>
