<template>
  <a-layout-header class="fc-layout__header">
    <span class="fc-layout__header-main-title">{{ renderTitle }}</span>
    <a-popover trigger="hover" placement="bottom" arrow-point-at-right>
      <template slot="content">
        <a-menu
          style="width: 192px"
          :default-selected-keys="['1']"
          :default-open-keys="['sub1']"
          mode="vertical"
          theme="light"
        >
          <a-menu-item key="1">
            <nuxt-link to="/accounts">
              <span class="fc-custom-icon">
                <AccountIcon />
              </span>
              My Account
            </nuxt-link>
          </a-menu-item>
          <a-menu-item key="2" @click="handleLogOut">
            <span class="fc-custom-icon">
              <SignOutIcon />
            </span>
            Sign Out
          </a-menu-item>
        </a-menu>
      </template>
      <div class="fc-tiny-info">
        <div class="fc-tiny-info__content">
          <span class="fc-tiny-info__content-plan">"Description Here"</span>
          <span class="fc-tiny-info__content-name">Techfix admin</span>
        </div>
        <a-avatar
          src="https://img.rankedboost.com/wp-content/plugins/pokemon-quest/pokemon-quest-pokemon-images/1143.png"
        />
      </div>
    </a-popover>
  </a-layout-header>
</template>
<script>
import { AccountIcon, SignOutIcon } from "~/components/Icons/index.js";
import { mapState } from "vuex";

export default {
  name: "HeaderLayout",
  data() {
    return {
      AccountIcon,
      SignOutIcon,
    };
  },
  computed: {
    ...mapState({
      current_menu: (state) => state.menu.current_menu,
    }),
    renderTitle() {
      const { childrenLabel, label } = this.current_menu;
      return label
        ? label === childrenLabel
          ? `${label}`
          : `${label} / ${childrenLabel}`
        : ``;
    },
  },
  methods: {
    async handleLogOut() {
      // await this.$auth.logout();
      await window.localStorage.removeItem("fakeLoginTechfix");
      this.$message.success("Đăng xuất thành công!");
      this.$router.push("/sign_in");
    },
  },
};
</script>
<style lang="less">
.fc-tiny-info {
  cursor: pointer;
}
.fc-layout__header {
  position: sticky;
  top: 0;
  z-index: 1;
  width: 100%;
  background: #fff;
  padding: 0 34px 0 16px;
  border-bottom: 0.8px solid #dbdde0;
  .fc-layout__header-main-title {
    font-size: 24px;
    color: #25282b;
  }
}
.ant-popover-inner-content {
  padding: 4px 0px;
  border-radius: 8px;
  .ant-menu-vertical > .ant-menu-item {
    height: 48px;
    line-height: 48px;
  }
}
.fc-custom-icon {
  min-width: 14px;
  margin-right: 10px;
  font-size: 14px;
  transition: font-size 0.15s cubic-bezier(0.215, 0.61, 0.355, 1),
    margin 0.3s cubic-bezier(0.645, 0.045, 0.355, 1);
  display: inline-block;
  color: inherit;
  font-style: normal;
  line-height: 0;
  text-align: center;
  text-transform: none;
  vertical-align: -0.125em;
  text-rendering: optimizeLegibility;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
}
</style>
