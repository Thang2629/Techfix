<template>
  <div class="widget-card">
    <a-card :title="title">
      <a slot="extra">
        <overview-icon />
      </a>

      <div class="widget-card-content" v-for="(c, i) in content" :key="i">
        <p>{{ c.name }}</p>
        <p>
          <b>{{ c.number }}</b>
        </p>
      </div>
    </a-card>
  </div>
</template>

<script>
import OverviewIcon from "../../../Icons/OverviewIcon.vue";
export default {
  components: { OverviewIcon },
  name: "WidgetCard",
  props: {
    icon: {
      type: String,
      default: "",
    },
    title: {
      type: String,
      default: "",
    },
    content: {
      type: Array,
      default: [],
    },
  },
};
</script>

<style lang="less">
.widget-card {
  .ant-card-head-title {
    font-size: 20px;
    color: #ff6337;
  }
  .widget-card-content {
    display: flex;
    justify-content: space-between;
  }
}
</style>
