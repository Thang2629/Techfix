<template>
  <user-index :dataSource="users" :pagination="pagination" />
</template>

<script>
import RSVP from "rsvp";
import { mapState } from "vuex";
import UserIndex from "../../components/Pages/Users/Core/UserIndex.vue";

export default {
  head() {
    return {
      title: this.current_menu.title,
    };
  },
  components: {
    UserIndex,
  },
  asyncData({ store, query }) {
    return RSVP.all([
      store.dispatch(
        "users/getUsers"
        // { page: 1, perPage: 10 }
      ),
    ]);
  },
  computed: {
    ...mapState({
      current_menu: (state) => state.menu.current_menu,
      pagination: (state) => state.users.pagination,
      users: (state) => state.users.users,
    }),
  },
};
</script>

<style lang="less" scoped></style>
