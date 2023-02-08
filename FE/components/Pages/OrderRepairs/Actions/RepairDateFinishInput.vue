<template>
  <a-date-picker
    format="DD/MM/YYYY"
    placeholder="dd/mm/yyyy"
    @change="onChangeDateFinish"
  />
</template>

<script>
export default {
  name: "RepairDateFinishInput",
  props: {
    keyIndex: {
      type: Number,
    },
  },
  methods: {
    onChangeDateFinish(date, dateString) {
      this.$emit("onChangeDateFinish", date, this.keyIndex);
    },
  },
};
</script>

<style></style>
