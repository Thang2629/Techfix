<template>
  <receipt-index :dataSource="receipts" :pagination="pagination" />
</template>

<script>
import RSVP from 'rsvp';
import { mapState } from 'vuex';
import ReceiptIndex from '../../components/Pages/Receipt/Core/ReceiptIndex.vue';

export default {
  name: 'ReceiptPage',

  head() {
    return {
      title: this.current_menu.title,
    };
  },

  components: {
    ReceiptIndex,
  },

  asyncData({ store, query }) {
    return RSVP.all([
      store.dispatch('receipts/getReceipts', { page: 1, perPage: 10 }),
    ]);
  },

  computed: {
    ...mapState({
      receipts: (state) => state.receipts.receipts,
      pagination: (state) => state.receipts.pagination,
      current_menu: (state) => state.menu.current_menu,
    }),
  },
};
</script>

<style></style>
