<template>
  <div class="s-statistic-card">
    <a-statistic :title="title" :value="value" />
  </div>
</template>

<script>
export default {
  name: "SStatisticCard",
  props: {
    title: "",
    value: 0,
  },
};
</script>

<style lang="less">
.s-statistic-card {
  padding: 1rem;
  border: 1px dashed gray;
  border-radius: 1rem;
  width: 100%;
  margin-top: 0.5rem;
}
</style>
