<template>
  <process-warranties-index
    :dataSource="warranties"
    :pagination="pagination"
    :users="users"
  />
</template>

<script>
import RSVP from "rsvp";
import { mapState } from "vuex";
import ProcessWarrantiesIndex from "../../components/Pages/ProcessWarranties/Core/ProcessWarrantiesIndex.vue";

export default {
  head() {
    return {
      title: this.current_menu.title,
    };
  },
  components: { ProcessWarrantiesIndex },
  asyncData({ store, query }) {
    return RSVP.all([
      store.dispatch("warranties/getProcessWarranties", {
        page: 1,
        perPage: 10,
      }),
      store.dispatch("users/getUsers"),
    ]);
  },
  computed: {
    ...mapState({
      warranties: (state) => state.warranties.process_warranties,
      pagination: (state) => state.warranties.pagination,
      current_menu: (state) => state.menu.current_menu,
      users: (state) => state.users.users,
    }),
  },
};
</script>

<style></style>
