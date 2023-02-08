<template>
  <div>
    <a-tag v-if="renderStatus" color="green">Hoạt động</a-tag>
    <a-tag v-else color="gray">Đã dừng</a-tag>
  </div>
</template>

<script>
export default {
  props: {
    status: {
      type: String,
      default: "",
    },
  },
  computed: {
    renderStatus() {
      const ACTIVE = "Hoạt động";
      return ACTIVE ? true : false;
    },
  },
};
</script>

<style></style>
