<template>
  <a-layout-sider
    v-model="collapsed"
    :trigger="null"
    collapsible
    theme="dark"
    :width="244"
    :style="{
      overflow: 'auto',
      height: '100vh',
      position: 'sticky',
      left: 0,
      top: 0,
      boxShadow: '2px 0 6px rgb(0 21 41 / 35%)',
    }"
    :class="foldClass"
  >
    <div class="fc_portal--logo">
      <transition name="slide-fade">
        <div class="fc_portal--logo-title" v-if="!collapsed">
          <p class="fc_portal--logo-title-main">
            <span>TechFix</span>
          </p>
          <p class="fc_portal--logo-title-sub">TechFix CRM portal</p>
        </div>
      </transition>
      <div class="fc_portal--logo-collapsed">
        <a-icon
          class="trigger"
          :component="collapsed ? SideMenuFoldIcon : SideMenuUnFoldIcon"
          @click="() => (collapsed = !collapsed)"
        />
      </div>
    </div>
    <a-menu
      theme="dark"
      mode="inline"
      class="fc_portal--menu"
      :default-selected-keys="[default_menu_selected]"
    >
      <template v-for="item in nav_items">
        <a-sub-menu v-if="item.children.length > 0" :key="item.path">
          <template #title>
            <span>
              <a-icon :component="item.icon" />
              <span>{{ item.name }}</span>
            </span>
          </template>
          <a-menu-item v-for="children in item.children" :key="children.path">
            <nuxt-link
              :to="children.path"
              @click.native="setCurrentMenu(item, children)"
            >
              <span>{{ children.name }}</span>
            </nuxt-link>
          </a-menu-item>
        </a-sub-menu>

        <a-menu-item v-else :key="item.path">
          <nuxt-link :to="item.path" @click.native="setCurrentMenu(item, item)">
            <a-icon :component="item.icon" />
            <span>{{ item.name }}</span>
          </nuxt-link>
        </a-menu-item>
      </template>
      <!-- <a-sub-menu v-for="item in nav_items" :key="item.path">
        <template #title>
          <span>
            <a-icon :component="item.icon" />
            <span>{{ item.name }}</span>
          </span>
        </template>
        <a-menu-item v-for="children in item.children" :key="children.path">
          <nuxt-link
            :to="children.path"
            @click.native="setCurrentMenu(item, children)"
          >
            <span>{{ children.name }}</span>
          </nuxt-link>
        </a-menu-item>
      </a-sub-menu> -->
    </a-menu>
  </a-layout-sider>
</template>
<script>
import {
  SideMenuFoldIcon,
  SideMenuUnFoldIcon,
} from '~/components/Icons/index.js';
import { mapState } from 'vuex';

export default {
  name: 'SliderMenu',
  data() {
    return {
      collapsed: false,
      SideMenuFoldIcon,
      SideMenuUnFoldIcon,
    };
  },
  mounted() {
    this.$store.dispatch('menu/findNavItems').then(() => {});
  },

  methods: {
    setCurrentMenu(item, children) {
      this.$store.dispatch('menu/setCurrentMenu', {
        label: item.name,
        key: children.path,
        childrenLabel: children.name,
      });
    },
  },

  computed: {
    foldClass: function () {
      return this.collapsed ? 'fc-sider--fold' : 'fc-sider--unfold';
    },
    logoVisibleStyle: function () {
      return this.collapsed ? 'none' : 'block';
    },
    ...mapState({
      nav_items: (state) => state.menu.nav_items,
      current_menu: (state) => state.menu.current_menu,
    }),
    default_menu_selected: function () {
      return this.$route.path;
    },
  },
};
</script>
<style lang="less">
.fc-sider-bottom {
  position: absolute !important;
  bottom: 1em;
  right: 0;
}
.fc_portal--logo {
  background-color: #53575b;
}
.fc-sider--fold {
  .fc_portal--logo-collapsed {
    -webkit-transition: background-color 0.1s linear;
    -ms-transition: background-color 0.1s linear;
    transition: background-color 0.1s linear;
  }
  .ant-menu-dark.ant-menu-vertical .ant-menu-item {
    box-shadow: 2px 0 6px rgb(0 21 41 / 35%);
    border-radius: 8px;
    margin: 0 20px 4px 20px;
    padding: 0px 12px !important;
    margin-bottom: 4px;
    transition: none;
  }
  .ant-menu.ant-menu-dark .ant-menu-item-selected {
    border-radius: 8px;
    margin: 0 20px 4px 20px;
    padding: 0px 12px !important;
  }
}

.fc-sider--unfold {
  .ant-menu.ant-menu-dark .ant-menu-item {
    box-shadow: 2px 0 6px rgb(0 21 41 / 35%);
    border-radius: 8px;
    margin: 0 12px;
    width: auto;
    margin-bottom: 4px;
    padding-left: 12px !important;
    transition: none;
    transform: none;
  }
  .ant-menu.ant-menu-dark .ant-menu-item-selected {
    border-radius: 8px;
    margin: 0 12px;
    width: auto;
    margin-bottom: 4px;
    padding-left: 12px !important;
  }
}

.fc_portal--logo {
  display: flex;
  justify-content: space-between;
  align-items: center;
  height: 64px;

  .fc_portal--logo-title {
    margin-left: 2em;
    width: 100%;

    .fc_portal--logo-title-main {
      font-size: 16px;
      margin-top: 10px;
      margin-bottom: 0;
      color: #ff6337;
    }
    .fc_portal--logo-title-sub {
      color: #a0a4a8;
      font-size: 12px;
    }
  }
  .fc_portal--logo-collapsed {
    width: 79px;
    display: flex;
    justify-content: center;
    height: 64px;
    align-items: center;
  }
}

.ant-layout-sider-light {
  .fc_portal--logo {
    border-bottom: 1px solid #eef1f5;
    border-right: 1px solid #eef1f5;
  }
}
.ant-layout-sider .ant-layout-sider-dark {
  background: #32373d;
  .ant-menu-dark,
  .ant-menu-dark .ant-menu-sub {
    background: #32373d;
  }
  .fc_portal--logo
    .fc_portal--logo-title
    .fc_portal--logo-title-main
    span:last-child {
    color: #ffffff;
  }
}

.fc_portal--menu {
  padding: 18px 0;
  display: flex;
  flex-direction: column;
  .fc-menu-space {
    margin-top: auto !important;
  }
}
/* Enter and leave animations can use different */
/* durations and timing functions.              */
.slide-fade-enter-active {
  transition: all 0.1s ease;
}
.slide-fade-leave-active {
  transition: all 0;
}
.slide-fade-enter, .slide-fade-leave-to
/* .slide-fade-leave-active below version 2.1.8 */ {
  transform: translateX(10px);
  opacity: 0;
}
</style>
