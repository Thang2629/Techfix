<template>
  <div @mouseover="handleHover" @mouseleave="handleLeave">
    <a-input-search
      :placeholder="placeholder"
      size="large"
      class="fc-input-normal fc-search-input-custom"
      @search="onInputSearchSubmit"
      @change="handleChange"
      allowClear
      :defaultValue="initSearchText"
      :style="`width: ${width}px`"
    >
    </a-input-search>
  </div>
</template>

<script>
const defaultMinimunWidth = 39;

export default {
  name: "STableSearch",
  props: {
    initSearchText: {
      type: String,
      default: "",
    },
    widthProp: {
      type: Number,
      default: 200,
    },
    placeholder: {
      type: String,
      default: "Tìm kiếm",
    },
  },
  data() {
    return {
      value: this.initSearchText,
      width: defaultMinimunWidth,
    };
  },
  methods: {
    onInputSearchSubmit(value) {
      this.$emit("onInputSearchSubmit", value);
    },
    handleChange(e) {
      this.value = e.target.value;
    },
    handleHover() {
      this.width = this.widthProp;
    },
    handleLeave() {
      this.width = this.value.length > 0 ? this.widthProp : defaultMinimunWidth;
    },
  },
  mounted() {
    this.width = this.value.length > 0 ? this.widthProp : defaultMinimunWidth;
  },
};
</script>

<style lang="less">
.fc-search-input-custom {
  -webkit-transition: width 0.5s ease-in-out;
  -moz-transition: width 0.5s ease-in-out;
  -o-transition: width 0.5s ease-in-out;
  transition: width 0.5s ease-in-out;
}
.fc-search-input-custom__show {
  -webkit-transition: width 0.5s ease-in-out;
  -moz-transition: width 0.5s ease-in-out;
  -o-transition: width 0.5s ease-in-out;
  transition: width 0.5s ease-in-out;
}
</style>
