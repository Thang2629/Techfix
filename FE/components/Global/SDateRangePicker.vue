<template>
  <div class="s-date-range-picker">
    <a-range-picker
      @change="onChangeRangePicker"
      format="DD/MM/YYYY"
      :allowClear="false"
      v-model="dateRange"
    />
    <a-radio-group :default-value="1" @change="onChangeTypeMoment">
      <a-radio-button :value="1">Tuần</a-radio-button>
      <a-radio-button :value="2">Tháng</a-radio-button>
      <a-radio-button :value="3">Quý</a-radio-button>
    </a-radio-group>
  </div>
</template>

<script>
import moment from "moment";

export default {
  name: "SDateRangePicker",
  data() {
    return {
      dateRange: [this.moment().startOf("week"), this.moment().endOf("week")],
    };
  },
  methods: {
    moment,
    onSelectOption(val) {
      this.selectedValue = val;
      this.$emit("onSelectOption", this.selectedValue);
    },
    onChangeRangePicker() {
      this.$emit("onChangeRangePicker", this.dateRange);
    },
    onChangeTypeMoment(e) {
      const typeMoment = e.target.value;
      switch (typeMoment) {
        case 1:
          this.dateRange = [
            this.moment().startOf("week"),
            this.moment().endOf("week"),
          ];
          break;
        case 2:
          this.dateRange = [
            this.moment().startOf("month"),
            this.moment().endOf("month"),
          ];
          break;
        default:
          this.dateRange = [
            this.moment().startOf("quarter"),
            this.moment().endOf("quarter"),
          ];
          break;
      }
      this.$emit("onChangeTypeMoment", this.dateRange);
    },
  },
};
</script>

<style lang="less">
.s-date-range-picker {
  display: flex;
  flex-wrap: wrap;
  gap: 10px;
  align-items: center;
}
</style>
