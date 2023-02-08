import { resolve } from "path";

export default {
  alias: {
    globalComponent: resolve(__dirname, "./components/Global"),
    pagesComponent: resolve(__dirname, "./components/Pages"),
    components: resolve(__dirname, "./components/"),
  },

  /*
   ** Nuxt rendering mode
   ** See https://nuxtjs.org/api/configuration-mode
   ** mode: "universal",
   */
  mode: "spa",
  router: {
    base: "",
  },
  /*
   ** Nuxt target
   ** See https://nuxtjs.org/api/configuration-target
   ** target: "server",
   */
  target: "static",
  /*
   ** Headers of the page
   ** See https://nuxtjs.org/api/configuration-head
   */
  head: {
    title: "TechFix",
    meta: [
      { charset: "utf-8" },
      { name: "viewport", content: "width=device-width, initial-scale=1" },
      {
        hid: "description",
        name: "description",
        content: process.env.npm_package_description || "TechFix",
      },
    ],
    link: [
      { rel: "icon", type: "image/x-icon", href: "/techfix.ico" },
      {
        rel: "stylesheet",
        href: "https://fonts.googleapis.com/css2?family=Sarabun:ital,wght@0,100;0,200;0,300;0,400;0,500;0,600;0,700;0,800;1,100;1,200;1,300;1,400;1,600;1,700;1,800&display=swap",
      },
    ],
  },
  /*
   ** Global CSS
   */
  css: ["~assets/stylesheets/global-style.less"],
  /*
   ** Plugins to load before mounting the App
   ** https://nuxtjs.org/guide/plugins
   */
  plugins: ["@/plugins/antd-ui", "@/plugins/mixins"],
  /*
   ** Auto import components
   ** See https://nuxtjs.org/api/configuration-components
   */
  components: true,
  /*
   ** Nuxt.js dev-modules
   */
  buildModules: ["@nuxtjs/dotenv"],
  /*
   ** Nuxt.js modules
   */
  modules: ["@nuxtjs/axios", "@nuxtjs/auth"],
  /*
   ** Build configuration
   ** See https://nuxtjs.org/api/configuration-build/
   */
  build: {
    loaders: {
      less: {
        lessOptions: {
          javascriptEnabled: true,
        },
      },
    },
  },

  loading: {
    color: "orange",
    height: "3px",
  },

  axios: {
    proxy: true,
  },

  proxy: {
    "/api/": {
      target: process.env.BASE_API_URL,
      pathRewrite: { "^/api/": "" },
    },
    "/web/": {
      target: process.env.BASE_API_URL,
      pathRewrite: { "^/web/": "" },
    },
  },

  auth: {
    strategies: {
      local: {
        endpoints: {
          login: {
            url: "/managers/sign_in",
            method: "post",
            propertyName: "authenticate_token",
          },
          logout: false,
          user: {
            url: "/managers/profile",
            method: "get",
            propertyName: false,
          },
        },
        tokenRequired: true,
        tokenType: false,
        tokenName: "X-Authentication-Token",
      },
    },
    redirect: {
      callback: false,
    },
  },
};
