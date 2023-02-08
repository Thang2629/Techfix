<template>
  <a-input-number
    :default-value="0"
    :formatter="(value) => `${value}`.replace(/\B(?=(\d{3})+(?!\d))/g, ',')"
    :parser="(value) => value.replace(/\$\s?|(,*)/g, '')"
    @change="onChangePrice"
  />
</template>

<script>
export default {
  name: "RepairPriceInput",
  props: {
    keyIndex: {
      type: Number,
    },
  },
  methods: {
    onChangePrice(value) {
      this.$emit("onChangePrice", value, this.keyIndex);
    },
  },
};
</script>

<style></style>
