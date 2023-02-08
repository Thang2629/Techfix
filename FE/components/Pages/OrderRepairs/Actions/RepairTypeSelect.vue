<template>
  <a-select
    :default-value="dataRepairType[0].id"
    :getPopupContainer="(trigger) => trigger.parentNode"
    @change="onSelectOption"
  >
    <a-select-option
      v-for="type in dataRepairType"
      :key="type.id"
      :value="type.id"
    >
      {{ type.name }}
    </a-select-option>
  </a-select>
</template>

<script>
export default {
  name: "RepairTypeSelect",
  props: {
    keyIndex: {
      type: Number,
    },
    isRepairSelection: {
      type: Boolean,
      default: true,
    },
  },
  methods: {
    onSelectOption(val) {
      this.$emit("onChangeRepairType", val, this.keyIndex);
    },
  },
  computed: {
    dataRepairType() {
      const rType = [
        {
          id: 1,
          name: "Chậm",
        },
        {
          id: 2,
          name: "Thường",
        },
        {
          id: 3,
          name: "Gấp",
        },
      ];

      const wType = [
        {
          id: 1,
          name: "SC",
        },
        { id: 2, name: "LK" },
        {
          id: 3,
          name: "LT",
        },
        {
          id: 4,
          name: "PC",
        },
      ];
      return this.isRepairSelection ? rType : wType;
    },
  },
};
</script>

<style></style>
