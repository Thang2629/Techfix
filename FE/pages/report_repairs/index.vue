<template>
  <report-repair-index
    :dataSource="process_repairs"
    :pagination="pagination"
    :users="users"
  />
</template>

<script>
import RSVP from "rsvp";
import { mapState } from "vuex";
import ProcessRepairsIndex from "../../components/Pages/ProcessRepairs/Core/ProcessRepairsIndex.vue";
import ReportRepairIndex from "../../components/Pages/ReportRepairs/Core/ReportRepairIndex.vue";

export default {
  head() {
    return {
      title: this.current_menu.title,
    };
  },
  components: {
    ProcessRepairsIndex,
    ReportRepairIndex,
  },
  asyncData({ store, query }) {
    return RSVP.all([
      store.dispatch("process_repairs/getProcessRepairs", {
        page: 1,
        perPage: 10,
      }),
      store.dispatch("users/getUsers"),
    ]);
  },
  computed: {
    ...mapState({
      process_repairs: (state) => state.process_repairs.process_repairs,
      pagination: (state) => state.process_repairs.pagination,
      current_menu: (state) => state.menu.current_menu,
      users: (state) => state.users.users,
    }),
  },
};
</script>

<style></style>
