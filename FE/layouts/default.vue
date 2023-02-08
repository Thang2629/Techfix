<template>
  <a-layout id="fc_layout" style="min-height: 100vh">
    <SliderMenu :collapsed="collapsed" />
    <a-layout>
      <HeaderLayout />
      <div class="fc-main-drawer">
        <a-layout-content class="fc-main">
          <Nuxt />
        </a-layout-content>
      </div>
    </a-layout>
  </a-layout>
</template>
<script>
import SliderMenu from "../components/DefaultLayout/SliderMenu.vue";
import HeaderLayout from "../components/DefaultLayout/HeaderLayout.vue";
export default {
  middleware: "auth-guard",
  components: {
    SliderMenu,
    HeaderLayout,
  },
  data() {
    return {
      collapsed: false,
    };
  },
};
</script>
<style lang="less">
.ant-layout-footer {
  padding: 24px 16px;
}
.ant-menu-inline .ant-menu-item::after {
  left: 0;
  border-right: none;
  border-left: 3px solid #ff6337;
}
.fc-tiny-info {
  display: flex;
  float: right;
  align-items: center;
  height: 100%;

  .fc-tiny-info__content {
    display: flex;
    flex-direction: column;
    text-align: right;
    padding: 0 16px;

    .fc-tiny-info__content-plan {
      line-height: normal;
      font-size: 12px;
      color: #a0a4a8;
    }
    .fc-tiny-info__content-name {
      line-height: normal;
    }
  }
}
#fc_layout {
  .fc-article {
    background: #fff;
    min-height: 360px;
    border: 0.8px solid #f1f1f1;
    box-sizing: border-box;
    border-radius: 5px;
    margin-bottom: 22px;
  }
  .fc-main {
    margin: 22px 34px 0 16px;
  }
  .fc-main-drawer {
    height: 100%;
    overflow: hidden;
    position: relative;
  }
  .small-text {
    color: #a0a4a8;
    font-size: 12px;
  }
}
</style>
