<template>
  <div>
    <a-select
      :allowClear="true"
      :default-value="defaultValue"
      style="width: 200px"
      size="large"
      @change="onSelectOption"
      class="fc-select-normal"
      :placeholder="placeholder"
      :getPopupContainer="(trigger) => trigger.parentNode"
    >
      <a-select-option
        v-for="option in dataOptions"
        :key="option.id"
        :value="option.id"
      >
        {{ option.name }}
      </a-select-option>
    </a-select>
  </div>
</template>

<script>
export default {
  name: "SSingleSelect",
  props: {
    defaultValue: {
      type: String,
      default: undefined,
    },
    dataOptions: {
      type: Array,
      default: [],
    },
    placeholder: {
      type: String,
      default: "",
    },
  },
  data() {
    return {
      selectedValue: "",
    };
  },
  methods: {
    onSelectOption(val) {
      this.selectedValue = val;
      this.$emit("onSelectOption", this.selectedValue);
    },
  },
};
</script>

<style lang="less"></style>
