<template>
  <a-empty
    :image="NoData"
    :image-style="{
      height: '250px',
    }"
    :description="false"
  />
</template>

<script>
import NoData from "~/assets/images/no_data.png";
export default {
  name: "FcEmpty",
  data() {
    return {
      NoData,
    };
  },
};
</script>

<style lang="less">
.ant-empty {
  margin: 0;
}
</style>