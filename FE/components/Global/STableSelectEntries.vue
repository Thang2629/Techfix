<template>
  <div>
    <a-select
      :default-value="pageSize"
      style="width: 128px"
      size="large"
      @change="onShowSizeChange"
      class="fc-select-normal"
      :getPopupContainer="(trigger) => trigger.parentNode"
    >
      <a-select-option
        v-for="option in pageSizeOptions"
        :key="option"
        :value="option"
      >
        {{ `${option} entries` }}
      </a-select-option>
    </a-select>
  </div>
</template>

<script>
export default {
  name: "STableSelectEntries",
  data() {
    return {
      pageSizeOptions: [10, 25],
      pageSize: 10,
    };
  },
  methods: {
    onShowSizeChange(value) {
      this.pageSize = value;
      this.$emit("onShowSizeChange", this.pageSize);
    },
  },
};
</script>

<style lang="less"></style>
