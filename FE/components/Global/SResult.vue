<template>
  <a-result
    status="500"
    title="500"
    sub-title="Tính năng này vẫn đang trong giai đoạn phát triển ..."
  >
    <template #extra>
      <a-button type="primary">Trở về</a-button>
    </template>
  </a-result>
</template>

<script>
export default {
  name: "SResult",
};
</script>

<style></style>
